namespace GrpcService.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Roles { get; set; }
        public Address Address { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
