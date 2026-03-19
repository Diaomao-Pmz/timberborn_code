using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000E RID: 14
	public class LayeredBlockObstacleVisualizer : TickableComponent, IAwakableComponent, IUpdatableComponent
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002EAD File Offset: 0x000010AD
		public LayeredBlockObstacleVisualizer(ITickService tickService)
		{
			this._tickService = tickService;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002EBC File Offset: 0x000010BC
		public void Awake()
		{
			this._obstacle = base.GetComponent<LayeredBlockObstacle>();
			LayeredBlockObstacleVisualizerSpec component = base.GetComponent<LayeredBlockObstacleVisualizerSpec>();
			this._positionTransform = base.GameObject.FindChildTransform(component.PositionTransformName);
			this._scaleTransform = base.GameObject.FindChildTransform(component.ScaleTransformName);
			this._originalPosition = this._positionTransform.localPosition;
			this._originalScale = this._scaleTransform.localScale;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F2C File Offset: 0x0000112C
		public void Update()
		{
			this.UpdateTransforms();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F34 File Offset: 0x00001134
		public override void StartTickable()
		{
			this.UpdatePositionAndScale(this._obstacle.OccupancyRange);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F47 File Offset: 0x00001147
		public override void Tick()
		{
			this.UpdateOccupancyChangeRate();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F50 File Offset: 0x00001150
		public void UpdateTransforms()
		{
			float num = this._originalPosition.y - this._positionTransform.localPosition.y;
			float occupancyRange = this._obstacle.OccupancyRange;
			if (Math.Abs(num - occupancyRange) > LayeredBlockObstacleVisualizer.MaxOccupancyRangeDifference)
			{
				this.UpdatePositionAndScale(occupancyRange);
				return;
			}
			this.Interpolate(num, occupancyRange);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void UpdateOccupancyChangeRate()
		{
			float val = Math.Abs((this._obstacle.OccupancyRange - this._previousOccupancyRange) / this._tickService.TickIntervalInSeconds);
			this._occupancyChangeRate = Math.Max(LayeredBlockObstacleVisualizer.MinimumChangeRate, val);
			this._previousOccupancyRange = this._obstacle.OccupancyRange;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FFC File Offset: 0x000011FC
		public void Interpolate(float currentOccupancyRange, float targetOccupancyRange)
		{
			float newOccupancyRange = Mathf.MoveTowards(currentOccupancyRange, targetOccupancyRange, Time.deltaTime * this._occupancyChangeRate);
			this.UpdatePositionAndScale(newOccupancyRange);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003024 File Offset: 0x00001224
		public void UpdatePositionAndScale(float newOccupancyRange)
		{
			this._positionTransform.localPosition = this._originalPosition - new Vector3(0f, newOccupancyRange, 0f);
			this._scaleTransform.localScale = this._originalScale + new Vector3(0f, newOccupancyRange, 0f);
		}

		// Token: 0x04000021 RID: 33
		public static readonly float MinimumChangeRate = 0.001f;

		// Token: 0x04000022 RID: 34
		public static readonly float MaxOccupancyRangeDifference = 0.25f;

		// Token: 0x04000023 RID: 35
		public readonly ITickService _tickService;

		// Token: 0x04000024 RID: 36
		public LayeredBlockObstacle _obstacle;

		// Token: 0x04000025 RID: 37
		public Transform _positionTransform;

		// Token: 0x04000026 RID: 38
		public Transform _scaleTransform;

		// Token: 0x04000027 RID: 39
		public Vector3 _originalPosition;

		// Token: 0x04000028 RID: 40
		public Vector3 _originalScale;

		// Token: 0x04000029 RID: 41
		public float _previousOccupancyRange;

		// Token: 0x0400002A RID: 42
		public float _occupancyChangeRate;
	}
}
