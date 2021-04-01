using System.Collections.Generic;
using System.Linq;

namespace Mapper
{
    public class ConfigurationMapCollection<TSource, TDestination>
    {
        private static List<ConfigurationMap<TSource, TDestination>> ConfigurationMaps { get; set; } = new List<ConfigurationMap<TSource, TDestination>>();

        public static void Set(ConfigurationMap<TSource, TDestination> configurationMap) =>
            ConfigurationMaps.Add(configurationMap);

        public static ConfigurationMap<TSource, TDestination> Get(string nameProperty) =>
            ConfigurationMaps.FirstOrDefault(x => x.DestinationText == nameProperty);
    
        public static IEnumerable<ConfigurationMap<TSource, TDestination>> GetAll() =>
            ConfigurationMaps;
        
    }
}
