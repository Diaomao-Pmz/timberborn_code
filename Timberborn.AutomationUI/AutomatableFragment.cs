using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200000A RID: 10
	public class AutomatableFragment : IEntityPanelFragment
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002248 File Offset: 0x00000448
		public AutomatableFragment(VisualElementLoader visualElementLoader, TransmitterSelectorInitializer transmitterSelectorInitializer)
		{
			this._visualElementLoader = visualElementLoader;
			this._transmitterSelectorInitializer = transmitterSelectorInitializer;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002260 File Offset: 0x00000460
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/AutomatableFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._inputSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "Input", null);
			this._transmitterSelectorInitializer.InitializeStandalone(this._inputSelector, new Func<Automator>(this.GetInput), new Action<Automator>(this.SetInput));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D7 File Offset: 0x000004D7
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Automatable>(out this._automatable))
			{
				this._inputSelector.Show(this._automatable);
				this._automatable.InputReconnected += this.OnInputReconnected;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
		public void UpdateFragment()
		{
			if (this._automatable != null)
			{
				if (this._automatable.IsNeeded())
				{
					this._root.ToggleDisplayStyle(true);
					this._inputSelector.UpdateStateIcon();
					this._inputSelector.EnableInClassList(AutomatableFragment.TransmitterSelectorNoneClass, !this._automatable.IsAutomated);
					return;
				}
				this._root.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002374 File Offset: 0x00000574
		public void ClearFragment()
		{
			if (this._automatable != null)
			{
				this._automatable.InputReconnected -= this.OnInputReconnected;
				this._automatable = null;
			}
			this._inputSelector.ClearItems();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023B3 File Offset: 0x000005B3
		public Automator GetInput()
		{
			return this._automatable.Input;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C0 File Offset: 0x000005C0
		public void SetInput(Automator automator)
		{
			this._automatable.SetInput(automator);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023CE File Offset: 0x000005CE
		public void OnInputReconnected(object sender, EventArgs e)
		{
			this._inputSelector.UpdateSelectedValue();
		}

		// Token: 0x0400000E RID: 14
		public static readonly string TransmitterSelectorNoneClass = "transmitter-selector--automatable-none";

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly TransmitterSelectorInitializer _transmitterSelectorInitializer;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;

		// Token: 0x04000012 RID: 18
		public TransmitterSelector _inputSelector;

		// Token: 0x04000013 RID: 19
		public Automatable _automatable;
	}
}
