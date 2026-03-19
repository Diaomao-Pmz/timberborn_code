using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000011 RID: 17
	public class TypeBlacklist
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void Verify(Type type)
		{
			if (TypeBlacklist.BlacklistedTypes.Contains(type))
			{
				string text = "BaseComponent";
				string arg = text + ".AllComponents";
				throw new InvalidOperationException(string.Format("Retrieving {0} through {1} is not allowed. Use {2} instead.", type, text, arg));
			}
		}

		// Token: 0x0400001E RID: 30
		public static readonly HashSet<Type> BlacklistedTypes = new HashSet<Type>
		{
			typeof(BaseComponent),
			typeof(ComponentSpec),
			typeof(object)
		};
	}
}
