using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x02000016 RID: 22
	public class IlluminatorLightObjectsSpec : ComponentSpec, IEquatable<IlluminatorLightObjectsSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000348F File Offset: 0x0000168F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(IlluminatorLightObjectsSpec);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000349B File Offset: 0x0000169B
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000034A3 File Offset: 0x000016A3
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x060000AA RID: 170 RVA: 0x000034AC File Offset: 0x000016AC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IlluminatorLightObjectsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000034F8 File Offset: 0x000016F8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			return true;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003542 File Offset: 0x00001742
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IlluminatorLightObjectsSpec left, IlluminatorLightObjectsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000354E File Offset: 0x0000174E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IlluminatorLightObjectsSpec left, IlluminatorLightObjectsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003562 File Offset: 0x00001762
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003581 File Offset: 0x00001781
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IlluminatorLightObjectsSpec);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000358F File Offset: 0x0000178F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IlluminatorLightObjectsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000035C0 File Offset: 0x000017C0
		[CompilerGenerated]
		protected IlluminatorLightObjectsSpec([Nullable(1)] IlluminatorLightObjectsSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002290 File Offset: 0x00000490
		public IlluminatorLightObjectsSpec()
		{
		}
	}
}
