using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.KeyBindingSystem;
using Timberborn.Localization;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000011 RID: 17
	public class KeyRebinder
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002C4E File Offset: 0x00000E4E
		public KeyRebinder(DialogBoxShower dialogBoxShower, InputBindingListener inputBindingListener, KeyBindingRegistry keyBindingRegistry, KeyBindingSpecService keyBindingSpecService, ILoc loc)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._inputBindingListener = inputBindingListener;
			this._keyBindingRegistry = keyBindingRegistry;
			this._keyBindingSpecService = keyBindingSpecService;
			this._loc = loc;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C7B File Offset: 0x00000E7B
		public void StartRebinding(DefinableInputBinding singleInputBinding)
		{
			Asserts.FieldIsNull<KeyRebinder>(this, this._definableInputBinding, "_definableInputBinding");
			this._definableInputBinding = singleInputBinding;
			this.ShowRebinder();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void ShowRebinder()
		{
			this.StartListeningForInput();
			this._dialogBox = this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(KeyRebinder.RebindingMessageLocKey, this._definableInputBinding.KeyBinding.DisplayName)).SetConfirmButton(new Action(this.ClearBinding), this._loc.T(KeyRebinder.ClearBindingLocKey)).SetCancelButton(new Action(this.Cancel), this._loc.T(CommonLocKeys.CancelKey)).Show();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void StartListeningForInput()
		{
			this._inputBindingListener.WaitForInput(new Action<CustomInputBinding>(this.InputCallback));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D48 File Offset: 0x00000F48
		public void InputCallback(CustomInputBinding customInputBinding)
		{
			List<KeyBinding> list = this.CollidingBindings(customInputBinding).ToList<KeyBinding>();
			if (list.Count > 0)
			{
				this.ShowCollidingBindingsDialog(list, customInputBinding);
				return;
			}
			this.RebindAndClose(customInputBinding);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D7B File Offset: 0x00000F7B
		public IEnumerable<KeyBinding> CollidingBindings(CustomInputBinding customInputBinding)
		{
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				if (keyBinding.IsUsingBinding(customInputBinding))
				{
					keyBinding.Lock();
					if (keyBinding != this._definableInputBinding.KeyBinding)
					{
						yield return keyBinding;
					}
				}
			}
			List<KeyBinding>.Enumerator enumerator = default(List<KeyBinding>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D94 File Offset: 0x00000F94
		public void ShowCollidingBindingsDialog(IReadOnlyList<KeyBinding> collidingBindings, CustomInputBinding customInputBinding)
		{
			this._dialogBoxShower.Create().SetMessage(this.GetCollidingBindingsMessage(collidingBindings)).SetConfirmButton(delegate()
			{
				this.RebindAndClose(customInputBinding);
			}).SetCancelButton(new Action(this.StartListeningForInput)).Show();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public string GetCollidingBindingsMessage(IReadOnlyList<KeyBinding> collidingBindings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < collidingBindings.Count; i++)
			{
				if (i >= 20)
				{
					stringBuilder.AppendLine(KeyRebinder.MaxBindingCollisionLine);
					break;
				}
				stringBuilder.AppendLine(SpecialStrings.RowStarter + collidingBindings[i].DisplayName);
			}
			return this._loc.T<string>(KeyRebinder.DuplicatedBindingLocKey, stringBuilder.ToStringWithoutNewLineEnd());
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E5E File Offset: 0x0000105E
		public void RebindAndClose(CustomInputBinding customInputBinding)
		{
			this.RebindKey(customInputBinding);
			this._dialogBox.Close();
			this.ClearRebinder();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E78 File Offset: 0x00001078
		public void RebindKey(CustomInputBinding customInputBinding)
		{
			this._keyBindingSpecService.RebindInputBinding(this._definableInputBinding, customInputBinding);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E8C File Offset: 0x0000108C
		public void ClearRebinder()
		{
			this._dialogBox = null;
			this._definableInputBinding = null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E9C File Offset: 0x0000109C
		public void ClearBinding()
		{
			this.RebindKey(CustomInputBinding.UndefinedBinding);
			this.Cancel();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EAF File Offset: 0x000010AF
		public void Cancel()
		{
			this._inputBindingListener.FinishListening();
			this.ClearRebinder();
		}

		// Token: 0x04000033 RID: 51
		public static readonly string RebindingMessageLocKey = "KeyBindingBox.RebindingMessage";

		// Token: 0x04000034 RID: 52
		public static readonly string ClearBindingLocKey = "KeyBindingBox.ClearBinding";

		// Token: 0x04000035 RID: 53
		public static readonly string DuplicatedBindingLocKey = "KeyBindingBox.DuplicatedBinding";

		// Token: 0x04000036 RID: 54
		public static readonly string MaxBindingCollisionLine = "...";

		// Token: 0x04000037 RID: 55
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000038 RID: 56
		public readonly InputBindingListener _inputBindingListener;

		// Token: 0x04000039 RID: 57
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x0400003A RID: 58
		public readonly KeyBindingSpecService _keyBindingSpecService;

		// Token: 0x0400003B RID: 59
		public readonly ILoc _loc;

		// Token: 0x0400003C RID: 60
		public DefinableInputBinding _definableInputBinding;

		// Token: 0x0400003D RID: 61
		public DialogBox _dialogBox;
	}
}
