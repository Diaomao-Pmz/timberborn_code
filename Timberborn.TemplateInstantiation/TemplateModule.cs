using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using Timberborn.Common;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x0200000E RID: 14
	public class TemplateModule
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002741 File Offset: 0x00000941
		public FrozenDictionary<Type, ImmutableArray<DecoratorDefinition>> Decorators { get; }

		// Token: 0x06000032 RID: 50 RVA: 0x0000274C File Offset: 0x0000094C
		public TemplateModule(Dictionary<Type, List<DecoratorDefinition>> decorators)
		{
			this.Decorators = decorators.ToFrozenDictionary((KeyValuePair<Type, List<DecoratorDefinition>> pair) => pair.Key, (KeyValuePair<Type, List<DecoratorDefinition>> pair) => pair.Value.ToImmutableArray<DecoratorDefinition>(), null);
		}

		// Token: 0x04000026 RID: 38
		public const ImplicitUseKindFlags Flags = 8;

		// Token: 0x0200000F RID: 15
		public class Builder
		{
			// Token: 0x06000033 RID: 51 RVA: 0x000027AC File Offset: 0x000009AC
			public void AddDecorator<[MeansImplicitUse(8)] TSubject, [MeansImplicitUse(8)] TDecorator>()
			{
				DecoratorDefinition item = DecoratorDefinition.CreateSingleton(typeof(TDecorator));
				this._decorators.GetOrAdd(typeof(TSubject)).Add(item);
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000027E4 File Offset: 0x000009E4
			public void AddDedicatedDecorator<[MeansImplicitUse(8)] TSubject, [MeansImplicitUse(8)] TDecorator>(IDedicatedDecoratorInitializer<TSubject, TDecorator> initializer)
			{
				DecoratorDefinition item = DecoratorDefinition.CreateDedicated(typeof(TDecorator), delegate(object subject, object decorator)
				{
					initializer.Initialize((TSubject)((object)subject), (TDecorator)((object)decorator));
				});
				this._decorators.GetOrAdd(typeof(TSubject)).Add(item);
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002835 File Offset: 0x00000A35
			public TemplateModule Build()
			{
				return new TemplateModule(this._decorators);
			}

			// Token: 0x04000027 RID: 39
			public readonly Dictionary<Type, List<DecoratorDefinition>> _decorators = new Dictionary<Type, List<DecoratorDefinition>>();
		}
	}
}
