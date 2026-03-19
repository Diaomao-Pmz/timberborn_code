using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DeteriorationSystem;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.DeteriorationSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DeteriorableDebugFragment : IEntityPanelFragment
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		public DeteriorableDebugFragment(DebugFragmentFactory debugFragmentFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FC File Offset: 0x000003FC
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.Expire), "Set durability to zero");
			this._root = this._debugFragmentFactory.Create(debugFragmentButton);
			return this._root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002239 File Offset: 0x00000439
		public void ShowFragment(BaseComponent entity)
		{
			this._deteriorable = entity.GetComponent<Deteriorable>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002247 File Offset: 0x00000447
		public void ClearFragment()
		{
			this._deteriorable = null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002250 File Offset: 0x00000450
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._deteriorable && this._deteriorable.Enabled);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002278 File Offset: 0x00000478
		public void Expire()
		{
			if (this._deteriorable && this._deteriorable.Enabled)
			{
				this._deteriorable.SetDeteriorationToZero();
			}
		}

		// Token: 0x0400000F RID: 15
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000010 RID: 16
		public Deteriorable _deteriorable;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;
	}
}
