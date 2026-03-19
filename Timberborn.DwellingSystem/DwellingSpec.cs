using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.DwellingSystem
{
	// Token: 0x0200000F RID: 15
	public class DwellingSpec : ComponentSpec, IEquatable<DwellingSpec>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002F8F File Offset: 0x0000118F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DwellingSpec);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F9B File Offset: 0x0000119B
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FA3 File Offset: 0x000011A3
		[Serialize]
		public int MaxBeavers { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002FAC File Offset: 0x000011AC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002FB4 File Offset: 0x000011B4
		[Serialize]
		public ImmutableArray<ContinuousEffectSpec> SleepEffects { get; set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002FC0 File Offset: 0x000011C0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DwellingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000300C File Offset: 0x0000120C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxBeavers = ");
			builder.Append(this.MaxBeavers.ToString());
			builder.Append(", SleepEffects = ");
			builder.Append(this.SleepEffects.ToString());
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000307D File Offset: 0x0000127D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DwellingSpec left, DwellingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003089 File Offset: 0x00001289
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DwellingSpec left, DwellingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000309D File Offset: 0x0000129D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxBeavers>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.GetHashCode(this.<SleepEffects>k__BackingField);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030D3 File Offset: 0x000012D3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DwellingSpec);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030E1 File Offset: 0x000012E1
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030EC File Offset: 0x000012EC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DwellingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxBeavers>k__BackingField, other.<MaxBeavers>k__BackingField) && EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.Equals(this.<SleepEffects>k__BackingField, other.<SleepEffects>k__BackingField));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003140 File Offset: 0x00001340
		[CompilerGenerated]
		protected DwellingSpec([Nullable(1)] DwellingSpec original) : base(original)
		{
			this.MaxBeavers = original.<MaxBeavers>k__BackingField;
			this.SleepEffects = original.<SleepEffects>k__BackingField;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003161 File Offset: 0x00001361
		public DwellingSpec()
		{
		}
	}
}
