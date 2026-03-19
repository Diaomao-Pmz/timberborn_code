using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemUI
{
	// Token: 0x02000006 RID: 6
	public class DistrictCrossingFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		public DistrictCrossingFragment(IBatchControlBox batchControlBox, BatchControlDistrict batchControlDistrict, ImportGoodIconFactory importGoodIconFactory, VisualElementLoader visualElementLoader)
		{
			this._batchControlBox = batchControlBox;
			this._batchControlDistrict = batchControlDistrict;
			this._importGoodIconFactory = importGoodIconFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DistrictCrossingFragment");
			this._root.ToggleDisplayStyle(false);
			UQueryExtensions.Q<Button>(this._root, "DistributionButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDistributionButtonClicked), 0);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "ImportGoodsWrapper", null);
			this._importGoodIcons = this._importGoodIconFactory.CreateImportGoods(parent).ToImmutableArray<ImportGoodIcon>();
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E1 File Offset: 0x000003E1
		public void ShowFragment(BaseComponent entity)
		{
			this._districtCrossing = entity.GetComponent<DistrictCrossing>();
			this.UpdateRootAndIcons();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		public void ClearFragment()
		{
			this._districtCrossing = null;
			this._root.ToggleDisplayStyle(false);
			foreach (ImportGoodIcon importGoodIcon in this._importGoodIcons)
			{
				importGoodIcon.Clear();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000223B File Offset: 0x0000043B
		public void UpdateFragment()
		{
			this.UpdateRootAndIcons();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002244 File Offset: 0x00000444
		public void UpdateRootAndIcons()
		{
			if (this._districtCrossing && this._districtCrossing.DistrictDistributableGoodProvider)
			{
				this._root.ToggleDisplayStyle(true);
				foreach (ImportGoodIcon importGoodIcon in this._importGoodIcons)
				{
					importGoodIcon.SetDistrictDistributableGoodProvider(this._districtCrossing.DistrictDistributableGoodProvider);
					importGoodIcon.Update();
				}
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C0 File Offset: 0x000004C0
		public void OnDistributionButtonClicked(ClickEvent evt)
		{
			DistrictCenter district = this._districtCrossing.GetComponent<DistrictBuilding>().District;
			this._batchControlDistrict.SetDistrict(district);
			this._batchControlBox.OpenDistributionTab();
		}

		// Token: 0x04000008 RID: 8
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x04000009 RID: 9
		public readonly BatchControlDistrict _batchControlDistrict;

		// Token: 0x0400000A RID: 10
		public readonly ImportGoodIconFactory _importGoodIconFactory;

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public ImmutableArray<ImportGoodIcon> _importGoodIcons;

		// Token: 0x0400000D RID: 13
		public DistrictCrossing _districtCrossing;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;
	}
}
