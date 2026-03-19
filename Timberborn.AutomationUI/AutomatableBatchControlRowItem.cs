using System;
using Timberborn.Automation;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000007 RID: 7
	public class AutomatableBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public VisualElement Root { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public AutomatableBatchControlRowItem(VisualElement root, Automatable automatable, AutomationStateIcon automationStateIcon)
		{
			this.Root = root;
			this._automatable = automatable;
			this._automationStateIcon = automationStateIcon;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		public static AutomatableBatchControlRowItem Create(VisualElement root, Automatable automatable, AutomationStateIcon automationStateIcon)
		{
			AutomatableBatchControlRowItem automatableBatchControlRowItem = new AutomatableBatchControlRowItem(root, automatable, automationStateIcon);
			automatable.InputStateChanged += automatableBatchControlRowItem.OnAutomatableInputStateChanged;
			automatableBatchControlRowItem.UpdateItemState();
			return automatableBatchControlRowItem;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002157 File Offset: 0x00000357
		public void ClearRowItem()
		{
			this._automatable.InputStateChanged -= this.OnAutomatableInputStateChanged;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		public void OnAutomatableInputStateChanged(object sender, EventArgs e)
		{
			this.UpdateItemState();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002178 File Offset: 0x00000378
		public void UpdateItemState()
		{
			if (this._automatable.IsAutomated)
			{
				this.Root.ToggleDisplayStyle(true);
				this._automationStateIcon.Update();
				return;
			}
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000009 RID: 9
		public readonly Automatable _automatable;

		// Token: 0x0400000A RID: 10
		public readonly AutomationStateIcon _automationStateIcon;
	}
}
