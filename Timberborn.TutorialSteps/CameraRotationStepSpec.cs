using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class CameraRotationStepSpec : ComponentSpec, IEquatable<CameraRotationStepSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000036A5 File Offset: 0x000018A5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CameraRotationStepSpec);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000036B1 File Offset: 0x000018B1
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000036B9 File Offset: 0x000018B9
		[Serialize]
		public RotationDirection Direction { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000036C2 File Offset: 0x000018C2
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000036CA File Offset: 0x000018CA
		[Serialize]
		public float Angle { get; set; }

		// Token: 0x060000A8 RID: 168 RVA: 0x000036D4 File Offset: 0x000018D4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CameraRotationStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003720 File Offset: 0x00001920
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Direction = ");
			builder.Append(this.Direction.ToString());
			builder.Append(", Angle = ");
			builder.Append(this.Angle.ToString());
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003791 File Offset: 0x00001991
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CameraRotationStepSpec left, CameraRotationStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000379D File Offset: 0x0000199D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CameraRotationStepSpec left, CameraRotationStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037B1 File Offset: 0x000019B1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<RotationDirection>.Default.GetHashCode(this.<Direction>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Angle>k__BackingField);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000037E7 File Offset: 0x000019E7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CameraRotationStepSpec);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000037F8 File Offset: 0x000019F8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CameraRotationStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<RotationDirection>.Default.Equals(this.<Direction>k__BackingField, other.<Direction>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Angle>k__BackingField, other.<Angle>k__BackingField));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000384C File Offset: 0x00001A4C
		[CompilerGenerated]
		protected CameraRotationStepSpec(CameraRotationStepSpec original) : base(original)
		{
			this.Direction = original.<Direction>k__BackingField;
			this.Angle = original.<Angle>k__BackingField;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000239D File Offset: 0x0000059D
		public CameraRotationStepSpec()
		{
		}
	}
}
