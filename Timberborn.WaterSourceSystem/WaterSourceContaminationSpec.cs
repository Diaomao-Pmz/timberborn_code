using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterSourceContaminationSpec : ComponentSpec, IEquatable<WaterSourceContaminationSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000350C File Offset: 0x0000170C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceContaminationSpec);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003518 File Offset: 0x00001718
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003520 File Offset: 0x00001720
		[Serialize]
		public float DefaultContamination { get; set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x0000352C File Offset: 0x0000172C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceContaminationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003578 File Offset: 0x00001778
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DefaultContamination = ");
			builder.Append(this.DefaultContamination.ToString());
			return true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000035C2 File Offset: 0x000017C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceContaminationSpec left, WaterSourceContaminationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000035CE File Offset: 0x000017CE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceContaminationSpec left, WaterSourceContaminationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000035E2 File Offset: 0x000017E2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultContamination>k__BackingField);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003601 File Offset: 0x00001801
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceContaminationSpec);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000360F File Offset: 0x0000180F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceContaminationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<DefaultContamination>k__BackingField, other.<DefaultContamination>k__BackingField));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003640 File Offset: 0x00001840
		[CompilerGenerated]
		protected WaterSourceContaminationSpec(WaterSourceContaminationSpec original) : base(original)
		{
			this.DefaultContamination = original.<DefaultContamination>k__BackingField;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceContaminationSpec()
		{
		}
	}
}
