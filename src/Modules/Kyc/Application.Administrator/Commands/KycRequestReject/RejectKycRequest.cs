using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestReject
{
    public record RejectKycRequest : ICommandBase, IPersistable
    {
        public Guid UserId { get; init; }

    }
}
