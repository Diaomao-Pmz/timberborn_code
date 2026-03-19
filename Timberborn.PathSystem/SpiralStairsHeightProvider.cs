using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001B RID: 27
	public class SpiralStairsHeightProvider : BaseComponent, IAwakableComponent, IPathHeightProvider
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000392F File Offset: 0x00001B2F
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003940 File Offset: 0x00001B40
		public bool TryGetHeight(Vector3 worldPosition, out float pathHeight)
		{
			if (this._blockObject.IsFinished)
			{
				Vector3 vector = base.Transform.InverseTransformPoint(worldPosition);
				float num = SpiralStairsHeightProvider.MaxAngle - Mathf.Atan2(vector.x, 1f - vector.z);
				pathHeight = (float)this._blockObject.Coordinates.z + Mathf.Clamp01(SpiralStairsHeightProvider.MinHeight + num / SpiralStairsHeightProvider.MaxAngle);
				return true;
			}
			pathHeight = 0f;
			return false;
		}

		// Token: 0x0400004F RID: 79
		public static readonly float MinHeight = 0.075f;

		// Token: 0x04000050 RID: 80
		public static readonly float MaxAngle = 1.5707964f;

		// Token: 0x04000051 RID: 81
		public BlockObject _blockObject;
	}
}
