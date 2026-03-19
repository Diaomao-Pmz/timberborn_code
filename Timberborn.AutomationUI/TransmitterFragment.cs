using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000016 RID: 22
	public class TransmitterFragment : IEntityPanelFragment
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002D2B File Offset: 0x00000F2B
		public TransmitterFragment(VisualElementLoader visualElementLoader, AutomationStateIconBuilder automationStateIconBuilder, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._automationStateIconBuilder = automationStateIconBuilder;
			this._loc = loc;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D48 File Offset: 0x00000F48
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/TransmitterFragment");
			this._root.ToggleDisplayStyle(false);
			this._automationStateIcon = this._automationStateIconBuilder.Create(UQueryExtensions.Q<Image>(this._root, "StateIcon", null), () => this._automator).Build();
			this._stateLabel = UQueryExtensions.Q<Label>(this._root, "StateLabel", null);
			this._usagesLabel = UQueryExtensions.Q<Label>(this._root, "Usages", null);
			return this._root;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DDE File Offset: 0x00000FDE
		public void ShowFragment(BaseComponent entity)
		{
			this._automator = entity.GetComponent<Automator>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DEC File Offset: 0x00000FEC
		public void ClearFragment()
		{
			this._automator = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E04 File Offset: 0x00001004
		public void UpdateFragment()
		{
			if (this._automator && this._automator.IsTransmitter)
			{
				this._root.ToggleDisplayStyle(true);
				this._stateLabel.text = this.GetStateText();
				this._stateLabel.EnableInClassList(TransmitterFragment.StateLabelUnfinishedClass, !this._automator.Enabled);
				this._automationStateIcon.Update();
				this._usagesLabel.text = this._loc.T<int>(TransmitterFragment.UsagesLocKey, this._automator.Usages);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EA4 File Offset: 0x000010A4
		public string GetStateText()
		{
			string result;
			if (this._automator.IsProcessingNewInput)
			{
				result = this._loc.T(TransmitterFragment.StateProcessingLocKey);
			}
			else
			{
				string text;
				switch (this._automator.UnfinishedState)
				{
				case AutomatorState.Off:
					text = this._loc.T(TransmitterFragment.StateOffLocKey);
					break;
				case AutomatorState.On:
					text = this._loc.T(TransmitterFragment.StateOnLocKey);
					break;
				case AutomatorState.Error:
					text = this._loc.T(TransmitterFragment.StateErrorLocKey);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0400003F RID: 63
		public static readonly string StateOffLocKey = "Automation.State.Off";

		// Token: 0x04000040 RID: 64
		public static readonly string StateOnLocKey = "Automation.State.On";

		// Token: 0x04000041 RID: 65
		public static readonly string StateErrorLocKey = "Automation.State.Error";

		// Token: 0x04000042 RID: 66
		public static readonly string StateProcessingLocKey = "Automation.State.Processing";

		// Token: 0x04000043 RID: 67
		public static readonly string UsagesLocKey = "Automation.Usages";

		// Token: 0x04000044 RID: 68
		public static readonly string StateLabelUnfinishedClass = "transmitter-fragment__state-label--unfinished";

		// Token: 0x04000045 RID: 69
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000046 RID: 70
		public readonly AutomationStateIconBuilder _automationStateIconBuilder;

		// Token: 0x04000047 RID: 71
		public readonly ILoc _loc;

		// Token: 0x04000048 RID: 72
		public VisualElement _root;

		// Token: 0x04000049 RID: 73
		public AutomationStateIcon _automationStateIcon;

		// Token: 0x0400004A RID: 74
		public Label _stateLabel;

		// Token: 0x0400004B RID: 75
		public Label _usagesLabel;

		// Token: 0x0400004C RID: 76
		public Automator _automator;
	}
}
