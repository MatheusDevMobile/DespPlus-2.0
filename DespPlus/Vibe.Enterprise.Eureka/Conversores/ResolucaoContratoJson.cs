using System.Linq;
using System.Reflection;
using PowerDev.Enterprise.Eureka.Extensoes;

namespace PowerDev.Enterprise.Eureka.Conversores
{
    public class ResolucaoContratoJson //: DefaultContractResolver
    {
        //protected override IValueProvider CreateMemberValueProvider(MemberInfo memberInfo)
        //{
        //    if (memberInfo is PropertyInfo)
        //    {
        //        var prop = memberInfo.como<PropertyInfo>();
        //        if (prop.PropertyType.GetTypeInfo().IsGenericType && prop.PropertyType.isNullable())
        //            return new ProvedorJsonNulo(memberInfo, prop.PropertyType.GetGenericArguments().First());
        //    }

        //    if (memberInfo is FieldInfo)
        //    {
        //        var campo = memberInfo.como<FieldInfo>();
        //        if (campo.FieldType.GetTypeInfo().IsGenericType && campo.FieldType.isNullable())
        //            return new ProvedorJsonNulo(memberInfo, campo.FieldType.GetGenericArguments().First());
        //    }

        //    return base.CreateMemberValueProvider(memberInfo);
        //}
    }
}