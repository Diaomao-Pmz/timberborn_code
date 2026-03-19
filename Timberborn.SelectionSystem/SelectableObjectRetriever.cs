using System;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000017 RID: 23
	public class SelectableObjectRetriever
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000036C8 File Offset: 0x000018C8
		public SelectableObject GetSelectableObject(BaseComponent target)
		{
			SelectableObject result;
			if (this.TryGetSelectableObject(target.GameObject, out result))
			{
				return result;
			}
			throw new Exception("SelectableObject component not found on object " + target.GameObject.name + "!");
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003706 File Offset: 0x00001906
		public bool TryGetSelectableObject(GameObject gameObject, out SelectableObject selectableObject)
		{
			selectableObject = gameObject.GetComponentInParentSlow<SelectableObject>();
			return selectableObject != null;
		}
	}
}
