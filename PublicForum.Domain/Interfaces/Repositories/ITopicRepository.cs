using PublicForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Domain.Interfaces.Repositories
{
    public interface ITopicRepository : IAsyncRepository<Topic>
    {
        Task<Topic> GetById(Guid id);
        Task<List<Topic>> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>>? filters = null, string orderBy = "Id", bool ascending = true);
        Task<int> GetCountFiltered(List<Dictionary<string, string>>? filters);
        Task<Topic> GetByIdAndOwnerId(Guid id, Guid ownerId);
    }
}
