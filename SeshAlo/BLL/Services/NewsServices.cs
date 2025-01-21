using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;

namespace BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        public void Add(NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            _newsRepository.Add(news);
        }

        public void Update(NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            _newsRepository.Update(news);
        }

        public NewsDTO Get(int id)
        {
            var news = _newsRepository.Get(id);
            return _mapper.Map<NewsDTO>(news);
        }

        public List<NewsDTO> GetAll()
        {
            var newsList = _newsRepository.Get();
            return _mapper.Map<List<NewsDTO>>(newsList);
        }

        public void Delete(int id) => _newsRepository.Delete(id);

        public List<NewsDTO> SearchByTitle(string title) =>
            _mapper.Map<List<NewsDTO>>(_newsRepository.SearchByTitle(title));

        public List<NewsDTO> SearchByCategory(string category) =>
            _mapper.Map<List<NewsDTO>>(_newsRepository.SearchByCategory(category));

        public List<NewsDTO> SearchByDate(DateTime date) =>
            _mapper.Map<List<NewsDTO>>(_newsRepository.SearchByDate(date));

        public List<NewsDTO> SearchByDateAndCategory(DateTime date, string category) =>
            _mapper.Map<List<NewsDTO>>(_newsRepository.SearchByDateAndCategory(date, category));

        public List<NewsDTO> SearchByDateAndTitle(DateTime date, string title) =>
            _mapper.Map<List<NewsDTO>>(_newsRepository.SearchByDateAndTitle(date, title));
    }
}