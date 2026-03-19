using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SecureObjectPool<[Nullable(2)] T, [Nullable(0)] TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00009BE9 File Offset: 0x00007DE9
		public static void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00009C0D File Offset: 0x00007E0D
		public static bool TryTake(TCaller caller, [Nullable(new byte[]
		{
			2,
			1
		})] out SecurePooledObject<T> item)
		{
			if (caller.PoolUserId != -1 && AllocFreeConcurrentStack<SecurePooledObject<T>>.TryTake(out item))
			{
				item.Owner = caller.PoolUserId;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00009C41 File Offset: 0x00007E41
		public static SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			return new SecurePooledObject<T>(newValue)
			{
				Owner = caller.PoolUserId
			};
		}
	}
}
