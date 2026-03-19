using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Attractions
{
	// Token: 0x02000013 RID: 19
	public class GoodConsumingAttractionSurfaceControllerSpec : ComponentSpec, IEquatable<GoodConsumingAttractionSurfaceControllerSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000030F7 File Offset: 0x000012F7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodConsumingAttractionSurfaceControllerSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003103 File Offset: 0x00001303
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000310B File Offset: 0x0000130B
		[Serialize]
		public string SurfaceName { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000311C File Offset: 0x0000131C
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003128 File Offset: 0x00001328
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodConsumingAttractionSurfaceControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003174 File Offset: 0x00001374
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SurfaceName = ");
			builder.Append(this.SurfaceName);
			builder.Append(", AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031D7 File Offset: 0x000013D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodConsumingAttractionSurfaceControllerSpec left, GoodConsumingAttractionSurfaceControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031E3 File Offset: 0x000013E3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodConsumingAttractionSurfaceControllerSpec left, GoodConsumingAttractionSurfaceControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031F7 File Offset: 0x000013F7
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SurfaceName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000322D File Offset: 0x0000142D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodConsumingAttractionSurfaceControllerSpec);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002772 File Offset: 0x00000972
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000323C File Offset: 0x0000143C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodConsumingAttractionSurfaceControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SurfaceName>k__BackingField, other.<SurfaceName>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003290 File Offset: 0x00001490
		[CompilerGenerated]
		protected GoodConsumingAttractionSurfaceControllerSpec([Nullable(1)] GoodConsumingAttractionSurfaceControllerSpec original) : base(original)
		{
			this.SurfaceName = original.<SurfaceName>k__BackingField;
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000027C1 File Offset: 0x000009C1
		public GoodConsumingAttractionSurfaceControllerSpec()
		{
		}
	}
}
