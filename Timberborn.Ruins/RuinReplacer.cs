using System;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Ruins
{
	// Token: 0x02000010 RID: 16
	public class RuinReplacer : ILoadableSingleton
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public RuinReplacer(BlockObjectFactory blockObjectFactory, TemplateService templateService, EntityService entityService, EntitySelectionService entitySelectionService, RuinModelFactory ruinModelFactory)
		{
			this._blockObjectFactory = blockObjectFactory;
			this._templateService = templateService;
			this._entityService = entityService;
			this._entitySelectionService = entitySelectionService;
			this._ruinModelFactory = ruinModelFactory;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BDE File Offset: 0x00000DDE
		public void Load()
		{
			this._ruinTemplates = this._templateService.GetAll<RuinSpec>().ToImmutableArray<RuinSpec>();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void Shuffle(Ruin originalRuin)
		{
			bool wasSelected = this._entitySelectionService.IsSelected(originalRuin.GetComponent<SelectableObject>());
			this._entityService.Delete(originalRuin);
			RuinSpec ruinForHeight = this.GetRuinForHeight(originalRuin.SpecifiedHeight);
			Ruin ruin = this.Instantiate(ruinForHeight, originalRuin);
			this.CreateModels(ruin, null, wasSelected);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C44 File Offset: 0x00000E44
		public void Shrink(Ruin originalRuin)
		{
			bool wasSelected = this._entitySelectionService.IsSelected(originalRuin.GetComponent<SelectableObject>());
			int amount = originalRuin.Yielder.Yield.Amount;
			this._entityService.Delete(originalRuin);
			RuinSpec nextRuinTemplate;
			if (this.TryGetNextRuin(originalRuin, out nextRuinTemplate))
			{
				Ruin ruin = this.Instantiate(nextRuinTemplate, originalRuin);
				this.CreateModels(ruin, originalRuin.GetComponent<RuinModels>().VariantId, wasSelected);
				RuinReplacer.UpdateYield(ruin, amount);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public bool TryGetNextRuin(Ruin originalRuin, out RuinSpec nextRuin)
		{
			int num = originalRuin.SpecifiedHeight - 1;
			if (num == 0)
			{
				nextRuin = null;
				return false;
			}
			nextRuin = this.GetRuinForHeight(num);
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CDC File Offset: 0x00000EDC
		public RuinSpec GetRuinForHeight(int nextHeight)
		{
			foreach (RuinSpec ruinSpec in this._ruinTemplates)
			{
				if (ruinSpec.RuinHeight == nextHeight)
				{
					return ruinSpec;
				}
			}
			throw new ArgumentException("No ruin template found for height " + nextHeight.ToString());
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D29 File Offset: 0x00000F29
		public void CreateModels(Ruin ruin, string variantId, bool wasSelected)
		{
			this._ruinModelFactory.CreateModels(variantId, ruin);
			if (wasSelected)
			{
				this._entitySelectionService.Select(ruin.GetComponent<SelectableObject>());
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static void UpdateYield(Ruin instantiatedRuin, int currentYield)
		{
			GoodAmountSpec yield = instantiatedRuin.YielderSpec.Yield;
			int num = yield.Amount - currentYield;
			if (num > 0)
			{
				instantiatedRuin.Yielder.DecreaseYield(new GoodAmount(yield.Id, num));
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D8C File Offset: 0x00000F8C
		public Ruin Instantiate(RuinSpec nextRuinTemplate, Ruin originalRuin)
		{
			Placement placement = originalRuin.GetComponent<BlockObject>().Placement;
			return this._blockObjectFactory.CreateFinished(nextRuinTemplate.GetSpec<BlockObjectSpec>(), placement).GetComponent<Ruin>();
		}

		// Token: 0x04000028 RID: 40
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x04000029 RID: 41
		public readonly TemplateService _templateService;

		// Token: 0x0400002A RID: 42
		public readonly EntityService _entityService;

		// Token: 0x0400002B RID: 43
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400002C RID: 44
		public readonly RuinModelFactory _ruinModelFactory;

		// Token: 0x0400002D RID: 45
		public ImmutableArray<RuinSpec> _ruinTemplates;
	}
}
