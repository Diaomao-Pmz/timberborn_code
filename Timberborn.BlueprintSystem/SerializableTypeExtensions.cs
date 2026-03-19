using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000023 RID: 35
	public static class SerializableTypeExtensions
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x0000421C File Offset: 0x0000241C
		public static IEnumerable<PropertyInfo> GetSerializedProperties(this Type type)
		{
			return type.GetProperties(SerializableTypeExtensions.AllInstanceFlag).Where(delegate(PropertyInfo propertyInfo)
			{
				SerializeAttribute customAttribute = propertyInfo.GetCustomAttribute<SerializeAttribute>();
				return customAttribute != null && !customAttribute.HasSource;
			}).OrderBy(delegate(PropertyInfo property)
			{
				if (!(property.DeclaringType != type))
				{
					return 1;
				}
				return 0;
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000427C File Offset: 0x0000247C
		public static bool IsSerializable(this Type type)
		{
			if (!typeof(ComponentSpec).IsAssignableFrom(type))
			{
				return type.GetProperties(SerializableTypeExtensions.AllInstanceFlag).Any((PropertyInfo propertyInfo) => propertyInfo.GetCustomAttribute<SerializeAttribute>() != null);
			}
			return true;
		}

		// Token: 0x04000053 RID: 83
		public static readonly BindingFlags AllInstanceFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
