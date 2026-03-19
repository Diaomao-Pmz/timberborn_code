using System;
using Timberborn.Automation;
using Timberborn.AutomationBuildings;
using Timberborn.AutomationUI;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.TimeSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200002E RID: 46
	public class TimerFragment : IEntityPanelFragment
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00006500 File Offset: 0x00004700
		public TimerFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, EnumDropdownProviderFactory enumDropdownProviderFactory, TransmitterSelectorInitializer transmitterSelectorInitializer, TimerIntervalElement timerIntervalAElement, TimerIntervalElement timerIntervalBElement, TimerModeDescriptions timerModeDescriptions, ILoc loc, IDayNightCycle dayNightCycle)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._transmitterSelectorInitializer = transmitterSelectorInitializer;
			this._timerIntervalAElement = timerIntervalAElement;
			this._timerIntervalBElement = timerIntervalBElement;
			this._timerModeDescriptions = timerModeDescriptions;
			this._loc = loc2;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000065C0 File Offset: 0x000047C0
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/TimerFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<TimerMode>(() => this._timer.Mode, delegate(TimerMode mode)
			{
				this._timer.SetMode(mode);
			}, TimerFragment.ModeLocKey);
			this._inputSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "Input", null);
			this._transmitterSelectorInitializer.Initialize(this._inputSelector, () => this._timer.Input, delegate(Automator automator)
			{
				this._timer.SetInput(automator);
			});
			this._resetInputSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "ResetInput", null);
			this._transmitterSelectorInitializer.InitializeOptional(this._resetInputSelector, () => this._timer.ResetInput, delegate(Automator automator)
			{
				this._timer.SetResetInput(automator);
			});
			this._timerIntervalAElement.Initialize(UQueryExtensions.Q<VisualElement>(this._root, "TimerIntervalA", null));
			this._timerIntervalBElement.Initialize(UQueryExtensions.Q<VisualElement>(this._root, "TimerIntervalB", null));
			this._modeDescription = UQueryExtensions.Q<Label>(this._root, "ModeDescription", null);
			this._timerProgressBar = UQueryExtensions.Q<ProgressBar>(this._root, "TimerProgressBar", null);
			this._timerProgressLabel = UQueryExtensions.Q<Label>(this._root, "TimerProgressLabel", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000673C File Offset: 0x0000493C
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Timer>(out this._timer))
			{
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
				this._inputSelector.Show(this._timer);
				this._resetInputSelector.Show(this._timer);
				this._timerIntervalAElement.Show(this._timer.TimerIntervalA);
				this._timerIntervalBElement.Show(this._timer.TimerIntervalB);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000067C8 File Offset: 0x000049C8
		public void UpdateFragment()
		{
			if (this._timer)
			{
				this._inputSelector.UpdateStateIcon();
				this._resetInputSelector.UpdateStateIcon();
				this._timerIntervalAElement.Update();
				if (this._timer.UsesIntervalB)
				{
					this._timerIntervalBElement.Update();
					this._timerIntervalBElement.SetDisplayStyle(true);
				}
				else
				{
					this._timerIntervalBElement.SetDisplayStyle(false);
				}
				this.UpdateTimerProgress();
				this._modeDescription.text = this._timerModeDescriptions.GetDescription(this._timer.Mode);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000685C File Offset: 0x00004A5C
		public void ClearFragment()
		{
			this._modeDropdown.ClearItems();
			this._inputSelector.ClearItems();
			this._resetInputSelector.ClearItems();
			this._timerIntervalAElement.Clear();
			this._timerIntervalBElement.Clear();
			this._timer = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000068B4 File Offset: 0x00004AB4
		public void UpdateTimerProgress()
		{
			bool flag;
			float progress = this._timer.GetProgress(out flag);
			float progress2 = flag ? (1f - progress) : progress;
			this._timerProgressBar.SetProgress(progress2);
			this._timerProgressBar.EnableInClassList(TimerFragment.ProgressFlippedClass, flag);
			int ticksLeft = this._timer.GetTicksLeft();
			if (this._timer.IsUsingTicks())
			{
				this._timerProgressLabel.text = this._loc.T<int>(this._ticksPhrase, ticksLeft);
				return;
			}
			float num = this._dayNightCycle.TicksToHours(ticksLeft);
			this._timerProgressLabel.text = ((num > 24f) ? this._loc.T<float>(this._daysShortPhrase, num / 24f) : this._loc.T<float>(this._hoursShortPhrase, num));
		}

		// Token: 0x0400014D RID: 333
		public static readonly string ModeLocKey = "Building.Timer.Mode.";

		// Token: 0x0400014E RID: 334
		public static readonly string ProgressFlippedClass = "timer-fragment__timer-progress--flipped";

		// Token: 0x0400014F RID: 335
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000150 RID: 336
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000151 RID: 337
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x04000152 RID: 338
		public readonly TransmitterSelectorInitializer _transmitterSelectorInitializer;

		// Token: 0x04000153 RID: 339
		public readonly TimerIntervalElement _timerIntervalAElement;

		// Token: 0x04000154 RID: 340
		public readonly TimerIntervalElement _timerIntervalBElement;

		// Token: 0x04000155 RID: 341
		public readonly TimerModeDescriptions _timerModeDescriptions;

		// Token: 0x04000156 RID: 342
		public readonly ILoc _loc;

		// Token: 0x04000157 RID: 343
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000158 RID: 344
		public readonly Phrase _ticksPhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatTicks));

		// Token: 0x04000159 RID: 345
		public readonly Phrase _hoursShortPhrase = Phrase.New().Format<float>((float value, ILoc loc) => UnitFormatter.FormatHours(string.Format("{0:F1}", value), loc));

		// Token: 0x0400015A RID: 346
		public readonly Phrase _daysShortPhrase = Phrase.New().Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDays));

		// Token: 0x0400015B RID: 347
		public VisualElement _root;

		// Token: 0x0400015C RID: 348
		public Dropdown _modeDropdown;

		// Token: 0x0400015D RID: 349
		public EnumDropdownProvider<TimerMode> _modeDropdownProvider;

		// Token: 0x0400015E RID: 350
		public TransmitterSelector _inputSelector;

		// Token: 0x0400015F RID: 351
		public TransmitterSelector _resetInputSelector;

		// Token: 0x04000160 RID: 352
		public Label _modeDescription;

		// Token: 0x04000161 RID: 353
		public ProgressBar _timerProgressBar;

		// Token: 0x04000162 RID: 354
		public Label _timerProgressLabel;

		// Token: 0x04000163 RID: 355
		public Timer _timer;
	}
}
