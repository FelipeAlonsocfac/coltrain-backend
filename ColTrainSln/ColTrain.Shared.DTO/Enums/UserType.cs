using System.Text.Json.Serialization;

namespace ColTrain.Shared.DTO.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType : short
    {
        Admin = 1,
        Company = 2,
        User = 3
    }
}
