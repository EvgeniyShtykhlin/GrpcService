using GrpcService.Models;

namespace GrpcService.Intreafaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<int> CreateUserAsync(UserModel user);
    }
}
