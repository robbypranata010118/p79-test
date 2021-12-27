using P79.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P79.Domain.Common;
using System.Linq.Dynamic.Core;
using P79.Base.Wrappers;
using P79.Base.Parameters;
using P79.Base.Extensions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace P79.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly AppDBContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private bool _isDeactivable = typeof(IDeactivable).IsAssignableFrom(typeof(T));
        private bool _isSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(T));
        private bool _isAuditable = typeof(IAuditable).IsAssignableFrom(typeof(T));
        private bool _isGuidEntity = typeof(IEntity<Guid>).IsAssignableFrom(typeof(T));
        private bool _isIntEntity = typeof(IEntity<int>).IsAssignableFrom(typeof(T));
        private bool _isAudit = typeof(IAudit).IsAssignableFrom(typeof(T));

        public GenericRepositoryAsync(AppDBContext dbContext,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public virtual async Task<T> GetByIdAsync(int id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.Equals({1})", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetByIdAsync(Guid id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.ToString().Equals(\"{1}\")", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetByIdAsync(string id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.Equals(\"{1}\")", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<PagedRepositoryResponse<IReadOnlyList<T>>> GetPagedReponseAsync(IRequestParameter request, string[] includes = null)
        {
            var data = _dbContext.Set<T>().AsQueryable();
            data = _Includes(data, includes);
            data = _Filters(data, request.Filters);

            PagedInfoRepositoryResponse info = new PagedInfoRepositoryResponse
            {
                CurrentPage = request.Page,
                Length = request.Length,
                TotalPage = (int)Math.Ceiling((decimal)await data.CountAsync() / request.Length)
        };
            data = _Orders(data, request.Orders, request.SortType);
            if (request.Page > 0 && request.Length > 0)
            {
                data = data
                .Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                .Skip((request.Page - 1) * request.Length)
                .Take(request.Length);
            }
            data = data.AsNoTracking();
            return new PagedRepositoryResponse<IReadOnlyList<T>>
            {
                Results = await data.ToListAsync(),
                Info = info
            };
        }
        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(predicate);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public IQueryable<T> GetQueryByPredicate(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = query.Where(predicate);
            return query;
        }
        public Task<int> GetCountByPredicate(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = query.Where(predicate);
            return query.CountAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            if (_isDeactivable)
            {
                (entity as IDeactivable).IsActive = true;
            }
            //if (_isSoftDelete)
            //{
            //    (entity as ISoftDelete).IsDeleted = false;
            //}

            if (_isAudit)
            {
                (entity as IAudit).UserIn = _currentUserService.UserName;
                (entity as IAudit).DateIn = DateTime.Now;

            }
            await _dbContext.Set<T>().AddAsync(entity);          
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> AddAsync(T entity, bool preventSave)
        {
            if (_isDeactivable)
            {
                (entity as IDeactivable).IsActive = true;
            }
            if (_isSoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = false;
            }
            if (_isAudit)
            {
                (entity as IAudit).UserIn = _currentUserService.UserName;
                (entity as IAudit).DateIn = DateTime.Now;

            }
            await _dbContext.Set<T>().AddAsync(entity);
            if (!preventSave)
            {
                await _dbContext.SaveChangesAsync();
            }
            return entity;
        }
        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public T Add(T entity)
        {
            if (_isDeactivable)
            {
                (entity as IDeactivable).IsActive = true;
            }
            if (_isSoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = false;
            }
            if (_isAudit)
            {
                (entity as IAudit).UserIn = _currentUserService.UserName;
                (entity as IAudit).DateIn = DateTime.Now;

            }
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        public List<T> AddRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (_isDeactivable)
                {
                    (entity as IDeactivable).IsActive = true;
                }
                if (_isSoftDelete)
                {
                    (entity as ISoftDelete).IsDeleted = false;
                }
                if (_isAudit)
                {
                    (entity as IAudit).UserIn = _currentUserService.UserName;
                    (entity as IAudit).DateIn = DateTime.Now;

                }
            }
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }
        public async Task UpdateAsync(T entity)
        {
            if (_isAudit)
            {
                (entity as IAudit).UserUp = _currentUserService.UserName;
                (entity as IAudit).DateUp = DateTime.Now;
            }
            _dbContext.Attach(entity);
            EntityEntry entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;          
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(List<T> entities)
        {
            _dbContext.UpdateRange(entities);
            foreach (var entity in entities)
            {
                if (_isAudit)
                {
                    (entity as IAudit).UserUp = _currentUserService.UserName;
                    (entity as IAudit).DateUp = DateTime.Now;
                }
            }
            await _dbContext.SaveChangesAsync();
        }
        public void UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (_isAudit)
                {
                    (entity as IAudit).UserUp = _currentUserService.UserName;
                    (entity as IAudit).DateUp = DateTime.Now;
                }
            }
            _dbContext.UpdateRange(entities);
            _dbContext.SaveChanges();
        }
        public async Task DeleteRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public void DeleteRange(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(string[] includes = null)
        {
            var query = _dbContext
                 .Set<T>()
                 .Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                 .AsQueryable();
            query = _Includes(query, includes);
            return await query.AsNoTracking()
                .ToListAsync();
        }
        public IReadOnlyList<T> GetAll()
        {
            return _dbContext
                 .Set<T>()
                 //.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                 .AsNoTracking()
                 .ToList();
        }
        public void DisableAuditable() {
            _isAuditable = false;
        }
        #region "Private Access"
        public string _GetComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
            {
                case "like":
                    result = ".Contains(";
                    break;
                case "not like":
                    result = ".Contains(";
                    break;
                case "!=":
                    result = "!=";
                    break;
                case "<":
                    result = " < ";
                    break;
                case ">":
                    result = " > ";
                    break;
                case "<=":
                    result = " <= ";
                    break;
                case ">=":
                    result = " >= ";
                    break;
                case "<>":
                    result = " <> ";
                    break;
                default:
                    result = "=";
                    break;

            }
            return result;
        }

        private string _GetClosedTagComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
            {
                case "like":
                    result = ")";
                    break;
                case "not like":
                    result = ") == false";
                    break;
            }
            return result;
        }
        private string _GetConverter(object value)
        {
            var result = "";
            switch (value)
            {
                case Guid:
                    result = ".ToString()";
                    break;
            }
            return result;
        }
        private object _GetValue(object value)
        {
            var result = value;
            switch (value)
            {
                case Enum:
                    result = (int)value;
                    break;
            }
            return result;
        }
        protected IQueryable<T> _Includes(IQueryable<T> query, string[] includes = null)
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        private bool _IsUseDoubleQuote(object value)
        {
            switch (value)
            {
                case int:
                case Enum:
                    return false;
            }
            return true;
        }
        protected IQueryable<T> _Filters(IQueryable<T> query, List<RequestFilterParameter> filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.Type != "raw")
                    {
                        query = query.Where(
                        string.Format(
                            "{0}{5}{1}{3}{2}{3}{4}"
                            , filter.Field
                            , _GetComparisonOperator(filter.ComparisonOperator)
                            , _GetValue(filter.GetFilterValue())
                            , _IsUseDoubleQuote(filter.GetFilterValue()) ? "\"" : ""
                            , _GetClosedTagComparisonOperator(filter.ComparisonOperator)
                            , _GetConverter(filter.GetFilterValue())
                            )
                        );
                    }
                    else {
                        query = query.Where((string)filter.GetFilterValue());
                    }
                }
            }
            return query;
        }
        protected IQueryable<T> _Orders(IQueryable<T> query, List<string> orders, string sortType)
        {
            if (orders != null && orders.Count > 0)
            {
                query = query.OrderBy(
                        string.Format(
                            "{0} {1}"
                            , string.Join(",", orders)
                            , sortType
                            )
                        );
            }
            return query;
        }

        public async Task<List<T>> GetListByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(predicate);
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
