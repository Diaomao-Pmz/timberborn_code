using System;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AccessibilitySettingsSystem
{
	// Token: 0x02000004 RID: 4
	public class AccessibilitySettings : IPostLoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public AccessibilitySettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool StarfieldRotationDisabled
		{
			get
			{
				return this._settings.GetBool(AccessibilitySettings.StarfieldRotationDisabledKey, false);
			}
			set
			{
				this._settings.SetBool(AccessibilitySettings.StarfieldRotationDisabledKey, value);
				this.UpdateShaderProperties();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F9 File Offset: 0x000002F9
		public void PostLoad()
		{
			this.UpdateShaderProperties();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public void UpdateShaderProperties()
		{
			Shader.SetGlobalInt(AccessibilitySettings.StarfieldRotationDisabledProperty, this.StarfieldRotationDisabled ? 1 : 0);
		}

		// Token: 0x04000006 RID: 6
		public static readonly int StarfieldRotationDisabledProperty = Shader.PropertyToID("_StarfieldRotationDisabled");

		// Token: 0x04000007 RID: 7
		public static readonly string StarfieldRotationDisabledKey = "StarfieldRotationDisabled";

		// Token: 0x04000008 RID: 8
		public readonly ISettings _settings;
	}
}
