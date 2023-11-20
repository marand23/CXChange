using CXChange.BuildingBlocks.Application.Query;
using CXChange.Modules.Kyc.Domain.User;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet
{
    internal class GetKycRequestsHandler : IConsumer<GetKycRequests>
    {
        IQueryHandler<GetKycRequests, KycRequestDto> _queryHandler;
        public GetKycRequestsHandler(IQueryHandler<GetKycRequests, KycRequestDto> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task Consume(ConsumeContext<GetKycRequests> context)
        {

            if (context.Message.Filters.Name != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.Name, nameof(context.Message.Filters.Name));
            }
            if (context.Message.Filters.NationalId != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.NationalId, nameof(context.Message.Filters.NationalId));
            }

            if (context.Message.Filters.SurName != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.SurName, nameof(context.Message.Filters.SurName));

            }
            if (context.Message.Filters.BirthDate != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.BirthDate, nameof(context.Message.Filters.BirthDate));
            }
            var initalQuery = $@"
    SELECT {nameof(User.Id).ToSnakeCase()},
           {nameof(KycRequestDto.Name).ToSnakeCase()} ,
           {nameof(KycRequestDto.SurName).ToSnakeCase()},
           {nameof(KycRequestDto.NationalId).ToSnakeCase()},
           {nameof(KycRequestDto.BirthDate).ToSnakeCase()}
    FROM   {"kyc.kyc_requests".ToSnakeCase()}";
            var result = await _queryHandler.ExecuteWithPaginationAndOrderingAsync(context.Message, initalQuery);
            if (result == null) { throw new ArgumentNullException("query did not return any results"); }
            await context.RespondAsync(new KycRequestList { KycRequests = result });

        }
    }
}
