using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class BrushColorSpec : ComponentSpec, IEquatable<BrushColorSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AA7 File Offset: 0x00000CA7
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BrushColorSpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002AB3 File Offset: 0x00000CB3
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002ABB File Offset: 0x00000CBB
		[Serialize]
		public Color Neutral { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AC4 File Offset: 0x00000CC4
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002ACC File Offset: 0x00000CCC
		[Serialize]
		public Color Positive { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AD5 File Offset: 0x00000CD5
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002ADD File Offset: 0x00000CDD
		[Serialize]
		public Color Negative { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AE6 File Offset: 0x00000CE6
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002AEE File Offset: 0x00000CEE
		[Serialize]
		public Color Objects { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BrushColorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B44 File Offset: 0x00000D44
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Neutral = ");
			builder.Append(this.Neutral.ToString());
			builder.Append(", Positive = ");
			builder.Append(this.Positive.ToString());
			builder.Append(", Negative = ");
			builder.Append(this.Negative.ToString());
			builder.Append(", Objects = ");
			builder.Append(this.Objects.ToString());
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C03 File Offset: 0x00000E03
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BrushColorSpec left, BrushColorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C0F File Offset: 0x00000E0F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BrushColorSpec left, BrushColorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C24 File Offset: 0x00000E24
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Neutral>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Positive>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Negative>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Objects>k__BackingField);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C93 File Offset: 0x00000E93
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BrushColorSpec);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CA1 File Offset: 0x00000EA1
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CAC File Offset: 0x00000EAC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BrushColorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<Neutral>k__BackingField, other.<Neutral>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Positive>k__BackingField, other.<Positive>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Negative>k__BackingField, other.<Negative>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Objects>k__BackingField, other.<Objects>k__BackingField));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D30 File Offset: 0x00000F30
		[CompilerGenerated]
		protected BrushColorSpec(BrushColorSpec original) : base(original)
		{
			this.Neutral = original.<Neutral>k__BackingField;
			this.Positive = original.<Positive>k__BackingField;
			this.Negative = original.<Negative>k__BackingField;
			this.Objects = original.<Objects>k__BackingField;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D69 File Offset: 0x00000F69
		public BrushColorSpec()
		{
		}
	}
}
