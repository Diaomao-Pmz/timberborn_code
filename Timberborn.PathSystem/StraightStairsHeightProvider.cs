using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001E RID: 30
	public class StraightStairsHeightProvider : BaseComponent, IAwakableComponent, IPathHeightProvider
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003DEC File Offset: 0x00001FEC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003DFC File Offset: 0x00001FFC
		public bool TryGetHeight(Vector3 worldPosition, out float pathHeight)
		{
			if (this._blockObject.IsFinished)
			{
				Vector3 vector = base.Transform.InverseTransformPoint(worldPosition);
				pathHeight = (float)this._blockObject.Coordinates.z + (StraightStairsHeightProvider.LinearCoefficient * vector.z + StraightStairsHeightProvider.LinearConstant);
				return true;
			}
			pathHeight = 0f;
			return false;
		}

		// Token: 0x04000055 RID: 85
		public static readonly float LinearCoefficient = -1f;

		// Token: 0x04000056 RID: 86
		public static readonly float LinearConstant = 1f;

		// Token: 0x04000057 RID: 87
		public BlockObject _blockObject;
	}
}
