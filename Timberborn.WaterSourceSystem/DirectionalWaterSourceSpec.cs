using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class DirectionalWaterSourceSpec : ComponentSpec, IEquatable<DirectionalWaterSourceSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023F0 File Offset: 0x000005F0
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DirectionalWaterSourceSpec);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023FC File Offset: 0x000005FC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DirectionalWaterSourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022B8 File Offset: 0x000004B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002448 File Offset: 0x00000648
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DirectionalWaterSourceSpec left, DirectionalWaterSourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002454 File Offset: 0x00000654
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DirectionalWaterSourceSpec left, DirectionalWaterSourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022E1 File Offset: 0x000004E1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002468 File Offset: 0x00000668
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DirectionalWaterSourceSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002300 File Offset: 0x00000500
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DirectionalWaterSourceSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002317 File Offset: 0x00000517
		[CompilerGenerated]
		protected DirectionalWaterSourceSpec(DirectionalWaterSourceSpec original) : base(original)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002320 File Offset: 0x00000520
		public DirectionalWaterSourceSpec()
		{
		}
	}
}
