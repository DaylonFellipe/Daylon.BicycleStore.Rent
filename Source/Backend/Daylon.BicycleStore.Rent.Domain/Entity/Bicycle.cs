using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class Bicycle
    {
        // Basic Properties
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Technical Properties
        public BrandEnum Brand { get; set; }
        public ModelEnum Model { get; set; }
        public ColorEnum Color { get; set; }

        // Stock Properties
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}
