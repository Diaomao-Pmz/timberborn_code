using System;
using Timberborn.AlertPanelSystem;
using Timberborn.CoreUI;
using Timberborn.GameSound;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000020 RID: 32
	public class WellbeingHighscoreAlertFragment : IAlertFragment
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000046DD File Offset: 0x000028DD
		public WellbeingHighscoreAlertFragment(AlertPanelRowFactory alertPanelRowFactory, EventBus eventBus, GameUISoundController gameUISoundController, PopulationWellbeingBox populationWellbeingBox, ILoc loc)
		{
			this._alertPanelRowFactory = alertPanelRowFactory;
			this._eventBus = eventBus;
			this._gameUISoundController = gameUISoundController;
			this._populationWellbeingBox = populationWellbeingBox;
			this._loc = loc;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000470C File Offset: 0x0000290C
		public void InitializeAlertFragment(VisualElement root)
		{
			this._root = this._alertPanelRowFactory.CreateClosable("WellbeingHighscore");
			UQueryExtensions.Q<Button>(this._root, "Button", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClicked), 0);
			Button button = UQueryExtensions.Q<Button>(this._root, "Close", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Hide();
			}, 0);
			button.ToggleDisplayStyle(true);
			this._eventBus.Register(this);
			this.Hide();
			root.Add(this._root);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000479A File Offset: 0x0000299A
		public void UpdateAlertFragment()
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000479C File Offset: 0x0000299C
		[OnEvent]
		public void OnNewWellbeingHighscore(NewWellbeingHighscoreEvent newWellbeingHighscoreEvent)
		{
			int wellbeingHighscore = newWellbeingHighscoreEvent.WellbeingHighscore;
			UQueryExtensions.Q<Button>(this._root, "Button", null).text = string.Format("{0} {1}", this._loc.T(WellbeingHighscoreAlertFragment.HighscoreReachedLocKey), wellbeingHighscore);
			this._root.ToggleDisplayStyle(true);
			this._gameUISoundController.PlayWellbeingHighscoreSound();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000047FD File Offset: 0x000029FD
		public void OnClicked(ClickEvent evt)
		{
			this.Hide();
			this._populationWellbeingBox.ShowWellbeingHighscore();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004810 File Offset: 0x00002A10
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x040000A3 RID: 163
		public static readonly string HighscoreReachedLocKey = "Wellbeing.HighscoreReached.Short";

		// Token: 0x040000A4 RID: 164
		public readonly AlertPanelRowFactory _alertPanelRowFactory;

		// Token: 0x040000A5 RID: 165
		public readonly EventBus _eventBus;

		// Token: 0x040000A6 RID: 166
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x040000A7 RID: 167
		public readonly PopulationWellbeingBox _populationWellbeingBox;

		// Token: 0x040000A8 RID: 168
		public readonly ILoc _loc;

		// Token: 0x040000A9 RID: 169
		public VisualElement _root;
	}
}
