using System;
using Timberborn.CoreUI;
using Timberborn.FileSystem;
using Timberborn.GameSaveRepositorySystem;
using UnityEngine.UIElements;

namespace Timberborn.SettlementNameSystemUI
{
	// Token: 0x02000004 RID: 4
	public class SettlementNameBox : IPanelController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public SettlementNameBox(PanelStack panelStack, GameSaveRepository gameSaveRepository, DialogBoxShower dialogBoxShower, Action<string> confirmButtonCallback, VisualElement root, string initialSettlementName)
		{
			this._panelStack = panelStack;
			this._gameSaveRepository = gameSaveRepository;
			this._dialogBoxShower = dialogBoxShower;
			this._confirmButtonCallback = confirmButtonCallback;
			this._root = root;
			this._input = UQueryExtensions.Q<TextField>(this._root, "Input", null);
			this._initialSettlementName = initialSettlementName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002117 File Offset: 0x00000317
		public string SettlementName
		{
			get
			{
				return this._input.text;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public VisualElement GetPanel()
		{
			this._input.SetValueWithoutNotify(this._initialSettlementName);
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002140 File Offset: 0x00000340
		public bool OnUIConfirmed()
		{
			string text = this._input.text;
			if (!string.IsNullOrEmpty(text))
			{
				switch (this._gameSaveRepository.CreateDirectoryForSettlement(text))
				{
				case DirectoryCreationResult.OK:
					this._panelStack.Pop(this);
					this._confirmButtonCallback(text);
					break;
				case DirectoryCreationResult.NameTaken:
					this.ShowDialogBox(SettlementNameBox.TakenNameLocKey);
					break;
				case DirectoryCreationResult.NameInvalid:
					this.ShowDialogBox(SettlementNameBox.InvalidNameLocKey);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BD File Offset: 0x000003BD
		public void OnUICancelled()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BF File Offset: 0x000003BF
		public void ShowDialogBox(string textLocKey)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(textLocKey).Show();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string InvalidNameLocKey = "Saving.InvalidName";

		// Token: 0x04000007 RID: 7
		public static readonly string TakenNameLocKey = "Saving.TakenName";

		// Token: 0x04000008 RID: 8
		public readonly PanelStack _panelStack;

		// Token: 0x04000009 RID: 9
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x0400000A RID: 10
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000B RID: 11
		public readonly Action<string> _confirmButtonCallback;

		// Token: 0x0400000C RID: 12
		public readonly VisualElement _root;

		// Token: 0x0400000D RID: 13
		public readonly TextField _input;

		// Token: 0x0400000E RID: 14
		public readonly string _initialSettlementName;
	}
}
