using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	internal interface IBinaryTree<[Nullable(2)] out T> : IBinaryTree
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000098 RID: 152
		T Value { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000099 RID: 153
		[Nullable(new byte[]
		{
			2,
			1
		})]
		IBinaryTree<T> Left { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154
		[Nullable(new byte[]
		{
			2,
			1
		})]
		IBinaryTree<T> Right { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }
	}
}
