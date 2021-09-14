using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class HookahService : Hookah.HookahBase
    {
        private readonly ILogger<GreeterService> _logger;
        public HookahService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<CoalResponse> GetCoalRoasting(CoalRequest request, ServerCallContext context)
        {
            CoalFried coalFried = CoalFried.Middle;
            switch ((request.Type, request.Coals, request.WarmUpTime))
            {
                case (CoalType.Firebrand, < 3, > 5):
                    coalFried = CoalFried.Hard;
                    break;
                case (CoalType.Firebrand, < 3, 5):
                    coalFried = CoalFried.Middle;
                    break;
                case (CoalType.Firebrand, < 3, < 5):
                    coalFried = CoalFried.Easy;
                    break;
            }

            return Task.FromResult(new CoalResponse
            {
                Fried = coalFried
            });
        }
    }
}