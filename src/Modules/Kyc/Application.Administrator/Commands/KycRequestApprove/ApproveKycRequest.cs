using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestApprove
{
    public record ApproveKycRequest : IPersistable, ICommandBase
    {
        public Guid UserId { get; set; }

    }
}
