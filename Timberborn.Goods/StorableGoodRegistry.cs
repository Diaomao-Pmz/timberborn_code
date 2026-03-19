using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Goods
{
	// Token: 0x02000020 RID: 32
	public class StorableGoodRegistry
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003F24 File Offset: 0x00002124
		public ReadOnlyList<StorableGoodAmount> Goods
		{
			get
			{
				return this._storableGoods.AsReadOnlyList<StorableGoodAmount>();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003F31 File Offset: 0x00002131
		public ReadOnlyHashSet<string> InputGoods
		{
			get
			{
				return this._inputGoods.AsReadOnlyHashSet<string>();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003F3E File Offset: 0x0000213E
		public ReadOnlyHashSet<string> OutputGoods
		{
			get
			{
				return this._outputGoods.AsReadOnlyHashSet<string>();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F4B File Offset: 0x0000214B
		public bool HasInputGoods
		{
			get
			{
				return this._inputGoods.Count > 0;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003F5B File Offset: 0x0000215B
		public bool HasOutputGoods
		{
			get
			{
				return this._outputGoods.Count > 0;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003F6C File Offset: 0x0000216C
		public void Add(IEnumerable<StorableGoodAmount> storableGoodAmounts)
		{
			foreach (StorableGoodAmount storableGoodAmount in storableGoodAmounts)
			{
				this.Add(storableGoodAmount);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003FB4 File Offset: 0x000021B4
		public bool Contains(string goodId)
		{
			foreach (StorableGoodAmount storableGoodAmount in this._storableGoods)
			{
				if (storableGoodAmount.StorableGood.GoodId == goodId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004020 File Offset: 0x00002220
		public int GetAmount(string goodId)
		{
			foreach (StorableGoodAmount storableGoodAmount in this._storableGoods)
			{
				if (storableGoodAmount.StorableGood.GoodId == goodId)
				{
					return storableGoodAmount.Amount;
				}
			}
			return 0;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004090 File Offset: 0x00002290
		public override string ToString()
		{
			return this._storableGoods.CollectionToString("_storableGoods");
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000040A4 File Offset: 0x000022A4
		public void Add(StorableGoodAmount storableGoodAmount)
		{
			Asserts.ValueIsInRange<int>(storableGoodAmount.Amount, 0, int.MaxValue, "StorableGoodAmount");
			this._storableGoods.Add(storableGoodAmount);
			StorableGood storableGood = storableGoodAmount.StorableGood;
			if (storableGood.Givable)
			{
				this._inputGoods.Add(storableGood.GoodId);
			}
			if (storableGood.Takeable)
			{
				this._outputGoods.Add(storableGood.GoodId);
			}
		}

		// Token: 0x04000053 RID: 83
		public readonly List<StorableGoodAmount> _storableGoods = new List<StorableGoodAmount>();

		// Token: 0x04000054 RID: 84
		public readonly HashSet<string> _inputGoods = new HashSet<string>();

		// Token: 0x04000055 RID: 85
		public readonly HashSet<string> _outputGoods = new HashSet<string>();
	}
}
