using System;
using System.Collections.Immutable;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x02000009 RID: 9
	public class GraphicsQualitySettings : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000022BC File Offset: 0x000004BC
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x000022F4 File Offset: 0x000004F4
		public event EventHandler<SettingChangedEventArgs<bool>> AnisotropicFilteringQualityChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000012 RID: 18 RVA: 0x0000232C File Offset: 0x0000052C
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00000564
		public event EventHandler<SettingChangedEventArgs<AntialiasingType>> AntiAliasingTypeChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000014 RID: 20 RVA: 0x0000239C File Offset: 0x0000059C
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000023D4 File Offset: 0x000005D4
		public event EventHandler<SettingChangedEventArgs<int>> LightQualityChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000016 RID: 22 RVA: 0x0000240C File Offset: 0x0000060C
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002444 File Offset: 0x00000644
		public event EventHandler<SettingChangedEventArgs<int>> ShadowQualityChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000018 RID: 24 RVA: 0x0000247C File Offset: 0x0000067C
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000024B4 File Offset: 0x000006B4
		public event EventHandler<SettingChangedEventArgs<int>> TextureQualityChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001A RID: 26 RVA: 0x000024EC File Offset: 0x000006EC
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002524 File Offset: 0x00000724
		public event EventHandler<SettingChangedEventArgs<int>> WaterQualityChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600001C RID: 28 RVA: 0x0000255C File Offset: 0x0000075C
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002594 File Offset: 0x00000794
		public event EventHandler<SettingChangedEventArgs<bool>> BloomChanged;

		// Token: 0x0600001E RID: 30 RVA: 0x000025C9 File Offset: 0x000007C9
		public GraphicsQualitySettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025D8 File Offset: 0x000007D8
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000025FC File Offset: 0x000007FC
		public string OverallGraphicsQuality
		{
			get
			{
				return this._settings.GetSafeString(GraphicsQualitySettings.GraphicsQualityPresetKey, GraphicsQualitySettings.DefaultPreset.ToString());
			}
			set
			{
				this._settings.SetString(GraphicsQualitySettings.GraphicsQualityPresetKey, value);
				GraphicsQualityPreset preset = GraphicsQualitySettings.GetPreset(value);
				if (preset != GraphicsQualityPreset.Custom)
				{
					this.AnisotropicFilteringEnabled = AnisotropicFilteringSetting.GetValueForPreset(preset);
					this.AntiAliasingType = AntiAliasingTypeSetting.GetValueForPreset(preset);
					this.LightQuality = LightQualitySetting.GetValueForPreset(preset);
					this.ShadowQuality = ShadowQualityGraphicsSettings.GetValueForPreset(preset);
					this.TextureQuality = TextureQualitySetting.GetValueForPreset(preset);
					this.WaterQuality = WaterQualitySetting.GetValueForPreset(preset);
					this.BloomEnabled = BloomSetting.GetValueForPreset(preset);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000026B4 File Offset: 0x000008B4
		public bool AnisotropicFilteringEnabled
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeBool(GraphicsQualitySettings.AnisotropicFilteringKey, AnisotropicFilteringSetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return AnisotropicFilteringSetting.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetBool(GraphicsQualitySettings.AnisotropicFilteringKey, value);
				EventHandler<SettingChangedEventArgs<bool>> anisotropicFilteringQualityChanged = this.AnisotropicFilteringQualityChanged;
				if (anisotropicFilteringQualityChanged == null)
				{
					return;
				}
				anisotropicFilteringQualityChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026DE File Offset: 0x000008DE
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000026E6 File Offset: 0x000008E6
		public AntialiasingType AntiAliasingType
		{
			get
			{
				return this.GetAntiAliasing();
			}
			set
			{
				this._settings.SetInt(GraphicsQualitySettings.AntiAliasingTypeKey, (int)value);
				EventHandler<SettingChangedEventArgs<AntialiasingType>> antiAliasingTypeChanged = this.AntiAliasingTypeChanged;
				if (antiAliasingTypeChanged == null)
				{
					return;
				}
				antiAliasingTypeChanged(this, new SettingChangedEventArgs<AntialiasingType>(value));
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002710 File Offset: 0x00000910
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002748 File Offset: 0x00000948
		public int LightQuality
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeInt(GraphicsQualitySettings.LightQualityKey, LightQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return LightQualitySetting.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetInt(GraphicsQualitySettings.LightQualityKey, value);
				EventHandler<SettingChangedEventArgs<int>> lightQualityChanged = this.LightQualityChanged;
				if (lightQualityChanged == null)
				{
					return;
				}
				lightQualityChanged(this, new SettingChangedEventArgs<int>(value));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002774 File Offset: 0x00000974
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000027AC File Offset: 0x000009AC
		public int ShadowQuality
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeInt(GraphicsQualitySettings.ShadowQualityKey, ShadowQualityGraphicsSettings.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return ShadowQualityGraphicsSettings.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetInt(GraphicsQualitySettings.ShadowQualityKey, value);
				EventHandler<SettingChangedEventArgs<int>> shadowQualityChanged = this.ShadowQualityChanged;
				if (shadowQualityChanged == null)
				{
					return;
				}
				shadowQualityChanged(this, new SettingChangedEventArgs<int>(value));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002810 File Offset: 0x00000A10
		public int TextureQuality
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeInt(GraphicsQualitySettings.TextureQualityKey, TextureQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return TextureQualitySetting.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetInt(GraphicsQualitySettings.TextureQualityKey, value);
				EventHandler<SettingChangedEventArgs<int>> textureQualityChanged = this.TextureQualityChanged;
				if (textureQualityChanged == null)
				{
					return;
				}
				textureQualityChanged(this, new SettingChangedEventArgs<int>(value));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000283C File Offset: 0x00000A3C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002874 File Offset: 0x00000A74
		public int WaterQuality
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeInt(GraphicsQualitySettings.WaterQualityKey, WaterQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return WaterQualitySetting.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetInt(GraphicsQualitySettings.WaterQualityKey, value);
				EventHandler<SettingChangedEventArgs<int>> waterQualityChanged = this.WaterQualityChanged;
				if (waterQualityChanged == null)
				{
					return;
				}
				waterQualityChanged(this, new SettingChangedEventArgs<int>(value));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool BloomEnabled
		{
			get
			{
				GraphicsQualityPreset preset;
				if (!this.IsStandardPreset(out preset))
				{
					return this._settings.GetSafeBool(GraphicsQualitySettings.BloomKey, BloomSetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
				}
				return BloomSetting.GetValueForPreset(preset);
			}
			set
			{
				this._settings.SetBool(GraphicsQualitySettings.BloomKey, value);
				EventHandler<SettingChangedEventArgs<bool>> bloomChanged = this.BloomChanged;
				if (bloomChanged == null)
				{
					return;
				}
				bloomChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002902 File Offset: 0x00000B02
		public void ChangeToCustom()
		{
			this.OverallGraphicsQuality = "Custom";
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000290F File Offset: 0x00000B0F
		public void Load()
		{
			this.EnsureBackwardPresetCompatibility();
			this.ValidateSavedSettings();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002920 File Offset: 0x00000B20
		public void EnsureBackwardPresetCompatibility()
		{
			string safeString = this._settings.GetSafeString("GraphicsQuality", string.Empty);
			string safeString2 = this._settings.GetSafeString(GraphicsQualitySettings.GraphicsQualityPresetKey, string.Empty);
			if (safeString != string.Empty && safeString2 == string.Empty)
			{
				this.OverallGraphicsQuality = safeString;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000297C File Offset: 0x00000B7C
		public void ValidateSavedSettings()
		{
			this._settings.ValidateString(GraphicsQualitySettings.GraphicsQualityPresetKey, GraphicsQualitySettings.AllowedPresets, "High");
			this._settings.ValidateInt(GraphicsQualitySettings.AntiAliasingTypeKey, AntiAliasingTypeSetting.ValidValues, (int)AntiAliasingTypeSetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
			this._settings.ValidateInt(GraphicsQualitySettings.LightQualityKey, LightQualitySetting.ValidValues, LightQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
			this._settings.ValidateInt(GraphicsQualitySettings.ShadowQualityKey, ShadowQualityGraphicsSettings.ValidValues, ShadowQualityGraphicsSettings.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
			this._settings.ValidateInt(GraphicsQualitySettings.TextureQualityKey, TextureQualitySetting.ValidValues, TextureQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
			this._settings.ValidateInt(GraphicsQualitySettings.WaterQualityKey, WaterQualitySetting.ValidValues, WaterQualitySetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A3E File Offset: 0x00000C3E
		public bool IsStandardPreset(out GraphicsQualityPreset preset)
		{
			preset = GraphicsQualitySettings.GetPreset(this.OverallGraphicsQuality);
			return preset != GraphicsQualityPreset.Custom;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A58 File Offset: 0x00000C58
		public static GraphicsQualityPreset GetPreset(string presetName)
		{
			GraphicsQualityPreset result;
			if (!(presetName == "Low"))
			{
				if (!(presetName == "Medium"))
				{
					if (!(presetName == "High"))
					{
						if (!(presetName == "Ultra"))
						{
							result = GraphicsQualityPreset.Custom;
						}
						else
						{
							result = GraphicsQualityPreset.Ultra;
						}
					}
					else
					{
						result = GraphicsQualityPreset.High;
					}
				}
				else
				{
					result = GraphicsQualityPreset.Medium;
				}
			}
			else
			{
				result = GraphicsQualityPreset.Low;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public AntialiasingType GetAntiAliasing()
		{
			GraphicsQualityPreset preset;
			if (this.IsStandardPreset(out preset))
			{
				return AntiAliasingTypeSetting.GetValueForPreset(preset);
			}
			int safeInt = this._settings.GetSafeInt(GraphicsQualitySettings.AntiAliasingTypeKey, -1);
			if (safeInt >= 0)
			{
				return (AntialiasingType)safeInt;
			}
			string key = "AntiAliasingQuality";
			if (this._settings.GetSafeInt(key, -1) <= 1)
			{
				return AntialiasingType.Off;
			}
			return AntiAliasingTypeSetting.GetValueForPreset(GraphicsQualitySettings.DefaultPreset);
		}

		// Token: 0x04000018 RID: 24
		public static readonly ImmutableArray<string> AllowedPresets = new string[]
		{
			"Low",
			"Medium",
			"High",
			"Ultra",
			"Custom"
		}.ToImmutableArray<string>();

		// Token: 0x04000019 RID: 25
		public static readonly GraphicsQualityPreset DefaultPreset = GraphicsQualityPreset.High;

		// Token: 0x0400001A RID: 26
		public static readonly string GraphicsQualityPresetKey = "GraphicsQualityPreset";

		// Token: 0x0400001B RID: 27
		public static readonly string AnisotropicFilteringKey = "AnisotropicTexturesQuality";

		// Token: 0x0400001C RID: 28
		public static readonly string AntiAliasingTypeKey = "AntiAliasingType";

		// Token: 0x0400001D RID: 29
		public static readonly string LightQualityKey = "LightQuality";

		// Token: 0x0400001E RID: 30
		public static readonly string ShadowQualityKey = "ShadowQuality";

		// Token: 0x0400001F RID: 31
		public static readonly string TextureQualityKey = "TextureQuality";

		// Token: 0x04000020 RID: 32
		public static readonly string WaterQualityKey = "WaterQuality";

		// Token: 0x04000021 RID: 33
		public static readonly string BloomKey = "Bloom";

		// Token: 0x04000029 RID: 41
		public readonly ISettings _settings;
	}
}
