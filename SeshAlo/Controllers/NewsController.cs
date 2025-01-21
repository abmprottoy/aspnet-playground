using System;
using System.Web.Http;
using BLL.Interfaces;
using BLL.DTOs; // Adjust namespace as needed

namespace SeshAlo.Controllers
{
    public class NewsController : ApiController
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
        [Route("api/news")]
        public IHttpActionResult Add(NewsDTO newsDto)
        {
            if (newsDto == null)
            {
                return BadRequest("News data cannot be null.");
            }

            _newsService.Add(newsDto);
            return Ok("News added successfully.");
        }

        [HttpPut]
        [Route("api/news")]
        public IHttpActionResult Update(NewsDTO newsDto)
        {
            if (newsDto == null)
            {
                return BadRequest("News data cannot be null.");
            }

            _newsService.Update(newsDto);
            return Ok("News updated successfully.");
        }

        [HttpGet]
        [Route("api/news/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var news = _newsService.Get(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [HttpGet]
        [Route("api/news")]
        public IHttpActionResult GetAll()
        {
            var newsList = _newsService.GetAll();
            return Ok(newsList);
        }

        [HttpDelete]
        [Route("api/news/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _newsService.Delete(id);
            return Ok("News deleted successfully.");
        }

        [HttpGet]
        [Route("api/news/search/title")]
        public IHttpActionResult SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title cannot be null or empty.");
            }

            var results = _newsService.SearchByTitle(title);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/news/search/category")]
        public IHttpActionResult SearchByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category cannot be null or empty.");
            }

            var results = _newsService.SearchByCategory(category);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/news/search/date")]
        public IHttpActionResult SearchByDate(DateTime date)
        {
            var results = _newsService.SearchByDate(date);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/news/search/date-category")]
        public IHttpActionResult SearchByDateAndCategory(DateTime date, string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category cannot be null or empty.");
            }

            var results = _newsService.SearchByDateAndCategory(date, category);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/news/search/date-title")]
        public IHttpActionResult SearchByDateAndTitle(DateTime date, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title cannot be null or empty.");
            }

            var results = _newsService.SearchByDateAndTitle(date, title);
            return Ok(results);
        }
    }
}
