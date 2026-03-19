using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000009 RID: 9
	public class DryObjectModel : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000024D0 File Offset: 0x000006D0
		public void Awake()
		{
			this._dryObject = base.GetComponent<DryObject>();
			DryObjectModelSpec component = base.GetComponent<DryObjectModelSpec>();
			this._wetModel = base.GameObject.FindChild(component.WetModelName);
			this._dryModel = base.GameObject.FindChild(component.DryModelName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000251E File Offset: 0x0000071E
		public void PostInitializeEntity()
		{
			this._dryObject.EnteredDryState += delegate(object _, EventArgs _)
			{
				this.UpdateModel();
			};
			this._dryObject.ExitedDryState += delegate(object _, EventArgs _)
			{
				this.UpdateModel();
			};
			this.UpdateModel();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002554 File Offset: 0x00000754
		public void UpdateModel()
		{
			this.ToggleModel(this._dryObject.IsDry);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002567 File Offset: 0x00000767
		public void ToggleModel(bool dry)
		{
			this._wetModel.SetActive(!dry);
			this._dryModel.SetActive(dry);
		}

		// Token: 0x04000015 RID: 21
		public GameObject _wetModel;

		// Token: 0x04000016 RID: 22
		public GameObject _dryModel;

		// Token: 0x04000017 RID: 23
		public DryObject _dryObject;
	}
}
