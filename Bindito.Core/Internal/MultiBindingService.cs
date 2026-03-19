using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000AE RID: 174
	public class MultiBindingService : IMultiBindingService
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000044BE File Offset: 0x000026BE
		public bool IsMultiBound(Type parameterType, out Type multiBoundType)
		{
			if (MultiBindingService.TypeOfIEnumerable.IsAssignableFrom(parameterType))
			{
				multiBoundType = parameterType.GenericTypeArguments[0];
				return true;
			}
			multiBoundType = null;
			return false;
		}

		// Token: 0x040000AE RID: 174
		private static readonly Type TypeOfIEnumerable = typeof(IEnumerable<object>);
	}
}
