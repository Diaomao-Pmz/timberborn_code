using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.PathSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000019 RID: 25
	public class GatePlacement : BaseComponent, IAwakableComponent, IPreInitializableEntity, IPathConnectionEnforcer
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003F0B File Offset: 0x0000210B
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00003F13 File Offset: 0x00002113
		public Vector3Int Start { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003F1C File Offset: 0x0000211C
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00003F24 File Offset: 0x00002124
		public Vector3Int End { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003F2D File Offset: 0x0000212D
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00003F35 File Offset: 0x00002135
		public Vector3Int Center { get; private set; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00003F3E File Offset: 0x0000213E
		public GatePlacement(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003F4D File Offset: 0x0000214D
		public void Awake()
		{
			this._spec = base.GetComponent<GateSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003F68 File Offset: 0x00002168
		public void PreInitializeEntity()
		{
			this.Start = this._blockObject.TransformCoordinates(this._spec.Start);
			this.End = this._blockObject.TransformCoordinates(this._spec.End);
			this.Center = this._blockObject.Coordinates;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003FC0 File Offset: 0x000021C0
		public bool CanConnectPath(Vector3Int origin, Vector3Int target)
		{
			if (!this._blockObject.IsFinished)
			{
				return false;
			}
			if (target == this.Center && (origin == this.Start || origin == this.End))
			{
				return true;
			}
			BlockObject pathObjectAt = this._blockService.GetPathObjectAt(target);
			return pathObjectAt != null && pathObjectAt.IsFinished && origin == this.Center && (target == this.Start || target == this.End);
		}

		// Token: 0x0400006C RID: 108
		public readonly IBlockService _blockService;

		// Token: 0x0400006D RID: 109
		public GateSpec _spec;

		// Token: 0x0400006E RID: 110
		public BlockObject _blockObject;
	}
}
