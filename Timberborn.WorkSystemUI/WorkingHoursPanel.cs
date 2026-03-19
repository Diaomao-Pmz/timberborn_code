using System;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using Timberborn.WorkSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200000E RID: 14
	public class WorkingHoursPanel : ILoadableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002968 File Offset: 0x00000B68
		public WorkingHoursPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, WorkingHoursManager workingHoursManager, ILoc loc, ITooltipRegistrar tooltipRegistrar, BindableButtonFactory bindableButtonFactory, EventBus eventBus)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._workingHoursManager = workingHoursManager;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableButtonFactory = bindableButtonFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029CC File Offset: 0x00000BCC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/WorkingHoursPanel");
			this._workingHoursPanel = UQueryExtensions.Q<VisualElement>(this._root, "WorkingHours", null);
			this._tooltipRegistrar.RegisterLocalizable(this._root, WorkingHoursPanel.WorkingHoursTooltipLocKey);
			this._increaseHoursButton = this._bindableButtonFactory.CreateAndBind(UQueryExtensions.Q<Button>(this._root, "Plus", null), WorkingHoursPanel.IncreaseHoursKey, new Action(this.IncreaseHours));
			this._decreaseHoursButton = this._bindableButtonFactory.CreateAndBind(UQueryExtensions.Q<Button>(this._root, "Minus", null), WorkingHoursPanel.DecreaseHoursKey, new Action(this.DecreaseHours));
			this._title = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._hours = Mathf.CeilToInt(this._workingHoursManager.WorkedPartOfDay * 24f);
			this.UpdateTitle();
			this._eventBus.Register(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AC7 File Offset: 0x00000CC7
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, 3);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002ADB File Offset: 0x00000CDB
		public void TogglePanelHighlight(bool state)
		{
			this._workingHoursPanel.EnableInClassList(WorkingHoursPanel.HighlightClass, state);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AEE File Offset: 0x00000CEE
		public void IncreaseHours()
		{
			this._hours = Math.Min(24, this._hours + 1);
			this._decreaseHoursButton.Enable();
			if (this._hours == 24)
			{
				this._increaseHoursButton.Disable();
			}
			this.OnHoursChanged();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B2B File Offset: 0x00000D2B
		public void DecreaseHours()
		{
			this._hours = Math.Max(0, this._hours - 1);
			this._increaseHoursButton.Enable();
			if (this._hours == 0)
			{
				this._decreaseHoursButton.Disable();
			}
			this.OnHoursChanged();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B65 File Offset: 0x00000D65
		public void OnHoursChanged()
		{
			this._workingHoursManager.WorkedPartOfDay = (float)this._hours / 24f;
			this.UpdateTitle();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B85 File Offset: 0x00000D85
		public void UpdateTitle()
		{
			this._title.text = this._loc.T<int>(this._titlePhrase, this._hours);
		}

		// Token: 0x04000033 RID: 51
		public static readonly string WorkingHoursTooltipLocKey = "Work.WorkingHoursTooltip";

		// Token: 0x04000034 RID: 52
		public static readonly string IncreaseHoursKey = "IncreaseWorkingHours";

		// Token: 0x04000035 RID: 53
		public static readonly string DecreaseHoursKey = "DecreaseWorkingHours";

		// Token: 0x04000036 RID: 54
		public static readonly string HighlightClass = "highlight";

		// Token: 0x04000037 RID: 55
		public readonly UILayout _uiLayout;

		// Token: 0x04000038 RID: 56
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000039 RID: 57
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x0400003A RID: 58
		public readonly ILoc _loc;

		// Token: 0x0400003B RID: 59
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400003C RID: 60
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400003D RID: 61
		public readonly EventBus _eventBus;

		// Token: 0x0400003E RID: 62
		public BindableButton _increaseHoursButton;

		// Token: 0x0400003F RID: 63
		public BindableButton _decreaseHoursButton;

		// Token: 0x04000040 RID: 64
		public Label _title;

		// Token: 0x04000041 RID: 65
		public int _hours;

		// Token: 0x04000042 RID: 66
		public VisualElement _root;

		// Token: 0x04000043 RID: 67
		public VisualElement _workingHoursPanel;

		// Token: 0x04000044 RID: 68
		public readonly Phrase _titlePhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatHours));
	}
}
