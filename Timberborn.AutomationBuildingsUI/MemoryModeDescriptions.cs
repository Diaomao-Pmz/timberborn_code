using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AutomationBuildings;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000017 RID: 23
	public class MemoryModeDescriptions : ILoadableSingleton
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003F33 File Offset: 0x00002133
		public MemoryModeDescriptions(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003F50 File Offset: 0x00002150
		public void Load()
		{
			string str = this._loc.T<string>(MemoryModeDescriptions.ResetsWhenResetInputLocKey, this._loc.T(MemoryModeDescriptions.ResetInputLocKey));
			string param = this._loc.T(MemoryModeDescriptions.InputALocKey);
			string param2 = this._loc.T(MemoryModeDescriptions.InputBLocKey);
			foreach (MemoryMode memoryMode in Enum.GetValues(typeof(MemoryMode)).Cast<MemoryMode>())
			{
				string key = string.Format("{0}{1}{2}", MemoryModeDescriptions.DescriptionLocKeyPrefix, memoryMode, MemoryModeDescriptions.DescriptionLocKeyPostfix);
				this._dictionary.Add(memoryMode, this._loc.T<string, string>(key, param, param2) + "\n" + str);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000402C File Offset: 0x0000222C
		public string GetDescription(MemoryMode memoryMode)
		{
			return this._dictionary[memoryMode];
		}

		// Token: 0x04000099 RID: 153
		public static readonly string DescriptionLocKeyPrefix = "Building.Memory.Mode.";

		// Token: 0x0400009A RID: 154
		public static readonly string DescriptionLocKeyPostfix = ".Description";

		// Token: 0x0400009B RID: 155
		public static readonly string InputALocKey = "Automation.Input.A";

		// Token: 0x0400009C RID: 156
		public static readonly string InputBLocKey = "Automation.Input.B";

		// Token: 0x0400009D RID: 157
		public static readonly string ResetInputLocKey = "Automation.Input.Reset";

		// Token: 0x0400009E RID: 158
		public static readonly string ResetsWhenResetInputLocKey = "Automation.ResetsWhenResetInput";

		// Token: 0x0400009F RID: 159
		public readonly ILoc _loc;

		// Token: 0x040000A0 RID: 160
		public readonly Dictionary<MemoryMode, string> _dictionary = new Dictionary<MemoryMode, string>();
	}
}
