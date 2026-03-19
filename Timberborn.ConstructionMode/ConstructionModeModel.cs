using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Rendering;

namespace Timberborn.ConstructionMode
{
	// Token: 0x02000009 RID: 9
	public class ConstructionModeModel : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IRegisteredComponent
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002166 File Offset: 0x00000366
		public ConstructionModeModel(MaterialColorer materialColorer)
		{
			this._materialColorer = materialColorer;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002175 File Offset: 0x00000375
		public void Awake()
		{
			this._buildingModel = base.GetComponent<BuildingModel>();
			base.DisableComponent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		public void OnEnterUnfinishedState()
		{
			this._materialColorer.EnableGrayscale(this._buildingModel.FinishedModel);
			if (this._buildingModel.FinishedUncoveredModel)
			{
				this._materialColorer.EnableGrayscale(this._buildingModel.FinishedUncoveredModel);
			}
			base.EnableComponent();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E0 File Offset: 0x000003E0
		public void OnExitUnfinishedState()
		{
			this._materialColorer.DisableGrayscale(this._buildingModel.FinishedModel);
			if (this._buildingModel.FinishedUncoveredModel)
			{
				this._materialColorer.DisableGrayscale(this._buildingModel.FinishedUncoveredModel);
			}
			base.DisableComponent();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002231 File Offset: 0x00000431
		public void EnterConstructionMode()
		{
			this._buildingModel.ShowFinishedModel();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000223E File Offset: 0x0000043E
		public void ExitConstructionMode()
		{
			this._buildingModel.ShowUnfinishedModel();
		}

		// Token: 0x04000009 RID: 9
		public readonly MaterialColorer _materialColorer;

		// Token: 0x0400000A RID: 10
		public BuildingModel _buildingModel;
	}
}
