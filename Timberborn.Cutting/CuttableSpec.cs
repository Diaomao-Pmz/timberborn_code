using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Yielding;

namespace Timberborn.Cutting
{
	// Token: 0x0200000A RID: 10
	public class CuttableSpec : ComponentSpec, IYielderDecorable, IEquatable<CuttableSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023DB File Offset: 0x000005DB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CuttableSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023E7 File Offset: 0x000005E7
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000023EF File Offset: 0x000005EF
		[Serialize]
		public bool RemoveOnCut { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002400 File Offset: 0x00000600
		[Serialize]
		public string LeftoverModelName { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002409 File Offset: 0x00000609
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002411 File Offset: 0x00000611
		[Serialize]
		public YielderSpec Yielder { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x0000241C File Offset: 0x0000061C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CuttableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002468 File Offset: 0x00000668
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RemoveOnCut = ");
			builder.Append(this.RemoveOnCut.ToString());
			builder.Append(", LeftoverModelName = ");
			builder.Append(this.LeftoverModelName);
			builder.Append(", Yielder = ");
			builder.Append(this.Yielder);
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024E4 File Offset: 0x000006E4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CuttableSpec left, CuttableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024F0 File Offset: 0x000006F0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CuttableSpec left, CuttableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002504 File Offset: 0x00000704
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RemoveOnCut>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeftoverModelName>k__BackingField)) * -1521134295 + EqualityComparer<YielderSpec>.Default.GetHashCode(this.<Yielder>k__BackingField);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000255C File Offset: 0x0000075C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CuttableSpec);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000256A File Offset: 0x0000076A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002574 File Offset: 0x00000774
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CuttableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<RemoveOnCut>k__BackingField, other.<RemoveOnCut>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LeftoverModelName>k__BackingField, other.<LeftoverModelName>k__BackingField) && EqualityComparer<YielderSpec>.Default.Equals(this.<Yielder>k__BackingField, other.<Yielder>k__BackingField));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025E0 File Offset: 0x000007E0
		[CompilerGenerated]
		protected CuttableSpec([Nullable(1)] CuttableSpec original) : base(original)
		{
			this.RemoveOnCut = original.<RemoveOnCut>k__BackingField;
			this.LeftoverModelName = original.<LeftoverModelName>k__BackingField;
			this.Yielder = original.<Yielder>k__BackingField;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000260D File Offset: 0x0000080D
		public CuttableSpec()
		{
		}
	}
}
