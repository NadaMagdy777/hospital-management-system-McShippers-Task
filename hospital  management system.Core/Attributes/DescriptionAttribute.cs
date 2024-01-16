using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class DescriptionAttribute : Attribute
{
    public string Description { get; set; }
    public DescriptionAttribute(string description)
    {
        Description = description;
    }

    public static string GetDescription(object obj)
    {
        Type? type = obj?.GetType();

        MemberInfo? member = type?.GetMember(obj.ToString()).FirstOrDefault();

        DescriptionAttribute? attribute = 
            (DescriptionAttribute?)
            member?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault();

        string description = attribute?.Description ?? "NO AVAILABLE DESCRIPTION";

        return description;
    }
}
