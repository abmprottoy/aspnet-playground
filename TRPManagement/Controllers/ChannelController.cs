using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRPManagement.DTOs;
using TRPManagement.EF;

namespace TRPManagement.Controllers
{
    public class ChannelController : Controller
    {
        private readonly TRPManagementEntities _db = new TRPManagementEntities();

        // DTO Converter for Channels
        private ChannelDTO ConvertToChannelDTO(Channel channel)
        {
            return new ChannelDTO
            {
                ChannelId = channel.ChannelId,
                ChannelName = channel.ChannelName,
                EstablishedYear = channel.EstablishedYear,
                Country = channel.Country,
                Programs = channel.Programs.Select(p => ConvertToProgramDTO(p)).ToList()
            };
        }

        private Channel ConvertToChannelEntity(ChannelDTO channelDTO)
        {
            return new Channel
            {
                ChannelId = channelDTO.ChannelId,
                ChannelName = channelDTO.ChannelName,
                EstablishedYear = channelDTO.EstablishedYear,
                Country = channelDTO.Country
            };
        }

        private ProgramDTO ConvertToProgramDTO(Program program)
        {
            return new ProgramDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                TRPScore = program.TRPScore,
                ChannelId = program.ChannelId,
                AirTime = program.AirTime
            };
        }

        // GET: Channel
        public ActionResult Index()
        {
            var channels = _db.Channels.ToList().Select(ConvertToChannelDTO);
            return View(channels);
        }

        // GET: Channel/Create
        public ActionResult Create()
        {
            return View(new ChannelDTO());
        }

        // POST: Channel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChannelDTO channelDTO)
        {
            if (ModelState.IsValid)
            {
                var channel = ConvertToChannelEntity(channelDTO);
                _db.Channels.Add(channel);
                _db.SaveChanges();
                TempData["Success"] = "Channel added successfully.";
                return RedirectToAction("Index");
            }

            return View(channelDTO);
        }

        // GET: Channel/Edit/5
        public ActionResult Edit(int id)
        {
            var channel = _db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }

            return View(ConvertToChannelDTO(channel));
        }

        // POST: Channel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChannelDTO channelDTO)
        {
            if (ModelState.IsValid)
            {
                var channel = _db.Channels.Find(channelDTO.ChannelId);
                if (channel == null)
                {
                    return HttpNotFound();
                }

                channel.ChannelName = channelDTO.ChannelName;
                channel.EstablishedYear = channelDTO.EstablishedYear;
                channel.Country = channelDTO.Country;

                _db.SaveChanges();
                TempData["Success"] = "Channel updated successfully.";
                return RedirectToAction("Index");
            }

            return View(channelDTO);
        }

        // POST: Channel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var channel = _db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }

            if (channel.Programs.Any())
            {
                TempData["Error"] = "Cannot delete channel with associated programs.";
                return RedirectToAction("Index");
            }

            _db.Channels.Remove(channel);
            _db.SaveChanges();
            TempData["Success"] = "Channel deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
