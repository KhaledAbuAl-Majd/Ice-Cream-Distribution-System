using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class UserService
    {
        public static async Task<User?> Login(string userName, string password)
        {
            var user = await UserData.Get(userName);

            if (user is null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;

            return null;
        }

        public static async Task<User?> Add(User user, string password)
        {
            if (user is null)
                return null;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            user.PasswordHash = passwordHash;

            return await UserData.Add(user);
        }

        public static async Task<bool> Update(User user)
        {
            if (user is null)
                return false;

            return await UserData.Update(user);
        }

        public static async Task<bool> Delete(User user)
        {
            if (user is null)
                return false;

            return await UserData.Delete(user);
        }

        public static async Task<User?> Get(int userID)
        {
            return await UserData.Get(userID);
        }
        public static async Task<User?> Get(string userName)
        {
            return await UserData.Get(userName);
        }

        public static async Task<List<User>?> GetAll()
        {
            return await UserData.GetAll();
        }
    }
}
