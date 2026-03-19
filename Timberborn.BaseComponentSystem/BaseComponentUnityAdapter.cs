using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000007 RID: 7
	public class BaseComponentUnityAdapter : MonoBehaviour
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002321 File Offset: 0x00000521
		public bool StartIsEnabled { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000232A File Offset: 0x0000052A
		public void Awake()
		{
			this._componentCache = base.GetComponent<ComponentCache>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002338 File Offset: 0x00000538
		public void Start()
		{
			this.StartIsEnabled = true;
			int count = this._componentCache.AllComponents.Count;
			ReadOnlyList<object> allComponents = this._componentCache.AllComponents;
			for (int i = 0; i < count; i++)
			{
				IStartableComponent startableComponent = allComponents[i] as IStartableComponent;
				if (startableComponent != null)
				{
					BaseComponent baseComponent = startableComponent as BaseComponent;
					if (baseComponent != null && baseComponent.Enabled && !baseComponent.Started)
					{
						startableComponent.Start();
						baseComponent.Started = true;
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023B9 File Offset: 0x000005B9
		public void OnEnable()
		{
			if (!this._activated)
			{
				this._activated = true;
				this._componentCache.SetActive();
			}
		}

		// Token: 0x0400000B RID: 11
		[HideInInspector]
		public ComponentCache _componentCache;

		// Token: 0x0400000C RID: 12
		[HideInInspector]
		public bool _activated;
	}
}
