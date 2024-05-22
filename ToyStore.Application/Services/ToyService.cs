using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;

namespace ToyStore.Application.Services
{
    public class ToyService : IToyService
    {
        private readonly IToyRepository _repository;
        public ToyService(IToyRepository repository) 
        {
            _repository = repository;
        }

        public async Task AddToyAsync(Toy toy)
        {
            await _repository.AddToyAsync(toy);
        }

        public Task DeleteToyAsync(int id)
        {
            return _repository.DeleteToyAsync(id);
        }

        public async Task<IList<Toy>> GetAllToysWithStoresAsync()
        {
            return await _repository.GetAllToysWithStoresAsync();
        }

        public async Task<Toy> GetToyDetailsAsync(int id)
        {
            var toy = await _repository.GetToyWithStoreByIdAsync(id); 

            if (toy == null)
            {
                throw new KeyNotFoundException("Brinquedo não encontrado");
            }

            return toy;
        }

        public async Task UpdateToyAsync(Toy toy)
        {
            if (toy == null)
            {
                throw new ArgumentNullException(nameof(toy)); 
            }

            await _repository.UpdateToyAsync(toy); 
        }
    }
}
