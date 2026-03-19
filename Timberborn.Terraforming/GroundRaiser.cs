using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.DeconstructionSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000F RID: 15
	public class GroundRaiser : BaseComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedPausable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000030E1 File Offset: 0x000012E1
		public bool ShouldRaiseTerrain
		{
			get
			{
				return this._blockObject.IsFinished;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000030EE File Offset: 0x000012EE
		public Vector3Int Coordinates
		{
			get
			{
				return this._blockObject.Coordinates;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030FB File Offset: 0x000012FB
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._deconstructible = base.GetComponent<Deconstructible>();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003115 File Offset: 0x00001315
		public void OnEnterFinishedState()
		{
			this._deconstructible.DisableDeconstruction();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003122 File Offset: 0x00001322
		public void OnExitFinishedState()
		{
		}

		// Token: 0x04000036 RID: 54
		public BlockObject _blockObject;

		// Token: 0x04000037 RID: 55
		public Deconstructible _deconstructible;
	}
}
