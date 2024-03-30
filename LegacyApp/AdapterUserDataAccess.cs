namespace LegacyApp;

public class AdapterUserDataAccess : IUserDataAccess
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}