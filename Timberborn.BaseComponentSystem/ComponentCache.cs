using System;
using System.Collections.Generic;
using Bindito.Core;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x0200000A RID: 10
	public class ComponentCache : MonoBehaviour
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025FC File Offset: 0x000007FC
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002604 File Offset: 0x00000804
		public GameObject CachedGameObject { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000260D File Offset: 0x0000080D
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002615 File Offset: 0x00000815
		public Transform CachedTransform { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x0000261E File Offset: 0x0000081E
		[Inject]
		public void InjectDependencies(ComponentCacheService componentCacheService, TypeBlacklist typeBlacklist)
		{
			this._componentCacheService = componentCacheService;
			this._typeBlacklist = typeBlacklist;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000262E File Offset: 0x0000082E
		public ReadOnlyList<object> AllComponents
		{
			get
			{
				return this._components.AsReadOnlyList<object>();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000263B File Offset: 0x0000083B
		public bool StartIsEnabled
		{
			get
			{
				return this._adapter.StartIsEnabled;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002648 File Offset: 0x00000848
		public void Initialize(List<object> instantiatedComponents, string blueprintName, TypeIndexMap typeIndexMap)
		{
			this._typeIndexMap = typeIndexMap;
			this.CachedGameObject = base.gameObject;
			this.CachedTransform = base.transform;
			this._name = blueprintName;
			this._components = instantiatedComponents;
			this._componentCacheService.SaveComponentsCount(this._name, this._components.Count);
			this._adapter = base.GetComponent<BaseComponentUnityAdapter>();
			this._updateAdapter = base.GetComponent<BaseComponentUpdateUnityAdapter>();
			this._lateUpdateAdapter = base.GetComponent<BaseComponentLateUpdateUnityAdapter>();
			foreach (object obj in instantiatedComponents)
			{
				BaseComponent baseComponent = obj as BaseComponent;
				if (baseComponent != null)
				{
					baseComponent.Initialize(this);
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000270C File Offset: 0x0000090C
		public void SetActive()
		{
			int count = this._components.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = this._components[i];
				IUpdatableComponent updatableComponent = obj as IUpdatableComponent;
				if (updatableComponent != null)
				{
					this._updateAdapter.Add(updatableComponent);
				}
				ILateUpdatableComponent lateUpdatableComponent = obj as ILateUpdatableComponent;
				if (lateUpdatableComponent != null)
				{
					this._lateUpdateAdapter.Add(lateUpdatableComponent);
				}
				IAwakableComponent awakableComponent = obj as IAwakableComponent;
				if (awakableComponent != null)
				{
					awakableComponent.Awake();
				}
			}
			this._componentCacheService.SaveComponentsCount(this._name, this._components.Count);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002798 File Offset: 0x00000998
		public void AddEnabledComponent(BaseComponent baseComponent)
		{
			IUpdatableComponent updatableComponent = baseComponent as IUpdatableComponent;
			if (updatableComponent != null)
			{
				this._updateAdapter.Add(updatableComponent);
			}
			ILateUpdatableComponent lateUpdatableComponent = baseComponent as ILateUpdatableComponent;
			if (lateUpdatableComponent != null)
			{
				this._lateUpdateAdapter.Add(lateUpdatableComponent);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027D4 File Offset: 0x000009D4
		public void RemoveDisabledComponent(BaseComponent baseComponent)
		{
			IUpdatableComponent updatableComponent = baseComponent as IUpdatableComponent;
			if (updatableComponent != null)
			{
				this._updateAdapter.Remove(updatableComponent);
			}
			ILateUpdatableComponent lateUpdatableComponent = baseComponent as ILateUpdatableComponent;
			if (lateUpdatableComponent != null)
			{
				this._lateUpdateAdapter.Remove(lateUpdatableComponent);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002810 File Offset: 0x00000A10
		public T GetCachedComponent<T>()
		{
			object index = this.GetIndex<T>();
			T result;
			if (index is int)
			{
				int index2 = (int)index;
				result = (T)((object)this._components[index2]);
			}
			else
			{
				if (index is List<int>)
				{
					throw new InvalidOperationException(this.GetMoreThenOneErrorMessage<T>(false));
				}
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000286C File Offset: 0x00000A6C
		public T GetCachedEnabledComponent<T>()
		{
			object index = this.GetIndex<T>();
			if (index is int)
			{
				int index2 = (int)index;
				T t = (T)((object)this._components[index2]);
				if (!ComponentCache.IsEnabled(t))
				{
					return default(T);
				}
				return t;
			}
			else
			{
				List<int> list = index as List<int>;
				if (list == null)
				{
					return default(T);
				}
				int num = -1;
				for (int i = 0; i < list.Count; i++)
				{
					if (ComponentCache.IsEnabled(this._components[list[i]]))
					{
						if (num != -1)
						{
							throw new InvalidOperationException(this.GetMoreThenOneErrorMessage<T>(true));
						}
						num = list[i];
					}
				}
				if (num == -1)
				{
					return default(T);
				}
				return (T)((object)this._components[num]);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002948 File Offset: 0x00000B48
		public void GetCachedComponents<T>(List<T> results)
		{
			object index = this.GetIndex<T>();
			if (index is int)
			{
				int index2 = (int)index;
				results.Add((T)((object)this._components[index2]));
				return;
			}
			List<int> list = index as List<int>;
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				results.Add((T)((object)this._components[list[i]]));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029C0 File Offset: 0x00000BC0
		public bool TryGetCachedComponent<T>(out T returnedComponent)
		{
			object index = this.GetIndex<T>();
			if (index is int)
			{
				int index2 = (int)index;
				returnedComponent = (T)((object)this._components[index2]);
				return true;
			}
			if (!(index is List<int>))
			{
				returnedComponent = default(T);
				return false;
			}
			throw new InvalidOperationException(this.GetMoreThenOneErrorMessage<T>(false));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A20 File Offset: 0x00000C20
		public void OnDestroy()
		{
			this.CachedGameObject = null;
			this.CachedTransform = null;
			this._componentCacheService = null;
			this._typeBlacklist = null;
			this._typeIndexMap = null;
			this._components = null;
			this._adapter = null;
			this._updateAdapter = null;
			this._lateUpdateAdapter = null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A6C File Offset: 0x00000C6C
		public object GetIndex<T>()
		{
			Type typeFromHandle = typeof(T);
			object result;
			if (this._typeIndexMap.TryGetIndex(typeFromHandle, out result))
			{
				return result;
			}
			this._typeBlacklist.Verify(typeFromHandle);
			this._typeIndexMap.CacheType<T>(this._components.AsReadOnlyList<object>());
			return this._typeIndexMap.GetIndex(typeFromHandle);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public static bool IsEnabled(object obj)
		{
			MonoBehaviour monoBehaviour = obj as MonoBehaviour;
			if (monoBehaviour != null)
			{
				if (!monoBehaviour.enabled)
				{
					goto IL_2A;
				}
			}
			else
			{
				BaseComponent baseComponent = obj as BaseComponent;
				if (baseComponent == null || !baseComponent.Enabled)
				{
					goto IL_2A;
				}
			}
			return true;
			IL_2A:
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B00 File Offset: 0x00000D00
		public string GetMoreThenOneErrorMessage<T>(bool enabledComponent = false)
		{
			string arg = enabledComponent ? "enabled component" : "component";
			return string.Format("More than one {0} of type {1} found in {2}", arg, typeof(T), base.name);
		}

		// Token: 0x04000014 RID: 20
		[HideInInspector]
		public ComponentCacheService _componentCacheService;

		// Token: 0x04000015 RID: 21
		[HideInInspector]
		public TypeBlacklist _typeBlacklist;

		// Token: 0x04000016 RID: 22
		[HideInInspector]
		public TypeIndexMap _typeIndexMap;

		// Token: 0x04000017 RID: 23
		[HideInInspector]
		public List<object> _components;

		// Token: 0x04000018 RID: 24
		[HideInInspector]
		public BaseComponentUnityAdapter _adapter;

		// Token: 0x04000019 RID: 25
		[HideInInspector]
		public BaseComponentUpdateUnityAdapter _updateAdapter;

		// Token: 0x0400001A RID: 26
		[HideInInspector]
		public BaseComponentLateUpdateUnityAdapter _lateUpdateAdapter;

		// Token: 0x0400001B RID: 27
		[HideInInspector]
		public string _name;
	}
}
