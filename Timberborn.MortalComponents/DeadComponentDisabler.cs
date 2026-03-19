using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.MortalComponents
{
	// Token: 0x02000004 RID: 4
	public class DeadComponentDisabler
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void DisableComponentsDeadDoNotNeed(BaseComponent entity)
		{
			foreach (object obj in entity.AllComponents)
			{
				if (!(obj is IDeadNeededComponent))
				{
					BaseComponent baseComponent = obj as BaseComponent;
					if (baseComponent != null)
					{
						baseComponent.DisableComponent();
					}
				}
			}
		}
	}
}
