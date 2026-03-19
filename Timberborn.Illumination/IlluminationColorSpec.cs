using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x0200000D RID: 13
	public class IlluminationColorSpec : ComponentSpec, IEquatable<IlluminationColorSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028A4 File Offset: 0x00000AA4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(IlluminationColorSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000028B8 File Offset: 0x00000AB8
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000028C1 File Offset: 0x00000AC1
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000028C9 File Offset: 0x00000AC9
		[Serialize]
		public Color Color { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x000028D4 File Offset: 0x00000AD4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IlluminationColorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002920 File Offset: 0x00000B20
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Color = ");
			builder.Append(this.Color.ToString());
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IlluminationColorSpec left, IlluminationColorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000298F File Offset: 0x00000B8F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IlluminationColorSpec left, IlluminationColorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000029A3 File Offset: 0x00000BA3
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000029D9 File Offset: 0x00000BD9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IlluminationColorSpec);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029E8 File Offset: 0x00000BE8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IlluminationColorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A3C File Offset: 0x00000C3C
		[CompilerGenerated]
		protected IlluminationColorSpec([Nullable(1)] IlluminationColorSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Color = original.<Color>k__BackingField;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002290 File Offset: 0x00000490
		public IlluminationColorSpec()
		{
		}
	}
}
