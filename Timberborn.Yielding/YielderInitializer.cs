using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Goods;
using Timberborn.MapStateSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Yielding
{
	// Token: 0x02000012 RID: 18
	public class YielderInitializer : IDedicatedDecoratorInitializer<IYielderDecorable, Yielder>
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002FB3 File Offset: 0x000011B3
		public YielderInitializer(IGoodService goodService, MapEditorMode mapEditorMode, RemoveYieldStrategySpecService removeYieldStrategySpecService)
		{
			this._goodService = goodService;
			this._mapEditorMode = mapEditorMode;
			this._removeYieldStrategySpecService = removeYieldStrategySpecService;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002FDC File Offset: 0x000011DC
		public void Initialize(IYielderDecorable subject, Yielder decorator)
		{
			string id = subject.Yielder.Yield.Id;
			int amount = this._goodService.HasGood(id) ? subject.Yielder.Yield.Amount : 0;
			if (this._mapEditorMode.IsMapEditor)
			{
				decorator.Initialize(subject.Yielder, new GoodAmount(id, amount), null, string.Empty);
				return;
			}
			IRemoveYieldStrategy removeYieldStrategy = this.GetRemoveYieldStrategy(subject, decorator);
			string animation = this._removeYieldStrategySpecService.GetRemoveYieldStrategySpec(removeYieldStrategy.Id).Animation;
			decorator.Initialize(subject.Yielder, new GoodAmount(id, amount), removeYieldStrategy, animation);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003078 File Offset: 0x00001278
		public IRemoveYieldStrategy GetRemoveYieldStrategy(IYielderDecorable subject, Yielder decorator)
		{
			decorator.GetComponents<IRemoveYieldStrategy>(this._removeYieldStrategies);
			IRemoveYieldStrategy removeYieldStrategy = this._removeYieldStrategies.SingleOrDefault((IRemoveYieldStrategy strategy) => this.ContainsResourceGroup(subject, strategy));
			if (removeYieldStrategy == null)
			{
				this.ThrowNullRemoveYieldStrategy(subject, decorator);
			}
			this._removeYieldStrategies.Clear();
			return removeYieldStrategy;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030D8 File Offset: 0x000012D8
		public bool ContainsResourceGroup(IYielderDecorable subject, IRemoveYieldStrategy removeYieldStrategy)
		{
			return this._removeYieldStrategySpecService.GetRemoveYieldStrategySpec(removeYieldStrategy.Id).CompatibleResourceGroups.Contains(subject.Yielder.ResourceGroup);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003110 File Offset: 0x00001310
		public void ThrowNullRemoveYieldStrategy(IYielderDecorable subject, BaseComponent component)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("No or to many remove yield strategy component or spec for resource group \"" + subject.Yielder.ResourceGroup + "\" in template: " + component.Name);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("IRemoveYieldStrategy components: ");
			stringBuilder.AppendLine();
			foreach (IRemoveYieldStrategy removeYieldStrategy in this._removeYieldStrategies)
			{
				ImmutableArray<string> compatibleResourceGroups = this._removeYieldStrategySpecService.GetRemoveYieldStrategySpec(removeYieldStrategy.Id).CompatibleResourceGroups;
				stringBuilder.Append(removeYieldStrategy.GetType().Name + "; ");
				stringBuilder.Append("Id: " + removeYieldStrategy.Id + "; ");
				stringBuilder.AppendLine("Compatible groups: " + string.Join(", ", compatibleResourceGroups));
			}
			throw new InvalidOperationException(stringBuilder.ToString());
		}

		// Token: 0x04000030 RID: 48
		public readonly IGoodService _goodService;

		// Token: 0x04000031 RID: 49
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000032 RID: 50
		public readonly RemoveYieldStrategySpecService _removeYieldStrategySpecService;

		// Token: 0x04000033 RID: 51
		public readonly List<IRemoveYieldStrategy> _removeYieldStrategies = new List<IRemoveYieldStrategy>();
	}
}
