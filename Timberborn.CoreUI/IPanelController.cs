using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000020 RID: 32
	public interface IPanelController
	{
		// Token: 0x06000084 RID: 132
		VisualElement GetPanel();

		// Token: 0x06000085 RID: 133
		bool OnUIConfirmed();

		// Token: 0x06000086 RID: 134
		void OnUICancelled();
	}
}
