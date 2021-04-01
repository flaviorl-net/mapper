using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mapper
{
    public interface IMapper
    {
        Mapper SetConfig<TSource, TDestination>(
            Expression<Func<TSource, object>> Source,
            Expression<Func<TDestination, object>> Destination);

        Mapper SetConfig<TSource, TDestination>(
            Expression<Func<TSource, object>> source,
            Expression<Func<TDestination, object>> destination,
            Expression<Func<TSource, object>> appendValue,
            string SeparatorAppend);

        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);

        TDestination Map<TSource, TDestination>(TSource source);

        T CreateEntity<T>(Form form);
    }
}
