using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Attractions;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.TimeSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x0200000A RID: 10
	public class AttractionLoadRateBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026C8 File Offset: 0x000008C8
		public VisualElement Root { get; }

		// Token: 0x06000025 RID: 37 RVA: 0x000026D0 File Offset: 0x000008D0
		public AttractionLoadRateBatchControlRowItem(IDayNightCycle dayNightCycle, VisualElement root, AttractionLoadRate attractionLoadRate, IEnumerable<VisualElement> loadRateRoots)
		{
			this._dayNightCycle = dayNightCycle;
			this.Root = root;
			this._attractionLoadRate = attractionLoadRate;
			this._loadRateRoots = loadRateRoots.ToImmutableArray<VisualElement>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026FC File Offset: 0x000008FC
		public void Initialize()
		{
			this._loadRates = (from rate in this._loadRateRoots
			select UQueryExtensions.Q<VisualElement>(rate, "Rate", null)).ToImmutableArray<VisualElement>();
			this._hourMarkers = (from rate in this._loadRateRoots
			select UQueryExtensions.Q<VisualElement>(rate, "CurrentHourMarker", null)).ToImmutableArray<VisualElement>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002774 File Offset: 0x00000974
		public void UpdateRowItem()
		{
			for (int i = 0; i < this._loadRates.Length; i++)
			{
				VisualElement visualElement = this._loadRates[i];
				VisualElement visualElement2 = this._hourMarkers[i];
				float loadRate = this._attractionLoadRate.GetLoadRate(i);
				bool visible = i == (int)this._dayNightCycle.HoursPassedToday;
				visualElement.style.height = new StyleLength(Length.Percent(loadRate * 100f));
				visualElement2.ToggleDisplayStyle(visible);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027EF File Offset: 0x000009EF
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x04000022 RID: 34
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000023 RID: 35
		public readonly AttractionLoadRate _attractionLoadRate;

		// Token: 0x04000024 RID: 36
		public readonly ImmutableArray<VisualElement> _loadRateRoots;

		// Token: 0x04000025 RID: 37
		public ImmutableArray<VisualElement> _loadRates;

		// Token: 0x04000026 RID: 38
		public ImmutableArray<VisualElement> _hourMarkers;
	}
}
