using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Goods
{
	// Token: 0x0200000A RID: 10
	public class GoodAmountSpec : IEquatable<GoodAmountSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002232 File Offset: 0x00000432
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodAmountSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000223E File Offset: 0x0000043E
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002246 File Offset: 0x00000446
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000224F File Offset: 0x0000044F
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002257 File Offset: 0x00000457
		[Serialize]
		public int Amount { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002260 File Offset: 0x00000460
		public GoodAmount ToGoodAmount()
		{
			return new GoodAmount(this.Id, this.Amount);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002273 File Offset: 0x00000473
		public override string ToString()
		{
			return string.Format("{0}x {1}", this.Amount, this.Id);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Amount = ");
			builder.Append(this.Amount.ToString());
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022E3 File Offset: 0x000004E3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodAmountSpec left, GoodAmountSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022EF File Offset: 0x000004EF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodAmountSpec left, GoodAmountSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002303 File Offset: 0x00000503
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Amount>k__BackingField);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002343 File Offset: 0x00000543
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodAmountSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002354 File Offset: 0x00000554
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodAmountSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Amount>k__BackingField, other.<Amount>k__BackingField));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B5 File Offset: 0x000005B5
		[CompilerGenerated]
		protected GoodAmountSpec([Nullable(1)] GoodAmountSpec original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Amount = original.<Amount>k__BackingField;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000020F8 File Offset: 0x000002F8
		public GoodAmountSpec()
		{
		}
	}
}
