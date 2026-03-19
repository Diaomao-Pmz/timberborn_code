using System;
using Timberborn.BaseComponentSystem;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000004 RID: 4
	public class ConstructionSiteDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public ConstructionSiteDebugFragment(DebugFragmentFactory debugFragmentFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.OnFinishNowClick), "Finish now");
			this._root = this._debugFragmentFactory.Create(debugFragmentButton);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210D File Offset: 0x0000030D
		public void ShowFragment(BaseComponent entity)
		{
			this._constructionSite = entity.GetComponent<ConstructionSite>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		public void ClearFragment()
		{
			this._constructionSite = null;
			this.UpdateFragment();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212A File Offset: 0x0000032A
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._constructionSite && this._constructionSite.Enabled);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002152 File Offset: 0x00000352
		public void OnFinishNowClick()
		{
			if (this._constructionSite && this._constructionSite.Enabled)
			{
				this._constructionSite.FinishNow();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000007 RID: 7
		public ConstructionSite _constructionSite;

		// Token: 0x04000008 RID: 8
		public VisualElement _root;
	}
}
