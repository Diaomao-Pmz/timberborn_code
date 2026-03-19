using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bindito.Core;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.PrefabOptimization;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x0200000C RID: 12
	public class TemplateInstantiatorProvider : IProvider<TemplateInstantiator>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000261F File Offset: 0x0000081F
		public TemplateInstantiatorProvider(BaseInstantiator baseInstantiator, OptimizedPrefabInstantiator optimizedPrefabInstantiator, IEnumerable<TemplateModule> templateModules)
		{
			this._baseInstantiator = baseInstantiator;
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._templateModules = templateModules.ToImmutableArray<TemplateModule>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002644 File Offset: 0x00000844
		public TemplateInstantiator Get()
		{
			IEnumerable<KeyValuePair<Type, IEnumerable<DecoratorDefinition>>> decorators = from pair in this.GetDecoratorsFromAllModules()
			select new KeyValuePair<Type, IEnumerable<DecoratorDefinition>>(pair.Key, pair.Value);
			return new TemplateInstantiator(this._baseInstantiator, this._optimizedPrefabInstantiator, decorators);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002690 File Offset: 0x00000890
		public Dictionary<Type, List<DecoratorDefinition>> GetDecoratorsFromAllModules()
		{
			Dictionary<Type, List<DecoratorDefinition>> dictionary = new Dictionary<Type, List<DecoratorDefinition>>();
			foreach (TemplateModule templateModule in this._templateModules)
			{
				foreach (KeyValuePair<Type, ImmutableArray<DecoratorDefinition>> keyValuePair in templateModule.Decorators)
				{
					dictionary.GetOrAdd(keyValuePair.Key).AddRange(keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x04000020 RID: 32
		public readonly BaseInstantiator _baseInstantiator;

		// Token: 0x04000021 RID: 33
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000022 RID: 34
		public readonly ImmutableArray<TemplateModule> _templateModules;
	}
}
