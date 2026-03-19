using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.BuilderPrioritySystemUI;
using Timberborn.CoreUI;
using Timberborn.Demolishing;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PrioritySystemUI;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DemolishingUI
{
	// Token: 0x02000007 RID: 7
	public class DemolishableFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		public DemolishableFragment(DemolishableScienceRewardLabelFactory demolishableScienceRewardLabelFactory, BuilderPriorityToggleGroupFactory builderPriorityToggleGroupFactory, VisualElementLoader visualElementLoader, ILoc loc, InputService inputService, ITooltipRegistrar tooltipRegistrar)
		{
			this._demolishableScienceRewardLabelFactory = demolishableScienceRewardLabelFactory;
			this._builderPriorityToggleGroupFactory = builderPriorityToggleGroupFactory;
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._inputService = inputService;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DemolishableFragment");
			this._buttonWrapper = UQueryExtensions.Q<VisualElement>(this._root, "ButtonWrapper", null);
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ChangeDemolishState();
			}, 0);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "PriorityWrapper", null);
			this._priorityToggleGroup = this._builderPriorityToggleGroupFactory.Create(parent, DemolishableFragment.PriorityLabelLocKey);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._progressLabel = UQueryExtensions.Q<Label>(this._root, "Progress", null);
			this._hidable = UQueryExtensions.Q<VisualElement>(this._root, "HidableWrapper", null);
			this._demolishableScienceRewardLabel = this._demolishableScienceRewardLabelFactory.Create();
			this._scienceRewardWrapper = UQueryExtensions.Q<VisualElement>(this._root, "ScienceRewardWrapper", null);
			this._scienceRewardWrapper.Add(this._demolishableScienceRewardLabel.Root);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000225C File Offset: 0x0000045C
		public void ShowFragment(BaseComponent entity)
		{
			Demolishable component = entity.GetComponent<Demolishable>();
			if (component && (component.IsMarked || component.ShowDemolishButtonInEntityPanel))
			{
				this._demolishable = component;
				this._root.ToggleDisplayStyle(true);
				this._priorityToggleGroup.Enable(entity.GetComponent<BuilderPrioritizable>());
				DemolishableScienceRewardSpec component2 = component.GetComponent<DemolishableScienceRewardSpec>();
				this._demolishableScienceRewardLabel.Show(component2);
				this._scienceRewardWrapper.ToggleDisplayStyle(component2 != null);
				if (this._demolishable.ShowDemolishButtonInEntityPanel)
				{
					this._inputService.AddInputProcessor(this);
				}
				this._buttonWrapper.ToggleDisplayStyle(this._demolishable.ShowDemolishButtonInEntityPanel);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002303 File Offset: 0x00000503
		public void ClearFragment()
		{
			this._demolishable = null;
			this._priorityToggleGroup.Disable();
			this._scienceRewardWrapper.ToggleDisplayStyle(false);
			this._root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000233C File Offset: 0x0000053C
		public void UpdateFragment()
		{
			if (this._demolishable)
			{
				string text = this._demolishable.IsMarked ? this._loc.T(DemolishableFragment.CancelLocKey) : this._loc.T(DemolishableFragment.MarkLocKey);
				this._button.text = text;
				this._tooltipRegistrar.RegisterWithKeyBinding(this._button, text, DemolishableFragment.UniqueBuildingActionKey);
				if (this._demolishable.IsMarked)
				{
					this._priorityToggleGroup.UpdateGroup();
					float num = Mathf.Clamp01(this._demolishable.DemolishingProgress);
					this._progressBar.SetProgress(num);
					this._progressLabel.text = NumberFormatter.FormatAsPercentCeiled((double)num);
					this._hidable.ToggleDisplayStyle(true);
					return;
				}
				this._hidable.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000240C File Offset: 0x0000060C
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(DemolishableFragment.UniqueBuildingActionKey))
			{
				this.ChangeDemolishState();
				return true;
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002429 File Offset: 0x00000629
		public void ChangeDemolishState()
		{
			if (this._demolishable.IsMarked)
			{
				this._demolishable.Unmark();
				return;
			}
			this._demolishable.Mark();
		}

		// Token: 0x04000008 RID: 8
		public static readonly string MarkLocKey = "Demolish.Mark";

		// Token: 0x04000009 RID: 9
		public static readonly string CancelLocKey = "Demolish.Cancel";

		// Token: 0x0400000A RID: 10
		public static readonly string PriorityLabelLocKey = "Demolish.PriorityTitle";

		// Token: 0x0400000B RID: 11
		public static readonly string UniqueBuildingActionKey = "UniqueBuildingAction";

		// Token: 0x0400000C RID: 12
		public readonly DemolishableScienceRewardLabelFactory _demolishableScienceRewardLabelFactory;

		// Token: 0x0400000D RID: 13
		public readonly BuilderPriorityToggleGroupFactory _builderPriorityToggleGroupFactory;

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public readonly InputService _inputService;

		// Token: 0x04000011 RID: 17
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000012 RID: 18
		public Demolishable _demolishable;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;

		// Token: 0x04000014 RID: 20
		public VisualElement _buttonWrapper;

		// Token: 0x04000015 RID: 21
		public Button _button;

		// Token: 0x04000016 RID: 22
		public PriorityToggleGroup _priorityToggleGroup;

		// Token: 0x04000017 RID: 23
		public ProgressBar _progressBar;

		// Token: 0x04000018 RID: 24
		public Label _progressLabel;

		// Token: 0x04000019 RID: 25
		public VisualElement _hidable;

		// Token: 0x0400001A RID: 26
		public VisualElement _scienceRewardWrapper;

		// Token: 0x0400001B RID: 27
		public DemolishableScienceRewardLabel _demolishableScienceRewardLabel;
	}
}
