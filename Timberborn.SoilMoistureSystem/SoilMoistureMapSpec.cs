using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class SoilMoistureMapSpec : ComponentSpec, IEquatable<SoilMoistureMapSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F3B File Offset: 0x0000113B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SoilMoistureMapSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F47 File Offset: 0x00001147
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002F4F File Offset: 0x0000114F
		[Serialize]
		public float MaxDesertIntensity { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F58 File Offset: 0x00001158
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002F60 File Offset: 0x00001160
		[Serialize]
		public int DesertMoistureThreshold { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x00002F6C File Offset: 0x0000116C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SoilMoistureMapSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FB8 File Offset: 0x000011B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxDesertIntensity = ");
			builder.Append(this.MaxDesertIntensity.ToString());
			builder.Append(", DesertMoistureThreshold = ");
			builder.Append(this.DesertMoistureThreshold.ToString());
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003029 File Offset: 0x00001229
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SoilMoistureMapSpec left, SoilMoistureMapSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003035 File Offset: 0x00001235
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SoilMoistureMapSpec left, SoilMoistureMapSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003049 File Offset: 0x00001249
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxDesertIntensity>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DesertMoistureThreshold>k__BackingField);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000307F File Offset: 0x0000127F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SoilMoistureMapSpec);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000026C9 File Offset: 0x000008C9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003090 File Offset: 0x00001290
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SoilMoistureMapSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxDesertIntensity>k__BackingField, other.<MaxDesertIntensity>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DesertMoistureThreshold>k__BackingField, other.<DesertMoistureThreshold>k__BackingField));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000030E4 File Offset: 0x000012E4
		[CompilerGenerated]
		protected SoilMoistureMapSpec(SoilMoistureMapSpec original) : base(original)
		{
			this.MaxDesertIntensity = original.<MaxDesertIntensity>k__BackingField;
			this.DesertMoistureThreshold = original.<DesertMoistureThreshold>k__BackingField;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002749 File Offset: 0x00000949
		public SoilMoistureMapSpec()
		{
		}
	}
}
