using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000019 RID: 25
	public class LabeledEntitySpec : ComponentSpec, IEquatable<LabeledEntitySpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028FA File Offset: 0x00000AFA
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(LabeledEntitySpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002906 File Offset: 0x00000B06
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000290E File Offset: 0x00000B0E
		[Serialize]
		public string DisplayNameLocKey { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002917 File Offset: 0x00000B17
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000291F File Offset: 0x00000B1F
		[Serialize]
		public string DescriptionLocKey { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002930 File Offset: 0x00000B30
		[Serialize]
		public string FlavorDescriptionLocKey { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002939 File Offset: 0x00000B39
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002941 File Offset: 0x00000B41
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x0600004B RID: 75 RVA: 0x0000294C File Offset: 0x00000B4C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LabeledEntitySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002998 File Offset: 0x00000B98
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DisplayNameLocKey = ");
			builder.Append(this.DisplayNameLocKey);
			builder.Append(", DescriptionLocKey = ");
			builder.Append(this.DescriptionLocKey);
			builder.Append(", FlavorDescriptionLocKey = ");
			builder.Append(this.FlavorDescriptionLocKey);
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A1F File Offset: 0x00000C1F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LabeledEntitySpec left, LabeledEntitySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A2B File Offset: 0x00000C2B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LabeledEntitySpec left, LabeledEntitySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A40 File Offset: 0x00000C40
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DescriptionLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FlavorDescriptionLocKey>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AAF File Offset: 0x00000CAF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LabeledEntitySpec);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002ABD File Offset: 0x00000CBD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AC8 File Offset: 0x00000CC8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LabeledEntitySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DescriptionLocKey>k__BackingField, other.<DescriptionLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FlavorDescriptionLocKey>k__BackingField, other.<FlavorDescriptionLocKey>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B4C File Offset: 0x00000D4C
		[CompilerGenerated]
		protected LabeledEntitySpec([Nullable(1)] LabeledEntitySpec original) : base(original)
		{
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.DescriptionLocKey = original.<DescriptionLocKey>k__BackingField;
			this.FlavorDescriptionLocKey = original.<FlavorDescriptionLocKey>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B85 File Offset: 0x00000D85
		public LabeledEntitySpec()
		{
		}
	}
}
