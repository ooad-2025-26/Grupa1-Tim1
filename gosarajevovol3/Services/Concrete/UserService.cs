using gosarajevovol3.Models;
using gosarajevovol3.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Services.Concrete;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<IdentityResult> RegisterUserAsync(string email, string password)
    {
        var noviKorisnik = new RegisteredUser
        {
            UserName = email,
            Email = email
        };
        return await _userManager.CreateAsync(noviKorisnik, password);
    }
    public async Task<IdentityResult> RegisterOperatorAsync(string email, string password)
    {
        var noviOperator = new Operator
        {
            UserName = email,
            Email = email
        };
        return await _userManager.CreateAsync(noviOperator, password);
    }
    public async Task<IdentityResult> RegisterAdminAsync(string email, string password)
    {
        var noviAdmin = new Admin
        {
            UserName = email,
            Email = email
        };
        return await _userManager.CreateAsync(noviAdmin, password);
    }
    
    public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        return await _signInManager.PasswordSignInAsync(user.UserName!, password, rememberMe, lockoutOnFailure: false);
    }
    
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<IdentityResult> DeleteUserAsync(int id)
    {
        var korisnik = await GetUserByIdAsync(id);
        if (korisnik == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        return await _userManager.DeleteAsync(korisnik);
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
}