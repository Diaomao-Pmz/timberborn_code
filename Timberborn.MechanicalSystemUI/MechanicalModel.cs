using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MechanicalSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000016 RID: 22
	public class MechanicalModel : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002D06 File Offset: 0x00000F06
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalModelUpdater = base.GetComponent<IMechanicalModelUpdater>();
			this._mechanicalNode.AddedToGraph += this.OnAddedToGraph;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D37 File Offset: 0x00000F37
		public void UpdateModel()
		{
			IMechanicalModelUpdater mechanicalModelUpdater = this._mechanicalModelUpdater;
			if (mechanicalModelUpdater == null)
			{
				return;
			}
			mechanicalModelUpdater.UpdateModel();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D49 File Offset: 0x00000F49
		public void OnAddedToGraph(object sender, EventArgs eventArgs)
		{
			if (this._mechanicalNode.Powered)
			{
				this.UpdateModel();
			}
		}

		// Token: 0x04000039 RID: 57
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400003A RID: 58
		public IMechanicalModelUpdater _mechanicalModelUpdater;
	}
}
