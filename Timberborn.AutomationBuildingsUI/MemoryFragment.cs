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
	// Token: 0x02000016 RID: 22
	public class MemoryFragment : IEntityPanelFragment
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003B7B File Offset: 0x00001D7B
		public MemoryFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, EnumDropdownProviderFactory enumDropdownProviderFactory, TransmitterSelectorInitializer transmitterSelectorInitializer, MemoryModeDescriptions memoryModeDescriptions)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._transmitterSelectorInitializer = transmitterSelectorInitializer;
			this._memoryModeDescriptions = memoryModeDescriptions;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/MemoryFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<MemoryMode>(() => this._memory.Mode, delegate(MemoryMode relayMode)
			{
				this._memory.SetMode(relayMode);
			}, MemoryFragment.RelayModeLocKeyPrefix);
			this._inputASelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "InputA", null);
			this._transmitterSelectorInitializer.Initialize(this._inputASelector, () => this._memory.InputA, delegate(Automator automator)
			{
				this._memory.SetInputA(automator);
			});
			this._inputBSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "InputB", null);
			this._transmitterSelectorInitializer.Initialize(this._inputBSelector, () => this._memory.InputB, delegate(Automator automator)
			{
				this._memory.SetInputB(automator);
			});
			this._resetInputSelector = UQueryExtensions.Q<TransmitterSelector>(this._root, "ResetInput", null);
			this._transmitterSelectorInitializer.InitializeOptional(this._resetInputSelector, () => this._memory.ResetInput, delegate(Automator automator)
			{
				this._memory.SetResetInput(automator);
			});
			this._modeDescription = UQueryExtensions.Q<Label>(this._root, "ModeDescription", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003CFC File Offset: 0x00001EFC
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Memory>(out this._memory))
			{
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
				this._inputASelector.Show(this._memory);
				this._inputBSelector.Show(this._memory);
				this._resetInputSelector.Show(this._memory);
				this._showInputB = this._memory.UsesInputB;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003D80 File Offset: 0x00001F80
		public void UpdateFragment()
		{
			if (this._memory)
			{
				this._inputASelector.UpdateStateIcon();
				if (this._showInputB != this._memory.UsesInputB)
				{
					this._showInputB = this._memory.UsesInputB;
					if (this._showInputB)
					{
						if (this._lastInputB)
						{
							this._memory.SetInputB(this._lastInputB);
						}
						this._inputBSelector.UpdateSelectedValue();
					}
				}
				if (this._showInputB)
				{
					this._inputBSelector.ToggleDisplayStyle(true);
					this._inputBSelector.UpdateStateIcon();
					this._lastInputB = this._memory.InputB;
				}
				else
				{
					this._inputBSelector.ToggleDisplayStyle(false);
				}
				this._resetInputSelector.UpdateStateIcon();
				this._modeDescription.text = this._memoryModeDescriptions.GetDescription(this._memory.Mode);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E68 File Offset: 0x00002068
		public void ClearFragment()
		{
			this._memory = null;
			this._lastInputB = null;
			this._modeDropdown.ClearItems();
			this._inputASelector.ClearItems();
			this._inputBSelector.ClearItems();
			this._resetInputSelector.ClearItems();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000089 RID: 137
		public static readonly string RelayModeLocKeyPrefix = "Building.Memory.Mode.";

		// Token: 0x0400008A RID: 138
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400008B RID: 139
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400008C RID: 140
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x0400008D RID: 141
		public readonly TransmitterSelectorInitializer _transmitterSelectorInitializer;

		// Token: 0x0400008E RID: 142
		public readonly MemoryModeDescriptions _memoryModeDescriptions;

		// Token: 0x0400008F RID: 143
		public EnumDropdownProvider<MemoryMode> _modeDropdownProvider;

		// Token: 0x04000090 RID: 144
		public TransmitterSelector _inputASelector;

		// Token: 0x04000091 RID: 145
		public TransmitterSelector _inputBSelector;

		// Token: 0x04000092 RID: 146
		public TransmitterSelector _resetInputSelector;

		// Token: 0x04000093 RID: 147
		public VisualElement _root;

		// Token: 0x04000094 RID: 148
		public Dropdown _modeDropdown;

		// Token: 0x04000095 RID: 149
		public Label _modeDescription;

		// Token: 0x04000096 RID: 150
		public Memory _memory;

		// Token: 0x04000097 RID: 151
		public bool _showInputB;

		// Token: 0x04000098 RID: 152
		public Automator _lastInputB;
	}
}
