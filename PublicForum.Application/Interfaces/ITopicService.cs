using PublicForum.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.Interfaces
{
    public interface ITopicService : IDisposable
    {
        Task<TopicViewModel> GetById(Guid id);
        Task<TabulatorViewModel> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>>? filters = null, string? ownerId = null, string orderBy = "Id", bool ascending = true);
        Task<TopicViewModel> Create(TopicViewModel topic);
        Task<TopicViewModel> Update(TopicViewModel topic);
        Task<TopicViewModel> Delete(Guid id, Guid ownerId);
    }
}
