using System;
using Timberborn.Automation;
using Timberborn.AutomationBuildings;
using Timberborn.AutomationUI;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000024 RID: 36
	public class RelayFragment : IEntityPanelFragment
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000054BD File Offset: 0x000036BD
		public RelayFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, EnumDropdownProviderFactory enumDropdownProviderFactory, TransmitterSelectorInitializer transmitterSelectorInitializer, RelayModeDescriptions relayModeDescriptions)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._transmitterSelectorInitializer = transmitterSelectorInitializer;
			this._relayModeDescriptions = relayModeDescriptions;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000054EC File Offset: 0x000036EC
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/RelayFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<RelayMode>(() => this._relay.Mode, delegate(RelayMode relayMode)
			{
				this._relay.SetMode(relayMode);
			}, RelayFragment.RelayModeLocKeyPrefix);
			this._inputASelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "InputA", null);
			this._transmitterSelectorInitializer.Initialize(this._inputASelector, () => this._relay.InputA, delegate(Automator automator)
			{
				this._relay.SetInputA(automator);
			});
			this._inputBSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "InputB", null);
			this._transmitterSelectorInitializer.Initialize(this._inputBSelector, () => this._relay.InputB, delegate(Automator automator)
			{
				this._relay.SetInputB(automator);
			});
			this._modeDescription = UQueryExtensions.Q<Label>(this._root, "ModeDescription", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005600 File Offset: 0x00003800
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Relay>(out this._relay))
			{
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
				this._inputASelector.Show(this._relay);
				this._inputBSelector.Show(this._relay);
				this._showInputB = this._relay.UsesInputB;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005674 File Offset: 0x00003874
		public void UpdateFragment()
		{
			if (this._relay)
			{
				this._inputASelector.UpdateStateIcon();
				if (this._showInputB != this._relay.UsesInputB)
				{
					this._showInputB = this._relay.UsesInputB;
					if (this._showInputB)
					{
						if (this._lastInputB)
						{
							this._relay.SetInputB(this._lastInputB);
						}
						this._inputBSelector.UpdateSelectedValue();
					}
				}
				if (this._showInputB)
				{
					this._inputBSelector.ToggleDisplayStyle(true);
					this._inputBSelector.UpdateStateIcon();
					this._lastInputB = this._relay.InputB;
				}
				else
				{
					this._inputBSelector.ToggleDisplayStyle(false);
				}
				this._modeDescription.text = this._relayModeDescriptions.GetDescription(this._relay.Mode);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005750 File Offset: 0x00003950
		public void ClearFragment()
		{
			this._relay = null;
			this._lastInputB = null;
			this._modeDropdown.ClearItems();
			this._inputASelector.ClearItems();
			this._inputBSelector.ClearItems();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x040000F8 RID: 248
		public static readonly string RelayModeLocKeyPrefix = "Building.Relay.Mode.";

		// Token: 0x040000F9 RID: 249
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000FA RID: 250
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x040000FB RID: 251
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x040000FC RID: 252
		public readonly TransmitterSelectorInitializer _transmitterSelectorInitializer;

		// Token: 0x040000FD RID: 253
		public readonly RelayModeDescriptions _relayModeDescriptions;

		// Token: 0x040000FE RID: 254
		public EnumDropdownProvider<RelayMode> _modeDropdownProvider;

		// Token: 0x040000FF RID: 255
		public TransmitterSelector _inputASelector;

		// Token: 0x04000100 RID: 256
		public TransmitterSelector _inputBSelector;

		// Token: 0x04000101 RID: 257
		public VisualElement _root;

		// Token: 0x04000102 RID: 258
		public Dropdown _modeDropdown;

		// Token: 0x04000103 RID: 259
		public Label _modeDescription;

		// Token: 0x04000104 RID: 260
		public Relay _relay;

		// Token: 0x04000105 RID: 261
		public bool _showInputB;

		// Token: 0x04000106 RID: 262
		public Automator _lastInputB;
	}
}
