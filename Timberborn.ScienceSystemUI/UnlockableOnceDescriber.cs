using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x0200000D RID: 13
	public class UnlockableOnceDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002682 File Offset: 0x00000882
		public UnlockableOnceDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002691 File Offset: 0x00000891
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000269F File Offset: 0x0000089F
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._blockObject.IsPreview)
			{
				string str = this._loc.T(UnlockableOnceDescriber.UnlockableOnceLocKey);
				yield return EntityDescription.CreateTextSection(SpecialStrings.RowStarter + str, 3000);
			}
			yield break;
		}

		// Token: 0x04000027 RID: 39
		public static readonly string UnlockableOnceLocKey = "Science.UnlockableOnce";

		// Token: 0x04000028 RID: 40
		public readonly ILoc _loc;

		// Token: 0x04000029 RID: 41
		public BlockObject _blockObject;
	}
}
