using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AutomationBuildings;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000025 RID: 37
	public class RelayModeDescriptions : ILoadableSingleton
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000057EA File Offset: 0x000039EA
		public RelayModeDescriptions(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005804 File Offset: 0x00003A04
		public void Load()
		{
			foreach (RelayMode relayMode in Enum.GetValues(typeof(RelayMode)).Cast<RelayMode>())
			{
				string key = string.Format("{0}{1}{2}", RelayModeDescriptions.DescriptionLocKeyPrefix, relayMode, RelayModeDescriptions.DescriptionLocKeyPostfix);
				this._dictionary.Add(relayMode, this._loc.T(key));
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000588C File Offset: 0x00003A8C
		public string GetDescription(RelayMode relayMode)
		{
			return this._dictionary[relayMode];
		}

		// Token: 0x04000107 RID: 263
		public static readonly string DescriptionLocKeyPrefix = "Building.Relay.Mode.";

		// Token: 0x04000108 RID: 264
		public static readonly string DescriptionLocKeyPostfix = ".Description";

		// Token: 0x04000109 RID: 265
		public readonly ILoc _loc;

		// Token: 0x0400010A RID: 266
		public readonly Dictionary<RelayMode, string> _dictionary = new Dictionary<RelayMode, string>();
	}
}
