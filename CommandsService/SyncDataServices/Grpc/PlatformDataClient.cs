using AutoMapper;
using CommandService.SyncDataServices.Grpc;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcPlatform"]}");

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"], new GrpcChannelOptions { HttpHandler = handler });
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequests();

            try
            {
                var reply = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not call gRPC Server: {ex}");
                return null;
            }
            
        }
    }
}