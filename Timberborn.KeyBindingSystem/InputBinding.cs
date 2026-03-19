using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000D RID: 13
	public class InputBinding
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002508 File Offset: 0x00000708
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002510 File Offset: 0x00000710
		public InputControl InputControl { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002519 File Offset: 0x00000719
		public InputBindingSpec InputBindingSpec { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002521 File Offset: 0x00000721
		public string DefaultName { get; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002529 File Offset: 0x00000729
		public InputBinding(InputBindingSpec inputBindingSpec, string defaultName, bool allowOtherModifiers)
		{
			this.InputBindingSpec = inputBindingSpec;
			this.DefaultName = defaultName;
			this._allowOtherModifiers = allowOtherModifiers;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002551 File Offset: 0x00000751
		public static InputBinding Create(InputBindingSpec inputBindingSpec, string defaultName, bool allowOtherModifiers)
		{
			InputBinding inputBinding = new InputBinding(inputBindingSpec ?? new InputBindingSpec(), defaultName, allowOtherModifiers);
			inputBinding.SearchForInputControl();
			return inputBinding;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000256A File Offset: 0x0000076A
		public bool IsDefined
		{
			get
			{
				return this.InputBindingSpec.IsDefined;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002577 File Offset: 0x00000777
		public bool IsPressed(InputModifiers currentModifiers)
		{
			return this.CanBePressed(currentModifiers) && InputControlExtensions.IsPressed(this.InputControl, this._pressedButtonThreshold);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002595 File Offset: 0x00000795
		public bool WasPressedInEvent(InputEventPtr inputEvent, InputModifiers currentModifiers)
		{
			return this.CanBePressed(currentModifiers) && this.InputControl.WasPressedInEvent(inputEvent);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025AE File Offset: 0x000007AE
		public void DeviceRemoved(InputDevice device)
		{
			InputControl inputControl = this.InputControl;
			if (((inputControl != null) ? inputControl.device : null) == device)
			{
				this.InputControl = null;
			}
			this.SearchForInputControl();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025D2 File Offset: 0x000007D2
		public void DeviceAdded()
		{
			if (this.InputControl == null)
			{
				this.SearchForInputControl();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025E2 File Offset: 0x000007E2
		public bool HasModifier(InputModifiers modifier)
		{
			return (this.InputModifiers & modifier) == modifier;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025F0 File Offset: 0x000007F0
		public unsafe float GetRawValue()
		{
			InputControl<float> inputControl = this.InputControl as InputControl<float>;
			if (inputControl != null)
			{
				return *inputControl.value;
			}
			return 0f;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002619 File Offset: 0x00000819
		public InputModifiers InputModifiers
		{
			get
			{
				return this.InputBindingSpec.InputModifiers;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002626 File Offset: 0x00000826
		public void SearchForInputControl()
		{
			this.InputControl = (this.IsDefined ? InputSystem.FindControl(this.InputBindingSpec.Path) : null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002649 File Offset: 0x00000849
		public bool CanBePressed(InputModifiers currentModifiers)
		{
			return this.InputControl != null && this.AreModifiersMatching(currentModifiers);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000265C File Offset: 0x0000085C
		public bool AreModifiersMatching(InputModifiers currentModifiers)
		{
			if (this._allowOtherModifiers)
			{
				return (currentModifiers & this.InputModifiers) == this.InputModifiers;
			}
			return currentModifiers == this.InputModifiers;
		}

		// Token: 0x0400001B RID: 27
		public readonly bool _allowOtherModifiers;

		// Token: 0x0400001C RID: 28
		public readonly float _pressedButtonThreshold = 0.05f;
	}
}
