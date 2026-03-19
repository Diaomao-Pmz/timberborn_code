using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000009 RID: 9
	public class CriticalNeedSpec : ComponentSpec, IEquatable<CriticalNeedSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024F9 File Offset: 0x000006F9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CriticalNeedSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002505 File Offset: 0x00000705
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000250D File Offset: 0x0000070D
		[Serialize]
		public CriticalNeedType CriticalNeedType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002516 File Offset: 0x00000716
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000251E File Offset: 0x0000071E
		[Serialize]
		public string SpriteName { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002527 File Offset: 0x00000727
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000252F File Offset: 0x0000072F
		[Serialize("DescriptionLocKey")]
		public LocalizedText Description { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002538 File Offset: 0x00000738
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002540 File Offset: 0x00000740
		[Serialize("DescriptionShortLocKey")]
		public LocalizedText DescriptionShort { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002549 File Offset: 0x00000749
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002551 File Offset: 0x00000751
		[Serialize]
		private string DescriptionLocKey { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000255A File Offset: 0x0000075A
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002562 File Offset: 0x00000762
		[Serialize]
		private string DescriptionShortLocKey { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000256C File Offset: 0x0000076C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CriticalNeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025B8 File Offset: 0x000007B8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CriticalNeedType = ");
			builder.Append(this.CriticalNeedType.ToString());
			builder.Append(", SpriteName = ");
			builder.Append(this.SpriteName);
			builder.Append(", Description = ");
			builder.Append(this.Description);
			builder.Append(", DescriptionShort = ");
			builder.Append(this.DescriptionShort);
			return true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000264D File Offset: 0x0000084D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CriticalNeedSpec left, CriticalNeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002659 File Offset: 0x00000859
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CriticalNeedSpec left, CriticalNeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002670 File Offset: 0x00000870
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<CriticalNeedType>.Default.GetHashCode(this.<CriticalNeedType>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SpriteName>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DescriptionShort>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionShortLocKey>k__BackingField);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000270D File Offset: 0x0000090D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CriticalNeedSpec);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000271B File Offset: 0x0000091B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002724 File Offset: 0x00000924
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CriticalNeedSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<CriticalNeedType>.Default.Equals(this.<CriticalNeedType>k__BackingField, other.<CriticalNeedType>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SpriteName>k__BackingField, other.<SpriteName>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DescriptionShort>k__BackingField, other.<DescriptionShort>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionLocKey>k__BackingField, other.<DescriptionLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionShortLocKey>k__BackingField, other.<DescriptionShortLocKey>k__BackingField));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027E0 File Offset: 0x000009E0
		[CompilerGenerated]
		protected CriticalNeedSpec([Nullable(1)] CriticalNeedSpec original) : base(original)
		{
			this.CriticalNeedType = original.<CriticalNeedType>k__BackingField;
			this.SpriteName = original.<SpriteName>k__BackingField;
			this.Description = original.<Description>k__BackingField;
			this.DescriptionShort = original.<DescriptionShort>k__BackingField;
			this.DescriptionLocKey = original.<DescriptionLocKey>k__BackingField;
			this.DescriptionShortLocKey = original.<DescriptionShortLocKey>k__BackingField;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000283C File Offset: 0x00000A3C
		public CriticalNeedSpec()
		{
		}
	}
}
