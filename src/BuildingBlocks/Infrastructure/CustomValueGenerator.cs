using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CXChange.BuildingBlocks.Infrastructure
{
    public class CustomValueGenerator<T> : ValueGenerator<T>
    {
        private readonly Func<T> _generator;
        public CustomValueGenerator(Func<T> generator)
        {
            _generator = generator;
        }
        public override T Next(EntityEntry entry)
        {
            return _generator();
        }
        public override bool GeneratesTemporaryValues => false;
    }
}
