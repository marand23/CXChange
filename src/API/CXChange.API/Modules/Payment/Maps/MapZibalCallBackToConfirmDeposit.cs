using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.Maps
{
    public class MapZibalCallBackToConfirmDeposit : IMapper<ZibalGateWayRequest, VerifyTomanDeposit>
    {
        public VerifyTomanDeposit Map(ZibalGateWayRequest source, object extraData = null)
        {
            return new VerifyTomanDeposit
            {
                DepositId = Guid.Parse(source.orderId),
                Success = true,
                TrackId = source.trackId
            };
        }
    }
}

