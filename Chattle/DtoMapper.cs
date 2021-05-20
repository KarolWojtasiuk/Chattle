using Chattle.Models;
using Chattle.ModelsDto;
using MapsterMapper;

namespace Chattle
{
    public class DtoMapper
    {
        public DtoMapper()
        {
            _mapper = new Mapper();
        }

        private readonly IMapper _mapper;

        public TDomain DtoToDomain<TDto, TDomain>(TDto entity) where TDto : IDtoObject where TDomain : IIdentifiable
        {
            return _mapper.Map<TDomain>(entity);
        }

        public TDto DomainToDto<TDomain, TDto>(TDomain domainObject) where TDto : IDtoObject where TDomain : IIdentifiable
        {
            return _mapper.Map<TDto>(domainObject);
        }
    }
}