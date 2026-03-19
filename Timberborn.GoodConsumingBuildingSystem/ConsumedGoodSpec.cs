using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x02000007 RID: 7
	public class ConsumedGoodSpec : ComponentSpec, IEquatable<ConsumedGoodSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ConsumedGoodSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public string GoodId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public float GoodPerHour { get; set; }

		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConsumedGoodSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("GoodId = ");
			builder.Append(this.GoodId);
			builder.Append(", GoodPerHour = ");
			builder.Append(this.GoodPerHour.ToString());
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DB File Offset: 0x000003DB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConsumedGoodSpec left, ConsumedGoodSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E7 File Offset: 0x000003E7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConsumedGoodSpec left, ConsumedGoodSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FB File Offset: 0x000003FB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodId>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<GoodPerHour>k__BackingField);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002231 File Offset: 0x00000431
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConsumedGoodSpec);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000223F File Offset: 0x0000043F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002248 File Offset: 0x00000448
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConsumedGoodSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<GoodId>k__BackingField, other.<GoodId>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<GoodPerHour>k__BackingField, other.<GoodPerHour>k__BackingField));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229C File Offset: 0x0000049C
		[CompilerGenerated]
		protected ConsumedGoodSpec([Nullable(1)] ConsumedGoodSpec original) : base(original)
		{
			this.GoodId = original.<GoodId>k__BackingField;
			this.GoodPerHour = original.<GoodPerHour>k__BackingField;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022BD File Offset: 0x000004BD
		public ConsumedGoodSpec()
		{
		}
	}
}
