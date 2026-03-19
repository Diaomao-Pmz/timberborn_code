using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalNodeSpec : ComponentSpec, IEquatable<MechanicalNodeSpec>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003C19 File Offset: 0x00001E19
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeSpec);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003C25 File Offset: 0x00001E25
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003C2D File Offset: 0x00001E2D
		[Serialize]
		public int PowerOutput { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003C36 File Offset: 0x00001E36
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003C3E File Offset: 0x00001E3E
		[Serialize]
		public int PowerInput { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003C47 File Offset: 0x00001E47
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003C4F File Offset: 0x00001E4F
		[Serialize]
		public bool IsShaft { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00003C58 File Offset: 0x00001E58
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003CA4 File Offset: 0x00001EA4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PowerOutput = ");
			builder.Append(this.PowerOutput.ToString());
			builder.Append(", PowerInput = ");
			builder.Append(this.PowerInput.ToString());
			builder.Append(", IsShaft = ");
			builder.Append(this.IsShaft.ToString());
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003D3C File Offset: 0x00001F3C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeSpec left, MechanicalNodeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003D48 File Offset: 0x00001F48
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeSpec left, MechanicalNodeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D5C File Offset: 0x00001F5C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PowerOutput>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PowerInput>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsShaft>k__BackingField);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeSpec);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003DC4 File Offset: 0x00001FC4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<PowerOutput>k__BackingField, other.<PowerOutput>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PowerInput>k__BackingField, other.<PowerInput>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsShaft>k__BackingField, other.<IsShaft>k__BackingField));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003E30 File Offset: 0x00002030
		[CompilerGenerated]
		protected MechanicalNodeSpec(MechanicalNodeSpec original) : base(original)
		{
			this.PowerOutput = original.<PowerOutput>k__BackingField;
			this.PowerInput = original.<PowerInput>k__BackingField;
			this.IsShaft = original.<IsShaft>k__BackingField;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000257C File Offset: 0x0000077C
		public MechanicalNodeSpec()
		{
		}
	}
}
