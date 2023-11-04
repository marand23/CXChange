using CXChange.AdminAPI.Modules.Kyc.Requests;
using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet;

namespace CXChange.AdminAPI.Modules.Kyc.Maps
{
    public class MapKycRequestToKycRequestQuery : IMapper<KycRequests, GetKycRequests>
    {
        IMapService _mapperService;
        public MapKycRequestToKycRequestQuery(IMapService mapperService)
        {
            _mapperService = mapperService;
        }
        public GetKycRequests Map(KycRequests source, object extraData = null)
        {
            return new GetKycRequests
            {
                Page = source.Page,
                PageSize = source.PageSize,
                OrderBy = source.OrderBy != null ? source.OrderBy.Select(x => new OrderByQuery { Field = x.Field, Dir = x.Dir }).ToList() : null,
                Filters = MapFilterItemRequestToFilterItem(source.Filters),


            };
        }
        private KycRequestFilters MapFilterItemRequestToFilterItem(KycFiltersRequestReq source)
        {
            return new()
            {

                // BirthDate = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.BirthDate),
                Name = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.Name),
                NationalId = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.NationalId),
                SurName = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.SurName),
                UserId = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.UserId),
                BirthDate = null





            };
        }
    }
}

