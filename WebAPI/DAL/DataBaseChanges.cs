using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;

namespace WebAPI.DAL
{
    public class DataBaseChanges : IDataBaseChanges
    {
        private ShoppingCartContext _context;
        private ILogger<DataBaseChanges> _logger;

        public DataBaseChanges(ShoppingCartContext context, ILogger<DataBaseChanges> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task AddAsync<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            await set.AddAsync(obj);
            _logger.LogInformation("Added object");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Update<T>(T obj)where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Query<T>()where T : class
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newEmployee"></param>
        public void Attach<T>(T newEmployee) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(newEmployee);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _context = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Remove<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Remove(obj);
        }
    }
}
