using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000010 RID: 16
	public class BlockObjectCenter : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003367 File Offset: 0x00001567
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000336F File Offset: 0x0000156F
		public Vector3 GridCenter { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003380 File Offset: 0x00001580
		public Vector3 WorldCenter { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003389 File Offset: 0x00001589
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003391 File Offset: 0x00001591
		public Vector3 GridCenterGrounded { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000339A File Offset: 0x0000159A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000033A2 File Offset: 0x000015A2
		public Vector3 GridCenterAtBaseZ { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000033B3 File Offset: 0x000015B3
		public Vector3 WorldCenterGrounded { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000033C4 File Offset: 0x000015C4
		public Vector3 WorldCenterAtBaseZ { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x000033CD File Offset: 0x000015CD
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000033DC File Offset: 0x000015DC
		public void UpdateCenter()
		{
			Vector3 vector = this._blockObject.Blocks.Pivot(this._blockObject.Coordinates, this._blockObject.Orientation);
			Vector3 vector2 = this._blockObject.GetComponent<BlockObjectSpec>().CalculateCenterOffset(this._blockObject.Orientation);
			this.GridCenter = vector + vector2;
			this.WorldCenter = CoordinateSystem.GridToWorld(this.GridCenter);
			this.GridCenterGrounded = new Vector3(this.GridCenter.x, this.GridCenter.y, (float)this._blockObject.Coordinates.z);
			this.GridCenterAtBaseZ = this.GridCenterGrounded + new Vector3Int(0, 0, this._blockObject.BaseZ);
			this.WorldCenterGrounded = CoordinateSystem.GridToWorld(this.GridCenterGrounded);
			this.WorldCenterAtBaseZ = new Vector3(this.WorldCenterGrounded.x, (float)this._blockObject.CoordinatesAtBaseZ.z, this.WorldCenterGrounded.z);
		}

		// Token: 0x04000052 RID: 82
		public BlockObject _blockObject;
	}
}
