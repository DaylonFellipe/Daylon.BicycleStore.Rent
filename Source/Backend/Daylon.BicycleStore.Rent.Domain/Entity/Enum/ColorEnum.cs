using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ColorEnum
    {
        Indefinite = 0,
        Black = 1,
        White = 2,
        Gray = 3,
        Red = 4,
        Blue = 5,
        Green = 6,
        Yellow = 7,
        Orange = 8,
        Purple = 9
    }
}
