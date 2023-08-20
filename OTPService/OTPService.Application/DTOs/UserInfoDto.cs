using Newtonsoft.Json;
using OTPService.Application.Utils;

namespace OTPService.Application.DTOs;

public class UserInfoDto
{
    [JsonProperty("sub")]
    public Guid SubjectId { get; set; }
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("last_name")]
    public string LastName { get; set; }
    
    [JsonProperty("gsm")]
    public string PhoneNumber { get; set; }
    
    [JsonProperty("dob")]
    [JsonConverter(typeof(UnixEpochDateTimeConverter))]
    public DateTime DateOfBirth { get; set; }
    
    [JsonProperty("mfa_enabled")]
    public bool MfaEnabled { get; set; }
    
    [JsonProperty("student_exp")]
    [JsonConverter(typeof(UnixEpochDateTimeConverter))]
    public DateTime StudentExpiration { get; set; }
    
    [JsonProperty("name")]
    public string Email { get; set; }
    
    [JsonProperty("preferred_username")]
    public string PreferredUsername { get; set; }
}