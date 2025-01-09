using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface INewsService
    {
        void Add(NewsDTO newsDto);
        void Update(NewsDTO newsDto);
        NewsDTO Get(int id);
        List<NewsDTO> GetAll();
        void Delete(int id);
        List<NewsDTO> SearchByTitle(string title);
        List<NewsDTO> SearchByCategory(string category);
        List<NewsDTO> SearchByDate(DateTime date);
        List<NewsDTO> SearchByDateAndCategory(DateTime date, string category);
        List<NewsDTO> SearchByDateAndTitle(DateTime date, string title);
    }
}
