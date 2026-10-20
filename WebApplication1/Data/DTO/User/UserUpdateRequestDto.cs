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

    [JsonProperty("gender")]
    public string? Gender { get; set; }

    [JsonProperty("first_name")]
    public string? FirstName { get; set; }

    [JsonProperty("last_name")]
    public string? LastName { get; set; }

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("date_of_birth")]
    public string? DateOfBirth { get; set; }

    [JsonProperty("city")]
    public string? City { get; set; }

    [JsonProperty("zip_code")]
    public string? ZipCode { get; set; }

}