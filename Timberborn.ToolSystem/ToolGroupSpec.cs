using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000015 RID: 21
	public class ToolGroupSpec : ComponentSpec, IEquatable<ToolGroupSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002454 File Offset: 0x00000654
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ToolGroupSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002460 File Offset: 0x00000660
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002468 File Offset: 0x00000668
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002471 File Offset: 0x00000671
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002479 File Offset: 0x00000679
		[Serialize]
		public string DisplayNameLocKey { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002482 File Offset: 0x00000682
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000248A File Offset: 0x0000068A
		[Serialize]
		public AssetRef<Sprite> Icon { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002494 File Offset: 0x00000694
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ToolGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024E0 File Offset: 0x000006E0
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
			builder.Append(", DisplayNameLocKey = ");
			builder.Append(this.DisplayNameLocKey);
			builder.Append(", Icon = ");
			builder.Append(this.Icon);
			return true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000254E File Offset: 0x0000074E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ToolGroupSpec left, ToolGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000255A File Offset: 0x0000075A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ToolGroupSpec left, ToolGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002570 File Offset: 0x00000770
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Sprite>>.Default.GetHashCode(this.<Icon>k__BackingField);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025C8 File Offset: 0x000007C8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ToolGroupSpec);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025D6 File Offset: 0x000007D6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025E0 File Offset: 0x000007E0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ToolGroupSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField) && EqualityComparer<AssetRef<Sprite>>.Default.Equals(this.<Icon>k__BackingField, other.<Icon>k__BackingField));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000264C File Offset: 0x0000084C
		[CompilerGenerated]
		protected ToolGroupSpec([Nullable(1)] ToolGroupSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
			this.Icon = original.<Icon>k__BackingField;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002679 File Offset: 0x00000879
		public ToolGroupSpec()
		{
		}
	}
}
