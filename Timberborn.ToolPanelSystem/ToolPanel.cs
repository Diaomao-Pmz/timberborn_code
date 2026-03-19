using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.ToolPanelSystem
{
	// Token: 0x02000006 RID: 6
	public class ToolPanel : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020DE File Offset: 0x000002DE
		public ToolPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, IEnumerable<ToolPanelModule> toolPanelModules)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._toolPanelModules = toolPanelModules.ToImmutableArray<ToolPanelModule>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/ToolPanel/ToolPanel");
			this.AddFragments(visualElement);
			this._uiLayout.AddBottomBar(visualElement, 50);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		public void AddFragments(VisualElement root)
		{
			List<OrderedToolFragment> list = new List<OrderedToolFragment>();
			foreach (ToolPanelModule toolPanelModule in this._toolPanelModules)
			{
				list.AddRange(toolPanelModule.ToolFragments);
			}
			foreach (OrderedToolFragment orderedToolFragment in from fragment in list
			orderby fragment.Order descending
			select fragment)
			{
				root.Add(orderedToolFragment.ToolFragment.InitializeFragment());
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly UILayout _uiLayout;

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ImmutableArray<ToolPanelModule> _toolPanelModules;
	}
}
