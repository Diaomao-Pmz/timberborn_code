using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200001F RID: 31
	public class YieldRemovingBuildingSpec : ComponentSpec, IEquatable<YieldRemovingBuildingSpec>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003E95 File Offset: 0x00002095
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(YieldRemovingBuildingSpec);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003EA1 File Offset: 0x000020A1
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003EA9 File Offset: 0x000020A9
		[Serialize]
		public string ResourceGroup { get; set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x00003EB4 File Offset: 0x000020B4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YieldRemovingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003F00 File Offset: 0x00002100
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ResourceGroup = ");
			builder.Append(this.ResourceGroup);
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F31 File Offset: 0x00002131
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(YieldRemovingBuildingSpec left, YieldRemovingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003F3D File Offset: 0x0000213D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(YieldRemovingBuildingSpec left, YieldRemovingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003F51 File Offset: 0x00002151
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResourceGroup>k__BackingField);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003F70 File Offset: 0x00002170
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as YieldRemovingBuildingSpec);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002AFA File Offset: 0x00000CFA
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F7E File Offset: 0x0000217E
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(YieldRemovingBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ResourceGroup>k__BackingField, other.<ResourceGroup>k__BackingField));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003FAF File Offset: 0x000021AF
		[CompilerGenerated]
		protected YieldRemovingBuildingSpec([Nullable(1)] YieldRemovingBuildingSpec original) : base(original)
		{
			this.ResourceGroup = original.<ResourceGroup>k__BackingField;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002B9D File Offset: 0x00000D9D
		public YieldRemovingBuildingSpec()
		{
		}
	}
}
