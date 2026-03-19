using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class CameraZoomStepSpec : ComponentSpec, IEquatable<CameraZoomStepSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003A63 File Offset: 0x00001C63
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CameraZoomStepSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003A6F File Offset: 0x00001C6F
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003A77 File Offset: 0x00001C77
		[Serialize]
		public ZoomDirection Direction { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003A80 File Offset: 0x00001C80
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003A88 File Offset: 0x00001C88
		[Serialize]
		public float Threshold { get; set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003A94 File Offset: 0x00001C94
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CameraZoomStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003AE0 File Offset: 0x00001CE0
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

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B51 File Offset: 0x00001D51
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CameraZoomStepSpec left, CameraZoomStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003B5D File Offset: 0x00001D5D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CameraZoomStepSpec left, CameraZoomStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003B71 File Offset: 0x00001D71
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ZoomDirection>.Default.GetHashCode(this.<Direction>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Threshold>k__BackingField);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003BA7 File Offset: 0x00001DA7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CameraZoomStepSpec);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BB8 File Offset: 0x00001DB8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CameraZoomStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ZoomDirection>.Default.Equals(this.<Direction>k__BackingField, other.<Direction>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Threshold>k__BackingField, other.<Threshold>k__BackingField));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C0C File Offset: 0x00001E0C
		[CompilerGenerated]
		protected CameraZoomStepSpec(CameraZoomStepSpec original) : base(original)
		{
			this.Direction = original.<Direction>k__BackingField;
			this.Threshold = original.<Threshold>k__BackingField;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000239D File Offset: 0x0000059D
		public CameraZoomStepSpec()
		{
		}
	}
}
