using Domain.Entities;

namespace Application.Contratos;
public interface IJwtGenerator{
    string CreateToken(User user);
}
