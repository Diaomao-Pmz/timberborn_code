using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class MovementAnimatorSpec : ComponentSpec, IEquatable<MovementAnimatorSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E3C File Offset: 0x0000103C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MovementAnimatorSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002E50 File Offset: 0x00001050
		[Serialize]
		public float AnimationSpeedScale { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002E5C File Offset: 0x0000105C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MovementAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EA8 File Offset: 0x000010A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AnimationSpeedScale = ");
			builder.Append(this.AnimationSpeedScale.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EF2 File Offset: 0x000010F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MovementAnimatorSpec left, MovementAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EFE File Offset: 0x000010FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MovementAnimatorSpec left, MovementAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F12 File Offset: 0x00001112
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<AnimationSpeedScale>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F31 File Offset: 0x00001131
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MovementAnimatorSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F3F File Offset: 0x0000113F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F48 File Offset: 0x00001148
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MovementAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<AnimationSpeedScale>k__BackingField, other.<AnimationSpeedScale>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F79 File Offset: 0x00001179
		[CompilerGenerated]
		protected MovementAnimatorSpec(MovementAnimatorSpec original) : base(original)
		{
			this.AnimationSpeedScale = original.<AnimationSpeedScale>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F8E File Offset: 0x0000118E
		public MovementAnimatorSpec()
		{
		}
	}
}
