using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.PowerManagement;
using UnityEngine.UIElements;

namespace Timberborn.PowerManagementUI
{
	// Token: 0x02000004 RID: 4
	public class ClutchFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ClutchFragment(VisualElementLoader visualElementLoader, ILoc loc, ClutchModeToggleFactory clutchModeToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._clutchModeToggleFactory = clutchModeToggleFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ClutchFragment");
			this._modeToggle = this._clutchModeToggleFactory.Create(UQueryExtensions.Q<VisualElement>(this._root, "ModeToggle", null));
			this._modeLabel = UQueryExtensions.Q<Label>(this._root, "ModeLabel", null);
			this._automatedEngagedText = this.BuildAutomatedStateText(ClutchFragment.EngagedLocKey);
			this._automatedDisengagedText = this.BuildAutomatedStateText(ClutchFragment.DisengagedLocKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216C File Offset: 0x0000036C
		public void ShowFragment(BaseComponent entity)
		{
			this._clutch = entity.GetComponent<Clutch>();
			if (this._clutch)
			{
				this._modeToggle.Show(this._clutch);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021A4 File Offset: 0x000003A4
		public void ClearFragment()
		{
			this._clutch = null;
			this._modeToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C4 File Offset: 0x000003C4
		public void UpdateFragment()
		{
			if (this._clutch)
			{
				this.UpdateModeToggle();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D9 File Offset: 0x000003D9
		public string BuildAutomatedStateText(string stateLocKey)
		{
			return this._loc.T(ClutchFragment.AutomatedLocKey) + " (" + this._loc.T(stateLocKey) + ")";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002208 File Offset: 0x00000408
		public void UpdateModeToggle()
		{
			this._modeToggle.Update();
			Label modeLabel = this._modeLabel;
			string text;
			switch (this._clutch.Mode)
			{
			case ClutchMode.Engaged:
				text = this._loc.T(ClutchFragment.EngagedLocKey);
				break;
			case ClutchMode.Disengaged:
				text = this._loc.T(ClutchFragment.DisengagedLocKey);
				break;
			case ClutchMode.Automated:
				text = (this._clutch.IsEngaged ? this._automatedEngagedText : this._automatedDisengagedText);
				break;
			default:
				throw new ArgumentException(string.Format("Unexpected mode: {0}", this._clutch.Mode));
			}
			modeLabel.text = text;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string EngagedLocKey = "Building.Clutch.Mode.Engaged";

		// Token: 0x04000007 RID: 7
		public static readonly string DisengagedLocKey = "Building.Clutch.Mode.Disengaged";

		// Token: 0x04000008 RID: 8
		public static readonly string AutomatedLocKey = "Automation.Mode.Automated";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly ClutchModeToggleFactory _clutchModeToggleFactory;

		// Token: 0x0400000C RID: 12
		public VisualElement _root;

		// Token: 0x0400000D RID: 13
		public ClutchModeToggle _modeToggle;

		// Token: 0x0400000E RID: 14
		public Label _modeLabel;

		// Token: 0x0400000F RID: 15
		public string _automatedEngagedText;

		// Token: 0x04000010 RID: 16
		public string _automatedDisengagedText;

		// Token: 0x04000011 RID: 17
		public Clutch _clutch;
	}
}
