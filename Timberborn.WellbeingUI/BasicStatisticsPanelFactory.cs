using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameFactionSystem;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000008 RID: 8
	public class BasicStatisticsPanelFactory : ILoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002378 File Offset: 0x00000578
		public BasicStatisticsPanelFactory(VisualElementLoader visualElementLoader, PopulationWellbeingBox populationWellbeingBox, FactionService factionService, ScienceService scienceService, ITooltipRegistrar tooltipRegistrar, IBatchControlBox batchControlBox, PopulationService populationService, ILoc loc, BindableButtonFactory bindableButtonFactory, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._populationWellbeingBox = populationWellbeingBox;
			this._factionService = factionService;
			this._scienceService = scienceService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._batchControlBox = batchControlBox;
			this._populationService = populationService;
			this._loc = loc;
			this._bindableButtonFactory = bindableButtonFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023D8 File Offset: 0x000005D8
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E8 File Offset: 0x000005E8
		public VisualElement Create()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/Population/BasicStatisticsPanel");
			this._wellbeingButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(visualElement, "Wellbeing", null), BasicStatisticsPanelFactory.OpenWellbeingBoxKey, new Action(this._populationWellbeingBox.Show), true);
			this._tooltipRegistrar.RegisterLocalizable(UQueryExtensions.Q<Button>(visualElement, "Wellbeing", null), new Func<string>(this.GetWellbeingTooltip));
			Button button = UQueryExtensions.Q<Button>(visualElement, "BatchControl", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._batchControlBox.OpenBatchControlBox();
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button, BasicStatisticsPanelFactory.BatchControlLocKey);
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(visualElement, "ScienceCountHeader", null), new Func<string>(this.GetScienceTooltip));
			UQueryExtensions.Q<Image>(visualElement, "FactionIcon", null).sprite = this._factionService.Current.Logo.Asset;
			return visualElement;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024D8 File Offset: 0x000006D8
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._wellbeingButton.Bind();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024E5 File Offset: 0x000006E5
		public string GetWellbeingTooltip()
		{
			if (this._populationService.OnlyBotsAlive)
			{
				return BasicStatisticsPanelFactory.BeaversPerishedLocKey;
			}
			if (!this._populationService.AllDead)
			{
				return BasicStatisticsPanelFactory.WellbeingLocKey;
			}
			return BasicStatisticsPanelFactory.AllPerishedLocKey;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002512 File Offset: 0x00000712
		public string GetScienceTooltip()
		{
			return string.Format("{0}: {1}", this._loc.T(BasicStatisticsPanelFactory.ScienceLocKey), this._scienceService.SciencePoints);
		}

		// Token: 0x0400001A RID: 26
		public static readonly string WellbeingLocKey = "Wellbeing.DisplayName";

		// Token: 0x0400001B RID: 27
		public static readonly string BatchControlLocKey = "BatchControl.ShowInfo";

		// Token: 0x0400001C RID: 28
		public static readonly string BeaversPerishedLocKey = "Bot.BeaversPerished";

		// Token: 0x0400001D RID: 29
		public static readonly string AllPerishedLocKey = "Population.AllPerished";

		// Token: 0x0400001E RID: 30
		public static readonly string ScienceLocKey = "TopBar.Science";

		// Token: 0x0400001F RID: 31
		public static readonly string OpenWellbeingBoxKey = "OpenWellbeingBox";

		// Token: 0x04000020 RID: 32
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000021 RID: 33
		public readonly PopulationWellbeingBox _populationWellbeingBox;

		// Token: 0x04000022 RID: 34
		public readonly FactionService _factionService;

		// Token: 0x04000023 RID: 35
		public readonly ScienceService _scienceService;

		// Token: 0x04000024 RID: 36
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000025 RID: 37
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x04000026 RID: 38
		public readonly PopulationService _populationService;

		// Token: 0x04000027 RID: 39
		public readonly ILoc _loc;

		// Token: 0x04000028 RID: 40
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public BindableButton _wellbeingButton;
	}
}
