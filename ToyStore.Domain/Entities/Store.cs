﻿namespace ToyStore.Domain.Entities
{
    public class Store
    {
        public int Id { get; set; } 

        public string Name { get; set; } 

        public ICollection<Toy>? Toys { get; set; }
    }
}
