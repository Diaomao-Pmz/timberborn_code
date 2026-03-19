using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000025 RID: 37
	[NullableContext(2)]
	internal interface IBinaryTree
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000093 RID: 147
		int Height { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000094 RID: 148
		bool IsEmpty { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000095 RID: 149
		int Count { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000096 RID: 150
		IBinaryTree Left { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000097 RID: 151
		IBinaryTree Right { get; }
	}
}
