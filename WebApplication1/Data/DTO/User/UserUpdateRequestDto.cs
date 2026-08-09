using Newtonsoft.Json;

namespace WebApplication1.Data.DTO.User;

public class UserUpdateRequestDto
{

    [JsonProperty("full_name")]
    public string? FullName { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("address")]
    public string? Address { get; set; }

    [JsonProperty("phone")]
    public string? Phone { get; set; }

    [JsonProperty("telegram")]
    public string? Telegram { get; set; }

    [JsonProperty("whatapp")]
    public string? Whatapp { get; set; }

}