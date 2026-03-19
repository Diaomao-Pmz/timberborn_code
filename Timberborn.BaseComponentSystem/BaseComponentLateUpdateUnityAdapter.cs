using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000005 RID: 5
	public class BaseComponentLateUpdateUnityAdapter : MonoBehaviour
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002253 File Offset: 0x00000453
		public void Start()
		{
			base.enabled = (this._lateUpdatableComponents.Count > 0);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002269 File Offset: 0x00000469
		public void Add(ILateUpdatableComponent component)
		{
			this._lateUpdatableComponents.Add(component);
			base.enabled = true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000227E File Offset: 0x0000047E
		public void Remove(ILateUpdatableComponent component)
		{
			this._lateUpdatableComponents.Remove(component);
			if (this._lateUpdatableComponents.Count == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A4 File Offset: 0x000004A4
		public void LateUpdate()
		{
			for (int i = 0; i < this._lateUpdatableComponents.Count; i++)
			{
				this._lateUpdatableComponents[i].LateUpdate();
			}
		}

		// Token: 0x04000009 RID: 9
		[HideInInspector]
		public readonly List<ILateUpdatableComponent> _lateUpdatableComponents = new List<ILateUpdatableComponent>();
	}
}
