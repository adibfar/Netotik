using AutoMapper;

namespace Netotik.AutoMapperProfiles.Extentions
{
    public static class AutoIgnore
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            foreach (var property in expression.TypeMap.GetUnmappedPropertyNames())
            {
                try
                {
                    expression.ForMember(property, a => a.Ignore());
                }
                catch
                {
                    throw;
                }
            }
            return expression;
        }
    }
}
