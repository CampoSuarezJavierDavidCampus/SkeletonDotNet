using Domain.Interface.Security;

namespace Application.Helpers.Security;
public class JWT: IJWT{
    public string Key { get; set; } = null!;   
    public string Issuer { get; set; } = null!;    
    public string Audience { get; set; } = null!;    
    public string DurationInMinutes { get; set; } = null!;    
}
