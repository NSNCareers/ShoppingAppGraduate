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

        public async Task AddAsync<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            await set.AddAsync(obj);
            _logger.LogInformation("Added object");
        }

        public void Update<T>(T obj)where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public IQueryable<T> Query<T>()where T : class
        {
            return _context.Set<T>();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Attach<T>(T newEmployee) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(newEmployee);
        }

        public void Dispose()
        {
            _context = null;
        }

        public void Remove<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Remove(obj);
        }
    }
}
