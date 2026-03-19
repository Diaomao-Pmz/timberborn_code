using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.NaturalResources;
using UnityEngine;

namespace Timberborn.NaturalResourcesModelSystem
{
	// Token: 0x02000007 RID: 7
	public class NaturalResourceCenterProvider : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._coordinatesOffsetter = base.GetComponent<CoordinatesOffsetter>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public Vector3 GetWorldCenter()
		{
			Vector3 worldCenterGrounded = this._blockObjectCenter.WorldCenterGrounded;
			Vector3 vector = CoordinateSystem.GridToWorld(this._coordinatesOffsetter.CoordinatesOffset.XYZ());
			return worldCenterGrounded + vector;
		}

		// Token: 0x04000008 RID: 8
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000009 RID: 9
		public CoordinatesOffsetter _coordinatesOffsetter;
	}
}
