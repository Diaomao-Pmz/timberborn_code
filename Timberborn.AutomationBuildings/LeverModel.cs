using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000022 RID: 34
	public class LeverModel : BaseComponent, IAwakableComponent, IStartableComponent, IAutomatorListener
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00004FA8 File Offset: 0x000031A8
		public void Awake()
		{
			LeverModelSpec component = base.GetComponent<LeverModelSpec>();
			this._onModel = base.GameObject.FindChild(component.OnModelName);
			this._offModel = base.GameObject.FindChild(component.OffModelName);
			this._lever = base.GetComponent<Lever>();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004FF6 File Offset: 0x000031F6
		public void Start()
		{
			this.UpdateModels();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004FF6 File Offset: 0x000031F6
		public void OnAutomatorStateChanged()
		{
			this.UpdateModels();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004FFE File Offset: 0x000031FE
		public void UpdateModels()
		{
			this._onModel.SetActive(this._lever.IsOn);
			this._offModel.SetActive(!this._lever.IsOn);
		}

		// Token: 0x040000A1 RID: 161
		public Lever _lever;

		// Token: 0x040000A2 RID: 162
		public GameObject _onModel;

		// Token: 0x040000A3 RID: 163
		public GameObject _offModel;
	}
}
