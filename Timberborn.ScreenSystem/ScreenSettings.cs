using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ScreenSystem
{
	// Token: 0x02000008 RID: 8
	public class ScreenSettings : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000023C0 File Offset: 0x000005C0
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000023F8 File Offset: 0x000005F8
		public event EventHandler<SettingChangedEventArgs<ScreenResolution>> ScreenResolutionChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000016 RID: 22 RVA: 0x00002430 File Offset: 0x00000630
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002468 File Offset: 0x00000668
		public event EventHandler<SettingChangedEventArgs<bool>> FullScreenChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000018 RID: 24 RVA: 0x000024A0 File Offset: 0x000006A0
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000024D8 File Offset: 0x000006D8
		public event EventHandler<SettingChangedEventArgs<float>> ResolutionScaleChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600001A RID: 26 RVA: 0x00002510 File Offset: 0x00000710
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002548 File Offset: 0x00000748
		public event EventHandler<SettingChangedEventArgs<int>> VSyncCountChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002580 File Offset: 0x00000780
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000025B8 File Offset: 0x000007B8
		public event EventHandler<SettingChangedEventArgs<float>> BrightnessChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001E RID: 30 RVA: 0x000025F0 File Offset: 0x000007F0
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x00002628 File Offset: 0x00000828
		public event EventHandler<SettingChangedEventArgs<int?>> FrameRateLimitChanged;

		// Token: 0x06000020 RID: 32 RVA: 0x0000265D File Offset: 0x0000085D
		public ScreenSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000026A8 File Offset: 0x000008A8
		public ScreenResolution ScreenResolution
		{
			get
			{
				return new ScreenResolution(this._settings.GetSafeInt(ScreenSettings.ResolutionWidthKey, Display.main.systemWidth), this._settings.GetSafeInt(ScreenSettings.ResolutionHeightKey, Display.main.systemHeight));
			}
			set
			{
				this._settings.SetInt(ScreenSettings.ResolutionWidthKey, value.Width);
				this._settings.SetInt(ScreenSettings.ResolutionHeightKey, value.Height);
				EventHandler<SettingChangedEventArgs<ScreenResolution>> screenResolutionChanged = this.ScreenResolutionChanged;
				if (screenResolutionChanged == null)
				{
					return;
				}
				screenResolutionChanged(this, new SettingChangedEventArgs<ScreenResolution>(value));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026FA File Offset: 0x000008FA
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000270D File Offset: 0x0000090D
		public bool FullScreen
		{
			get
			{
				return this._settings.GetSafeBool(ScreenSettings.FullScreenKey, true);
			}
			set
			{
				this._settings.SetBool(ScreenSettings.FullScreenKey, value);
				EventHandler<SettingChangedEventArgs<bool>> fullScreenChanged = this.FullScreenChanged;
				if (fullScreenChanged == null)
				{
					return;
				}
				fullScreenChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002737 File Offset: 0x00000937
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000274E File Offset: 0x0000094E
		public float ResolutionScale
		{
			get
			{
				return this._settings.GetSafeFloat(ScreenSettings.ResolutionScaleKey, 1f);
			}
			set
			{
				this._settings.SetFloat(ScreenSettings.ResolutionScaleKey, value);
				EventHandler<SettingChangedEventArgs<float>> resolutionScaleChanged = this.ResolutionScaleChanged;
				if (resolutionScaleChanged == null)
				{
					return;
				}
				resolutionScaleChanged(this, new SettingChangedEventArgs<float>(this.ResolutionScale));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002790 File Offset: 0x00000990
		public int VSyncCount
		{
			get
			{
				return this._settings.GetSafeInt(ScreenSettings.VSyncCountKey, 1);
			}
			set
			{
				this._settings.SetInt(ScreenSettings.VSyncCountKey, value);
				EventHandler<SettingChangedEventArgs<int>> vsyncCountChanged = this.VSyncCountChanged;
				if (vsyncCountChanged == null)
				{
					return;
				}
				vsyncCountChanged(this, new SettingChangedEventArgs<int>(value));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027BA File Offset: 0x000009BA
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000027D1 File Offset: 0x000009D1
		public float Brightness
		{
			get
			{
				return this._settings.GetSafeFloat(ScreenSettings.BrightnessKey, 1f);
			}
			set
			{
				this._settings.SetFloat(ScreenSettings.BrightnessKey, value);
				EventHandler<SettingChangedEventArgs<float>> brightnessChanged = this.BrightnessChanged;
				if (brightnessChanged == null)
				{
					return;
				}
				brightnessChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002840 File Offset: 0x00000A40
		public int? FrameRateLimit
		{
			get
			{
				if (!this._settings.GetSafeBool(ScreenSettings.FrameRateLimitEnabledKey, false))
				{
					return null;
				}
				return new int?(this._settings.GetSafeInt(ScreenSettings.FrameRateLimitKey, ScreenSettings.DefaultFrameRateLimit));
			}
			set
			{
				this._settings.SetBool(ScreenSettings.FrameRateLimitEnabledKey, value != null);
				if (value != null)
				{
					this._settings.SetInt(ScreenSettings.FrameRateLimitKey, value.Value);
				}
				else
				{
					this._settings.Clear(ScreenSettings.FrameRateLimitKey);
				}
				EventHandler<SettingChangedEventArgs<int?>> frameRateLimitChanged = this.FrameRateLimitChanged;
				if (frameRateLimitChanged == null)
				{
					return;
				}
				frameRateLimitChanged(this, new SettingChangedEventArgs<int?>(value));
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void Load()
		{
			this._settings.ValidateInt(ScreenSettings.VSyncCountKey, ScreenSettings.VSyncValues, ScreenSettings.DefaultVSyncCount);
			ImmutableArray<int> validValues = (from value in ScreenSettings.FrameRateLimitValues
			where value != null
			select value).Cast<int>().ToImmutableArray<int>();
			this._settings.ValidateInt(ScreenSettings.FrameRateLimitKey, validValues, ScreenSettings.DefaultFrameRateLimit);
		}

		// Token: 0x04000011 RID: 17
		public static readonly ImmutableArray<int> VSyncValues = ImmutableArray.Create<int>(0, 1, 2);

		// Token: 0x04000012 RID: 18
		public static readonly ImmutableArray<int?> FrameRateLimitValues = new int?[]
		{
			null,
			new int?(30),
			new int?(60),
			new int?(80),
			new int?(100),
			new int?(120),
			new int?(144),
			new int?(160),
			new int?(165),
			new int?(180),
			new int?(200),
			new int?(240)
		}.ToImmutableArray<int?>();

		// Token: 0x04000013 RID: 19
		public static readonly int DefaultVSyncCount = 1;

		// Token: 0x04000014 RID: 20
		public static readonly int DefaultFrameRateLimit = 120;

		// Token: 0x04000015 RID: 21
		public static readonly string ResolutionWidthKey = "ResolutionWidth";

		// Token: 0x04000016 RID: 22
		public static readonly string ResolutionHeightKey = "ResolutionHeight";

		// Token: 0x04000017 RID: 23
		public static readonly string FullScreenKey = "FullScreen";

		// Token: 0x04000018 RID: 24
		public static readonly string ResolutionScaleKey = "ResolutionScale";

		// Token: 0x04000019 RID: 25
		public static readonly string VSyncCountKey = "VSyncCount";

		// Token: 0x0400001A RID: 26
		public static readonly string BrightnessKey = "Brightness";

		// Token: 0x0400001B RID: 27
		public static readonly string FrameRateLimitEnabledKey = "FrameRateLimitEnabled";

		// Token: 0x0400001C RID: 28
		public static readonly string FrameRateLimitKey = "FrameRateLimit";

		// Token: 0x04000023 RID: 35
		public readonly ISettings _settings;
	}
}
