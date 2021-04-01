using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper
{
    public class Mapper : IMapper
    {
        public Mapper() { }

        public Mapper SetConfig<TSource, TDestination>(
            Expression<Func<TSource, object>> source,
            Expression<Func<TDestination, object>> destination)
        {
            ConfigurationMapCollection<TSource, TDestination>.Set(
                new ConfigurationMap<TSource, TDestination>()
                {
                    Source = source,
                    Destination = destination
                });

            return this;
        }

        public Mapper SetConfig<TSource, TDestination>(
            Expression<Func<TSource, object>> source,
            Expression<Func<TDestination, object>> destination,
            Expression<Func<TSource, object>> appendValue,
            string SeparatorAppend)
        {
            ConfigurationMapCollection<TSource, TDestination>.Set(
                new ConfigurationMap<TSource, TDestination>()
                {
                    Source = source,
                    Destination = destination,
                    AppendValue = appendValue,
                    SeparatorAppend = SeparatorAppend
                });

            return this;
        }


        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            if (source == null)
                return new List<TDestination>();

            return source.Select(x => CreateMap<TSource, TDestination>(x));
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
                return Destination<TDestination>.GetInstance();

            return CreateMap<TSource, TDestination>(source);
        }

        private TDestination CreateMap<TSource, TDestination>(TSource source)
        {
            Source.Set(source);
            Destination<TDestination>.Set();

            foreach (PropertyInfo propSource in Source.GetProperties())
            {
                foreach (PropertyInfo propDestination in Destination<TDestination>.GetProperties())
                {
                    var configMap = ConfigurationMapCollection<TSource, TDestination>.Get(propDestination.Name);
                    Destination<TDestination>.SetValue(propDestination, configMap);

                    if (propDestination.Name == propSource.Name)
                    {
                        Destination<TDestination>.SetValue(propSource, propDestination);
                        break;
                    }
                }
            }

            return Destination<TDestination>.Get();
        }

        public T CreateEntity<T>(Form form)
        {
            var entity = (T)Activator.CreateInstance(typeof(T));

            foreach (var field in form.Fields)
            {
                foreach (var property in entity.GetType().GetProperties())
                {
                    if (property.Name.ToLower() == field.FieldName.ToLower())
                    {
                        if (field.Value == null)
                            continue;

                        if (property.PropertyType.Name == field.Value.GetType().Name)
                        {
                            property.SetValue(entity, field.Value);
                            break;
                        }
                        else
                        {
                            if (property.PropertyType.Name != "Nullable`1")
                            {
                                property.SetValue(entity, Convert.ChangeType(field.Value, property.PropertyType, new CultureInfo("pt-BR")));
                                break;
                            }
                        }
                    }
                }
            }

            return entity;
        }

    }
}
