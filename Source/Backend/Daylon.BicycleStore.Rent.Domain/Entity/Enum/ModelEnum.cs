using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ModelEnum
    {
        Indefinite = 0,
        Road = 1,
        Mountain = 2,
        Urban = 3,
        Electric = 4,
        BMX = 5,
        Folding = 6,
        Kids = 7
    }
}
