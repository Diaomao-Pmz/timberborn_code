using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.DropdownSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using UnityEngine;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000028 RID: 40
	public class ResourceCounterGoodsDropdownProvider : BaseComponent, IAwakableComponent, IStartableComponent, IExtendedTooltipDropdownProvider, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005E2F File Offset: 0x0000402F
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005E37 File Offset: 0x00004037
		public IReadOnlyList<string> Items { get; private set; }

		// Token: 0x06000112 RID: 274 RVA: 0x00005E40 File Offset: 0x00004040
		public ResourceCounterGoodsDropdownProvider(IGoodService goodService, GoodDescriber goodDescriber)
		{
			this._goodService = goodService;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005E56 File Offset: 0x00004056
		public void Awake()
		{
			this._resourceCounter = base.GetComponent<ResourceCounter>();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005E64 File Offset: 0x00004064
		public void Start()
		{
			this.Items = (from good in this._goodService.Goods
			orderby this.FormatDisplayText(good, false)
			select good).ToImmutableArray<string>();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005E97 File Offset: 0x00004097
		public string GetValue()
		{
			return this._resourceCounter.GoodId;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005EA4 File Offset: 0x000040A4
		public void SetValue(string goodId)
		{
			this._resourceCounter.SetGoodId(goodId);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005EB2 File Offset: 0x000040B2
		public string FormatDisplayText(string goodId, bool selected)
		{
			return this._goodService.GetGood(goodId).DisplayName.Value;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005ECA File Offset: 0x000040CA
		public Sprite GetIcon(string goodId)
		{
			return this._goodDescriber.GetIcon(goodId);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005ED8 File Offset: 0x000040D8
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005EDF File Offset: 0x000040DF
		public string GetDropdownTooltip(string value)
		{
			return this.FormatDisplayText(value, false);
		}

		// Token: 0x04000127 RID: 295
		public readonly IGoodService _goodService;

		// Token: 0x04000128 RID: 296
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000129 RID: 297
		public ResourceCounter _resourceCounter;
	}
}
