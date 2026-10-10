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
        public BirthDay BirthDay { get; init; }
        public IFormFile Image { get; init; }
    };
    public record BirthDay
    {
        public int Day { get; init; }
        public int Month { get; init; }
        public int Year { get; init; }
    }
}
