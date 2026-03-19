using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.Localization;
using Timberborn.WaterSourceSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x02000007 RID: 7
	public class WaterSourceFragment : IEntityPanelFragment
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000224A File Offset: 0x0000044A
		public WaterSourceFragment(VisualElementLoader visualElementLoader, ILoc loc, WaterSettingFactory waterSettingFactory, EntityChangeRecorderFactory entityChangeRecorderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._waterSettingFactory = waterSettingFactory;
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000227A File Offset: 0x0000047A
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/WaterSourceFragment");
			this._root.ToggleDisplayStyle(false);
			this.AddSettings();
			return this._root;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022AC File Offset: 0x000004AC
		public void ShowFragment(BaseComponent entity)
		{
			this._waterSource = entity.GetComponent<WaterSource>();
			if (this._waterSource)
			{
				this._waterSourceContamination = entity.GetComponent<WaterSourceContamination>();
				this.UpdateWaterSettings();
				if (this._waterSettings.Any((WaterSetting setting) => setting.Visible))
				{
					this._root.ToggleDisplayStyle(true);
					return;
				}
			}
			else
			{
				this.ClearFragment();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002323 File Offset: 0x00000523
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._waterSource = null;
			this._waterSourceContamination = null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000233F File Offset: 0x0000053F
		public void UpdateFragment()
		{
			if (this._waterSource)
			{
				this.UpdateWaterSettings();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002354 File Offset: 0x00000554
		public void AddSettings()
		{
			this.AddSetting(this._loc.T(WaterSourceFragment.StrengthLocKey), new Action<float>(this.SetWaterSourceStrength), () => this._waterSource.SpecifiedStrength, false);
			this.AddSetting("Current strength", delegate(float _)
			{
			}, () => this._waterSource.CurrentStrength, true);
			this.AddSetting("Contamination", delegate(float value)
			{
				this._waterSourceContamination.SetContamination(value / 100f);
			}, () => this._waterSourceContamination.Contamination * 100f, true);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023EC File Offset: 0x000005EC
		public void AddSetting(string label, Action<float> setter, Func<float> getter, bool devModeOnly)
		{
			WaterSetting waterSetting = this._waterSettingFactory.Create(label, setter, getter, devModeOnly);
			this._waterSettings.Add(waterSetting);
			this._root.Add(waterSetting.Root);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002428 File Offset: 0x00000628
		public void SetWaterSourceStrength(float value)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._waterSource))
			{
				this._waterSource.SetSpecifiedStrength(value);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002470 File Offset: 0x00000670
		public void UpdateWaterSettings()
		{
			foreach (WaterSetting waterSetting in this._waterSettings)
			{
				waterSetting.UpdateState();
			}
		}

		// Token: 0x04000012 RID: 18
		public static readonly string StrengthLocKey = "Water.Strength";

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly ILoc _loc;

		// Token: 0x04000015 RID: 21
		public readonly WaterSettingFactory _waterSettingFactory;

		// Token: 0x04000016 RID: 22
		public readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x04000017 RID: 23
		public VisualElement _root;

		// Token: 0x04000018 RID: 24
		public WaterSource _waterSource;

		// Token: 0x04000019 RID: 25
		public WaterSourceContamination _waterSourceContamination;

		// Token: 0x0400001A RID: 26
		public readonly List<WaterSetting> _waterSettings = new List<WaterSetting>();
	}
}
