using System.Threading.Tasks;

namespace CarrinhoCompras.BLL.Contracts
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken();

        Task<bool> ValidateToken(string token);
    }
}