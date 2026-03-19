using System;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.BonusSystemUI
{
	// Token: 0x02000004 RID: 4
	public class BonusManagerDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BonusManagerDebugFragment(DebugFragmentFactory debugFragmentFactory, BonusTypeSpecService bonusTypeSpecService)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._bonusTypeSpecService = bonusTypeSpecService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create("BonusManager");
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002109 File Offset: 0x00000309
		public void ShowFragment(BaseComponent entity)
		{
			this._bonusManager = entity.GetComponent<BonusManager>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002117 File Offset: 0x00000317
		public void ClearFragment()
		{
			this._bonusManager = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		public void UpdateFragment()
		{
			if (this._bonusManager)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this._bonusTypeSpecService.BonusIds)
				{
					float num = this._bonusManager.Multiplier(text);
					stringBuilder.AppendLine(string.Format("{0}: {1}", text, num));
				}
				this._text.text = stringBuilder.ToStringWithoutNewLineEnd();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000007 RID: 7
		public readonly BonusTypeSpecService _bonusTypeSpecService;

		// Token: 0x04000008 RID: 8
		public BonusManager _bonusManager;

		// Token: 0x04000009 RID: 9
		public Label _text;

		// Token: 0x0400000A RID: 10
		public VisualElement _root;
	}
}
