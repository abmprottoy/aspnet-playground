using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class NewsRepository : Repository, INewsRepository
    {
        public void Add(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
        }

        public void Update(News news)
        {
            var existingNews = _context.News.Find(news.Id);
            if (existingNews == null)
                throw new KeyNotFoundException("News item not found.");

            existingNews.Title = news.Title;
            existingNews.Category = news.Category;
            existingNews.DateTime = news.DateTime;

            _context.SaveChanges();
        }

        public News Get(int id) => _context.News.Find(id);

        public List<News> Get() => _context.News.ToList();

        public void Delete(int id)
        {
            var news = _context.News.Find(id);
            if (news == null)
                throw new KeyNotFoundException("News item not found.");

            _context.News.Remove(news);
            _context.SaveChanges();
        }

        public List<News> SearchByTitle(string title) =>
            _context.News.Where(n => n.Title.Contains(title)).ToList();

        public List<News> SearchByCategory(string category) =>
            _context.News.Where(n => n.Category == category).ToList();

        public List<News> SearchByDate(DateTime date) =>
            _context.News.Where(n => n.DateTime.Date == date.Date).ToList();

        public List<News> SearchByDateAndCategory(DateTime date, string category) =>
            _context.News.Where(n => n.DateTime.Date == date.Date && n.Category == category).ToList();

        public List<News> SearchByDateAndTitle(DateTime date, string title) =>
            _context.News.Where(n => n.DateTime.Date == date.Date && n.Title.Contains(title)).ToList();
    }
}
