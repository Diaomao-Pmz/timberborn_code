using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000010 RID: 16
	public class StreamGaugeFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003A94 File Offset: 0x00001C94
		public StreamGaugeFragment(VisualElementLoader visualElementLoader, ILoc loc, InputService inputService, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._inputService = inputService;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003B5C File Offset: 0x00001D5C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/StreamGaugeFragment");
			Button button = UQueryExtensions.Q<Button>(this._root, "ResetGreatestDepthButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._streamGauge.ResetHighestWaterLevel();
			}, 0);
			this._tooltipRegistrar.RegisterWithKeyBinding(button, this._loc.T(StreamGaugeFragment.ResetGreatestDepthLocKey), StreamGaugeFragment.UniqueBuildingActionKey);
			this._root.ToggleDisplayStyle(false);
			this._depthLabel = UQueryExtensions.Q<Label>(this._root, "DepthLabel", null);
			this._greatestDepthLabel = UQueryExtensions.Q<Label>(this._root, "GreatestDepthLabel", null);
			this._currentLabel = UQueryExtensions.Q<Label>(this._root, "CurrentLabel", null);
			this._contaminationLevelLabel = UQueryExtensions.Q<Label>(this._root, "ContaminationLabel", null);
			return this._root;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C33 File Offset: 0x00001E33
		public void ShowFragment(BaseComponent entity)
		{
			this._streamGauge = entity.GetComponent<StreamGauge>();
			if (this._streamGauge != null)
			{
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003C55 File Offset: 0x00001E55
		public void ClearFragment()
		{
			this._streamGauge = null;
			this._root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003C78 File Offset: 0x00001E78
		public void UpdateFragment()
		{
			if (this._streamGauge && this._streamGauge.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this._depthLabel.text = this._loc.T<float>(this._depthPhrase, this._streamGauge.WaterLevel);
				this._greatestDepthLabel.text = this._loc.T<float>(this._greatestDepthPhrase, this._streamGauge.HighestWaterLevel);
				this._currentLabel.text = this._loc.T<float>(this._currentPhrase, this._streamGauge.WaterCurrent);
				this._contaminationLevelLabel.text = this._loc.T<float>(this._contaminationLevelPhrase, this._streamGauge.ContaminationLevel);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003D5A File Offset: 0x00001F5A
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(StreamGaugeFragment.UniqueBuildingActionKey))
			{
				this._streamGauge.ResetHighestWaterLevel();
				return true;
			}
			return false;
		}

		// Token: 0x04000070 RID: 112
		public static readonly string UniqueBuildingActionKey = "UniqueBuildingAction";

		// Token: 0x04000071 RID: 113
		public static readonly string ResetGreatestDepthLocKey = "Building.StreamGauge.Reset";

		// Token: 0x04000072 RID: 114
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000073 RID: 115
		public readonly ILoc _loc;

		// Token: 0x04000074 RID: 116
		public readonly InputService _inputService;

		// Token: 0x04000075 RID: 117
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000076 RID: 118
		public Label _depthLabel;

		// Token: 0x04000077 RID: 119
		public Label _greatestDepthLabel;

		// Token: 0x04000078 RID: 120
		public Label _currentLabel;

		// Token: 0x04000079 RID: 121
		public Label _contaminationLevelLabel;

		// Token: 0x0400007A RID: 122
		public StreamGauge _streamGauge;

		// Token: 0x0400007B RID: 123
		public VisualElement _root;

		// Token: 0x0400007C RID: 124
		public readonly Phrase _depthPhrase = Phrase.New("Building.StreamGauge.Depth").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x0400007D RID: 125
		public readonly Phrase _greatestDepthPhrase = Phrase.New("Building.StreamGauge.GreatestDepth").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x0400007E RID: 126
		public readonly Phrase _currentPhrase = Phrase.New("Building.StreamGauge.Current").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatFlow));

		// Token: 0x0400007F RID: 127
		public readonly Phrase _contaminationLevelPhrase = Phrase.New("Building.StreamGauge.Contamination").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));
	}
}
