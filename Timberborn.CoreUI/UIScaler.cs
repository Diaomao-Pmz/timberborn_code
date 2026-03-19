using System;
using Timberborn.AssetSystem;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000054 RID: 84
	public class UIScaler : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public UIScaler(UISettings uiSettings, IAssetLoader assetLoader)
		{
			this._uiSettings = uiSettings;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005ABA File Offset: 0x00003CBA
		public void Load()
		{
			this._panelSettings = this._assetLoader.Load<PanelSettings>("UI/Views/Core/ScalablePanelSettings");
			this._uiSettings.UIScaleFactorChanged += delegate(object _, SettingChangedEventArgs<float> _)
			{
				this.SetScaleFactor();
			};
			this.SetScaleFactor();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005AEF File Offset: 0x00003CEF
		public void Unload()
		{
			this._panelSettings.scale = 1f;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005B01 File Offset: 0x00003D01
		public float ClampScaleFactor(float value)
		{
			return Mathf.Clamp(value, UIScaler.MinScaleFactor, UIScaler.MaxScaleFactor);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005B13 File Offset: 0x00003D13
		public void SetScaleFactor()
		{
			this._panelSettings.scale = this._uiSettings.UIScaleFactor;
		}

		// Token: 0x040000B9 RID: 185
		public static readonly float MinScaleFactor = 0.8f;

		// Token: 0x040000BA RID: 186
		public static readonly float MaxScaleFactor = 1.4f;

		// Token: 0x040000BB RID: 187
		public readonly UISettings _uiSettings;

		// Token: 0x040000BC RID: 188
		public readonly IAssetLoader _assetLoader;

		// Token: 0x040000BD RID: 189
		public PanelSettings _panelSettings;
	}
}
