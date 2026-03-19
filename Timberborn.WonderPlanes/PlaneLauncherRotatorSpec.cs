using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.UnityEngineSpecs;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000E RID: 14
	public class PlaneLauncherRotatorSpec : ComponentSpec, IEquatable<PlaneLauncherRotatorSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000321C File Offset: 0x0000141C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlaneLauncherRotatorSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003228 File Offset: 0x00001428
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003230 File Offset: 0x00001430
		[Serialize]
		public string RotatedElementName { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003239 File Offset: 0x00001439
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003241 File Offset: 0x00001441
		[Serialize]
		public float FullRotationDuration { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000324A File Offset: 0x0000144A
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003252 File Offset: 0x00001452
		[Serialize]
		public AnimationCurveSpec RotationCurve { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x0000325C File Offset: 0x0000145C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaneLauncherRotatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032A8 File Offset: 0x000014A8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RotatedElementName = ");
			builder.Append(this.RotatedElementName);
			builder.Append(", FullRotationDuration = ");
			builder.Append(this.FullRotationDuration.ToString());
			builder.Append(", RotationCurve = ");
			builder.Append(this.RotationCurve);
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003324 File Offset: 0x00001524
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaneLauncherRotatorSpec left, PlaneLauncherRotatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003330 File Offset: 0x00001530
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaneLauncherRotatorSpec left, PlaneLauncherRotatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003344 File Offset: 0x00001544
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RotatedElementName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FullRotationDuration>k__BackingField)) * -1521134295 + EqualityComparer<AnimationCurveSpec>.Default.GetHashCode(this.<RotationCurve>k__BackingField);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000339C File Offset: 0x0000159C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaneLauncherRotatorSpec);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033AC File Offset: 0x000015AC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaneLauncherRotatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<RotatedElementName>k__BackingField, other.<RotatedElementName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FullRotationDuration>k__BackingField, other.<FullRotationDuration>k__BackingField) && EqualityComparer<AnimationCurveSpec>.Default.Equals(this.<RotationCurve>k__BackingField, other.<RotationCurve>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003418 File Offset: 0x00001618
		[CompilerGenerated]
		protected PlaneLauncherRotatorSpec([Nullable(1)] PlaneLauncherRotatorSpec original) : base(original)
		{
			this.RotatedElementName = original.<RotatedElementName>k__BackingField;
			this.FullRotationDuration = original.<FullRotationDuration>k__BackingField;
			this.RotationCurve = original.<RotationCurve>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public PlaneLauncherRotatorSpec()
		{
		}
	}
}
