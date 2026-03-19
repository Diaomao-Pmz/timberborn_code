using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public class PreviewShowerSpec : ComponentSpec, IEquatable<PreviewShowerSpec>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004520 File Offset: 0x00002720
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PreviewShowerSpec);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000452C File Offset: 0x0000272C
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004534 File Offset: 0x00002734
		[Serialize]
		public Color BuildablePreview { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000453D File Offset: 0x0000273D
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004545 File Offset: 0x00002745
		[Serialize]
		public Color UnbuildablePreview { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000454E File Offset: 0x0000274E
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004556 File Offset: 0x00002756
		[Serialize]
		public Color WarningPreview { get; set; }

		// Token: 0x060000EE RID: 238 RVA: 0x00004560 File Offset: 0x00002760
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PreviewShowerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000045AC File Offset: 0x000027AC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BuildablePreview = ");
			builder.Append(this.BuildablePreview.ToString());
			builder.Append(", UnbuildablePreview = ");
			builder.Append(this.UnbuildablePreview.ToString());
			builder.Append(", WarningPreview = ");
			builder.Append(this.WarningPreview.ToString());
			return true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004644 File Offset: 0x00002844
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PreviewShowerSpec left, PreviewShowerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004650 File Offset: 0x00002850
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PreviewShowerSpec left, PreviewShowerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004664 File Offset: 0x00002864
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BuildablePreview>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<UnbuildablePreview>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<WarningPreview>k__BackingField);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000046BC File Offset: 0x000028BC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PreviewShowerSpec);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002892 File Offset: 0x00000A92
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000046CC File Offset: 0x000028CC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PreviewShowerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<BuildablePreview>k__BackingField, other.<BuildablePreview>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<UnbuildablePreview>k__BackingField, other.<UnbuildablePreview>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<WarningPreview>k__BackingField, other.<WarningPreview>k__BackingField));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004738 File Offset: 0x00002938
		[CompilerGenerated]
		protected PreviewShowerSpec(PreviewShowerSpec original) : base(original)
		{
			this.BuildablePreview = original.<BuildablePreview>k__BackingField;
			this.UnbuildablePreview = original.<UnbuildablePreview>k__BackingField;
			this.WarningPreview = original.<WarningPreview>k__BackingField;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002935 File Offset: 0x00000B35
		public PreviewShowerSpec()
		{
		}
	}
}
