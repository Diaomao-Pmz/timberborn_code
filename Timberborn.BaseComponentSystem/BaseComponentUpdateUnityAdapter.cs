using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000008 RID: 8
	public class BaseComponentUpdateUnityAdapter : MonoBehaviour
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000023DD File Offset: 0x000005DD
		public void Start()
		{
			base.enabled = (this._updatableComponents.Count > 0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023F3 File Offset: 0x000005F3
		public void Add(IUpdatableComponent component)
		{
			this._updatableComponents.Add(component);
			base.enabled = true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002408 File Offset: 0x00000608
		public void Remove(IUpdatableComponent component)
		{
			this._updatableComponents.Remove(component);
			if (this._updatableComponents.Count == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000242C File Offset: 0x0000062C
		public void Update()
		{
			for (int i = 0; i < this._updatableComponents.Count; i++)
			{
				this._updatableComponents[i].Update();
			}
		}

		// Token: 0x0400000D RID: 13
		[HideInInspector]
		public readonly List<IUpdatableComponent> _updatableComponents = new List<IUpdatableComponent>();
	}
}
