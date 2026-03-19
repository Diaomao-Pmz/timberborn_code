using System;
using Timberborn.KeyBindingSystem;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000007 RID: 7
	public class KeyBindingGroup
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023B3 File Offset: 0x000005B3
		public VisualElement Root { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x000023BB File Offset: 0x000005BB
		public KeyBindingGroup(VisualElement root, KeyBindingGroupSpec keyBindingGroupSpec)
		{
			this._keyBindingGroupSpec = keyBindingGroupSpec;
			this.Root = root;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023D1 File Offset: 0x000005D1
		public bool IsHidden
		{
			get
			{
				return this._keyBindingGroupSpec.IsHiddenGroup;
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly KeyBindingGroupSpec _keyBindingGroupSpec;
	}
}
