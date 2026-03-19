using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.LinkedBuildingSystem;
using Timberborn.RecoverableGoodSystemUI;

namespace Timberborn.LinkedBuildingSystemUI
{
	// Token: 0x02000004 RID: 4
	public class LinkedBuildingRecoverableObjectAdder : BaseComponent, IAwakableComponent, IRecoverableObjectAdder
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public void Awake()
		{
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		public BlockObject GetAdditionalObjectToRecover()
		{
			return this._linkedBlockObject;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DF File Offset: 0x000002DF
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linkedBlockObject = e.GetComponent<BlockObject>();
		}

		// Token: 0x04000006 RID: 6
		public BlockObject _linkedBlockObject;
	}
}
