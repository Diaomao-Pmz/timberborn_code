using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000014 RID: 20
	public class IndicatorFragment : IEntityPanelFragment
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003608 File Offset: 0x00001808
		public IndicatorFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003618 File Offset: 0x00001818
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/IndicatorFragment");
			this._pinnedWhenOnToggle = UQueryExtensions.Q<Toggle>(this._root, "PinnedWhenOn", null);
			this._pinnedAlwaysToggle = UQueryExtensions.Q<Toggle>(this._root, "PinnedAlways", null);
			this._warningToggle = UQueryExtensions.Q<Toggle>(this._root, "Warning", null);
			this._journalEntryToggle = UQueryExtensions.Q<Toggle>(this._root, "JournalEntry", null);
			this._colorReplicationToggle = UQueryExtensions.Q<Toggle>(this._root, "ColorReplication", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._pinnedWhenOnToggle, new EventCallback<ChangeEvent<bool>>(this.OnPinnedWhenOnChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._pinnedAlwaysToggle, new EventCallback<ChangeEvent<bool>>(this.OnPinnedAlwaysChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._warningToggle, new EventCallback<ChangeEvent<bool>>(this.OnWarningToggleChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._journalEntryToggle, new EventCallback<ChangeEvent<bool>>(this.OnJournalEntryToggleChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._colorReplicationToggle, new EventCallback<ChangeEvent<bool>>(this.OnColorReplicationToggleChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003738 File Offset: 0x00001938
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Indicator>(out this._indicator))
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003754 File Offset: 0x00001954
		public void UpdateFragment()
		{
			if (this._indicator)
			{
				this._pinnedWhenOnToggle.SetValueWithoutNotify(this._indicator.PinnedMode == IndicatorPinnedMode.WhenOn);
				this._pinnedAlwaysToggle.SetValueWithoutNotify(this._indicator.PinnedMode == IndicatorPinnedMode.Always);
				this._warningToggle.SetValueWithoutNotify(this._indicator.IsWarningEnabled);
				this._journalEntryToggle.SetValueWithoutNotify(this._indicator.IsJournalEntryEnabled);
				this._colorReplicationToggle.SetValueWithoutNotify(this._indicator.IsColorReplicationEnabled);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037E2 File Offset: 0x000019E2
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._indicator = null;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037F7 File Offset: 0x000019F7
		public void OnPinnedWhenOnChanged(ChangeEvent<bool> evt)
		{
			this._indicator.SetPinnedMode(evt.newValue ? IndicatorPinnedMode.WhenOn : IndicatorPinnedMode.Never);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003810 File Offset: 0x00001A10
		public void OnPinnedAlwaysChanged(ChangeEvent<bool> evt)
		{
			this._indicator.SetPinnedMode(evt.newValue ? IndicatorPinnedMode.Always : IndicatorPinnedMode.Never);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003829 File Offset: 0x00001A29
		public void OnWarningToggleChanged(ChangeEvent<bool> evt)
		{
			this._indicator.SetWarningEnabled(evt.newValue);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000383C File Offset: 0x00001A3C
		public void OnJournalEntryToggleChanged(ChangeEvent<bool> evt)
		{
			this._indicator.SetJournalEntryEnabled(evt.newValue);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000384F File Offset: 0x00001A4F
		public void OnColorReplicationToggleChanged(ChangeEvent<bool> evt)
		{
			this._indicator.SetColorReplicationEnabled(evt.newValue);
		}

		// Token: 0x04000075 RID: 117
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000076 RID: 118
		public VisualElement _root;

		// Token: 0x04000077 RID: 119
		public Toggle _pinnedWhenOnToggle;

		// Token: 0x04000078 RID: 120
		public Toggle _pinnedAlwaysToggle;

		// Token: 0x04000079 RID: 121
		public Toggle _warningToggle;

		// Token: 0x0400007A RID: 122
		public Toggle _journalEntryToggle;

		// Token: 0x0400007B RID: 123
		public Toggle _colorReplicationToggle;

		// Token: 0x0400007C RID: 124
		public Indicator _indicator;
	}
}
