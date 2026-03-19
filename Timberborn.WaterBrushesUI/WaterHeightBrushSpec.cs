using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterBrushesUI
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterHeightBrushSpec : ComponentSpec, IEquatable<WaterHeightBrushSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002273 File Offset: 0x00000473
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterHeightBrushSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000227F File Offset: 0x0000047F
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002287 File Offset: 0x00000487
		[Serialize]
		public Color AddingTileColor { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002298 File Offset: 0x00000498
		[Serialize]
		public Color RemovingTileColor { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022A1 File Offset: 0x000004A1
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000022A9 File Offset: 0x000004A9
		[Serialize]
		public Color ContaminatedTileColor { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000022B4 File Offset: 0x000004B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterHeightBrushSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002300 File Offset: 0x00000500
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AddingTileColor = ");
			builder.Append(this.AddingTileColor.ToString());
			builder.Append(", RemovingTileColor = ");
			builder.Append(this.RemovingTileColor.ToString());
			builder.Append(", ContaminatedTileColor = ");
			builder.Append(this.ContaminatedTileColor.ToString());
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002398 File Offset: 0x00000598
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterHeightBrushSpec left, WaterHeightBrushSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023A4 File Offset: 0x000005A4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterHeightBrushSpec left, WaterHeightBrushSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023B8 File Offset: 0x000005B8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AddingTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RemovingTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ContaminatedTileColor>k__BackingField);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002410 File Offset: 0x00000610
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterHeightBrushSpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000241E File Offset: 0x0000061E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002428 File Offset: 0x00000628
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterHeightBrushSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<AddingTileColor>k__BackingField, other.<AddingTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<RemovingTileColor>k__BackingField, other.<RemovingTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ContaminatedTileColor>k__BackingField, other.<ContaminatedTileColor>k__BackingField));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002494 File Offset: 0x00000694
		[CompilerGenerated]
		protected WaterHeightBrushSpec(WaterHeightBrushSpec original) : base(original)
		{
			this.AddingTileColor = original.<AddingTileColor>k__BackingField;
			this.RemovingTileColor = original.<RemovingTileColor>k__BackingField;
			this.ContaminatedTileColor = original.<ContaminatedTileColor>k__BackingField;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024C1 File Offset: 0x000006C1
		public WaterHeightBrushSpec()
		{
		}
	}
}
