using gosarajevovol3.Models;
using Microsoft.AspNetCore.Identity;

namespace gosarajevovol3.Services.Abstract;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(string email, string password);
    Task<IdentityResult> RegisterOperatorAsync(string email, string password);
    Task<IdentityResult> RegisterAdminAsync(string email, string password);
    
    Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
    Task LogoutAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<IdentityResult> DeleteUserAsync(int id);
    Task<List<User>> GetAllUsersAsync();
}