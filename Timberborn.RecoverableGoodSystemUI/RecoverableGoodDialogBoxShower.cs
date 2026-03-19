using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x02000005 RID: 5
	public class RecoverableGoodDialogBoxShower
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020BE File Offset: 0x000002BE
		public RecoverableGoodDialogBoxShower(DialogBoxShower dialogBoxShower, RecoverableGoodElementFactory recoverableGoodElementFactory)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._recoverableGoodElementFactory = recoverableGoodElementFactory;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Show(BlockObject blockObject, Action confirmAction, string promptLocKey)
		{
			VisualElement content = this._recoverableGoodElementFactory.Create(Enumerables.One<BlockObject>(blockObject));
			this._dialogBoxShower.Create().SetLocalizedMessage(promptLocKey).SetConfirmButton(confirmAction).SetDefaultCancelButton().AddContent(content).Show();
		}

		// Token: 0x04000006 RID: 6
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000007 RID: 7
		public readonly RecoverableGoodElementFactory _recoverableGoodElementFactory;
	}
}
