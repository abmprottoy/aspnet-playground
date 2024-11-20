using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRPManagement.DTOs;
using TRPManagement.EF;

namespace TRPManagement.Controllers
{
    public class ProgramController : Controller
    {
        private readonly TRPManagementEntities _db = new TRPManagementEntities();

        // GET: Program
        public ActionResult Index()
        {
            var programs = _db.Programs.ToList()
                .Select(p => new ProgramDTO
                {
                    ProgramId = p.ProgramId,
                    ProgramName = p.ProgramName,
                    TRPScore = p.TRPScore,
                    ChannelId = p.ChannelId,
                    AirTime = p.AirTime
                });

            return View(programs);
        }

        // GET: Program/Create
        public ActionResult Create()
        {
            ViewBag.Channels = new SelectList(_db.Channels, "ChannelId", "ChannelName");
            return View();
        }

        // POST: Program/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgramDTO programDTO)
        {
            if (ModelState.IsValid)
            {
                var program = new Program
                {
                    ProgramName = programDTO.ProgramName,
                    TRPScore = programDTO.TRPScore,
                    ChannelId = programDTO.ChannelId,
                    AirTime = programDTO.AirTime
                };

                _db.Programs.Add(program);
                _db.SaveChanges();
                TempData["Success"] = "Program added successfully.";
                return RedirectToAction("Index");
            }

            ViewBag.Channels = new SelectList(_db.Channels, "ChannelId", "ChannelName", programDTO.ChannelId);
            return View(programDTO);
        }

        // GET: Program/Edit/5
        public ActionResult Edit(int id)
        {
            var program = _db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }

            ViewBag.Channels = new SelectList(_db.Channels, "ChannelId", "ChannelName", program.ChannelId);
            return View(new ProgramDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                TRPScore = program.TRPScore,
                ChannelId = program.ChannelId,
                AirTime = program.AirTime
            });
        }

        // POST: Program/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProgramDTO programDTO)
        {
            if (ModelState.IsValid)
            {
                var program = _db.Programs.Find(programDTO.ProgramId);
                if (program == null)
                {
                    return HttpNotFound();
                }

                program.ProgramName = programDTO.ProgramName;
                program.TRPScore = programDTO.TRPScore;
                program.ChannelId = programDTO.ChannelId;
                program.AirTime = programDTO.AirTime;

                _db.SaveChanges();
                TempData["Success"] = "Program updated successfully.";
                return RedirectToAction("Index");
            }

            ViewBag.Channels = new SelectList(_db.Channels, "ChannelId", "ChannelName", programDTO.ChannelId);
            return View(programDTO);
        }

        // POST: Program/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var program = _db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }

            _db.Programs.Remove(program);
            _db.SaveChanges();
            TempData["Success"] = "Program deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
