using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Illumination;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000014 RID: 20
	public class GateModel : BaseComponent, IAwakableComponent, IFinishedStateListener, IStartableComponent
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x0000398C File Offset: 0x00001B8C
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._gateNavMeshBlocker = base.GetComponent<GateNavMeshBlocker>();
			base.GetComponent<Gate>().StateChanged += delegate(object _, EventArgs _)
			{
				this.UpdateModels();
			};
			this._openModel = base.GameObject.FindChild(base.GetComponent<GateModelSpec>().OpenModelName);
			this._closedModel = base.GameObject.FindChild(base.GetComponent<GateModelSpec>().ClosedModelName);
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A11 File Offset: 0x00001C11
		public void Start()
		{
			this.UpdateModels();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003A11 File Offset: 0x00001C11
		public void OnEnterFinishedState()
		{
			this.UpdateModels();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003A11 File Offset: 0x00001C11
		public void OnExitFinishedState()
		{
			this.UpdateModels();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003A1C File Offset: 0x00001C1C
		public void UpdateModels()
		{
			bool navMeshBlocked = this._gateNavMeshBlocker.NavMeshBlocked;
			this._openModel.SetActive(!navMeshBlocked);
			this._closedModel.SetActive(navMeshBlocked);
			this._illuminatorToggle.Toggle(!navMeshBlocked && this._blockObject.IsFinished);
		}

		// Token: 0x04000053 RID: 83
		public BlockObject _blockObject;

		// Token: 0x04000054 RID: 84
		public GateNavMeshBlocker _gateNavMeshBlocker;

		// Token: 0x04000055 RID: 85
		public GameObject _openModel;

		// Token: 0x04000056 RID: 86
		public GameObject _closedModel;

		// Token: 0x04000057 RID: 87
		public IlluminatorToggle _illuminatorToggle;
	}
}
