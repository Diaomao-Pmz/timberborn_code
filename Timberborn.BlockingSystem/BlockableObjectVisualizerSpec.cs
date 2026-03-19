using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000D RID: 13
	public class BlockableObjectVisualizerSpec : ComponentSpec, IEquatable<BlockableObjectVisualizerSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002746 File Offset: 0x00000946
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockableObjectVisualizerSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002752 File Offset: 0x00000952
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000275A File Offset: 0x0000095A
		[Serialize]
		public string HideableObjectName { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002764 File Offset: 0x00000964
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockableObjectVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027B0 File Offset: 0x000009B0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HideableObjectName = ");
			builder.Append(this.HideableObjectName);
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027E1 File Offset: 0x000009E1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockableObjectVisualizerSpec left, BlockableObjectVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027ED File Offset: 0x000009ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockableObjectVisualizerSpec left, BlockableObjectVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002801 File Offset: 0x00000A01
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HideableObjectName>k__BackingField);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002820 File Offset: 0x00000A20
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockableObjectVisualizerSpec);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000023F7 File Offset: 0x000005F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000282E File Offset: 0x00000A2E
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockableObjectVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<HideableObjectName>k__BackingField, other.<HideableObjectName>k__BackingField));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000285F File Offset: 0x00000A5F
		[CompilerGenerated]
		protected BlockableObjectVisualizerSpec([Nullable(1)] BlockableObjectVisualizerSpec original) : base(original)
		{
			this.HideableObjectName = original.<HideableObjectName>k__BackingField;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002420 File Offset: 0x00000620
		public BlockableObjectVisualizerSpec()
		{
		}
	}
}
