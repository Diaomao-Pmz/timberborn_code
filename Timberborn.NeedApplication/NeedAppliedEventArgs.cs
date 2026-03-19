using System;
using Timberborn.Characters;
using Timberborn.Effects;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000013 RID: 19
	public readonly struct NeedAppliedEventArgs
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D3E File Offset: 0x00000F3E
		public Character Character { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D46 File Offset: 0x00000F46
		public InstantEffect NeedEffect { get; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002D4E File Offset: 0x00000F4E
		public NeedAppliedEventArgs(Character character, InstantEffect needEffect)
		{
			this.Character = character;
			this.NeedEffect = needEffect;
		}
	}
}
