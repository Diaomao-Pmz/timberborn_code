using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200001A RID: 26
	public class ConstructionSiteProgressVisualizerSpec : ComponentSpec, IEquatable<ConstructionSiteProgressVisualizerSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003C76 File Offset: 0x00001E76
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ConstructionSiteProgressVisualizerSpec);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003C82 File Offset: 0x00001E82
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003C8A File Offset: 0x00001E8A
		[Serialize]
		public ImmutableArray<float> ProgressThresholds { get; set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003C94 File Offset: 0x00001E94
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConstructionSiteProgressVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ProgressThresholds = ");
			builder.Append(this.ProgressThresholds.ToString());
			return true;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003D2A File Offset: 0x00001F2A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstructionSiteProgressVisualizerSpec left, ConstructionSiteProgressVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D36 File Offset: 0x00001F36
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstructionSiteProgressVisualizerSpec left, ConstructionSiteProgressVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003D4A File Offset: 0x00001F4A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<float>>.Default.GetHashCode(this.<ProgressThresholds>k__BackingField);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003D69 File Offset: 0x00001F69
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstructionSiteProgressVisualizerSpec);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000345F File Offset: 0x0000165F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D77 File Offset: 0x00001F77
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstructionSiteProgressVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<float>>.Default.Equals(this.<ProgressThresholds>k__BackingField, other.<ProgressThresholds>k__BackingField));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[CompilerGenerated]
		protected ConstructionSiteProgressVisualizerSpec([Nullable(1)] ConstructionSiteProgressVisualizerSpec original) : base(original)
		{
			this.ProgressThresholds = original.<ProgressThresholds>k__BackingField;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DBD File Offset: 0x00001FBD
		public ConstructionSiteProgressVisualizerSpec()
		{
		}
	}
}
