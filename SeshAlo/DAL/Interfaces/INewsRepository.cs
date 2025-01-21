using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface INewsRepository
    {
        void Add(News news);
        void Update(News news);
        News Get(int id);
        List<News> Get();
        void Delete(int id);
        List<News> SearchByTitle(string title);
        List<News> SearchByCategory(string category);
        List<News> SearchByDate(DateTime date);
        List<News> SearchByDateAndCategory(DateTime date, string category);
        List<News> SearchByDateAndTitle(DateTime date, string title);
    }
}
