using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000B RID: 11
	public class BlockObjectCameraTarget : BaseComponent, IAwakableComponent, ICameraTarget
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026FC File Offset: 0x000008FC
		public Vector3 CameraTargetPosition
		{
			get
			{
				return this._blockObjectCenter.WorldCenterAtBaseZ;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002709 File Offset: 0x00000909
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
		}

		// Token: 0x04000013 RID: 19
		public BlockObjectCenter _blockObjectCenter;
	}
}
