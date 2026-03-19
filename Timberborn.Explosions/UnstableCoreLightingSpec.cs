using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Explosions
{
	// Token: 0x0200001E RID: 30
	public class UnstableCoreLightingSpec : ComponentSpec, IEquatable<UnstableCoreLightingSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004641 File Offset: 0x00002841
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnstableCoreLightingSpec);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000464D File Offset: 0x0000284D
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004655 File Offset: 0x00002855
		[Serialize]
		public float MinInterval { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000465E File Offset: 0x0000285E
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004666 File Offset: 0x00002866
		[Serialize]
		public float MaxInterval { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000466F File Offset: 0x0000286F
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004677 File Offset: 0x00002877
		[Serialize]
		public float LightStrength { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004680 File Offset: 0x00002880
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004688 File Offset: 0x00002888
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00004694 File Offset: 0x00002894
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnstableCoreLightingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000046E0 File Offset: 0x000028E0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinInterval = ");
			builder.Append(this.MinInterval.ToString());
			builder.Append(", MaxInterval = ");
			builder.Append(this.MaxInterval.ToString());
			builder.Append(", LightStrength = ");
			builder.Append(this.LightStrength.ToString());
			builder.Append(", AttachmentId = ");
			builder.Append(this.AttachmentId);
			return true;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004791 File Offset: 0x00002991
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnstableCoreLightingSpec left, UnstableCoreLightingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000479D File Offset: 0x0000299D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnstableCoreLightingSpec left, UnstableCoreLightingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000047B4 File Offset: 0x000029B4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinInterval>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxInterval>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<LightStrength>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004823 File Offset: 0x00002A23
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnstableCoreLightingSpec);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004834 File Offset: 0x00002A34
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnstableCoreLightingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinInterval>k__BackingField, other.<MinInterval>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxInterval>k__BackingField, other.<MaxInterval>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<LightStrength>k__BackingField, other.<LightStrength>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000048B8 File Offset: 0x00002AB8
		[CompilerGenerated]
		protected UnstableCoreLightingSpec([Nullable(1)] UnstableCoreLightingSpec original) : base(original)
		{
			this.MinInterval = original.<MinInterval>k__BackingField;
			this.MaxInterval = original.<MaxInterval>k__BackingField;
			this.LightStrength = original.<LightStrength>k__BackingField;
			this.AttachmentId = original.<AttachmentId>k__BackingField;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002949 File Offset: 0x00000B49
		public UnstableCoreLightingSpec()
		{
		}
	}
}
