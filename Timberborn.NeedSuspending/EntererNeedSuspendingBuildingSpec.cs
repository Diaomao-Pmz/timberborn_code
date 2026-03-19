using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedSuspending
{
	// Token: 0x02000008 RID: 8
	public class EntererNeedSuspendingBuildingSpec : ComponentSpec, IEquatable<EntererNeedSuspendingBuildingSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021AD File Offset: 0x000003AD
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(EntererNeedSuspendingBuildingSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021C1 File Offset: 0x000003C1
		[Serialize]
		public NeedSuspender NeedSuspender { get; set; }

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntererNeedSuspendingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002218 File Offset: 0x00000418
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NeedSuspender = ");
			builder.Append(this.NeedSuspender);
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002249 File Offset: 0x00000449
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EntererNeedSuspendingBuildingSpec left, EntererNeedSuspendingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002255 File Offset: 0x00000455
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EntererNeedSuspendingBuildingSpec left, EntererNeedSuspendingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002269 File Offset: 0x00000469
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<NeedSuspender>.Default.GetHashCode(this.<NeedSuspender>k__BackingField);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002288 File Offset: 0x00000488
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntererNeedSuspendingBuildingSpec);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002296 File Offset: 0x00000496
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000229F File Offset: 0x0000049F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EntererNeedSuspendingBuildingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<NeedSuspender>.Default.Equals(this.<NeedSuspender>k__BackingField, other.<NeedSuspender>k__BackingField));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D0 File Offset: 0x000004D0
		[CompilerGenerated]
		protected EntererNeedSuspendingBuildingSpec([Nullable(1)] EntererNeedSuspendingBuildingSpec original) : base(original)
		{
			this.NeedSuspender = original.<NeedSuspender>k__BackingField;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022E5 File Offset: 0x000004E5
		public EntererNeedSuspendingBuildingSpec()
		{
		}
	}
}
