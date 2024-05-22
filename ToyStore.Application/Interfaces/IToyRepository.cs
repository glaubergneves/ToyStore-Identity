using ToyStore.Domain.Entities;

namespace ToyStore.Application.Interfaces
{
    public interface IToyRepository
    {
        Task<IList<Toy>> GetAllToysWithStoresAsync();
        Task AddToyAsync(Toy toy);
        Task<Toy> GetToyWithStoreByIdAsync(int id);
        Task UpdateToyAsync(Toy toy);
        Task DeleteToyAsync(int id);
    }
}
