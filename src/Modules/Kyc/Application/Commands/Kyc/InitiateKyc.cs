using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.Application.Commands.Kyc
{
    public record InitiateKyc : ICommandBase, IPersistable, IValidatable
    {
        public Guid UserId { get; init; }
        public string NationalId { get; init; }
        public string Name { get; init; }
        public string SurName { get; init; }
        public DateOnly BithDate { get; set; }
        public IFormFile Image { get; init; }
    };
}
