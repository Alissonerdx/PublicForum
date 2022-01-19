using Microsoft.EntityFrameworkCore;
using PublicForum.Domain.Entities;
using PublicForum.Domain.Interfaces.Repositories;

namespace PublicForum.Repository.Repositories
{
    public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(EFContext context) : base(context)
        {
        }

        private IQueryable<Topic> ApplyFilter(IQueryable<Topic> query, List<Dictionary<string, string>>? filters)
        {
            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    var field = filter["field"];
                    var value = filter["value"];

                    if (field.Equals("title"))
                    {
                        query = query.Where(q => q.Title.ToLower().Contains(value.ToLower()));
                    }

                    if (field.Equals("author"))
                    {
                        query = query.Where(q => q.Owner != null ? $"{q.Owner.FirstName} {q.Owner.LastName}".ToLower().Contains(value.ToLower()) : false);
                    }
                }
            }

            return query;
        }

        public async Task<List<Topic>> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>>? filters = null, string orderBy = "Id", bool ascending = true)
        {
            var query = _dbSet
                .Include(t => t.Owner)
                .Where(t => !t.IsDeleted)
                .AsQueryable();

            if (filters != null)
            {
                query = ApplyFilter(query, filters);
            }

            query = query.OrderByDescending(q => q.Created);

            query = query.Skip(pageSize * (page - 1)).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<int> GetCountFiltered(List<Dictionary<string, string>>? filters)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyFilter(query, filters);
            return await query.CountAsync();
        }

        public async Task<Topic> GetById(Guid id)
        {
            return await _dbSet
                .Include(t => t.Owner)
                .SingleOrDefaultAsync(q => q.Id == id && !q.IsDeleted);
        }

        public async Task<Topic> GetByIdAndOwnerId(Guid id, Guid ownerId)
        {
            return await _dbSet.SingleOrDefaultAsync(q => q.Id == id && q.OwnerId == ownerId.ToString() && !q.IsDeleted);
        }
    }
}
