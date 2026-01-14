using GarageManagement.DAL;
using GarageManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GarageManagement.BLL.Services
{
    public class UserService
    {
        private readonly GarageContext _context;

        public UserService(GarageContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Username == username && x.IsActive);
            if (user == null) return false;
            if (user.PasswordHash != password) return false;
            return true;
        }
    }
}
