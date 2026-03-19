using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Demolishing;
using Timberborn.DemolishingUI;
using Timberborn.EntityPanelSystem;

namespace Timberborn.MapEditorDemolishingUI
{
	// Token: 0x02000005 RID: 5
	public class DemolishableScienceRewardDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002175 File Offset: 0x00000375
		public DemolishableScienceRewardDescriber(DemolishableScienceRewardLabelFactory demolishableScienceRewardLabelFactory)
		{
			this._demolishableScienceRewardLabelFactory = demolishableScienceRewardLabelFactory;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public void Awake()
		{
			this._spec = base.GetComponent<DemolishableScienceRewardSpec>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002192 File Offset: 0x00000392
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!base.GameObject.activeInHierarchy)
			{
				DemolishableScienceRewardLabel demolishableScienceRewardLabel = this._demolishableScienceRewardLabelFactory.Create();
				demolishableScienceRewardLabel.Show(this._spec);
				yield return EntityDescription.CreateBottomSection(demolishableScienceRewardLabel.Root, 0);
			}
			yield break;
		}

		// Token: 0x0400000A RID: 10
		public readonly DemolishableScienceRewardLabelFactory _demolishableScienceRewardLabelFactory;

		// Token: 0x0400000B RID: 11
		public DemolishableScienceRewardSpec _spec;
	}
}
