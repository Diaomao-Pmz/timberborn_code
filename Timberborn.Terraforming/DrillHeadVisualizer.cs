using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x02000008 RID: 8
	public class DrillHeadVisualizer : BaseComponent, IAwakableComponent, IUpdatableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002394 File Offset: 0x00000594
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._drill = base.GetComponent<Drill>();
			this._drillHeadVisualizerSpec = base.GetComponent<DrillHeadVisualizerSpec>();
			this._headTransform = base.GameObject.FindChildTransform(this._drillHeadVisualizerSpec.HeadTransformName);
			base.DisableComponent();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E7 File Offset: 0x000005E7
		public void InitializeEntity()
		{
			base.GetComponent<DrillScrewRotator>().Add(this._headTransform);
			if (this._blockObject.IsFinished)
			{
				this._startedFinished = true;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000240E File Offset: 0x0000060E
		public void Update()
		{
			this.UpdateHeadPosition();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002416 File Offset: 0x00000616
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			if (this._startedFinished)
			{
				this._headTransform.position = this.GetTargetPosition();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002437 File Offset: 0x00000637
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002440 File Offset: 0x00000640
		public void UpdateHeadPosition()
		{
			Vector3 targetPosition = this.GetTargetPosition();
			Vector3 position = this._headTransform.position;
			if (Math.Abs(targetPosition.y - position.y) > 0.0001f)
			{
				this._headTransform.position = Vector3.MoveTowards(position, targetPosition, Time.deltaTime);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002490 File Offset: 0x00000690
		public Vector3 GetTargetPosition()
		{
			float y = CoordinateSystem.GridToWorldCentered(new Vector3Int(0, 0, this._drill.DrillingLevel)).y;
			Vector3 position = this._headTransform.position;
			float headOffset = this._drillHeadVisualizerSpec.HeadOffset;
			return new Vector3(position.x, y + headOffset, position.z);
		}

		// Token: 0x04000013 RID: 19
		public BlockObject _blockObject;

		// Token: 0x04000014 RID: 20
		public Drill _drill;

		// Token: 0x04000015 RID: 21
		public DrillHeadVisualizerSpec _drillHeadVisualizerSpec;

		// Token: 0x04000016 RID: 22
		public Transform _headTransform;

		// Token: 0x04000017 RID: 23
		public bool _startedFinished;
	}
}
