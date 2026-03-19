using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200000B RID: 11
	public class WorkerView
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000279D File Offset: 0x0000099D
		public VisualElement Root { get; }

		// Token: 0x06000030 RID: 48 RVA: 0x000027A5 File Offset: 0x000009A5
		public WorkerView(WorkerTypeHelper workerTypeHelper, VisualElement root, CharacterButton characterButton, Label name, VisualElement vacantIcon, WorkplaceWorkerType workplaceWorkerType)
		{
			this._workerTypeHelper = workerTypeHelper;
			this._characterButton = characterButton;
			this.Root = root;
			this._name = name;
			this._vacantIcon = vacantIcon;
			this._workplaceWorkerType = workplaceWorkerType;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027DA File Offset: 0x000009DA
		public void Fill(BaseComponent user, Action onClick, string description)
		{
			this._characterButton.ShowFilled(user, onClick);
			this._name.text = description;
			this._vacantIcon.ToggleDisplayStyle(false);
			this.Root.SetEnabled(true);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000280D File Offset: 0x00000A0D
		public void ShowEmpty()
		{
			this.ShowEmpty(false);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002816 File Offset: 0x00000A16
		public void ShowVacant()
		{
			this.ShowEmpty(true);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002820 File Offset: 0x00000A20
		public void ShowEmpty(bool vacant)
		{
			if (this._workerTypeHelper.IsBotWorkerType(this._workplaceWorkerType.WorkerType))
			{
				this._characterButton.ShowBotEmpty();
			}
			else
			{
				this._characterButton.ShowAdultEmpty();
			}
			this._name.text = "";
			this._vacantIcon.ToggleDisplayStyle(vacant);
			this.Root.SetEnabled(false);
		}

		// Token: 0x04000028 RID: 40
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x04000029 RID: 41
		public readonly CharacterButton _characterButton;

		// Token: 0x0400002A RID: 42
		public readonly Label _name;

		// Token: 0x0400002B RID: 43
		public readonly VisualElement _vacantIcon;

		// Token: 0x0400002C RID: 44
		public readonly WorkplaceWorkerType _workplaceWorkerType;
	}
}
