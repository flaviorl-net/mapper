using System.Linq;
using System.Reflection;

namespace Mapper
{
    public class Source
    {
        private static object objSource { get; set; }

        private static PropertyInfo[] Properties { get; set; }

        public static void Set<TSource>(TSource source)
        {
            objSource = source;
            Properties = typeof(TSource).GetProperties();
        }

        public static object Get() => objSource;

        public static PropertyInfo[] GetProperties() => Properties;

        public static PropertyInfo GetProperty(string nameProp) =>
            Properties.FirstOrDefault(x => x.Name == nameProp);

    }
}
