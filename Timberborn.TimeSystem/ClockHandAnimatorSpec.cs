using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000008 RID: 8
	public class ClockHandAnimatorSpec : ComponentSpec, IEquatable<ClockHandAnimatorSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021DB File Offset: 0x000003DB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ClockHandAnimatorSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021EF File Offset: 0x000003EF
		[Serialize]
		public float AngleOffset { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002200 File Offset: 0x00000400
		[Serialize]
		public string HandName { get; set; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000220C File Offset: 0x0000040C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ClockHandAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002258 File Offset: 0x00000458
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AngleOffset = ");
			builder.Append(this.AngleOffset.ToString());
			builder.Append(", HandName = ");
			builder.Append(this.HandName);
			return true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022BB File Offset: 0x000004BB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ClockHandAnimatorSpec left, ClockHandAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C7 File Offset: 0x000004C7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ClockHandAnimatorSpec left, ClockHandAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022DB File Offset: 0x000004DB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<AngleOffset>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HandName>k__BackingField);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002311 File Offset: 0x00000511
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ClockHandAnimatorSpec);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000231F File Offset: 0x0000051F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002328 File Offset: 0x00000528
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ClockHandAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<AngleOffset>k__BackingField, other.<AngleOffset>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HandName>k__BackingField, other.<HandName>k__BackingField));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000237C File Offset: 0x0000057C
		[CompilerGenerated]
		protected ClockHandAnimatorSpec([Nullable(1)] ClockHandAnimatorSpec original) : base(original)
		{
			this.AngleOffset = original.<AngleOffset>k__BackingField;
			this.HandName = original.<HandName>k__BackingField;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000239D File Offset: 0x0000059D
		public ClockHandAnimatorSpec()
		{
		}
	}
}
