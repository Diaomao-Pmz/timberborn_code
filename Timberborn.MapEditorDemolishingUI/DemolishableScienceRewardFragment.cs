using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Demolishing;
using Timberborn.DemolishingUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorDemolishingUI
{
	// Token: 0x02000004 RID: 4
	public class DemolishableScienceRewardFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DemolishableScienceRewardFragment(VisualElementLoader visualElementLoader, DemolishableScienceRewardLabelFactory demolishableScienceRewardLabelFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._demolishableScienceRewardLabelFactory = demolishableScienceRewardLabelFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/EntityPanel/DemolishableScienceRewardFragment");
			this._demolishableScienceRewardLabel = this._demolishableScienceRewardLabelFactory.Create();
			this._root.Add(this._demolishableScienceRewardLabel.Root);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		public void ShowFragment(BaseComponent entity)
		{
			DemolishableScienceRewardSpec component = entity.GetComponent<DemolishableScienceRewardSpec>();
			if (component != null)
			{
				this._demolishableScienceRewardLabel.Show(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002165 File Offset: 0x00000365
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002173 File Offset: 0x00000373
		public void UpdateFragment()
		{
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly DemolishableScienceRewardLabelFactory _demolishableScienceRewardLabelFactory;

		// Token: 0x04000008 RID: 8
		public VisualElement _root;

		// Token: 0x04000009 RID: 9
		public DemolishableScienceRewardLabel _demolishableScienceRewardLabel;
	}
}
