using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesMoisture;

namespace Timberborn.NaturalResourcesMoistureUI
{
	// Token: 0x02000004 RID: 4
	public class FloodableNaturalResourceDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public FloodableNaturalResourceDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Awake()
		{
			this._floodableNaturalResourceSpec = base.GetComponent<FloodableNaturalResourceSpec>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DB File Offset: 0x000002DB
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._floodableNaturalResourceSpec.MinWaterHeight > 0)
			{
				string content = SpecialStrings.RowStarter + this._loc.T(FloodableNaturalResourceDescriber.AquaticLocKey);
				yield return EntityDescription.CreateTextSection(content, 2100);
			}
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string AquaticLocKey = "NaturalResources.Aquatic";

		// Token: 0x04000007 RID: 7
		public readonly ILoc _loc;

		// Token: 0x04000008 RID: 8
		public FloodableNaturalResourceSpec _floodableNaturalResourceSpec;
	}
}
