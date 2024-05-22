namespace ToyStore.Mvc.Models
{
    public class ToyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public StoreViewModel? Store { get; set; }
    }
}
