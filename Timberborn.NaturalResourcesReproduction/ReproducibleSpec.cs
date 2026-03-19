using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class ReproducibleSpec : ComponentSpec, IEquatable<ReproducibleSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002905 File Offset: 0x00000B05
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ReproducibleSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002911 File Offset: 0x00000B11
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002919 File Offset: 0x00000B19
		[Serialize]
		public float ReproductionChance { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002924 File Offset: 0x00000B24
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ReproducibleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002970 File Offset: 0x00000B70
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ReproductionChance = ");
			builder.Append(this.ReproductionChance.ToString());
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029BA File Offset: 0x00000BBA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ReproducibleSpec left, ReproducibleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029C6 File Offset: 0x00000BC6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ReproducibleSpec left, ReproducibleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029DA File Offset: 0x00000BDA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ReproductionChance>k__BackingField);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029F9 File Offset: 0x00000BF9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ReproducibleSpec);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A07 File Offset: 0x00000C07
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A10 File Offset: 0x00000C10
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ReproducibleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<ReproductionChance>k__BackingField, other.<ReproductionChance>k__BackingField));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A41 File Offset: 0x00000C41
		[CompilerGenerated]
		protected ReproducibleSpec(ReproducibleSpec original) : base(original)
		{
			this.ReproductionChance = original.<ReproductionChance>k__BackingField;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A56 File Offset: 0x00000C56
		public ReproducibleSpec()
		{
		}
	}
}
