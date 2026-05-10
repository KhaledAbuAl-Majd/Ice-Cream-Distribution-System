using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class UserData
    {
        public static async Task<User?> Add(User user)
        {
            if (user is null)
                return null;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.AddAsync(user);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }

            return user;
        }

        public static async Task<bool> Update(User user)
        {
            if (user is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Update(user);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return false;
            }

            return true;
        }

        public static async Task<bool> Delete(User user)
        {
            if (user is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    user.IsDeleted = true;
                    context.Update(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                user.IsDeleted = false;
                return false;
            }

            user = null;
            return true;
        }

        public static async Task<User?> Get(int userID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Users.Include(u => u.Person).Where(u => !u.IsDeleted).FirstOrDefaultAsync(u => u.UserId == userID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<User?> Get(string userName)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Users.Include(u => u.Person).Where(u => !u.IsDeleted).FirstOrDefaultAsync(u => u.UserName == userName);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<List<User>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Users.Include(u => u.Person).Where(u => !u.IsDeleted).AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

    }
}
