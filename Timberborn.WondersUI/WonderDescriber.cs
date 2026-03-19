using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.WondersUI
{
	// Token: 0x02000005 RID: 5
	public class WonderDescriber : BaseComponent, IEntityDescriber
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002172 File Offset: 0x00000372
		public WonderDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002181 File Offset: 0x00000381
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string content = SpecialStrings.RowStarter + this._loc.T(WonderDescriber.WonderLocKey);
			yield return EntityDescription.CreateTextSection(content, 2040);
			yield break;
		}

		// Token: 0x0400000B RID: 11
		public static readonly string WonderLocKey = "Buildings.Wonder";

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;
	}
}
