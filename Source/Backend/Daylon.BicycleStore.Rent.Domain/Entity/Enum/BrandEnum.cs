using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BrandEnum
    {
        Indefinite = 0,
        Trek = 1,
        Specialized = 2,
        Caloi = 3,
        Giant = 4,
        Cannondale = 5,
        Scott = 6,
        Oggi = 7,
        Sense = 8
    }
}
