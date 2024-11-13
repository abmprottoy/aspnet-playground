using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        readonly MusicStoreEntities storeDB = new MusicStoreEntities();

        // GET: Store
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();
            return View(genres);
        }

        public ActionResult Details(int id)
        {
            var album = storeDB.Albums.Find(id);
            return View(album);
        }

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);
            return View(genreModel);
        }
    }
}