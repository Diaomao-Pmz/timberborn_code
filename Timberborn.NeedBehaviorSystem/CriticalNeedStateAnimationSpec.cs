using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000010 RID: 16
	public class CriticalNeedStateAnimationSpec : ComponentSpec, IEquatable<CriticalNeedStateAnimationSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028E0 File Offset: 0x00000AE0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CriticalNeedStateAnimationSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000028EC File Offset: 0x00000AEC
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000028F4 File Offset: 0x00000AF4
		[Serialize]
		public string NeedId { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028FD File Offset: 0x00000AFD
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002905 File Offset: 0x00000B05
		[Serialize]
		public string Animation { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002910 File Offset: 0x00000B10
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CriticalNeedStateAnimationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000295C File Offset: 0x00000B5C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NeedId = ");
			builder.Append(this.NeedId);
			builder.Append(", Animation = ");
			builder.Append(this.Animation);
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029B1 File Offset: 0x00000BB1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CriticalNeedStateAnimationSpec left, CriticalNeedStateAnimationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029BD File Offset: 0x00000BBD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CriticalNeedStateAnimationSpec left, CriticalNeedStateAnimationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029D1 File Offset: 0x00000BD1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Animation>k__BackingField);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A07 File Offset: 0x00000C07
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CriticalNeedStateAnimationSpec);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A15 File Offset: 0x00000C15
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A20 File Offset: 0x00000C20
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CriticalNeedStateAnimationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<NeedId>k__BackingField, other.<NeedId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Animation>k__BackingField, other.<Animation>k__BackingField));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A74 File Offset: 0x00000C74
		[CompilerGenerated]
		protected CriticalNeedStateAnimationSpec([Nullable(1)] CriticalNeedStateAnimationSpec original) : base(original)
		{
			this.NeedId = original.<NeedId>k__BackingField;
			this.Animation = original.<Animation>k__BackingField;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A95 File Offset: 0x00000C95
		public CriticalNeedStateAnimationSpec()
		{
		}
	}
}
