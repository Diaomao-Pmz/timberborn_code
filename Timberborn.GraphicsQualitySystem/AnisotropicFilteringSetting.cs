using System;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GraphicsQualitySystem
{
	// Token: 0x02000004 RID: 4
	public class AnisotropicFilteringSetting : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AnisotropicFilteringSetting(GraphicsQualitySettings graphicsQualitySettings)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public static bool GetValueForPreset(GraphicsQualityPreset preset)
		{
			return preset != GraphicsQualityPreset.Low;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		public void Load()
		{
			this._graphicsQualitySettings.AnisotropicFilteringQualityChanged += delegate(object _, SettingChangedEventArgs<bool> args)
			{
				this.Set(args.Value);
			};
			this.Set(this._graphicsQualitySettings.AnisotropicFilteringEnabled);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002102 File Offset: 0x00000302
		public void Set(bool value)
		{
			QualitySettings.anisotropicFiltering = (value ? 1 : 0);
		}

		// Token: 0x04000006 RID: 6
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000007 RID: 7
		public AnisotropicFiltering _initialFiltering;
	}
}
