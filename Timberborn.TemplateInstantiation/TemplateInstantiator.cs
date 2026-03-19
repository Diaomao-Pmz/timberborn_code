using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000009 RID: 9
	public class TemplateInstantiator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002194 File Offset: 0x00000394
		public TemplateInstantiator(BaseInstantiator baseInstantiator, OptimizedPrefabInstantiator optimizedPrefabInstantiator, IEnumerable<KeyValuePair<Type, IEnumerable<DecoratorDefinition>>> decorators)
		{
			this._baseInstantiator = baseInstantiator;
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._decorators = decorators.ToFrozenDictionary((KeyValuePair<Type, IEnumerable<DecoratorDefinition>> pair) => pair.Key, (KeyValuePair<Type, IEnumerable<DecoratorDefinition>> pair) => pair.Value.ToImmutableArray<DecoratorDefinition>(), null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000222D File Offset: 0x0000042D
		public void CacheInstance(Blueprint blueprint)
		{
			this.GetCachedTemplate(blueprint);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002238 File Offset: 0x00000438
		public GameObject Instantiate(Blueprint blueprint, Transform parent, BaseComponent initializingComponent = null)
		{
			CachedTemplate cachedTemplate = this.GetCachedTemplate(blueprint);
			List<object> list;
			GameObject gameObject = this._baseInstantiator.InstantiateInactive(cachedTemplate.Prefab, parent, blueprint, cachedTemplate.Components, initializingComponent, out list);
			ImmutableArray<CachedTemplateInitializer> initializers = cachedTemplate.Initializers;
			int num = (initializingComponent != null) ? 1 : 0;
			for (int i = 0; i < initializers.Length; i++)
			{
				CachedTemplateInitializer cachedTemplateInitializer = initializers[i];
				cachedTemplateInitializer.Method(list[cachedTemplateInitializer.SubjectIndex + num], list[cachedTemplateInitializer.DecoratorIndex + num]);
			}
			gameObject.SetActive(true);
			return gameObject;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CF File Offset: 0x000004CF
		public static GameObject CreateCacheContainer()
		{
			GameObject gameObject = new GameObject(TemplateInstantiator.CacheContainerName);
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E4 File Offset: 0x000004E4
		public CachedTemplate GetCachedTemplate(Blueprint blueprint)
		{
			if (!this._cache.ContainsKey(blueprint))
			{
				GameObject gameObject = this._optimizedPrefabInstantiator.InstantiateInactive(blueprint, this._cacheContainer.Value.transform);
				gameObject.name = blueprint.Name;
				List<CachedTemplateInitializer> initializers;
				List<Type> components;
				this.GetInstanceComponents(blueprint, out initializers, out components);
				this._cache.Add(blueprint, new CachedTemplate(gameObject, initializers, components));
			}
			return this._cache[blueprint];
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002354 File Offset: 0x00000554
		public void GetInstanceComponents(Blueprint blueprint, out List<CachedTemplateInitializer> initializers, out List<Type> components)
		{
			initializers = new List<CachedTemplateInitializer>();
			components = (from spec in blueprint.Specs
			select spec.GetType()).ToList<Type>();
			this._temporaryTypeCache.AddRange(components);
			for (int i = 0; i < this._temporaryTypeCache.Count; i++)
			{
				Type subjectType = this._temporaryTypeCache[i];
				foreach (DecoratorDefinition decoratorDefinition in this.DecorateTypeAndInterfaces(subjectType))
				{
					Type decoratorType = TemplateInstantiator.GetDecoratorType(components, decoratorDefinition);
					if (decoratorType != null)
					{
						components.Add(decoratorType);
						this._temporaryTypeCache.Add(decoratorType);
						if (decoratorDefinition.Dedicated)
						{
							initializers.Add(new CachedTemplateInitializer(decoratorDefinition.Initializer, i, this._temporaryTypeCache.Count - 1));
						}
					}
				}
			}
			this._temporaryTypeCache.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002468 File Offset: 0x00000668
		public static Type GetDecoratorType(List<Type> components, DecoratorDefinition decorator)
		{
			Type decoratorType = decorator.DecoratorType;
			if (decorator.Dedicated || !components.Contains(decoratorType))
			{
				return decoratorType;
			}
			return null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002490 File Offset: 0x00000690
		public IEnumerable<DecoratorDefinition> DecorateTypeAndInterfaces(Type subjectType)
		{
			Type[] interfaces = subjectType.GetInterfaces();
			return Enumerables.One<Type>(subjectType).Concat(interfaces).SelectMany(new Func<Type, IEnumerable<DecoratorDefinition>>(this.DecorateComponentType));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024C1 File Offset: 0x000006C1
		public IEnumerable<DecoratorDefinition> DecorateComponentType(Type decoratedType)
		{
			ImmutableArray<DecoratorDefinition> immutableArray;
			if (this._decorators.TryGetValue(decoratedType, out immutableArray))
			{
				foreach (DecoratorDefinition decoratorDefinition in immutableArray)
				{
					yield return decoratorDefinition;
				}
				ImmutableArray<DecoratorDefinition>.Enumerator enumerator = default(ImmutableArray<DecoratorDefinition>.Enumerator);
			}
			yield break;
		}

		// Token: 0x0400000E RID: 14
		public static readonly string CacheContainerName = "TemplateCache";

		// Token: 0x0400000F RID: 15
		public readonly BaseInstantiator _baseInstantiator;

		// Token: 0x04000010 RID: 16
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000011 RID: 17
		public readonly FrozenDictionary<Type, ImmutableArray<DecoratorDefinition>> _decorators;

		// Token: 0x04000012 RID: 18
		public readonly Dictionary<Blueprint, CachedTemplate> _cache = new Dictionary<Blueprint, CachedTemplate>();

		// Token: 0x04000013 RID: 19
		public readonly Lazy<GameObject> _cacheContainer = new Lazy<GameObject>(new Func<GameObject>(TemplateInstantiator.CreateCacheContainer));

		// Token: 0x04000014 RID: 20
		public readonly List<Type> _temporaryTypeCache = new List<Type>();
	}
}
