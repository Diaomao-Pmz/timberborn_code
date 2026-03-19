using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class ZiplineSystemColorsSpec : ComponentSpec, IEquatable<ZiplineSystemColorsSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002FE4 File Offset: 0x000011E4
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineSystemColorsSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002FF8 File Offset: 0x000011F8
		[Serialize]
		public Color OriginColor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003001 File Offset: 0x00001201
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003009 File Offset: 0x00001209
		[Serialize]
		public Color ConnectableColor { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003012 File Offset: 0x00001212
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000301A File Offset: 0x0000121A
		[Serialize]
		public Color NotConnectableColor { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00003024 File Offset: 0x00001224
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineSystemColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003070 File Offset: 0x00001270
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OriginColor = ");
			builder.Append(this.OriginColor.ToString());
			builder.Append(", ConnectableColor = ");
			builder.Append(this.ConnectableColor.ToString());
			builder.Append(", NotConnectableColor = ");
			builder.Append(this.NotConnectableColor.ToString());
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003108 File Offset: 0x00001308
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineSystemColorsSpec left, ZiplineSystemColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003114 File Offset: 0x00001314
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineSystemColorsSpec left, ZiplineSystemColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003128 File Offset: 0x00001328
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<OriginColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ConnectableColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<NotConnectableColor>k__BackingField);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003180 File Offset: 0x00001380
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineSystemColorsSpec);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000318E File Offset: 0x0000138E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003198 File Offset: 0x00001398
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineSystemColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<OriginColor>k__BackingField, other.<OriginColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ConnectableColor>k__BackingField, other.<ConnectableColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<NotConnectableColor>k__BackingField, other.<NotConnectableColor>k__BackingField));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003204 File Offset: 0x00001404
		[CompilerGenerated]
		protected ZiplineSystemColorsSpec(ZiplineSystemColorsSpec original) : base(original)
		{
			this.OriginColor = original.<OriginColor>k__BackingField;
			this.ConnectableColor = original.<ConnectableColor>k__BackingField;
			this.NotConnectableColor = original.<NotConnectableColor>k__BackingField;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003231 File Offset: 0x00001431
		public ZiplineSystemColorsSpec()
		{
		}
	}
}
