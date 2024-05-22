using Microsoft.EntityFrameworkCore;
using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;
using ToyStore.Infra;

namespace ToyStore.Application.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;
        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> GetStoreById(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task AddStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(Store store)
        {
            _context.Entry(store).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(int id)
        {
            var store = GetById(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
        }

        private Store GetById(int id)
        {
            return _context.Stores.Find(id);
        }
    }
}
