using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000013 RID: 19
	public class DistrictDistributionSetting : BaseComponent, IPersistentEntity, IInitializableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000091 RID: 145 RVA: 0x00003950 File Offset: 0x00001B50
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x00003988 File Offset: 0x00001B88
		public event EventHandler<GoodDistributionSetting> SettingChanged;

		// Token: 0x06000093 RID: 147 RVA: 0x000039BD File Offset: 0x00001BBD
		public DistrictDistributionSetting(GoodDistributionSettingSerializer goodDistributionSettingSerializer, IGoodService goodService)
		{
			this._goodDistributionSettingSerializer = goodDistributionSettingSerializer;
			this._goodService = goodService;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000039DE File Offset: 0x00001BDE
		public ReadOnlyList<GoodDistributionSetting> GoodDistributionSettings
		{
			get
			{
				return this._goodDistributionSettings.AsReadOnlyList<GoodDistributionSetting>();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000039EB File Offset: 0x00001BEB
		public void InitializeEntity()
		{
			this.AddMissingGoodSettings();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000039F3 File Offset: 0x00001BF3
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(DistrictDistributionSetting.DistrictDistributionSettingKey).Set<GoodDistributionSetting>(DistrictDistributionSetting.GoodDistributionSettingsKey, this._goodDistributionSettings, this._goodDistributionSettingSerializer);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A18 File Offset: 0x00001C18
		public void Load(IEntityLoader entityLoader)
		{
			foreach (GoodDistributionSetting goodDistributionSetting in entityLoader.GetComponent(DistrictDistributionSetting.DistrictDistributionSettingKey).Get<GoodDistributionSetting>(DistrictDistributionSetting.GoodDistributionSettingsKey, this._goodDistributionSettingSerializer))
			{
				this.AddGoodDistributionSetting(goodDistributionSetting);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A80 File Offset: 0x00001C80
		public GoodDistributionSetting GetGoodDistributionSetting(string goodId)
		{
			for (int i = 0; i < this._goodDistributionSettings.Count; i++)
			{
				if (this._goodDistributionSettings[i].GoodId == goodId)
				{
					return this._goodDistributionSettings[i];
				}
			}
			throw new ArgumentException("No good distribution setting for good " + goodId);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public IEnumerable<GoodDistributionSetting> GetGoodDistributionSettingsForGroup(string groupId)
		{
			foreach (GoodDistributionSetting goodDistributionSetting in this._goodDistributionSettings)
			{
				if (this._goodService.GetGood(goodDistributionSetting.GoodId).GoodGroupId == groupId)
				{
					yield return goodDistributionSetting;
				}
			}
			List<GoodDistributionSetting>.Enumerator enumerator = default(List<GoodDistributionSetting>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003AF0 File Offset: 0x00001CF0
		public void ResetToDefault()
		{
			foreach (GoodDistributionSetting goodDistributionSetting in this._goodDistributionSettings)
			{
				goodDistributionSetting.SetDefault();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B40 File Offset: 0x00001D40
		public void SetDistrictExportThreshold(int threshold)
		{
			foreach (GoodDistributionSetting goodDistributionSetting in this._goodDistributionSettings)
			{
				goodDistributionSetting.SetExportThreshold((float)threshold);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003B94 File Offset: 0x00001D94
		public void SetDistrictImportOption(ImportOption importOption)
		{
			foreach (GoodDistributionSetting goodDistributionSetting in this._goodDistributionSettings)
			{
				goodDistributionSetting.SetImportOption(importOption);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void AddMissingGoodSettings()
		{
			foreach (string text in this._goodService.Goods)
			{
				if (this.IsSettingMissing(text))
				{
					GoodSpec good = this._goodService.GetGood(text);
					this.AddGoodDistributionSetting(GoodDistributionSetting.CreateDefault(good));
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C60 File Offset: 0x00001E60
		public void AddGoodDistributionSetting(GoodDistributionSetting goodDistributionSetting)
		{
			this._goodDistributionSettings.Add(goodDistributionSetting);
			goodDistributionSetting.SettingChanged += this.OnSettingChanged;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C80 File Offset: 0x00001E80
		public void OnSettingChanged(object sender, EventArgs e)
		{
			EventHandler<GoodDistributionSetting> settingChanged = this.SettingChanged;
			if (settingChanged == null)
			{
				return;
			}
			settingChanged(this, (GoodDistributionSetting)sender);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C9C File Offset: 0x00001E9C
		public bool IsSettingMissing(string goodId)
		{
			using (List<GoodDistributionSetting>.Enumerator enumerator = this._goodDistributionSettings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GoodId == goodId)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000031 RID: 49
		public static readonly ComponentKey DistrictDistributionSettingKey = new ComponentKey("DistrictDistributionSetting");

		// Token: 0x04000032 RID: 50
		public static readonly ListKey<GoodDistributionSetting> GoodDistributionSettingsKey = new ListKey<GoodDistributionSetting>("GoodDistributionSettings");

		// Token: 0x04000034 RID: 52
		public readonly GoodDistributionSettingSerializer _goodDistributionSettingSerializer;

		// Token: 0x04000035 RID: 53
		public readonly IGoodService _goodService;

		// Token: 0x04000036 RID: 54
		public readonly List<GoodDistributionSetting> _goodDistributionSettings = new List<GoodDistributionSetting>();
	}
}
