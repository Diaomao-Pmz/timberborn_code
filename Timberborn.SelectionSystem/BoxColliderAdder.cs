using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000008 RID: 8
	public class BoxColliderAdder : BaseComponent, IStartableComponent
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021A5 File Offset: 0x000003A5
		public void Start()
		{
			this.AddCollider();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
		public void AddCollider()
		{
			BoxColliderAdderSpec component = base.GetComponent<BoxColliderAdderSpec>();
			BoxCollider boxCollider = base.GameObject.FindChild(component.TargetName).gameObject.AddComponent<BoxCollider>();
			boxCollider.center = component.Center;
			boxCollider.size = component.Size;
		}
	}
}
