using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200001B RID: 27
	public class KeyBindingGroupSpecService : ILoadableSingleton
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003B69 File Offset: 0x00001D69
		public KeyBindingGroupSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003B83 File Offset: 0x00001D83
		public ReadOnlyList<KeyBindingGroupSpec> KeyBindingGroupSpecs
		{
			get
			{
				return this._keyBindingGroupSpecs.AsReadOnlyList<KeyBindingGroupSpec>();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B90 File Offset: 0x00001D90
		public void Load()
		{
			this._keyBindingGroupSpecs.AddRange(this.GetOrderedGroups());
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public bool IsHiddenGroup(string groupId)
		{
			return this._keyBindingGroupSpecs.Single((KeyBindingGroupSpec group) => groupId == group.Id).IsHiddenGroup;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BDA File Offset: 0x00001DDA
		public IEnumerable<KeyBindingGroupSpec> GetOrderedGroups()
		{
			return from keyBindingGroupSpec in this._specService.GetSpecs<KeyBindingGroupSpec>()
			orderby keyBindingGroupSpec.Order
			select keyBindingGroupSpec;
		}

		// Token: 0x04000051 RID: 81
		public readonly ISpecService _specService;

		// Token: 0x04000052 RID: 82
		public readonly List<KeyBindingGroupSpec> _keyBindingGroupSpecs = new List<KeyBindingGroupSpec>();
	}
}
