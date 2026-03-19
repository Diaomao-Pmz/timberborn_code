using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;

namespace Timberborn.NaturalResourcesUI
{
	// Token: 0x02000004 RID: 4
	public class NaturalResourceDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public NaturalResourceDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Awake()
		{
			this._labeledEntitySpec = base.GetComponent<LabeledEntitySpec>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DB File Offset: 0x000002DB
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (base.GameObject.activeSelf)
			{
				string flavorDescriptionLocKey = this._labeledEntitySpec.FlavorDescriptionLocKey;
				if (!string.IsNullOrEmpty(flavorDescriptionLocKey))
				{
					yield return EntityDescription.CreateFlavorSection(this._loc.T(flavorDescriptionLocKey), 2);
				}
			}
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public readonly ILoc _loc;

		// Token: 0x04000007 RID: 7
		public LabeledEntitySpec _labeledEntitySpec;
	}
}
