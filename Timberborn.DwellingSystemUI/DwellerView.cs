using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DwellerView
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public DwellerView(VisualElement root, CharacterButton characterButton, Button viewButton, Label name, Label subtitle, Label wellbeingCounter)
		{
			this.Root = root;
			this._characterButton = characterButton;
			this._viewButton = viewButton;
			this._name = name;
			this._subtitle = subtitle;
			this._wellbeingCounter = wellbeingCounter;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public void SetAsAdult()
		{
			this._characterButton.ShowAdultEmpty();
			this._isChildSlot = false;
			this.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002116 File Offset: 0x00000316
		public void SetAsChild()
		{
			this._characterButton.ShowChildEmpty();
			this._isChildSlot = true;
			this.Clear();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public void Fill(BaseComponent user, Action onClick, string name, string subtitle, int wellbeing)
		{
			this._characterButton.ShowFilled(user, onClick);
			this._name.text = name;
			this._subtitle.text = subtitle;
			this._wellbeingCounter.ToggleDisplayStyle(true);
			this._wellbeingCounter.text = wellbeing.ToString();
			this._wellbeingCounter.EnableInClassList(DwellerView.NegativeWellbeingClass, wellbeing < 0);
			this._viewButton.SetEnabled(true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A2 File Offset: 0x000003A2
		public void Reset()
		{
			if (this._isChildSlot)
			{
				this.SetAsChild();
				return;
			}
			this.SetAsAdult();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public void Clear()
		{
			this._name.text = "";
			this._subtitle.text = "";
			this._wellbeingCounter.ToggleDisplayStyle(false);
			this._wellbeingCounter.RemoveFromClassList(DwellerView.NegativeWellbeingClass);
			this._viewButton.SetEnabled(false);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string NegativeWellbeingClass = "wellbeing--negative";

		// Token: 0x04000008 RID: 8
		public readonly CharacterButton _characterButton;

		// Token: 0x04000009 RID: 9
		public readonly Button _viewButton;

		// Token: 0x0400000A RID: 10
		public readonly Label _name;

		// Token: 0x0400000B RID: 11
		public readonly Label _subtitle;

		// Token: 0x0400000C RID: 12
		public readonly Label _wellbeingCounter;

		// Token: 0x0400000D RID: 13
		public bool _isChildSlot;
	}
}
