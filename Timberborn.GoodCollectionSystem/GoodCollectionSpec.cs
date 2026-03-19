using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GoodCollectionSystem
{
	// Token: 0x02000009 RID: 9
	public class GoodCollectionSpec : ComponentSpec, IEquatable<GoodCollectionSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021BF File Offset: 0x000003BF
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodCollectionSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021CB File Offset: 0x000003CB
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021D3 File Offset: 0x000003D3
		[Serialize]
		public string CollectionId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021DC File Offset: 0x000003DC
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021E4 File Offset: 0x000003E4
		[Serialize]
		public ImmutableArray<string> Goods { get; set; }

		// Token: 0x06000017 RID: 23 RVA: 0x000021F0 File Offset: 0x000003F0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodCollectionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000223C File Offset: 0x0000043C
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
			builder.Append(", Goods = ");
			builder.Append(this.Goods.ToString());
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000229F File Offset: 0x0000049F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodCollectionSpec left, GoodCollectionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022AB File Offset: 0x000004AB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodCollectionSpec left, GoodCollectionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022BF File Offset: 0x000004BF
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CollectionId>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Goods>k__BackingField);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022F5 File Offset: 0x000004F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodCollectionSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002303 File Offset: 0x00000503
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000230C File Offset: 0x0000050C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodCollectionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<CollectionId>k__BackingField, other.<CollectionId>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Goods>k__BackingField, other.<Goods>k__BackingField));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002360 File Offset: 0x00000560
		[CompilerGenerated]
		protected GoodCollectionSpec([Nullable(1)] GoodCollectionSpec original) : base(original)
		{
			this.CollectionId = original.<CollectionId>k__BackingField;
			this.Goods = original.<Goods>k__BackingField;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002381 File Offset: 0x00000581
		public GoodCollectionSpec()
		{
		}
	}
}
