using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.DropdownSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using UnityEngine;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x02000004 RID: 4
	public class FixedStockpileDropdownProvider : BaseComponent, IAwakableComponent, IStartableComponent, IExtendedTooltipDropdownProvider, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public IReadOnlyList<string> Items { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public FixedStockpileDropdownProvider(EntityChangeRecorderFactory entityChangeRecorderFactory, FixedStockpileGoodProvider fixedStockpileGoodProvider)
		{
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
			this._fixedStockpileGoodProvider = fixedStockpileGoodProvider;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E5 File Offset: 0x000002E5
		public void Awake()
		{
			this._fixedStockpileInventorySetter = base.GetComponent<FixedStockpileInventorySetter>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._stockpile = base.GetComponent<Stockpile>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210B File Offset: 0x0000030B
		public void Start()
		{
			this.Items = this._fixedStockpileGoodProvider.GetGoods(this._stockpile.WhitelistedGoodType);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212E File Offset: 0x0000032E
		public string GetValue()
		{
			return this._singleGoodAllower.AllowedGood;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void SetValue(string goodId)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._fixedStockpileInventorySetter))
			{
				this._fixedStockpileInventorySetter.SetGoodId(goodId);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002184 File Offset: 0x00000384
		public string FormatDisplayText(string goodId, bool selected)
		{
			return this._fixedStockpileGoodProvider.GetDisplayText(goodId);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002192 File Offset: 0x00000392
		public Sprite GetIcon(string goodId)
		{
			return this._fixedStockpileGoodProvider.GetIcon(goodId);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A0 File Offset: 0x000003A0
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A7 File Offset: 0x000003A7
		public string GetDropdownTooltip(string value)
		{
			return this._fixedStockpileGoodProvider.GetTooltip(value);
		}

		// Token: 0x04000007 RID: 7
		public readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x04000008 RID: 8
		public readonly FixedStockpileGoodProvider _fixedStockpileGoodProvider;

		// Token: 0x04000009 RID: 9
		public FixedStockpileInventorySetter _fixedStockpileInventorySetter;

		// Token: 0x0400000A RID: 10
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x0400000B RID: 11
		public Stockpile _stockpile;
	}
}
