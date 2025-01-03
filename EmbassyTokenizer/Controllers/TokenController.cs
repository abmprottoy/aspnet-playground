using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmbassyTokenizer.Models;

namespace EmbassyTokenizer.Controllers
{
    public class TokenController : Controller
    {
        private static readonly int DailyTokenLimit = 100;
        private static readonly int CounterTokenLimit = 25;
        private static DateTime LastReset = DateTime.Today;
        private static List<Token> Tokens = new List<Token>();

        // Method to reset tokens at 12 AM and carry forward unused tokens
        private void ResetDailyTokens()
        {
            if (DateTime.Now.Date > LastReset.Date)
            {
                var carriedForwardTokens = Tokens.Select(t => new Token
                {
                    Id = t.Id,
                    Category = t.Category,
                    Number = 1, // Reset token number to 1 for carried forward tokens
                    Timestamp = DateTime.Now
                }).ToList();

                Tokens.Clear();
                Tokens.AddRange(carriedForwardTokens);

                LastReset = DateTime.Now.Date;
            }
        }

        // Action: Generate
        [HttpGet]
        public ActionResult Generate()
        {
            ResetDailyTokens(); // This could be kept here if it's required before rendering the page
            return View();
        }

        [HttpPost]
        public ActionResult Generate(string category)
        {
            ResetDailyTokens(); // Ensure daily tokens are reset before processing

            if (Tokens.Count >= DailyTokenLimit)
            {
                TempData["Error"] = "Daily token limit reached.";
                return RedirectToAction("Generate");
            }

            if (Tokens.Count(t => t.Category == category) >= CounterTokenLimit)
            {
                TempData["Error"] = $"Token limit for {category} counter reached.";
                return RedirectToAction("Generate");
            }

            int tokenCount = Tokens.Count + 1;
            var token = new Token
            {
                Id = tokenCount,
                Category = category,
                Number = tokenCount,
                Timestamp = DateTime.Now
            };

            Tokens.Add(token);
            TempData["Success"] = $"Token generated: {token.Category}-{token.Number}";

            return RedirectToAction("Generate"); // Redirect to avoid duplicate submissions
        }

        // Action: Waitlist
        public ActionResult Waitlist()
        {
            ResetDailyTokens();

            // Group tokens by category
            var categories = new[] { "Med", "TR", "BS", "GO" };
            var categoryTokens = categories.ToDictionary(
                category => category,
                category => Tokens.Where(t => t.Category == category).OrderBy(t => t.Timestamp).ToList()
            );

            return View(categoryTokens);
        }

        // Action: CallApplicants (GET)
        public ActionResult CallApplicants()
        {
            // Create a dictionary for counters and the list of available tokens per counter
            var counters = new Dictionary<string, List<Token>>
    {
        { "Med", Tokens.Where(t => t.Category == "Med").Take(CounterTokenLimit).ToList() },
        { "TR", Tokens.Where(t => t.Category == "TR").Take(CounterTokenLimit).ToList() },
        { "BS", Tokens.Where(t => t.Category == "BS").Take(CounterTokenLimit).ToList() },
        { "GO", Tokens.Where(t => t.Category == "GO").Take(CounterTokenLimit).ToList() }
    };

            // Pass the available counters and tokens to the view
            ViewBag.Counters = counters;

            return View();
        }

        // Action: CallApplicants (POST)
        [HttpPost]
        public ActionResult CallApplicants(string selectedCounter, int selectedTokenId)
        {
            if (string.IsNullOrEmpty(selectedCounter) || selectedTokenId == 0)
            {
                TempData["Error"] = "Invalid selection.";
                return RedirectToAction("CallApplicants");
            }

            // Find the selected token based on ID
            var tokenToCall = Tokens.FirstOrDefault(t => t.Id == selectedTokenId);
            if (tokenToCall == null || tokenToCall.Category != selectedCounter)
            {
                TempData["Error"] = "Token not found or category mismatch.";
                return RedirectToAction("CallApplicants");
            }

            // Mark the token as "called"
            tokenToCall.IsCalled = true;

            // Remove the called token from the list
            Tokens.Remove(tokenToCall);

            // Optionally, you can add a confirmation message or update the called status
            TempData["Success"] = $"Token {tokenToCall.Category}-{tokenToCall.Number} has been called.";

            return RedirectToAction("CallApplicants");
        }

    }

}