using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class PriorityColorsSpec : ComponentSpec, IEquatable<PriorityColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000220E File Offset: 0x0000040E
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PriorityColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000221A File Offset: 0x0000041A
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002222 File Offset: 0x00000422
		[Serialize]
		public Color HighlightVeryLow { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000222B File Offset: 0x0000042B
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002233 File Offset: 0x00000433
		[Serialize]
		public Color HighlightLow { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002244 File Offset: 0x00000444
		[Serialize]
		public Color HighlightNormal { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002255 File Offset: 0x00000455
		[Serialize]
		public Color HighlightHigh { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002266 File Offset: 0x00000466
		[Serialize]
		public Color HighlightVeryHigh { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002277 File Offset: 0x00000477
		[Serialize]
		public Color ButtonVeryLow { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002288 File Offset: 0x00000488
		[Serialize]
		public Color ButtonLow { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002299 File Offset: 0x00000499
		[Serialize]
		public Color ButtonNormal { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022A2 File Offset: 0x000004A2
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000022AA File Offset: 0x000004AA
		[Serialize]
		public Color ButtonHigh { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022B3 File Offset: 0x000004B3
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000022BB File Offset: 0x000004BB
		[Serialize]
		public Color ButtonVeryHigh { get; set; }

		// Token: 0x06000021 RID: 33 RVA: 0x000022C4 File Offset: 0x000004C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PriorityColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002310 File Offset: 0x00000510
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("HighlightVeryLow = ");
			builder.Append(this.HighlightVeryLow.ToString());
			builder.Append(", HighlightLow = ");
			builder.Append(this.HighlightLow.ToString());
			builder.Append(", HighlightNormal = ");
			builder.Append(this.HighlightNormal.ToString());
			builder.Append(", HighlightHigh = ");
			builder.Append(this.HighlightHigh.ToString());
			builder.Append(", HighlightVeryHigh = ");
			builder.Append(this.HighlightVeryHigh.ToString());
			builder.Append(", ButtonVeryLow = ");
			builder.Append(this.ButtonVeryLow.ToString());
			builder.Append(", ButtonLow = ");
			builder.Append(this.ButtonLow.ToString());
			builder.Append(", ButtonNormal = ");
			builder.Append(this.ButtonNormal.ToString());
			builder.Append(", ButtonHigh = ");
			builder.Append(this.ButtonHigh.ToString());
			builder.Append(", ButtonVeryHigh = ");
			builder.Append(this.ButtonVeryHigh.ToString());
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B9 File Offset: 0x000006B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PriorityColorsSpec left, PriorityColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C5 File Offset: 0x000006C5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PriorityColorsSpec left, PriorityColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024DC File Offset: 0x000006DC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightVeryLow>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightLow>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightNormal>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightHigh>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HighlightVeryHigh>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ButtonVeryLow>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ButtonLow>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ButtonNormal>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ButtonHigh>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ButtonVeryHigh>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D5 File Offset: 0x000007D5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PriorityColorsSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025E3 File Offset: 0x000007E3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EC File Offset: 0x000007EC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PriorityColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<HighlightVeryLow>k__BackingField, other.<HighlightVeryLow>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HighlightLow>k__BackingField, other.<HighlightLow>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HighlightNormal>k__BackingField, other.<HighlightNormal>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HighlightHigh>k__BackingField, other.<HighlightHigh>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HighlightVeryHigh>k__BackingField, other.<HighlightVeryHigh>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ButtonVeryLow>k__BackingField, other.<ButtonVeryLow>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ButtonLow>k__BackingField, other.<ButtonLow>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ButtonNormal>k__BackingField, other.<ButtonNormal>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ButtonHigh>k__BackingField, other.<ButtonHigh>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ButtonVeryHigh>k__BackingField, other.<ButtonVeryHigh>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002714 File Offset: 0x00000914
		[CompilerGenerated]
		protected PriorityColorsSpec(PriorityColorsSpec original) : base(original)
		{
			this.HighlightVeryLow = original.<HighlightVeryLow>k__BackingField;
			this.HighlightLow = original.<HighlightLow>k__BackingField;
			this.HighlightNormal = original.<HighlightNormal>k__BackingField;
			this.HighlightHigh = original.<HighlightHigh>k__BackingField;
			this.HighlightVeryHigh = original.<HighlightVeryHigh>k__BackingField;
			this.ButtonVeryLow = original.<ButtonVeryLow>k__BackingField;
			this.ButtonLow = original.<ButtonLow>k__BackingField;
			this.ButtonNormal = original.<ButtonNormal>k__BackingField;
			this.ButtonHigh = original.<ButtonHigh>k__BackingField;
			this.ButtonVeryHigh = original.<ButtonVeryHigh>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027A0 File Offset: 0x000009A0
		public PriorityColorsSpec()
		{
		}
	}
}
