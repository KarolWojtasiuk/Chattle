using Chattle.Database.Entities;
using Chattle.Models;
using MapsterMapper;

namespace Chattle.Database
{
    public class EntityMapper
    {
        public EntityMapper()
        {
            _mapper = new Mapper();
        }

        private readonly IMapper _mapper;

        public TDomain EntityToDomain<TEntity, TDomain>(TEntity entity) where TEntity : IEntity where TDomain : IIdentifiable
        {
            return _mapper.Map<TDomain>(entity);
        }

        public TEntity DomainToEntity<TDomain, TEntity>(TDomain domainObject) where TEntity : IEntity where TDomain : IIdentifiable
        {
            return _mapper.Map<TEntity>(domainObject);
        }
    }
}