
using System;
using System.Linq;
using System.Reflection;

namespace PoS.Di.Extensions;

internal static class TypeExtensions
{
    public static object[] GetConstructorArguments(this ConstructorInfo constructor)
    {
        return constructor.GetParameters()
            .Select(param => param.ParameterType)
            .ToArray();
    }

    public static ConstructorInfo GetConstructor(this Type type) =>
       type.GetConstructors()
           .OrderByDescending(c => c.GetParameters().Length)
           .FirstOrDefault();
}
