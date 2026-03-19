using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public class TransmitterPickerColorsSpec : ComponentSpec, IEquatable<TransmitterPickerColorsSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F79 File Offset: 0x00001179
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TransmitterPickerColorsSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F85 File Offset: 0x00001185
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002F8D File Offset: 0x0000118D
		[Serialize]
		public Color TransmitterColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F96 File Offset: 0x00001196
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002F9E File Offset: 0x0000119E
		[Serialize]
		public Color UnfinishedTransmitterColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002FA7 File Offset: 0x000011A7
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002FAF File Offset: 0x000011AF
		[Serialize]
		public Color HoveredTransmitterColor { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002FB8 File Offset: 0x000011B8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransmitterPickerColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003004 File Offset: 0x00001204
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TransmitterColor = ");
			builder.Append(this.TransmitterColor.ToString());
			builder.Append(", UnfinishedTransmitterColor = ");
			builder.Append(this.UnfinishedTransmitterColor.ToString());
			builder.Append(", HoveredTransmitterColor = ");
			builder.Append(this.HoveredTransmitterColor.ToString());
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000309C File Offset: 0x0000129C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransmitterPickerColorsSpec left, TransmitterPickerColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030A8 File Offset: 0x000012A8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransmitterPickerColorsSpec left, TransmitterPickerColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030BC File Offset: 0x000012BC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<TransmitterColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<UnfinishedTransmitterColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HoveredTransmitterColor>k__BackingField);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003114 File Offset: 0x00001314
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransmitterPickerColorsSpec);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003122 File Offset: 0x00001322
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000312C File Offset: 0x0000132C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransmitterPickerColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<TransmitterColor>k__BackingField, other.<TransmitterColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<UnfinishedTransmitterColor>k__BackingField, other.<UnfinishedTransmitterColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HoveredTransmitterColor>k__BackingField, other.<HoveredTransmitterColor>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003198 File Offset: 0x00001398
		[CompilerGenerated]
		protected TransmitterPickerColorsSpec(TransmitterPickerColorsSpec original) : base(original)
		{
			this.TransmitterColor = original.<TransmitterColor>k__BackingField;
			this.UnfinishedTransmitterColor = original.<UnfinishedTransmitterColor>k__BackingField;
			this.HoveredTransmitterColor = original.<HoveredTransmitterColor>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031C5 File Offset: 0x000013C5
		public TransmitterPickerColorsSpec()
		{
		}
	}
}
