using System;
using Timberborn.Common;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000019 RID: 25
	public class KeyBindingFactory
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000381C File Offset: 0x00001A1C
		public KeyBindingFactory(KeyBindingGroupSpecService keyBindingGroupSpecService, ILoc loc)
		{
			this._keyBindingGroupSpecService = keyBindingGroupSpecService;
			this._loc = loc;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003834 File Offset: 0x00001A34
		public KeyBinding Create(KeyBindingDefinition keyBindingDefinition)
		{
			KeyBindingSpec keyBindingSpec = keyBindingDefinition.KeyBindingSpec;
			bool isHidden = this._keyBindingGroupSpecService.IsHiddenGroup(keyBindingSpec.GroupId);
			return new KeyBinding(this.GetDisplayName(keyBindingSpec, isHidden), keyBindingDefinition, isHidden);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000386C File Offset: 0x00001A6C
		public string GetDisplayName(KeyBindingSpec keyBindingSpec, bool isHidden)
		{
			string locKey = keyBindingSpec.LocKey;
			if (string.IsNullOrEmpty(locKey))
			{
				string id = keyBindingSpec.Id;
				if (!isHidden)
				{
					Debug.LogWarning("Loc key not defined for key binding: " + id);
				}
				return "<color=\"orange\">" + id.SplitPascalCase() + "</color>";
			}
			return this._loc.T(locKey);
		}

		// Token: 0x0400004B RID: 75
		public readonly KeyBindingGroupSpecService _keyBindingGroupSpecService;

		// Token: 0x0400004C RID: 76
		public readonly ILoc _loc;
	}
}
