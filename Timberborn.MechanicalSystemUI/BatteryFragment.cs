using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200000A RID: 10
	public class BatteryFragment : IEntityPanelFragment
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002268 File Offset: 0x00000468
		public BatteryFragment(VisualElementLoader visualElementLoader, ILoc loc, DevModeManager devModeManager)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._devModeManager = devModeManager;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D8 File Offset: 0x000004D8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/BatteryFragment");
			this._chargeLabel = UQueryExtensions.Q<Label>(this._root, "Charge", null);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._chargeSlider = UQueryExtensions.Q<Slider>(this._root, "ChargeSlider", null);
			this._chargeSlider.lowValue = 0f;
			this._chargeSlider.highValue = 1f;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._chargeSlider, new EventCallback<ChangeEvent<float>>(this.ChangeCharge));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000238A File Offset: 0x0000058A
		public void ShowFragment(BaseComponent entity)
		{
			this._mechanicalNode = entity.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002398 File Offset: 0x00000598
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._mechanicalNode = null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B0 File Offset: 0x000005B0
		public void UpdateFragment()
		{
			MechanicalNode mechanicalNode = this._mechanicalNode;
			if (mechanicalNode == null || !mechanicalNode.IsBattery || !mechanicalNode.Active)
			{
				this._root.ToggleDisplayStyle(false);
				return;
			}
			this._chargeLabel.text = this._loc.T<int, int>(this._chargePhrase, this._mechanicalNode.NominalBatteryCharge, this._mechanicalNode.NominalBatteryCapacity);
			this._progressBar.SetProgress(this._mechanicalNode.NominalBatteryChargeLevel);
			this._root.ToggleDisplayStyle(true);
			if (this._devModeManager.Enabled)
			{
				this._chargeSlider.SetValueWithoutNotify(this._mechanicalNode.NominalBatteryChargeLevel);
				this._chargeSlider.ToggleDisplayStyle(true);
				return;
			}
			this._chargeSlider.ToggleDisplayStyle(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000247D File Offset: 0x0000067D
		public void ChangeCharge(ChangeEvent<float> changeEvent)
		{
			this._mechanicalNode.Battery.ModifyCharge(float.MinValue);
			this._mechanicalNode.Battery.ModifyCharge(changeEvent.newValue * (float)this._mechanicalNode.NominalBatteryCapacity);
		}

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000015 RID: 21
		public VisualElement _root;

		// Token: 0x04000016 RID: 22
		public Label _chargeLabel;

		// Token: 0x04000017 RID: 23
		public ProgressBar _progressBar;

		// Token: 0x04000018 RID: 24
		public Slider _chargeSlider;

		// Token: 0x04000019 RID: 25
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400001A RID: 26
		public readonly Phrase _chargePhrase = Phrase.New("Mechanical.BatteryCharge").Format<int>((int value) => value.ToString()).Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPowerCapacity));
	}
}
