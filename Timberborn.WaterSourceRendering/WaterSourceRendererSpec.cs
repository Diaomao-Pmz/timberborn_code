using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceRendering
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSourceRendererSpec : ComponentSpec, IEquatable<WaterSourceRendererSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022BC File Offset: 0x000004BC
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceRendererSpec);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022C8 File Offset: 0x000004C8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceRendererSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002314 File Offset: 0x00000514
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000231D File Offset: 0x0000051D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceRendererSpec left, WaterSourceRendererSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002329 File Offset: 0x00000529
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceRendererSpec left, WaterSourceRendererSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000233D File Offset: 0x0000053D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002345 File Offset: 0x00000545
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceRendererSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002353 File Offset: 0x00000553
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000235C File Offset: 0x0000055C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceRendererSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002373 File Offset: 0x00000573
		[CompilerGenerated]
		protected WaterSourceRendererSpec(WaterSourceRendererSpec original) : base(original)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000237C File Offset: 0x0000057C
		public WaterSourceRendererSpec()
		{
		}
	}
}
