using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Goods
{
	// Token: 0x02000015 RID: 21
	public class GoodsGroupSpecService : ILoadableSingleton
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000030C7 File Offset: 0x000012C7
		public GoodsGroupSpecService(ISpecService specService, IGoodService goodService)
		{
			this._specService = specService;
			this._goodService = goodService;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030E8 File Offset: 0x000012E8
		public ReadOnlyList<GoodGroupSpec> GoodGroupSpecs
		{
			get
			{
				return this._goodGroupSpecs.AsReadOnlyList<GoodGroupSpec>();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030F8 File Offset: 0x000012F8
		public void Load()
		{
			foreach (GoodGroupSpec goodGroupSpec2 in from goodGroupSpec in this._specService.GetSpecs<GoodGroupSpec>()
			orderby goodGroupSpec.Order
			select goodGroupSpec)
			{
				if (this._goodService.GetGoodsForGroup(goodGroupSpec2.Id).Any<string>())
				{
					this._goodGroupSpecs.Add(goodGroupSpec2);
				}
				else
				{
					Debug.LogWarning("Good group " + goodGroupSpec2.Id + " has no goods");
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000031A8 File Offset: 0x000013A8
		public GoodGroupSpec GetSpec(string goodGroupId)
		{
			GoodGroupSpec goodGroupSpec2 = this._goodGroupSpecs.SingleOrDefault((GoodGroupSpec goodGroupSpec) => goodGroupSpec.Id == goodGroupId);
			if (goodGroupSpec2 != null)
			{
				return goodGroupSpec2;
			}
			throw new InvalidOperationException("Good group spec with id " + goodGroupId + " not found or multiple specs found");
		}

		// Token: 0x04000032 RID: 50
		public readonly ISpecService _specService;

		// Token: 0x04000033 RID: 51
		public readonly IGoodService _goodService;

		// Token: 0x04000034 RID: 52
		public readonly List<GoodGroupSpec> _goodGroupSpecs = new List<GoodGroupSpec>();
	}
}
