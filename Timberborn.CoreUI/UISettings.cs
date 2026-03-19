using System;
using Timberborn.SettingsSystem;
using UnityEngine;

namespace Timberborn.CoreUI
{
	// Token: 0x02000055 RID: 85
	public class UISettings
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600015B RID: 347 RVA: 0x00005B4C File Offset: 0x00003D4C
		// (remove) Token: 0x0600015C RID: 348 RVA: 0x00005B84 File Offset: 0x00003D84
		public event EventHandler<SettingChangedEventArgs<float>> UIScaleFactorChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600015D RID: 349 RVA: 0x00005BBC File Offset: 0x00003DBC
		// (remove) Token: 0x0600015E RID: 350 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public event EventHandler<SettingChangedEventArgs<bool>> RunInBackgroundChanged;

		// Token: 0x0600015F RID: 351 RVA: 0x00005C29 File Offset: 0x00003E29
		public UISettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005C38 File Offset: 0x00003E38
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005C4B File Offset: 0x00003E4B
		public bool ShowFps
		{
			get
			{
				return this._settings.GetBool(UISettings.ShowFpsKey, false);
			}
			set
			{
				this._settings.SetBool(UISettings.ShowFpsKey, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005C5E File Offset: 0x00003E5E
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005C87 File Offset: 0x00003E87
		public float UIScaleFactor
		{
			get
			{
				return (float)Mathf.RoundToInt(this._settings.GetSafeFloat(UISettings.UIScaleFactorKey, 1f) / UISettings.UIScaleStep) * UISettings.UIScaleStep;
			}
			set
			{
				this._settings.SetFloat(UISettings.UIScaleFactorKey, value);
				EventHandler<SettingChangedEventArgs<float>> uiscaleFactorChanged = this.UIScaleFactorChanged;
				if (uiscaleFactorChanged == null)
				{
					return;
				}
				uiscaleFactorChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005CB1 File Offset: 0x00003EB1
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public bool RunInBackground
		{
			get
			{
				return this._settings.GetBool(UISettings.RunInBackgroundKey, false);
			}
			set
			{
				this._settings.SetBool(UISettings.RunInBackgroundKey, value);
				EventHandler<SettingChangedEventArgs<bool>> runInBackgroundChanged = this.RunInBackgroundChanged;
				if (runInBackgroundChanged == null)
				{
					return;
				}
				runInBackgroundChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x040000BE RID: 190
		public static readonly float UIScaleStep = 0.01f;

		// Token: 0x040000BF RID: 191
		public static readonly string ShowFpsKey = "ShowFPS";

		// Token: 0x040000C0 RID: 192
		public static readonly string UIScaleFactorKey = "UIScaleFactor";

		// Token: 0x040000C1 RID: 193
		public static readonly string RunInBackgroundKey = "RunInBackground";

		// Token: 0x040000C4 RID: 196
		public readonly ISettings _settings;
	}
}
