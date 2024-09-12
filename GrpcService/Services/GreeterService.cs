using Grpc.Core;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogWarning($"Message from client: {request.Name}");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<DayTimeReply> DayTime(DayTimeRequest request, ServerCallContext context)
        {
            _logger.LogWarning("Request day time from client1");
            return Task.FromResult(new DayTimeReply
            {
                Message = DateTime.UtcNow.ToString()
            });
        }
    }
}
