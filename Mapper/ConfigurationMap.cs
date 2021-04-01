using System;
using System.Linq.Expressions;

namespace Mapper
{
    public class ConfigurationMap<TSource, TDestination>
    {
        public Expression<Func<TSource, object>> Source { get; set; }
        public Expression<Func<TDestination, object>> Destination { get; set; }
        public Expression<Func<TSource, object>> AppendValue { get; set; }
        public string SeparatorAppend { get; set; }

        public string SourceText
        {
            get
            {
                if (Source.Body is MemberExpression)
                {
                    var body = (MemberExpression)Source.Body;
                    return body.Member.Name;
                }
                else
                    if (!(Source.Body is MemberExpression))
                {
                    var body = ((UnaryExpression)Source.Body).Operand as MemberExpression;
                    return body.Member.Name;
                }
                return string.Empty;
            }
        }

        public string DestinationText
        {
            get
            {
                if (Destination.Body is MemberExpression)
                {
                    var body = (MemberExpression)Destination.Body;
                    return body.Member.Name;
                }
                else
                    if (!(Destination.Body is MemberExpression))
                {
                    var body = ((UnaryExpression)Destination.Body).Operand as MemberExpression;
                    return body.Member.Name;
                }
                return string.Empty;
            }
        }

        public string AppendValueText
        {
            get
            {
                if (AppendValue.Body is MemberExpression)
                {
                    var body = (MemberExpression)AppendValue.Body;
                    return body.Member.Name;
                }
                else
                    if (!(AppendValue.Body is MemberExpression))
                {
                    var body = ((UnaryExpression)AppendValue.Body).Operand as MemberExpression;
                    return body.Member.Name;
                }
                return string.Empty;
            }
        }

        
    }
}
