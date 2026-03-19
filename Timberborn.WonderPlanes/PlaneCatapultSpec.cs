using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.UnityEngineSpecs;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000B RID: 11
	public class PlaneCatapultSpec : ComponentSpec, IEquatable<PlaneCatapultSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029BC File Offset: 0x00000BBC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlaneCatapultSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000029C8 File Offset: 0x00000BC8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000029D0 File Offset: 0x00000BD0
		[Serialize]
		public AnimationCurveSpec SpeedCurve { get; set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000029DC File Offset: 0x00000BDC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaneCatapultSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A28 File Offset: 0x00000C28
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SpeedCurve = ");
			builder.Append(this.SpeedCurve);
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A59 File Offset: 0x00000C59
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaneCatapultSpec left, PlaneCatapultSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A65 File Offset: 0x00000C65
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaneCatapultSpec left, PlaneCatapultSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A79 File Offset: 0x00000C79
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<AnimationCurveSpec>.Default.GetHashCode(this.<SpeedCurve>k__BackingField);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A98 File Offset: 0x00000C98
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaneCatapultSpec);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AAF File Offset: 0x00000CAF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaneCatapultSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AnimationCurveSpec>.Default.Equals(this.<SpeedCurve>k__BackingField, other.<SpeedCurve>k__BackingField));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AE0 File Offset: 0x00000CE0
		[CompilerGenerated]
		protected PlaneCatapultSpec([Nullable(1)] PlaneCatapultSpec original) : base(original)
		{
			this.SpeedCurve = original.<SpeedCurve>k__BackingField;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public PlaneCatapultSpec()
		{
		}
	}
}
