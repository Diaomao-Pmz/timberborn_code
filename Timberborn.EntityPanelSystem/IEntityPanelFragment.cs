using System;
using Timberborn.BaseComponentSystem;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000018 RID: 24
	public interface IEntityPanelFragment
	{
		// Token: 0x060000A7 RID: 167
		VisualElement InitializeFragment();

		// Token: 0x060000A8 RID: 168
		void ShowFragment(BaseComponent entity);

		// Token: 0x060000A9 RID: 169
		void ClearFragment();

		// Token: 0x060000AA RID: 170
		void UpdateFragment();
	}
}
