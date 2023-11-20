using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;

namespace CXChange.API.Modules.Payment.GateWayAdapter
{
    public interface IGateWayAdapter
    {
        VerifyTomanDeposit Adapt(IAdaptee adaptee);
    }
}
