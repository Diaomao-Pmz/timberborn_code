using System;

namespace Timberborn.Modding
{
	// Token: 0x0200000D RID: 13
	public class Mod
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002927 File Offset: 0x00000B27
		public ModDirectory ModDirectory { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000292F File Offset: 0x00000B2F
		public ModManifest Manifest { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002937 File Offset: 0x00000B37
		public bool IsEnabled { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000293F File Offset: 0x00000B3F
		public Mod(ModDirectory modDirectory, ModManifest manifest, bool isEnabled)
		{
			this.ModDirectory = modDirectory;
			this.Manifest = manifest;
			this.IsEnabled = isEnabled;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000295C File Offset: 0x00000B5C
		public string DisplayName
		{
			get
			{
				if (!this._isIdDuplicated)
				{
					return this.Manifest.Name;
				}
				return this.ModDirectory.OriginName + "/" + this.Manifest.Name;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029A0 File Offset: 0x00000BA0
		public void MarkIdAsDuplicated()
		{
			this._isIdDuplicated = true;
		}

		// Token: 0x04000022 RID: 34
		public bool _isIdDuplicated;
	}
}
