using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000014 RID: 20
	public class SequentialTransmitterResetFragment : IEntityPanelFragment
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002ACB File Offset: 0x00000CCB
		public SequentialTransmitterResetFragment(VisualElementLoader visualElementLoader, AutomationResetter automationResetter)
		{
			this._visualElementLoader = visualElementLoader;
			this._automationResetter = automationResetter;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/ResettableEvaluatorFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Button>(this._root, "Reset", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnReset), 0);
			UQueryExtensions.Q<Button>(this._root, "ResetAll", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnResetAll), 0);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B61 File Offset: 0x00000D61
		public void ShowFragment(BaseComponent entity)
		{
			this._sequentialTransmitter = entity.GetComponent<ISequentialTransmitter>();
			if (this._sequentialTransmitter != null)
			{
				this._automator = entity.GetComponent<Automator>();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A19 File Offset: 0x00000C19
		public void UpdateFragment()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B8F File Offset: 0x00000D8F
		public void ClearFragment()
		{
			this._automator = null;
			this._sequentialTransmitter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BAB File Offset: 0x00000DAB
		public void OnReset(ClickEvent evt)
		{
			this._sequentialTransmitter.Reset();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void OnResetAll(ClickEvent evt)
		{
			this._automationResetter.ResetPartition(this._automator);
		}

		// Token: 0x04000032 RID: 50
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000033 RID: 51
		public readonly AutomationResetter _automationResetter;

		// Token: 0x04000034 RID: 52
		public VisualElement _root;

		// Token: 0x04000035 RID: 53
		public Automator _automator;

		// Token: 0x04000036 RID: 54
		public ISequentialTransmitter _sequentialTransmitter;
	}
}
