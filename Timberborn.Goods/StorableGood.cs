using System;

namespace Timberborn.Goods
{
	// Token: 0x0200001E RID: 30
	public readonly struct StorableGood : IEquatable<StorableGood>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003D36 File Offset: 0x00001F36
		public string GoodId { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003D3E File Offset: 0x00001F3E
		public bool Takeable { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003D46 File Offset: 0x00001F46
		public bool Givable { get; }

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D4E File Offset: 0x00001F4E
		public StorableGood(string goodId, bool takeable, bool givable)
		{
			this.GoodId = goodId;
			this.Takeable = takeable;
			this.Givable = givable;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003D65 File Offset: 0x00001F65
		public static StorableGood CreateAsTakeable(string goodId)
		{
			return new StorableGood(goodId, true, false);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003D6F File Offset: 0x00001F6F
		public static StorableGood CreateAsGivable(string goodId)
		{
			return new StorableGood(goodId, false, true);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003D79 File Offset: 0x00001F79
		public static StorableGood CreateGiveableAndTakeable(string goodId)
		{
			return new StorableGood(goodId, true, true);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003D83 File Offset: 0x00001F83
		public bool IsOnlyTakeable
		{
			get
			{
				return !this.Givable && this.Takeable;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003D95 File Offset: 0x00001F95
		public bool IsOnlyGivable
		{
			get
			{
				return this.Givable && !this.Takeable;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003DAC File Offset: 0x00001FAC
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3},", new object[]
			{
				"GoodId",
				this.GoodId,
				"Takeable",
				this.Takeable
			}) + string.Format(" {0}: {1}", "Givable", this.Givable);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003E0F File Offset: 0x0000200F
		public bool Equals(StorableGood other)
		{
			return object.Equals(this.GoodId, other.GoodId) && this.Takeable == other.Takeable && this.Givable == other.Givable;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003E48 File Offset: 0x00002048
		public override bool Equals(object obj)
		{
			if (obj is StorableGood)
			{
				StorableGood other = (StorableGood)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003E70 File Offset: 0x00002070
		public override int GetHashCode()
		{
			return (((this.GoodId != null) ? this.GoodId.GetHashCode() : 0) * 397 ^ this.Takeable.GetHashCode()) * 397 ^ this.Givable.GetHashCode();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003EBD File Offset: 0x000020BD
		public static bool operator ==(StorableGood left, StorableGood right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003EC7 File Offset: 0x000020C7
		public static bool operator !=(StorableGood left, StorableGood right)
		{
			return !left.Equals(right);
		}
	}
}
