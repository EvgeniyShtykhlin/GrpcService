using GrpcService;
using Grpc.Core;

public class UserService : User.UserBase
{
    public override Task<UserReply> GetUserInfo(UserRequest request, ServerCallContext context)
    {
        var userReply = new UserReply
        {
            Name = "John Doe",
            Age = 30,
            Roles = { "admin", "editor" },
            Address = new Address
            {
                Street = "123 Main St",
                City = "New York",
                Country = "USA"
            },
            Coordinates = new Coordinates
            {
                Latitude = 40.712776,
                Longitude = -74.005974
            }
        };

        return Task.FromResult(userReply);
    }

    public override Task<CreateUserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var newUserId = 1001;

        return Task.FromResult(new CreateUserReply
        {
            UserId = newUserId,
            Message = $"User {request.Name} created with ID {newUserId}"
        });
    }
}