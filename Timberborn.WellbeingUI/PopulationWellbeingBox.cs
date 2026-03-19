using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.GameDistricts;
using Timberborn.Population;
using Timberborn.SingletonSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000012 RID: 18
	public class PopulationWellbeingBox : IPanelController, ILoadableSingleton, IPanelBlocker
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002FC4 File Offset: 0x000011C4
		public PopulationWellbeingBox(VisualElementLoader visualElementLoader, PanelStack panelStack, WellbeingService wellbeingService, PopulationService populationService, DistrictContextService districtContextService, PopulationWellbeingCounterGroupFactory populationWellbeingCounterGroupFactory, PopulationWellbeingGoals populationWellbeingGoals)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._wellbeingService = wellbeingService;
			this._populationService = populationService;
			this._districtContextService = districtContextService;
			this._populationWellbeingCounterGroupFactory = populationWellbeingCounterGroupFactory;
			this._populationWellbeingGoals = populationWellbeingGoals;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003024 File Offset: 0x00001224
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/Population/PopulationWellbeingBox");
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._averageWellbeing = UQueryExtensions.Q<Label>(this._root, "AverageWellbeingScore", null);
			this._populationWellbeingGoals.Initialize(this._root);
			this.InitializeGroups(UQueryExtensions.Q<ScrollView>(this._root, "Items", null));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030A9 File Offset: 0x000012A9
		public VisualElement GetPanel()
		{
			this.UpdateAppliedNeedsCount();
			this.UpdateAverageWellbeing();
			this.UpdateCounters();
			this._populationWellbeingGoals.AddGoals();
			return this._root;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030CE File Offset: 0x000012CE
		public void Show()
		{
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030DC File Offset: 0x000012DC
		public void ShowWellbeingHighscore()
		{
			this._root.AddToClassList(PopulationWellbeingBox.WellbeingHighscoreClass);
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030FA File Offset: 0x000012FA
		public void ShowUnlockedFaction(FactionSpec factionSpec)
		{
			this._panelStack.PushOverlay(this);
			this._populationWellbeingGoals.StartBlinking(factionSpec);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003114 File Offset: 0x00001314
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003117 File Offset: 0x00001317
		public void OnUICancelled()
		{
			this._root.RemoveFromClassList(PopulationWellbeingBox.WellbeingHighscoreClass);
			this._appliedCount.Clear();
			this._populationWellbeingGoals.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000314B File Offset: 0x0000134B
		public PopulationData ContextualPopulationData
		{
			get
			{
				if (!this._districtContextService.SelectedDistrict)
				{
					return this._populationService.GlobalPopulationData;
				}
				return this._populationService.DistrictPopulationData;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003178 File Offset: 0x00001378
		public void InitializeGroups(VisualElement root)
		{
			foreach (PopulationWellbeingCounterGroup populationWellbeingCounterGroup in this._populationWellbeingCounterGroupFactory.Create())
			{
				root.Add(populationWellbeingCounterGroup.Root);
				this._counters.AddRange(populationWellbeingCounterGroup.Counters);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031E0 File Offset: 0x000013E0
		public void UpdateAppliedNeedsCount()
		{
			if (this._districtContextService.SelectedDistrict)
			{
				this._wellbeingService.DistrictAppliedNeeds(this._appliedCount);
				return;
			}
			this._wellbeingService.GlobalAppliedNeeds(this._appliedCount);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003218 File Offset: 0x00001418
		public void UpdateAverageWellbeing()
		{
			int num = this._districtContextService.SelectedDistrict ? this._wellbeingService.AverageDistrictWellbeing : this._wellbeingService.AverageGlobalWellbeing;
			this._averageWellbeing.text = num.ToString();
			this._averageWellbeing.EnableInClassList(PopulationWellbeingBox.NegativeWellbeingClass, num < 0);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003278 File Offset: 0x00001478
		public void UpdateCounters()
		{
			int numberOfBeavers = this.ContextualPopulationData.NumberOfBeavers;
			foreach (PopulationWellbeingCounter populationWellbeingCounter in this._counters)
			{
				int valueOrDefault = CollectionExtensions.GetValueOrDefault<string, int>(this._appliedCount, populationWellbeingCounter.NeedId, 0);
				populationWellbeingCounter.UpdateValues(valueOrDefault, numberOfBeavers);
			}
		}

		// Token: 0x0400004C RID: 76
		public static readonly string WellbeingHighscoreClass = "wellbeing-highscore";

		// Token: 0x0400004D RID: 77
		public static readonly string NegativeWellbeingClass = "wellbeing--negative";

		// Token: 0x0400004E RID: 78
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004F RID: 79
		public readonly PanelStack _panelStack;

		// Token: 0x04000050 RID: 80
		public readonly WellbeingService _wellbeingService;

		// Token: 0x04000051 RID: 81
		public readonly PopulationService _populationService;

		// Token: 0x04000052 RID: 82
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000053 RID: 83
		public readonly PopulationWellbeingCounterGroupFactory _populationWellbeingCounterGroupFactory;

		// Token: 0x04000054 RID: 84
		public readonly PopulationWellbeingGoals _populationWellbeingGoals;

		// Token: 0x04000055 RID: 85
		public readonly List<PopulationWellbeingCounter> _counters = new List<PopulationWellbeingCounter>();

		// Token: 0x04000056 RID: 86
		public readonly Dictionary<string, int> _appliedCount = new Dictionary<string, int>();

		// Token: 0x04000057 RID: 87
		public VisualElement _root;

		// Token: 0x04000058 RID: 88
		public Label _averageWellbeing;
	}
}
