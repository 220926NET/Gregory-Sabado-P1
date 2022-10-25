//handles interactions with user, and authrepository i.e. prompting user to login then sending login info to authrepository

using Models;
using DataAccess;

namespace BusinessLogic;

public interface IAuthService
{
    User Login(string username, string password);
    int Register(string username, string password);

    User GetUser(int id);

}
public class AuthService : IAuthService
{
    private IAuthRepository authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public User GetUser(int id)
    {
        return authRepository.GetUser(id);
    }

    //prompts user input
    //outputs validation of successful login
    public User Login(string username, string password)
    {
        User user = new();
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            user = authRepository.GetUser(username, password);
        }
        return user;
    }
    public int Register(string username, string password)
    {
        return authRepository.CreateUser(username, password);
    }



}

