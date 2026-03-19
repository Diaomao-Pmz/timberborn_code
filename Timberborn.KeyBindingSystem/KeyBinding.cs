using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000016 RID: 22
	public class KeyBinding
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003370 File Offset: 0x00001570
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003378 File Offset: 0x00001578
		public bool IsDown { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003381 File Offset: 0x00001581
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003389 File Offset: 0x00001589
		public bool IsHeld { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003392 File Offset: 0x00001592
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000339A File Offset: 0x0000159A
		public bool IsLongHeld { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033A3 File Offset: 0x000015A3
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000033AB File Offset: 0x000015AB
		public bool IsUp { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000033B4 File Offset: 0x000015B4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000033BC File Offset: 0x000015BC
		public bool IsUpAfterShortHeld { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033C5 File Offset: 0x000015C5
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000033CD File Offset: 0x000015CD
		public string DisplayName { get; private set; }

		// Token: 0x06000085 RID: 133 RVA: 0x000033D6 File Offset: 0x000015D6
		public KeyBinding(string displayName, KeyBindingDefinition keyBindingDefinition, bool isHidden)
		{
			this.DisplayName = displayName;
			this._keyBindingDefinition = keyBindingDefinition;
			this._isHidden = isHidden;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000033F3 File Offset: 0x000015F3
		public InputBinding PrimaryInputBinding
		{
			get
			{
				return this._keyBindingDefinition.PrimaryInputBinding;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003400 File Offset: 0x00001600
		public InputBinding SecondaryInputBinding
		{
			get
			{
				return this._keyBindingDefinition.SecondaryInputBinding;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000340D File Offset: 0x0000160D
		public string Id
		{
			get
			{
				return this._keyBindingDefinition.KeyBindingSpec.Id;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000341F File Offset: 0x0000161F
		public string GroupId
		{
			get
			{
				return this._keyBindingDefinition.KeyBindingSpec.GroupId;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003431 File Offset: 0x00001631
		public bool DevModeOnly
		{
			get
			{
				return this._keyBindingDefinition.KeyBindingSpec.DevModeOnly;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003443 File Offset: 0x00001643
		public void Lock()
		{
			this.UpdateUnpressedState();
			this._isLocked = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003452 File Offset: 0x00001652
		public float GetRawValue()
		{
			if (Application.isFocused && this._inputModifiers == InputModifiers.None)
			{
				return Mathf.Max(this.PrimaryInputBinding.GetRawValue(), this.SecondaryInputBinding.GetRawValue());
			}
			return 0f;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003484 File Offset: 0x00001684
		public void UpdateKeyState(InputModifiers inputModifiers)
		{
			this._inputModifiers = inputModifiers;
			if (Application.isFocused && this.IsPressed(inputModifiers))
			{
				this.UpdatePressedState();
				return;
			}
			this.UpdateUnpressedState();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000034AA File Offset: 0x000016AA
		public void UpdateEventState(InputEventPtr inputEvent, InputControl changedControl, InputModifiers inputModifiers)
		{
			this._inputModifiers = inputModifiers;
			if (Application.isFocused && this.IsUsingInput(changedControl) && this.WasPressedInEvent(inputEvent, inputModifiers))
			{
				this.UpdatePressedState();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034D3 File Offset: 0x000016D3
		public bool IsUsingBinding(CustomInputBinding customInputBinding)
		{
			return !this._isHidden && (customInputBinding.IsSame(this.PrimaryInputBinding.InputBindingSpec) || customInputBinding.IsSame(this.SecondaryInputBinding.InputBindingSpec));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003505 File Offset: 0x00001705
		public void Flush()
		{
			if (this.IsDown)
			{
				this.UpdatePressedState();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003515 File Offset: 0x00001715
		public bool IsPressed(InputModifiers inputModifiers)
		{
			return this.PrimaryInputBinding.IsPressed(inputModifiers) || this.SecondaryInputBinding.IsPressed(inputModifiers);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003533 File Offset: 0x00001733
		public bool IsUsingInput(InputControl inputControl)
		{
			return this.PrimaryInputBinding.InputControl == inputControl || this.SecondaryInputBinding.InputControl == inputControl;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003553 File Offset: 0x00001753
		public bool WasPressedInEvent(InputEventPtr inputEvent, InputModifiers inputModifiers)
		{
			return this.PrimaryInputBinding.WasPressedInEvent(inputEvent, inputModifiers) || this.SecondaryInputBinding.WasPressedInEvent(inputEvent, inputModifiers);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003574 File Offset: 0x00001774
		public void UpdatePressedState()
		{
			if (!this._isLocked)
			{
				if (this.IsHeld)
				{
					this.IsDown = false;
				}
				else
				{
					this.IsHeld = true;
					this.IsDown = true;
					this._holdingStartTime = Time.unscaledTime;
				}
				this.IsLongHeld = (Time.unscaledTime - this._holdingStartTime > KeyBinding.LongHeldThreshold);
				this.IsUp = false;
				this.IsUpAfterShortHeld = false;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000035DC File Offset: 0x000017DC
		public void UpdateUnpressedState()
		{
			this.IsUp = this.IsHeld;
			if (this.IsUp)
			{
				this.IsUpAfterShortHeld = !this.IsLongHeld;
			}
			else
			{
				this.IsUpAfterShortHeld = false;
			}
			this.IsDown = false;
			this.IsHeld = false;
			this.IsLongHeld = false;
			this._isLocked = false;
		}

		// Token: 0x0400003B RID: 59
		public static readonly float LongHeldThreshold = 0.2f;

		// Token: 0x04000042 RID: 66
		public readonly KeyBindingDefinition _keyBindingDefinition;

		// Token: 0x04000043 RID: 67
		public readonly bool _isHidden;

		// Token: 0x04000044 RID: 68
		public float _holdingStartTime;

		// Token: 0x04000045 RID: 69
		public bool _isLocked;

		// Token: 0x04000046 RID: 70
		public InputModifiers _inputModifiers;
	}
}
