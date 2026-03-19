using System;
using Timberborn.BaseComponentSystem;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Wonders;
using UnityEngine.UIElements;

namespace Timberborn.WondersUI
{
	// Token: 0x02000004 RID: 4
	public class WonderDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public WonderDebugFragment(DebugFragmentFactory debugFragmentFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.OnProgressConstructionClick), "Progress construction");
			this._root = this._debugFragmentFactory.Create("Wonder", debugFragmentButton);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002112 File Offset: 0x00000312
		public void ShowFragment(BaseComponent entity)
		{
			this._wonder = entity.GetComponent<Wonder>();
			this._constructionSite = entity.GetComponent<ConstructionSite>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._wonder);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		public void ClearFragment()
		{
			this._wonder = null;
			this._constructionSite = null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public void OnProgressConstructionClick()
		{
			this._constructionSite.IncreaseBuildTime(WonderDebugFragment.BuildTimeAmount);
		}

		// Token: 0x04000006 RID: 6
		public static readonly float BuildTimeAmount = 1000f;

		// Token: 0x04000007 RID: 7
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000008 RID: 8
		public VisualElement _root;

		// Token: 0x04000009 RID: 9
		public Wonder _wonder;

		// Token: 0x0400000A RID: 10
		public ConstructionSite _constructionSite;
	}
}
