using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000015 RID: 21
	public class PopulationWellbeingCounterGroupFactory
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000035CD File Offset: 0x000017CD
		public PopulationWellbeingCounterGroupFactory(VisualElementLoader visualElementLoader, NeedGroupSpecService needGroupSpecService, FactionNeedService factionNeedService)
		{
			this._visualElementLoader = visualElementLoader;
			this._needGroupSpecService = needGroupSpecService;
			this._factionNeedService = factionNeedService;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035EA File Offset: 0x000017EA
		public IEnumerable<PopulationWellbeingCounterGroup> Create()
		{
			foreach (NeedGroupSpec needGroupSpec in this._needGroupSpecService.NeedGroups)
			{
				string elementName = "Game/Population/PopulationWellbeingCounterGroup";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				UQueryExtensions.Q<Label>(visualElement, "Header", null).text = needGroupSpec.DisplayName.Value;
				IEnumerable<PopulationWellbeingCounter> counters = this.Create(UQueryExtensions.Q<VisualElement>(visualElement, "Items", null), needGroupSpec.Id);
				PopulationWellbeingCounterGroup populationWellbeingCounterGroup = new PopulationWellbeingCounterGroup(visualElement, counters);
				if (populationWellbeingCounterGroup.HasCounters)
				{
					yield return populationWellbeingCounterGroup;
				}
			}
			IEnumerator<NeedGroupSpec> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035FA File Offset: 0x000017FA
		public IEnumerable<PopulationWellbeingCounter> Create(VisualElement root, string needGroupId)
		{
			IEnumerable<IGrouping<string, NeedSpec>> enumerable = from need in this._factionNeedService.GetBeaverNeeds()
			where need.NeedGroupId == needGroupId && need.AffectsWellbeing
			group need by need.Id;
			foreach (IEnumerable<NeedSpec> source in enumerable)
			{
				string elementName = "Game/Population/PopulationWellbeingCounter";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				root.Add(visualElement);
				NeedSpec needSpec = source.First<NeedSpec>();
				VisualElement bar = UQueryExtensions.Q<VisualElement>(visualElement, "Progress", null);
				VisualElement barWrapper = UQueryExtensions.Q<VisualElement>(visualElement, "ProgressWrapper", null);
				Label appliedCount = UQueryExtensions.Q<Label>(visualElement, "Count", null);
				Label averageWellbeingShare = UQueryExtensions.Q<Label>(visualElement, "AverageWellbeingShare", null);
				PopulationWellbeingCounter populationWellbeingCounter = new PopulationWellbeingCounter(needSpec, visualElement, bar, barWrapper, appliedCount, averageWellbeingShare);
				UQueryExtensions.Q<Label>(visualElement, "Text", null).text = needSpec.DisplayName.Value;
				yield return populationWellbeingCounter;
			}
			IEnumerator<IGrouping<string, NeedSpec>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000064 RID: 100
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000065 RID: 101
		public readonly NeedGroupSpecService _needGroupSpecService;

		// Token: 0x04000066 RID: 102
		public readonly FactionNeedService _factionNeedService;
	}
}
