using System;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.GameFactionSystem;
using Timberborn.GameSound;
using Timberborn.GameWonderCompletion;
using Timberborn.Localization;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystem;
using Timberborn.MapThumbnail;
using Timberborn.SettlementStatistics;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameWonderCompletionUI
{
	// Token: 0x02000007 RID: 7
	public class WonderCompletionPanel : IPanelController, ILoadableSingleton, IPanelBlocker
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022CC File Offset: 0x000004CC
		public WonderCompletionPanel(MapNameService mapNameService, MapThumbnailCache mapThumbnailCache, FactionService factionService, VisualElementLoader visualElementLoader, PanelStack panelStack, EventBus eventBus, StatisticItemFactory statisticItemFactory, IncrementalStatisticCollector incrementalStatisticCollector, MapItemProvider mapItemProvider, GameWonderCompletionService gameWonderCompletionService, ILoc loc, DelayedButtonEnabler delayedButtonEnabler, GameUISoundController gameUISoundController)
		{
			this._mapNameService = mapNameService;
			this._mapThumbnailCache = mapThumbnailCache;
			this._factionService = factionService;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._eventBus = eventBus;
			this._statisticItemFactory = statisticItemFactory;
			this._mapItemProvider = mapItemProvider;
			this._gameWonderCompletionService = gameWonderCompletionService;
			this._loc = loc;
			this._delayedButtonEnabler = delayedButtonEnabler;
			this._gameUISoundController = gameUISoundController;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000233C File Offset: 0x0000053C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/WonderCompletion/WonderCompletionPanel");
			UQueryExtensions.Q<Button>(this._root, "ResumeButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._panelStack.Pop(this);
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).ToggleDisplayStyle(false);
			this._mapMasteryLabel = UQueryExtensions.Q<Label>(this._root, "MapMasteryInfo", null);
			this._mapPanel = UQueryExtensions.Q<VisualElement>(this._root, "MapPanel", null);
			this._thumbnail = UQueryExtensions.Q<VisualElement>(this._root, "ThumbnailImage", null);
			this._flexibleStartRoot = UQueryExtensions.Q<VisualElement>(this._root, "FlexibleStartRoot", null);
			this._mapMasteryFactionIcon = UQueryExtensions.Q<VisualElement>(this._root, "MapMasteryFactionIcon", null);
			this._statisticsContainer = UQueryExtensions.Q<VisualElement>(this._root, "StatisticsContainer", null);
			this._resumeButton = UQueryExtensions.Q<Button>(this._root, "ResumeButton", null);
			this._eventBus.Register(this);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002446 File Offset: 0x00000646
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000244E File Offset: 0x0000064E
		[OnEvent]
		public void OnWonderCompleted(WonderCompletedEvent wonderCompletedEvent)
		{
			this.Show();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002456 File Offset: 0x00000656
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002459 File Offset: 0x00000659
		public void OnUICancelled()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000245B File Offset: 0x0000065B
		public void Show()
		{
			this.ShowMainSection();
			this.ShowMapPanel();
			this.ShowStatistics();
			this._panelStack.PushOverlay(this);
			this._delayedButtonEnabler.Add(this._resumeButton);
			this._gameUISoundController.PlayWonderCongratulationSound();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002498 File Offset: 0x00000698
		public void ShowMainSection()
		{
			FactionWonderSpec spec = this._factionService.Current.GetSpec<FactionWonderSpec>();
			UQueryExtensions.Q<Label>(this._root, "Flavor", null).text = spec.WonderCompletionFlavor.Value;
			UQueryExtensions.Q<Label>(this._root, "Congratulations", null).text = spec.WonderCompletionMessage.Value;
			Sprite asset = spec.WonderCompletionImage.Asset;
			UQueryExtensions.Q<Image>(this._root, "WonderCompletionImage", null).style.backgroundImage = new StyleBackground(asset);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002528 File Offset: 0x00000728
		public void ShowMapPanel()
		{
			bool wasCompletedFirstTimeForMap = this._gameWonderCompletionService.WasCompletedFirstTimeForMap;
			bool wasCompletedFirstTimeForFaction = this._gameWonderCompletionService.WasCompletedFirstTimeForFaction;
			if (wasCompletedFirstTimeForMap || wasCompletedFirstTimeForFaction)
			{
				this._mapPanel.ToggleDisplayStyle(true);
				UQueryExtensions.Q<Label>(this._root, "MapName", null).text = this._mapNameService.Name;
				this._thumbnail.style.backgroundImage = this.GetMapThumbnail();
				this._flexibleStartRoot.ToggleDisplayStyle(wasCompletedFirstTimeForMap);
				Sprite asset = this._factionService.Current.Logo.Asset;
				this._mapMasteryFactionIcon.style.backgroundImage = new StyleBackground(asset);
				this._mapMasteryLabel.text = this._loc.T<string>(WonderCompletionPanel.WonderCompletedLocKey, this._factionService.Current.DisplayName.Value);
				return;
			}
			this._mapPanel.ToggleDisplayStyle(false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002614 File Offset: 0x00000814
		public void ShowStatistics()
		{
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.DaysPassed));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.BeaversBorn));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.WaterConsumed));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.TailsPainted));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.TreesCut));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.ChippedTeeth));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.BotsManufactured));
			this._statisticsContainer.Add(this._statisticItemFactory.Create(StatisticIds.DynamiteDetonated));
			this._statisticsContainer.Add(this._statisticItemFactory.CreateIfHasValue(StatisticIds.BeaversExploded));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002714 File Offset: 0x00000914
		public Texture2D GetMapThumbnail()
		{
			MapFileReference? mapFileReference = this._mapNameService.IsResource ? new MapFileReference?(MapFileReference.FromResource(this._mapNameService.Name)) : this.GetCustom();
			if (mapFileReference == null)
			{
				return null;
			}
			return this._mapThumbnailCache.GetThumbnail(mapFileReference.Value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000276C File Offset: 0x0000096C
		public MapFileReference? GetCustom()
		{
			MapItem mapItem2 = this._mapItemProvider.GetCustomMaps().LastOrDefault((MapItem mapItem) => mapItem.MapFileReference.Name == this._mapNameService.Name);
			if (mapItem2 == null)
			{
				return null;
			}
			return new MapFileReference?(mapItem2.MapFileReference);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string WonderCompletedLocKey = "WonderCompletion.WonderCompleted";

		// Token: 0x0400000E RID: 14
		public readonly MapNameService _mapNameService;

		// Token: 0x0400000F RID: 15
		public readonly MapThumbnailCache _mapThumbnailCache;

		// Token: 0x04000010 RID: 16
		public readonly FactionService _factionService;

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly PanelStack _panelStack;

		// Token: 0x04000013 RID: 19
		public readonly EventBus _eventBus;

		// Token: 0x04000014 RID: 20
		public readonly StatisticItemFactory _statisticItemFactory;

		// Token: 0x04000015 RID: 21
		public readonly MapItemProvider _mapItemProvider;

		// Token: 0x04000016 RID: 22
		public readonly GameWonderCompletionService _gameWonderCompletionService;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;

		// Token: 0x04000018 RID: 24
		public readonly DelayedButtonEnabler _delayedButtonEnabler;

		// Token: 0x04000019 RID: 25
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x0400001A RID: 26
		public VisualElement _root;

		// Token: 0x0400001B RID: 27
		public VisualElement _mapPanel;

		// Token: 0x0400001C RID: 28
		public VisualElement _flexibleStartRoot;

		// Token: 0x0400001D RID: 29
		public VisualElement _thumbnail;

		// Token: 0x0400001E RID: 30
		public Label _mapMasteryLabel;

		// Token: 0x0400001F RID: 31
		public VisualElement _mapMasteryFactionIcon;

		// Token: 0x04000020 RID: 32
		public VisualElement _statisticsContainer;

		// Token: 0x04000021 RID: 33
		public Button _resumeButton;
	}
}
