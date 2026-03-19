using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.EntitySystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000016 RID: 22
	public class ConstructionSiteModelUpdater : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000037DF File Offset: 0x000019DF
		public void Awake()
		{
			this._buildingModel = base.GetComponent<BuildingModel>();
			base.GetComponents<IConstructionSiteValidator>(this._constructionSiteValidators);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000037FC File Offset: 0x000019FC
		public void PostInitializeEntity()
		{
			foreach (IConstructionSiteValidator constructionSiteValidator in this._constructionSiteValidators)
			{
				constructionSiteValidator.ValidationStateChanged += this.OnValidationStateChanged;
			}
			this.UpdateModel();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003860 File Offset: 0x00001A60
		public void OnValidationStateChanged(object sender, EventArgs e)
		{
			this.UpdateModel();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003868 File Offset: 0x00001A68
		public void UpdateModel()
		{
			if (this._constructionSiteValidators.FastAll((IConstructionSiteValidator validator) => validator.IsModelValid))
			{
				this._buildingModel.UnblockUnfinishedModel();
				return;
			}
			this._buildingModel.BlockUnfinishedModel();
		}

		// Token: 0x04000050 RID: 80
		public readonly List<IConstructionSiteValidator> _constructionSiteValidators = new List<IConstructionSiteValidator>();

		// Token: 0x04000051 RID: 81
		public BuildingModel _buildingModel;
	}
}
