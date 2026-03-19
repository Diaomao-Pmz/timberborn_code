using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.StartingLocationSystem
{
	// Token: 0x02000008 RID: 8
	public class StartingLocationRenderer : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public void Awake()
		{
			this._renderers.AddRange(base.GameObject.GetComponentsInChildren<Renderer>(true));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		public void Show()
		{
			foreach (Renderer renderer in this._renderers)
			{
				renderer.enabled = true;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public void Hide()
		{
			foreach (Renderer renderer in this._renderers)
			{
				renderer.enabled = false;
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly List<Renderer> _renderers = new List<Renderer>();
	}
}
