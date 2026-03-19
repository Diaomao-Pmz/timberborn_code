using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000009 RID: 9
	public class DirectionalWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002328 File Offset: 0x00000528
		public DirectionalWaterSource(IWaterService waterService)
		{
			this._waterService = waterService;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002337 File Offset: 0x00000537
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002354 File Offset: 0x00000554
		public void InitializeEntity()
		{
			FlowDirection flowDirection = DirectionalWaterSource.OrientationToFlowDirection(this._blockObject.Orientation);
			foreach (Vector3Int coordinates in this._waterSource.Coordinates)
			{
				this._waterService.AddDirectionLimiter(coordinates, flowDirection);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023A8 File Offset: 0x000005A8
		public static FlowDirection OrientationToFlowDirection(Orientation orientation)
		{
			FlowDirection result;
			switch (orientation)
			{
			case Orientation.Cw0:
				result = FlowDirection.Top;
				break;
			case Orientation.Cw90:
				result = FlowDirection.Right;
				break;
			case Orientation.Cw180:
				result = FlowDirection.Bottom;
				break;
			case Orientation.Cw270:
				result = FlowDirection.Left;
				break;
			default:
				throw new ArgumentOutOfRangeException("orientation", orientation, null);
			}
			return result;
		}

		// Token: 0x0400000E RID: 14
		public readonly IWaterService _waterService;

		// Token: 0x0400000F RID: 15
		public WaterSource _waterSource;

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;
	}
}
