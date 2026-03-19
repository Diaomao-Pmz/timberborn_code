using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.GoodsUI;
using Timberborn.Planting;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000013 RID: 19
	public class PlantablePrioritizerDropdownProvider : BaseComponent, IAwakableComponent, IInitializableEntity, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public PlantablePrioritizerDropdownProvider(GoodDescriber goodDescriber)
		{
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E0B File Offset: 0x0000100B
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._items.AsReadOnlyList<string>();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E20 File Offset: 0x00001020
		public bool HasMultipleOptions
		{
			get
			{
				return this._planterBuilding.AllowedPlantables.Length > 1;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E43 File Offset: 0x00001043
		public void Awake()
		{
			this._plantablePrioritizer = base.GetComponent<PlantablePrioritizer>();
			this._planterBuilding = base.GetComponent<PlanterBuilding>();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E60 File Offset: 0x00001060
		public void InitializeEntity()
		{
			ImmutableArray<PlantableSpec> allowedPlantables = this._planterBuilding.AllowedPlantables;
			this._items.Add(PlantablePrioritizerDropdownProvider.NoPriorityItemLocKey);
			this._items.AddRange(allowedPlantables.Select(new Func<PlantableSpec, string>(PlantablePrioritizerDropdownProvider.PlantableLocKey)));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EA6 File Offset: 0x000010A6
		public string GetValue()
		{
			if (!(this._plantablePrioritizer.PrioritizedPlantableSpec != null))
			{
				return PlantablePrioritizerDropdownProvider.NoPriorityItemLocKey;
			}
			return PlantablePrioritizerDropdownProvider.PlantableLocKey(this._plantablePrioritizer.PrioritizedPlantableSpec);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002ED4 File Offset: 0x000010D4
		public void SetValue(string value)
		{
			PlantableSpec prioritizedPlantable = this.GetPrioritizedPlantable(value);
			this._plantablePrioritizer.PrioritizePlantable(prioritizedPlantable);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002EF5 File Offset: 0x000010F5
		public string FormatDisplayText(string value, bool selected)
		{
			return value;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EF8 File Offset: 0x000010F8
		public Sprite GetIcon(string value)
		{
			PlantableSpec prioritizedPlantable = this.GetPrioritizedPlantable(value);
			if (prioritizedPlantable != null)
			{
				return this.GetPlantableIcon(prioritizedPlantable);
			}
			return null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F1F File Offset: 0x0000111F
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F26 File Offset: 0x00001126
		public static string PlantableLocKey(PlantableSpec plantableSpec)
		{
			return plantableSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F34 File Offset: 0x00001134
		public PlantableSpec GetPrioritizedPlantable(string value)
		{
			return this._planterBuilding.AllowedPlantables.SingleOrDefault((PlantableSpec plantable) => plantable.GetSpec<LabeledEntitySpec>().DisplayNameLocKey == value);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F6C File Offset: 0x0000116C
		public Sprite GetPlantableIcon(PlantableSpec plantableSpec)
		{
			IPlantableGoodIdProvider spec = plantableSpec.GetSpec<IPlantableGoodIdProvider>();
			if (spec != null)
			{
				string goodId = spec.GetGoodId();
				return this._goodDescriber.GetIcon(goodId);
			}
			return null;
		}

		// Token: 0x0400003A RID: 58
		public static readonly string NoPriorityItemLocKey = "Planting.NoPriorityOption";

		// Token: 0x0400003B RID: 59
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400003C RID: 60
		public PlantablePrioritizer _plantablePrioritizer;

		// Token: 0x0400003D RID: 61
		public PlanterBuilding _planterBuilding;

		// Token: 0x0400003E RID: 62
		public readonly List<string> _items = new List<string>();
	}
}
