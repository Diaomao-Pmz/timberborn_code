using System;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Timberborn.BlueprintSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	[UsedImplicitly]
	public class PlantingToolGroupSpec : ComponentSpec, IEquatable<PlantingToolGroupSpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003E6B File Offset: 0x0000206B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PlantingToolGroupSpec);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003E78 File Offset: 0x00002078
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantingToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003EC4 File Offset: 0x000020C4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003ECD File Offset: 0x000020CD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantingToolGroupSpec left, PlantingToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003ED9 File Offset: 0x000020D9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantingToolGroupSpec left, PlantingToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EED File Offset: 0x000020ED
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003EF5 File Offset: 0x000020F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantingToolGroupSpec);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000027CE File Offset: 0x000009CE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F03 File Offset: 0x00002103
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantingToolGroupSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F1A File Offset: 0x0000211A
		[CompilerGenerated]
		protected PlantingToolGroupSpec(PlantingToolGroupSpec original) : base(original)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000281D File Offset: 0x00000A1D
		public PlantingToolGroupSpec()
		{
		}
	}
}
