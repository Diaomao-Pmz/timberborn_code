using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class CameraMovementStepSpec : ComponentSpec, IEquatable<CameraMovementStepSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000330C File Offset: 0x0000150C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CameraMovementStepSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003318 File Offset: 0x00001518
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003320 File Offset: 0x00001520
		[Serialize]
		public Direction2D Direction { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003329 File Offset: 0x00001529
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003331 File Offset: 0x00001531
		[Serialize]
		public float Threshold { get; set; }

		// Token: 0x0600008D RID: 141 RVA: 0x0000333C File Offset: 0x0000153C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CameraMovementStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003388 File Offset: 0x00001588
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Direction = ");
			builder.Append(this.Direction.ToString());
			builder.Append(", Threshold = ");
			builder.Append(this.Threshold.ToString());
			return true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000033F9 File Offset: 0x000015F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CameraMovementStepSpec left, CameraMovementStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003405 File Offset: 0x00001605
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CameraMovementStepSpec left, CameraMovementStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003419 File Offset: 0x00001619
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Direction2D>.Default.GetHashCode(this.<Direction>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Threshold>k__BackingField);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000344F File Offset: 0x0000164F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CameraMovementStepSpec);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CameraMovementStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Direction2D>.Default.Equals(this.<Direction>k__BackingField, other.<Direction>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Threshold>k__BackingField, other.<Threshold>k__BackingField));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034B4 File Offset: 0x000016B4
		[CompilerGenerated]
		protected CameraMovementStepSpec(CameraMovementStepSpec original) : base(original)
		{
			this.Direction = original.<Direction>k__BackingField;
			this.Threshold = original.<Threshold>k__BackingField;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000239D File Offset: 0x0000059D
		public CameraMovementStepSpec()
		{
		}
	}
}
