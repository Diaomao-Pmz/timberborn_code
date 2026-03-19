using System;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Class)]
	public class SpecAliasAttribute : Attribute
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000434B File Offset: 0x0000254B
		public ImmutableArray<string> Aliases { get; }

		// Token: 0x06000102 RID: 258 RVA: 0x00004353 File Offset: 0x00002553
		[UsedImplicitly]
		public SpecAliasAttribute(params string[] aliases)
		{
			this.Aliases = aliases.ToImmutableArray<string>();
		}
	}
}
