using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	public class GameSpeedStepSpec : ComponentSpec, IEquatable<GameSpeedStepSpec>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004B6A File Offset: 0x00002D6A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GameSpeedStepSpec);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004B76 File Offset: 0x00002D76
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004B7E File Offset: 0x00002D7E
		[Serialize]
		public int Speed { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004B87 File Offset: 0x00002D87
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004B8F File Offset: 0x00002D8F
		[Serialize]
		public bool OnlyOnce { get; set; }

		// Token: 0x0600013F RID: 319 RVA: 0x00004B98 File Offset: 0x00002D98
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameSpeedStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004BE4 File Offset: 0x00002DE4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Speed = ");
			builder.Append(this.Speed.ToString());
			builder.Append(", OnlyOnce = ");
			builder.Append(this.OnlyOnce.ToString());
			return true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004C55 File Offset: 0x00002E55
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameSpeedStepSpec left, GameSpeedStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004C61 File Offset: 0x00002E61
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameSpeedStepSpec left, GameSpeedStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004C75 File Offset: 0x00002E75
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Speed>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnlyOnce>k__BackingField);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004CAB File Offset: 0x00002EAB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameSpeedStepSpec);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004CBC File Offset: 0x00002EBC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameSpeedStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Speed>k__BackingField, other.<Speed>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnlyOnce>k__BackingField, other.<OnlyOnce>k__BackingField));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004D10 File Offset: 0x00002F10
		[CompilerGenerated]
		protected GameSpeedStepSpec(GameSpeedStepSpec original) : base(original)
		{
			this.Speed = original.<Speed>k__BackingField;
			this.OnlyOnce = original.<OnlyOnce>k__BackingField;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000239D File Offset: 0x0000059D
		public GameSpeedStepSpec()
		{
		}
	}
}
