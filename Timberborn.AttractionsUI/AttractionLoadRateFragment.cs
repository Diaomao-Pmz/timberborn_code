using System;
using System.Collections.Generic;
using Timberborn.Attractions;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.TimeSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x0200000E RID: 14
	public class AttractionLoadRateFragment : IEntityPanelFragment
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000029BF File Offset: 0x00000BBF
		public AttractionLoadRateFragment(VisualElementLoader visualElementLoader, IDayNightCycle dayNightCycle)
		{
			this._visualElementLoader = visualElementLoader;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029E0 File Offset: 0x00000BE0
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/AttractionLoadRateFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._root, "LoadRates", null);
			for (int i = 0; i < 24; i++)
			{
				VisualElement visualElement2 = this._visualElementLoader.LoadVisualElement("Game/AttractionLoadRate");
				visualElement.Add(visualElement2);
				AttractionLoadRateFragment.LoadRate item = new AttractionLoadRateFragment.LoadRate(UQueryExtensions.Q<VisualElement>(visualElement2, "Rate", null), UQueryExtensions.Q<VisualElement>(visualElement2, "CurrentHourMarker", null));
				this._loadRates.Add(item);
			}
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A7A File Offset: 0x00000C7A
		public void ShowFragment(BaseComponent entity)
		{
			this._attractionLoadRate = entity.GetComponent<AttractionLoadRate>();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A88 File Offset: 0x00000C88
		public void ClearFragment()
		{
			this._attractionLoadRate = null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A94 File Offset: 0x00000C94
		public void UpdateFragment()
		{
			if (this._attractionLoadRate && this._attractionLoadRate.Enabled)
			{
				for (int i = 0; i < this._loadRates.Count; i++)
				{
					this.UpdateLoadRate(i);
				}
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void UpdateLoadRate(int hour)
		{
			AttractionLoadRateFragment.LoadRate loadRate = this._loadRates[hour];
			float loadRate2 = this._attractionLoadRate.GetLoadRate(hour);
			loadRate.Rate.style.height = new StyleLength(Length.Percent(loadRate2 * 100f));
			bool visible = hour == (int)this._dayNightCycle.HoursPassedToday;
			loadRate.CurrentHourMarker.ToggleDisplayStyle(visible);
		}

		// Token: 0x04000035 RID: 53
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000036 RID: 54
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000037 RID: 55
		public VisualElement _root;

		// Token: 0x04000038 RID: 56
		public readonly List<AttractionLoadRateFragment.LoadRate> _loadRates = new List<AttractionLoadRateFragment.LoadRate>();

		// Token: 0x04000039 RID: 57
		public AttractionLoadRate _attractionLoadRate;

		// Token: 0x0200000F RID: 15
		public readonly struct LoadRate
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B5A File Offset: 0x00000D5A
			public VisualElement Rate { get; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B62 File Offset: 0x00000D62
			public VisualElement CurrentHourMarker { get; }

			// Token: 0x06000041 RID: 65 RVA: 0x00002B6A File Offset: 0x00000D6A
			public LoadRate(VisualElement rate, VisualElement currentHourMarker)
			{
				this.Rate = rate;
				this.CurrentHourMarker = currentHourMarker;
			}
		}
	}
}
