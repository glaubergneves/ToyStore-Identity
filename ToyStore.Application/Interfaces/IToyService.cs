using ToyStore.Domain.Entities;

namespace ToyStore.Application.Interfaces
{
    public interface IToyService
    {
        Task<IList<Toy>> GetAllToysWithStoresAsync();
        Task AddToyAsync(Toy toy);
        Task<Toy> GetToyDetailsAsync(int id);
        Task UpdateToyAsync(Toy model);
        Task DeleteToyAsync(int id);
    }
}
