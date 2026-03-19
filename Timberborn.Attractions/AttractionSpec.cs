using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.Attractions
{
	// Token: 0x0200000F RID: 15
	public class AttractionSpec : ComponentSpec, IEquatable<AttractionSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D3D File Offset: 0x00000F3D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AttractionSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D49 File Offset: 0x00000F49
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002D51 File Offset: 0x00000F51
		[Serialize]
		public ImmutableArray<ContinuousEffectSpec> Effects { get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002D5C File Offset: 0x00000F5C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AttractionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DA8 File Offset: 0x00000FA8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Effects = ");
			builder.Append(this.Effects.ToString());
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DF2 File Offset: 0x00000FF2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AttractionSpec left, AttractionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DFE File Offset: 0x00000FFE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AttractionSpec left, AttractionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E12 File Offset: 0x00001012
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.GetHashCode(this.<Effects>k__BackingField);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E31 File Offset: 0x00001031
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AttractionSpec);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002772 File Offset: 0x00000972
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E3F File Offset: 0x0000103F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AttractionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.Equals(this.<Effects>k__BackingField, other.<Effects>k__BackingField));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E70 File Offset: 0x00001070
		[CompilerGenerated]
		protected AttractionSpec([Nullable(1)] AttractionSpec original) : base(original)
		{
			this.Effects = original.<Effects>k__BackingField;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000027C1 File Offset: 0x000009C1
		public AttractionSpec()
		{
		}
	}
}
