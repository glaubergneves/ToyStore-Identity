using Microsoft.EntityFrameworkCore;
using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;
using ToyStore.Infra;

namespace ToyStore.Application.Repositories
{
    public class ToyRepository : IToyRepository
    {
        private readonly ApplicationDbContext _context;
        public ToyRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddToyAsync(Toy toy)
        {
            _context.Toys.Add(toy);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Toy>> GetAllToysWithStoresAsync()
        {
            return await _context.Toys.Include(x => x.Store).ToListAsync();
        }

        public async Task<Toy> GetToyWithStoreByIdAsync(int id)
        {
            return await _context.Toys
                .Include(t => t.Store) 
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateToyAsync(Toy toy)
        {
            _context.Entry(toy).State = EntityState.Modified; 
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteToyAsync(int id)
        {
            var toy = GetById(id);
            _context.Toys.Remove(toy);
            await _context.SaveChangesAsync();
        }

        private Toy GetById(int id)
        {
            return _context.Toys.Find(id);
        }
    }
}
