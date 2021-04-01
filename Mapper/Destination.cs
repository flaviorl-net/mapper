using System;
using System.Globalization;
using System.Reflection;

namespace Mapper
{
    public class Destination<TDestination>
    {
        private static TDestination objDestination { get; set; }

        private static PropertyInfo[] Properties { get; set; }

        public static TDestination Get() => objDestination;

        public static void Set()
        {
            objDestination = GetInstance();
            Properties = typeof(TDestination).GetProperties();
        }

        public static PropertyInfo[] GetProperties() => Properties;

        public static TDestination GetInstance() =>
            (TDestination)Activator.CreateInstance(typeof(TDestination));

        public static void SetValue(PropertyInfo propSource, PropertyInfo propDestination)
        {
            if (propSource.PropertyType.Name == propDestination.PropertyType.Name)
            {
                propDestination.SetValue(objDestination, propSource.GetValue(Source.Get()));
            }
            else
            {
                SetValue(propSource, propDestination, "pt-BR");
            }
        }

        public static void SetValue(PropertyInfo propSource, PropertyInfo propDestination, string culture)
        {
            var prop = propSource.GetValue(Source.Get());
            if (prop != null)
            {
                propDestination.SetValue(objDestination,
                    Convert.ChangeType(prop, propSource.PropertyType,
                    new CultureInfo(culture)));
            }
        }

        public static void SetValue(PropertyInfo propSource, string separator, PropertyInfo propDestination,
            PropertyInfo propSourceAppendValue) =>
            propDestination.SetValue(objDestination, string.Concat(propSource.GetValue(Source.Get()), separator,
                propSourceAppendValue.GetValue(Source.Get())));

        public static void SetValue<TSource>(PropertyInfo propDestination, ConfigurationMap<TSource, TDestination> configMap)
        {
            if (configMap == null)
                return;

            var propSource = Source.GetProperty(configMap.SourceText);

            if (configMap.AppendValue == null)
            {
                SetValue(propSource, propDestination);
            }
            else
            {
                var propAppendValue = Source.GetProperty(configMap.AppendValueText);
                SetValue(propSource, configMap.SeparatorAppend, propDestination, propAppendValue);
            }
        }

    }
}
