using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Bindito.Core;
using Bindito.Unity;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000009 RID: 9
	public class BaseInstantiator
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002473 File Offset: 0x00000673
		public BaseInstantiator(IInstantiator instantiator, IContainer container, ComponentCacheService componentCacheService)
		{
			this._instantiator = instantiator;
			this._container = container;
			this._componentCacheService = componentCacheService;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000249C File Offset: 0x0000069C
		public GameObject InstantiateInactive(GameObject prefab, Transform parent, Blueprint blueprint, ImmutableArray<Type> decoratedComponents, BaseComponent initializingComponent, out List<object> instantiatedComponents)
		{
			bool flag;
			GameObject gameObject = this._instantiator.InstantiateInactive(prefab, parent, ref flag);
			ComponentCache componentCache = this._instantiator.AddComponent<ComponentCache>(gameObject);
			instantiatedComponents = this.InstantiateComponents(blueprint, initializingComponent, decoratedComponents);
			instantiatedComponents.Add(componentCache);
			string text = (initializingComponent == null) ? blueprint.Name : (blueprint.Name + "." + initializingComponent.GetType().Name);
			this._instantiator.AddComponent<BaseComponentUnityAdapter>(gameObject);
			this._instantiator.AddComponent<BaseComponentUpdateUnityAdapter>(gameObject);
			this._instantiator.AddComponent<BaseComponentLateUpdateUnityAdapter>(gameObject);
			TypeIndexMap orAdd = this._typeMaps.GetOrAdd(text);
			componentCache.Initialize(instantiatedComponents, text, orAdd);
			return gameObject;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002548 File Offset: 0x00000748
		public List<object> InstantiateComponents(Blueprint blueprint, BaseComponent initializingComponent, ImmutableArray<Type> decoratedComponents)
		{
			List<object> list = new List<object>(this._componentCacheService.GetComponentsCount(blueprint.Name));
			if (initializingComponent != null)
			{
				list.Add(initializingComponent);
			}
			foreach (Type type in decoratedComponents)
			{
				object item = this.InstantiateComponent(blueprint, type);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025A4 File Offset: 0x000007A4
		public object InstantiateComponent(Blueprint blueprint, Type type)
		{
			if (!typeof(ComponentSpec).IsAssignableFrom(type))
			{
				return this._container.GetInstance(type);
			}
			object spec = blueprint.GetSpec(type);
			if (spec == null)
			{
				throw new InvalidOperationException("Blueprint " + blueprint.Name + " does not contain spec for component " + type.Name);
			}
			return spec;
		}

		// Token: 0x0400000E RID: 14
		public readonly IInstantiator _instantiator;

		// Token: 0x0400000F RID: 15
		public readonly IContainer _container;

		// Token: 0x04000010 RID: 16
		public readonly ComponentCacheService _componentCacheService;

		// Token: 0x04000011 RID: 17
		public readonly Dictionary<string, TypeIndexMap> _typeMaps = new Dictionary<string, TypeIndexMap>();
	}
}
