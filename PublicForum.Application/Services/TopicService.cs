using AutoMapper;
using FluentValidation.Results;
using PublicForum.Application.Configurations;
using PublicForum.Application.Interfaces;
using PublicForum.Application.ViewModels;
using PublicForum.Domain.Entities;
using PublicForum.Domain.Interfaces.Repositories;
using PublicForum.Domain.Validations;
using PublicForum.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.Services
{
    public class TopicService : BaseService, ITopicService
    {
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;
        public TopicService(IUnitOfWork unitOfWork, IMapper mapper, ITopicRepository topicRepository)
            : base(unitOfWork)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<TopicViewModel> Create(TopicViewModel data)
        {
            data.Created = DateTime.Now;
            var model = _mapper.Map<Topic>(data);

            data.Validation = new CreateTopicValidation().Validate(model);
            if (!data.Validation.IsValid)
                return data;

            BeginTransaction();
            
            data = _mapper.Map<TopicViewModel>(await _topicRepository.AddAsync(model));
            
            Commit();

            return data;
        }

        public async Task<TopicViewModel> Delete(Guid id, Guid ownerId)
        {
            var result = await _topicRepository.GetByIdAndOwnerId(id, ownerId);
            if(result != null)
            {
                result.IsDeleted = true;
                BeginTransaction();
                result = await _topicRepository.UpdateAsync(result);
                Commit();

                return _mapper.Map<TopicViewModel>(result);
            }

            return null;
        }

        public async Task<TabulatorViewModel> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>>? filters = null, string? ownerId = null, string orderBy = "Id", bool ascending = true)
        {
            var results = await _topicRepository.GetPaginated(page, pageSize, filters, orderBy, ascending);
            var count = await _topicRepository.GetCountFiltered(filters);
            var mapped = _mapper.Map<List<TopicViewModel>>(results);

            return new TabulatorViewModel
            {
                Data = mapped.Select(m => PrepareDataTabulator(m, ownerId)),
                Last_page = page,
                Row_count = count
            };
        }

        private Object PrepareDataTabulator(TopicViewModel topic, string? ownerId)
        {
            return new
            {
                Id = topic.Id,
                Title = topic.Title,
                Date = topic.Created.ToString("MM/dd/yyyy HH:mm:ss"),
                OwnerId = topic.OwnerId,
                Owner = $"{topic.Owner.FirstName} {topic.Owner.LastName}",
                IsOwner = ownerId != null ? topic.OwnerId == ownerId : false
            };
        }

        public async Task<TopicViewModel> Update(TopicViewModel data)
        {
            var model = _mapper.Map<Topic>(data);
            data.Validation = new UpdateTopicValidation().Validate(model);
            if (!data.Validation.IsValid)
                return data;

            var modelDb = await _topicRepository.GetById(data.Id);

            if (modelDb.OwnerId == data.OwnerId)
            {
                modelDb.Title = model.Title;
                modelDb.Description = model.Description;

                BeginTransaction();

                data = _mapper.Map<TopicViewModel>(await _topicRepository.UpdateAsync(modelDb));

                Commit();

                return data;
            }

            data.Validation.Errors.Add(new ValidationFailure("OwnerId", "Topic can only be changed by its owner"));
            return data;
        }

        public async Task<TopicViewModel> GetById(Guid id)
        {
            return _mapper.Map<TopicViewModel>(await _topicRepository.GetById(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
