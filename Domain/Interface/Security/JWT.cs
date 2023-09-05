namespace Domain.Interface.Security;
public interface IJWT{
    string Key { get; set; }
    string Issuer { get; set; }
    string Audience { get; set; }
    string DurationInMinutes { get; set; }
}
