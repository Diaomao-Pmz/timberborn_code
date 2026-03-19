using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SettlementStatistics;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.GameWonderCompletionUI
{
	// Token: 0x02000005 RID: 5
	public class StatisticItemFactory
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
		public StatisticItemFactory(ILoc loc, VisualElementLoader visualElementLoader, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210E File Offset: 0x0000030E
		public VisualElement Create(string statisticId)
		{
			return this.Create(statisticId, true);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		public VisualElement CreateIfHasValue(string statisticId)
		{
			return this.Create(statisticId, false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		public VisualElement Create(string statisticId, bool alwaysVisible)
		{
			int orDefault = this._incrementalStatisticCollector.GetOrDefault(statisticId);
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/WonderCompletion/StatisticItem");
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = this._loc.T("WonderCompletion.Statistic." + statisticId);
			UQueryExtensions.Q<Label>(visualElement, "Value", null).text = NumberFormatter.FormatFullNumber(orDefault);
			visualElement.ToggleDisplayStyle(alwaysVisible || orDefault > 0);
			return visualElement;
		}

		// Token: 0x04000006 RID: 6
		public readonly ILoc _loc;

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
