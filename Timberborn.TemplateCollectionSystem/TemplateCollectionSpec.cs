using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TemplateCollectionSystem
{
	// Token: 0x0200000D RID: 13
	public class TemplateCollectionSpec : ComponentSpec, IEquatable<TemplateCollectionSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000230B File Offset: 0x0000050B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TemplateCollectionSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002317 File Offset: 0x00000517
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000231F File Offset: 0x0000051F
		[Serialize]
		public string CollectionId { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002330 File Offset: 0x00000530
		[Serialize]
		public ImmutableArray<AssetRef<BlueprintAsset>> Blueprints { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x0000233C File Offset: 0x0000053C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TemplateCollectionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002388 File Offset: 0x00000588
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CollectionId = ");
			builder.Append(this.CollectionId);
			builder.Append(", Blueprints = ");
			builder.Append(this.Blueprints.ToString());
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023EB File Offset: 0x000005EB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TemplateCollectionSpec left, TemplateCollectionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023F7 File Offset: 0x000005F7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TemplateCollectionSpec left, TemplateCollectionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000240B File Offset: 0x0000060B
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CollectionId>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<BlueprintAsset>>>.Default.GetHashCode(this.<Blueprints>k__BackingField);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002441 File Offset: 0x00000641
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TemplateCollectionSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000244F File Offset: 0x0000064F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002458 File Offset: 0x00000658
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TemplateCollectionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<CollectionId>k__BackingField, other.<CollectionId>k__BackingField) && EqualityComparer<ImmutableArray<AssetRef<BlueprintAsset>>>.Default.Equals(this.<Blueprints>k__BackingField, other.<Blueprints>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024AC File Offset: 0x000006AC
		[CompilerGenerated]
		protected TemplateCollectionSpec([Nullable(1)] TemplateCollectionSpec original) : base(original)
		{
			this.CollectionId = original.<CollectionId>k__BackingField;
			this.Blueprints = original.<Blueprints>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024CD File Offset: 0x000006CD
		public TemplateCollectionSpec()
		{
		}
	}
}
