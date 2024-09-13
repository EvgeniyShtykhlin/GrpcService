using GrpcService.Models;
using System.Collections.Concurrent;

namespace GrpcService.Intreafaces
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<int, UserModel> _users = new ConcurrentDictionary<int, UserModel>();
        private int _nextId = 1;

        public InMemoryUserRepository()
        {
            _users.TryAdd(_nextId++, new UserModel
            {
                Id= _nextId, 
                Name = "Alice Smith",
                Age = 28,
                Roles = new List<string> { "user" },
                Address = new Address
                {
                    Street = "456 Elm St",
                    City = "Los Angeles",
                    Country = "USA"
                },
                Coordinates = new Coordinates
                {
                    Latitude = 34.052235,
                    Longitude = -118.243683
                }
            });

            _users.TryAdd(_nextId++, new UserModel
            {
                Id = _nextId,
                Name = "Bob Johnson",
                Age = 34,
                Roles = new List<string> { "admin", "user" },
                Address = new Address
                {
                    Street = "789 Maple Ave",
                    City = "San Francisco",
                    Country = "USA"
                },
                Coordinates = new Coordinates
                {
                    Latitude = 37.774929,
                    Longitude = -122.419418
                }
            });

            _users.TryAdd(_nextId++, new UserModel
            {
                Id = _nextId,
                Name = "Carol Davis",
                Age = 22,
                Roles = new List<string> { "user" },
                Address = new Address
                {
                    Street = "123 Oak St",
                    City = "Seattle",
                    Country = "USA"
                },
                Coordinates = new Coordinates
                {
                    Latitude = 47.606209,
                    Longitude = -122.332069
                }
            });

            _users.TryAdd(_nextId++, new UserModel
            {
                Id = _nextId,
                Name = "David Wilson",
                Age = 45,
                Roles = new List<string> { "editor" },
                Address = new Address
                {
                    Street = "321 Pine St",
                    City = "Chicago",
                    Country = "USA"
                },
                Coordinates = new Coordinates
                {
                    Latitude = 41.878113,
                    Longitude = -87.629799
                }
            });

            _users.TryAdd(_nextId++, new UserModel
            {
                Id = _nextId,  
                Name = "Eve Adams",
                Age = 30,
                Roles = new List<string> { "user", "moderator" },
                Address = new Address
                {
                    Street = "654 Cedar Blvd",
                    City = "Houston",
                    Country = "USA"
                },
                Coordinates = new Coordinates
                {
                    Latitude = 29.760427,
                    Longitude = -95.369804
                }
            });
        }

        public Task<UserModel> GetUserByIdAsync(int userId)
        {
            _users.TryGetValue(userId, out var user);
            return Task.FromResult(user);
        }

        public Task<int> CreateUserAsync(UserModel user)
        {
            user.Id = _nextId++;
            _users[user.Id] = user;
            return Task.FromResult(user.Id);
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.Values.AsEnumerable());
        }
    }
}
