namespace ToyStore.Domain.Entities
{
    public class Toy
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Type { get; set; } 
        public decimal Price { get; set; }
        public int StoreId { get; set; } 
        public Store? Store{ get; set; } 
    }
}
