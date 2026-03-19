using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x02000009 RID: 9
	public class BlockObjectModelSpec : ComponentSpec, IEquatable<BlockObjectModelSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002570 File Offset: 0x00000770
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectModelSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000257C File Offset: 0x0000077C
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002584 File Offset: 0x00000784
		[Serialize]
		public string FullModelName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000258D File Offset: 0x0000078D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002595 File Offset: 0x00000795
		[Serialize]
		public string UncoveredModelName { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000259E File Offset: 0x0000079E
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000025A6 File Offset: 0x000007A6
		[Serialize]
		public string UndergroundModelName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000025AF File Offset: 0x000007AF
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000025B7 File Offset: 0x000007B7
		[Serialize]
		public int UndergroundModelDepth { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x000025C0 File Offset: 0x000007C0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000260C File Offset: 0x0000080C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FullModelName = ");
			builder.Append(this.FullModelName);
			builder.Append(", UncoveredModelName = ");
			builder.Append(this.UncoveredModelName);
			builder.Append(", UndergroundModelName = ");
			builder.Append(this.UndergroundModelName);
			builder.Append(", UndergroundModelDepth = ");
			builder.Append(this.UndergroundModelDepth.ToString());
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026A1 File Offset: 0x000008A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectModelSpec left, BlockObjectModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026AD File Offset: 0x000008AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectModelSpec left, BlockObjectModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026C4 File Offset: 0x000008C4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FullModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UncoveredModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UndergroundModelName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<UndergroundModelDepth>k__BackingField);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002733 File Offset: 0x00000933
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectModelSpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002741 File Offset: 0x00000941
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000274C File Offset: 0x0000094C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<FullModelName>k__BackingField, other.<FullModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UncoveredModelName>k__BackingField, other.<UncoveredModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UndergroundModelName>k__BackingField, other.<UndergroundModelName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<UndergroundModelDepth>k__BackingField, other.<UndergroundModelDepth>k__BackingField));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000027D0 File Offset: 0x000009D0
		[CompilerGenerated]
		protected BlockObjectModelSpec([Nullable(1)] BlockObjectModelSpec original) : base(original)
		{
			this.FullModelName = original.<FullModelName>k__BackingField;
			this.UncoveredModelName = original.<UncoveredModelName>k__BackingField;
			this.UndergroundModelName = original.<UndergroundModelName>k__BackingField;
			this.UndergroundModelDepth = original.<UndergroundModelDepth>k__BackingField;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002809 File Offset: 0x00000A09
		public BlockObjectModelSpec()
		{
		}
	}
}
