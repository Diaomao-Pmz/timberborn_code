using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bindito.Core.Internal
{
	// Token: 0x020000B4 RID: 180
	public static class TypeFormatting
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00004938 File Offset: 0x00002B38
		public static string Format(Type type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string name = type.Name;
			int num = name.IndexOf('`');
			stringBuilder.Append((num >= 0) ? name.Remove(num) : name);
			if (type.IsGenericType)
			{
				stringBuilder.Append("<");
				stringBuilder.Append(string.Join(",", type.GenericTypeArguments.Select(new Func<Type, string>(TypeFormatting.Format))));
				stringBuilder.Append(">");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000049C0 File Offset: 0x00002BC0
		public static string FormatChain(IEnumerable<Type> dependencyChain)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Type> list = dependencyChain.ToList<Type>();
			if (list.Count > 0)
			{
				stringBuilder.Append(TypeFormatting.Format(list.First<Type>()));
				for (int i = 1; i < list.Count; i++)
				{
					stringBuilder.Append(" => " + TypeFormatting.Format(list[i]));
				}
			}
			return stringBuilder.ToString();
		}
	}
}
