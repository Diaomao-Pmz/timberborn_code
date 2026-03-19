using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000017 RID: 23
	public class MarkerPosition : BaseComponent, IAwakableComponent
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000384C File Offset: 0x00001A4C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003854 File Offset: 0x00001A54
		public Vector3 Position { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x0000385D File Offset: 0x00001A5D
		public MarkerPosition(BoundsCalculator boundsCalculator)
		{
			this._boundsCalculator = boundsCalculator;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000386C File Offset: 0x00001A6C
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000387C File Offset: 0x00001A7C
		public void UpdatePosition(Vector3 gridOffset = default(Vector3))
		{
			float enabledRendererYMaxBound = this._boundsCalculator.GetEnabledRendererYMaxBound(base.Transform);
			float num = (gridOffset.z > 0f) ? gridOffset.z : MarkerPosition.YOffset;
			float num2 = enabledRendererYMaxBound + num;
			Vector3 vector = CoordinateSystem.GridToWorld(this._blockObjectCenter.GridCenter + gridOffset);
			this.Position = new Vector3(vector.x, num2, vector.z);
		}

		// Token: 0x04000039 RID: 57
		public static readonly float YOffset = 0.75f;

		// Token: 0x0400003B RID: 59
		public readonly BoundsCalculator _boundsCalculator;

		// Token: 0x0400003C RID: 60
		public BlockObjectCenter _blockObjectCenter;
	}
}
