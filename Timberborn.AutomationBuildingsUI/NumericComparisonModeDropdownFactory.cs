using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.AutomationBuildings;
using Timberborn.DropdownSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000018 RID: 24
	public class NumericComparisonModeDropdownFactory : ILoadableSingleton
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00004078 File Offset: 0x00002278
		public NumericComparisonModeDropdownFactory(EnumDropdownProviderFactory enumDropdownProviderFactory, IAssetLoader assetLoader)
		{
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000409C File Offset: 0x0000229C
		public void Load()
		{
			foreach (object obj in Enum.GetValues(typeof(NumericComparisonMode)))
			{
				string text = obj.ToString();
				this._icons[text] = this._assetLoader.Load<Sprite>(NumericComparisonModeDropdownFactory.IconPath + "." + text);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004120 File Offset: 0x00002320
		public EnumDropdownProvider<NumericComparisonMode> Create(Func<NumericComparisonMode> getter, Action<NumericComparisonMode> setter)
		{
			return this._enumDropdownProviderFactory.CreateWithIcon<NumericComparisonMode>(getter, setter, (string _) => string.Empty, new Func<string, Sprite>(this.GetIcon));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000415A File Offset: 0x0000235A
		public Sprite GetIcon(string mode)
		{
			return this._icons[mode];
		}

		// Token: 0x040000A1 RID: 161
		public static readonly string IconPath = "Sprites/Comparison/Comparison";

		// Token: 0x040000A2 RID: 162
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x040000A3 RID: 163
		public readonly IAssetLoader _assetLoader;

		// Token: 0x040000A4 RID: 164
		public readonly Dictionary<string, Sprite> _icons = new Dictionary<string, Sprite>();
	}
}
