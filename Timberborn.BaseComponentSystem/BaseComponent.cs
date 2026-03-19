using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000004 RID: 4
	public abstract class BaseComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public bool Enabled { get; private set; } = true;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D9 File Offset: 0x000002D9
		internal bool Started { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E2 File Offset: 0x000002E2
		public GameObject GameObject
		{
			get
			{
				return this._componentCache.CachedGameObject;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020EF File Offset: 0x000002EF
		public Transform Transform
		{
			get
			{
				return this._componentCache.CachedTransform;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020FC File Offset: 0x000002FC
		public ReadOnlyList<object> AllComponents
		{
			get
			{
				return this._componentCache.AllComponents;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002109 File Offset: 0x00000309
		public string Name
		{
			get
			{
				return this._componentCache.name;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002118 File Offset: 0x00000318
		public void EnableComponent()
		{
			if (!this.Enabled)
			{
				this._componentCache.AddEnabledComponent(this);
				if (!this.Started && this._componentCache.StartIsEnabled)
				{
					IStartableComponent startableComponent = this as IStartableComponent;
					if (startableComponent != null)
					{
						startableComponent.Start();
						this.Started = true;
					}
				}
				this.Enabled = true;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216C File Offset: 0x0000036C
		public void DisableComponent()
		{
			if (this.Enabled)
			{
				this._componentCache.RemoveDisabledComponent(this);
				this.Enabled = false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002189 File Offset: 0x00000389
		public T GetEnabledComponent<T>()
		{
			return this._componentCache.GetCachedEnabledComponent<T>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002196 File Offset: 0x00000396
		public T GetComponent<T>()
		{
			return this._componentCache.GetCachedComponent<T>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A3 File Offset: 0x000003A3
		public bool HasComponent<T>()
		{
			return this.GetComponent<T>() != null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B3 File Offset: 0x000003B3
		public void GetComponents<T>(List<T> results)
		{
			this._componentCache.GetCachedComponents<T>(results);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C4 File Offset: 0x000003C4
		public List<T> GetComponentsAllocating<T>()
		{
			List<T> list = new List<T>();
			this._componentCache.GetCachedComponents<T>(list);
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E4 File Offset: 0x000003E4
		public bool TryGetComponent<T>(out T component)
		{
			return this._componentCache.TryGetCachedComponent<T>(out component);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021F4 File Offset: 0x000003F4
		public T GetComponentInChildren<T>(bool includeInactive = false)
		{
			T t = includeInactive ? this.GetComponent<T>() : this.GetEnabledComponent<T>();
			if (t == null)
			{
				return this.GameObject.GetComponentInChildren<T>(includeInactive);
			}
			return t;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002229 File Offset: 0x00000429
		public static implicit operator bool(BaseComponent baseComponent)
		{
			return baseComponent != null && baseComponent.GameObject;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000223B File Offset: 0x0000043B
		public void Initialize(ComponentCache componentCache)
		{
			this._componentCache = componentCache;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002244 File Offset: 0x00000444
		public BaseComponent()
		{
		}

		// Token: 0x04000008 RID: 8
		public ComponentCache _componentCache;
	}
}
