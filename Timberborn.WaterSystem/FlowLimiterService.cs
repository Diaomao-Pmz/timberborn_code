using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200000F RID: 15
	[MapEditorTickable]
	public class FlowLimiterService : IFlowLimiterService, ITickableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001A RID: 26 RVA: 0x00002638 File Offset: 0x00000838
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002670 File Offset: 0x00000870
		public event EventHandler<int> LimitedValueChanged;

		// Token: 0x0600001C RID: 28 RVA: 0x000026A5 File Offset: 0x000008A5
		public FlowLimiterService(MapIndexService mapIndexService)
		{
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026BF File Offset: 0x000008BF
		public ReadOnlyArray<int> LimitedDirections
		{
			get
			{
				return new ReadOnlyArray<int>(this._limitedDirections);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026CC File Offset: 0x000008CC
		public ReadOnlyArray<float> LimitedValues
		{
			get
			{
				return new ReadOnlyArray<float>(this._limitedValues);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026D9 File Offset: 0x000008D9
		public ReadOnlyArray<sbyte> FlowControllers
		{
			get
			{
				return new ReadOnlyArray<sbyte>(this._flowControllers);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026E6 File Offset: 0x000008E6
		public ReadOnlyArray<float> OutflowLimits
		{
			get
			{
				return new ReadOnlyArray<float>(this._outflowLimits);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026F4 File Offset: 0x000008F4
		public void Load()
		{
			this._stride = this._mapIndexService.Stride;
			int num = this._mapIndexService.TotalSize.z + 2;
			int num2 = this._mapIndexService.VerticalStride * num;
			this._limitedValues = new float[num2];
			this._limitedDirections = new int[num2];
			this._flowControllers = new sbyte[num2];
			this._outflowLimits = new float[num2];
			for (int i = 0; i < num2; i++)
			{
				this._limitedValues[i] = float.MinValue;
				this._outflowLimits[i] = float.MaxValue;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000278C File Offset: 0x0000098C
		public void PostLoad()
		{
			this.ProcessModifications();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000278C File Offset: 0x0000098C
		public void Tick()
		{
			this.ProcessModifications();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002794 File Offset: 0x00000994
		public void UpdateInflowLimiter(Vector3Int coordinates, float flowLimit)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateFlowModification(coordinates, flowLimit));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027A8 File Offset: 0x000009A8
		public void RemoveInflowLimiter(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateFlowModification(coordinates, float.MinValue));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027C0 File Offset: 0x000009C0
		public void SetOutflowLimit(Vector3Int coordinates, float outflowLimit)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateOutflowLimitModification(coordinates, outflowLimit));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D4 File Offset: 0x000009D4
		public void RemoveOutflowLimit(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateOutflowLimitModification(coordinates, float.MaxValue));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027EC File Offset: 0x000009EC
		public void AddDirectionLimiter(Vector3Int coordinates, FlowDirection flowDirection)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateDirectionModification(coordinates, flowDirection));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002800 File Offset: 0x00000A00
		public void RemoveDirectionLimiter(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateDirectionModification(coordinates, FlowDirection.Any));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002814 File Offset: 0x00000A14
		public void SetControllerToDecreaseFlow(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateControllerModification(coordinates, -1));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002828 File Offset: 0x00000A28
		public void SetControllerToIncreaseFlow(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateControllerModification(coordinates, 1));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000283C File Offset: 0x00000A3C
		public void RemoveFlowController(Vector3Int coordinates)
		{
			this._modifications.Enqueue(FlowLimiterService.Modification.CreateControllerModification(coordinates, 0));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002850 File Offset: 0x00000A50
		public void ProcessModifications()
		{
			while (!this._modifications.IsEmpty<FlowLimiterService.Modification>())
			{
				FlowLimiterService.Modification modification = this._modifications.Dequeue();
				this.ProcessModification(modification);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002880 File Offset: 0x00000A80
		public void ProcessModification(in FlowLimiterService.Modification modification)
		{
			int num = this._mapIndexService.CoordinatesToIndex3D(modification.Coordinates);
			if (modification.FlowDirection != null)
			{
				this._limitedDirections[num] = this.FlowDirectionToIntDirection(modification.FlowDirection.Value);
				return;
			}
			if (modification.FlowController != null)
			{
				this._flowControllers[num] = modification.FlowController.Value;
				return;
			}
			if (modification.OutflowLimit != null)
			{
				this._outflowLimits[num] = modification.OutflowLimit.Value;
				return;
			}
			this._limitedValues[num] = modification.FlowLimit;
			EventHandler<int> limitedValueChanged = this.LimitedValueChanged;
			if (limitedValueChanged == null)
			{
				return;
			}
			limitedValueChanged(this, num);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000293C File Offset: 0x00000B3C
		public int FlowDirectionToIntDirection(FlowDirection flowDirection)
		{
			int result;
			switch (flowDirection)
			{
			case FlowDirection.Any:
				result = 0;
				break;
			case FlowDirection.Bottom:
				result = -this._stride;
				break;
			case FlowDirection.Left:
				result = -1;
				break;
			case FlowDirection.Top:
				result = this._stride;
				break;
			case FlowDirection.Right:
				result = 1;
				break;
			default:
				throw new ArgumentOutOfRangeException("flowDirection", flowDirection, null);
			}
			return result;
		}

		// Token: 0x04000023 RID: 35
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000024 RID: 36
		public readonly Queue<FlowLimiterService.Modification> _modifications = new Queue<FlowLimiterService.Modification>();

		// Token: 0x04000025 RID: 37
		public float[] _limitedValues;

		// Token: 0x04000026 RID: 38
		public int[] _limitedDirections;

		// Token: 0x04000027 RID: 39
		public sbyte[] _flowControllers;

		// Token: 0x04000028 RID: 40
		public float[] _outflowLimits;

		// Token: 0x04000029 RID: 41
		public int _stride;

		// Token: 0x02000010 RID: 16
		public readonly struct Modification
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000030 RID: 48 RVA: 0x00002997 File Offset: 0x00000B97
			public Vector3Int Coordinates { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000031 RID: 49 RVA: 0x0000299F File Offset: 0x00000B9F
			public float FlowLimit { get; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000032 RID: 50 RVA: 0x000029A7 File Offset: 0x00000BA7
			public FlowDirection? FlowDirection { get; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000033 RID: 51 RVA: 0x000029AF File Offset: 0x00000BAF
			public sbyte? FlowController { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000034 RID: 52 RVA: 0x000029B7 File Offset: 0x00000BB7
			public float? OutflowLimit { get; }

			// Token: 0x06000035 RID: 53 RVA: 0x000029C0 File Offset: 0x00000BC0
			public Modification(Vector3Int coordinates, float? flowLimit, FlowDirection? flowDirection, sbyte? flowController, float? outflowLimit)
			{
				this.Coordinates = coordinates;
				this.FlowLimit = (flowLimit ?? float.MinValue);
				this.FlowDirection = flowDirection;
				this.FlowController = flowController;
				this.OutflowLimit = outflowLimit;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002A0C File Offset: 0x00000C0C
			public static FlowLimiterService.Modification CreateFlowModification(Vector3Int coordinates, float flowLimit)
			{
				return new FlowLimiterService.Modification(coordinates, new float?(flowLimit), null, null, null);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002A40 File Offset: 0x00000C40
			public static FlowLimiterService.Modification CreateDirectionModification(Vector3Int coordinates, FlowDirection flowDirection)
			{
				return new FlowLimiterService.Modification(coordinates, null, new FlowDirection?(flowDirection), null, null);
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002A74 File Offset: 0x00000C74
			public static FlowLimiterService.Modification CreateControllerModification(Vector3Int coordinates, sbyte flowController)
			{
				return new FlowLimiterService.Modification(coordinates, null, null, new sbyte?(flowController), null);
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002AA8 File Offset: 0x00000CA8
			public static FlowLimiterService.Modification CreateOutflowLimitModification(Vector3Int coordinates, float outflowLimit)
			{
				return new FlowLimiterService.Modification(coordinates, null, null, null, new float?(outflowLimit));
			}
		}
	}
}
