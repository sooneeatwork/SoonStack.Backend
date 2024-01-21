using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.UseCases.MapperInterface
{
    public interface IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source);
        public List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList);

        public IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> sourceList);
        IEnumerable<T2> MapEnumerable<T1, T2>(IEnumerable<IEnumerable<T1>> products);
    }
}
