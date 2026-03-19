using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class RangeEnterableHighlighterSpec : ComponentSpec, IEquatable<RangeEnterableHighlighterSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000380F File Offset: 0x00001A0F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RangeEnterableHighlighterSpec);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000381B File Offset: 0x00001A1B
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003823 File Offset: 0x00001A23
		[Serialize]
		public Color BuildingInRange { get; set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000382C File Offset: 0x00001A2C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RangeEnterableHighlighterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003878 File Offset: 0x00001A78
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BuildingInRange = ");
			builder.Append(this.BuildingInRange.ToString());
			return true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038C2 File Offset: 0x00001AC2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RangeEnterableHighlighterSpec left, RangeEnterableHighlighterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000038CE File Offset: 0x00001ACE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RangeEnterableHighlighterSpec left, RangeEnterableHighlighterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000038E2 File Offset: 0x00001AE2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BuildingInRange>k__BackingField);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003901 File Offset: 0x00001B01
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RangeEnterableHighlighterSpec);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000390F File Offset: 0x00001B0F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RangeEnterableHighlighterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<BuildingInRange>k__BackingField, other.<BuildingInRange>k__BackingField));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003940 File Offset: 0x00001B40
		[CompilerGenerated]
		protected RangeEnterableHighlighterSpec(RangeEnterableHighlighterSpec original) : base(original)
		{
			this.BuildingInRange = original.<BuildingInRange>k__BackingField;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000279E File Offset: 0x0000099E
		public RangeEnterableHighlighterSpec()
		{
		}
	}
}
