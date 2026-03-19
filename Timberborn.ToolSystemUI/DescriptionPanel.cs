using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DescriptionPanel
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public DescriptionPanel(VisualElement root)
		{
			this.Root = root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E2 File Offset: 0x000002E2
		public void AddUpdateCallback(Action callback)
		{
			this._updateCallbacks.Add(callback);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F0 File Offset: 0x000002F0
		public void Update()
		{
			foreach (Action action in this._updateCallbacks)
			{
				action();
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly List<Action> _updateCallbacks = new List<Action>();
	}
}
