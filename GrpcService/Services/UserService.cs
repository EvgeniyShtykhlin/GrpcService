using Grpc.Core;
using GrpcService.Models;
using GrpcService.Intreafaces;
using GrpcService;

public class UserService : User.UserBase
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<UserReply> GetUserInfo(UserRequest request, ServerCallContext context)
    {
        var userModel = await _userRepository.GetUserByIdAsync(request.UserId);

        if (userModel == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        return new UserReply
        {
            Name = userModel.Name,
            Age = userModel.Age,
            Roles = { userModel.Roles.ToArray() },
            Address = new Address
            {
                Street = userModel.Address.Street,
                City = userModel.Address.City,
                Country = userModel.Address.Country
            },
            Coordinates = new Coordinates
            {
                Latitude = userModel.Coordinates.Latitude,
                Longitude = userModel.Coordinates.Longitude
            }
        };
    }

    public override async Task<CreateUserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var userModel = new UserModel
        {
            Name = request.Name,
            Age = request.Age,
            Roles = request.Roles.ToList(),
            Address = new Address
            {
                Street = request.Address.Street,
                City = request.Address.City,
                Country = request.Address.Country
            },
            Coordinates = new Coordinates
            {
                Latitude = request.Coordinates.Latitude,
                Longitude = request.Coordinates.Longitude
            }
        };

        var newUserId = await _userRepository.CreateUserAsync(userModel);

        return new CreateUserReply
        {
            UserId = newUserId,
            Message = $"User {request.Name} created with ID {newUserId}"
        };
    }

    public override async Task<GetAllUsersReply> GetAllUsers(Empty request, ServerCallContext context)
    {
        var users = await _userRepository.GetAllUsersAsync();

        var reply = new GetAllUsersReply();
        foreach (var user in users)
        {
            reply.Users.Add(new UserReply
            {
                Name = user.Name,
                Age = user.Age,
                Roles = { user.Roles.ToArray() },
                Address = new Address
                {
                    Street = user.Address.Street,
                    City = user.Address.City,
                    Country = user.Address.Country
                },
                Coordinates = new Coordinates
                {
                    Latitude = user.Coordinates.Latitude,
                    Longitude = user.Coordinates.Longitude
                },
                Id=user.Id
            });
        }

        return reply;
    }
}