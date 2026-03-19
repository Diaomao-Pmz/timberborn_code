using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000C RID: 12
	public class DrillScrewRotator : BaseComponent, IAwakableComponent, IUpdatableComponent
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002C30 File Offset: 0x00000E30
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._workshop = base.GetComponent<Workshop>();
			this._drill = base.GetComponent<Drill>();
			this._drillScrewRotatorSpec = base.GetComponent<DrillScrewRotatorSpec>();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C64 File Offset: 0x00000E64
		public void Update()
		{
			float num = this.CalculateRotationSpeed() * Time.deltaTime;
			foreach (Transform transform in this._screwTransforms)
			{
				transform.Rotate(Vector3.up, num);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public void Add(Transform screwTransform)
		{
			this._screwTransforms.Add(screwTransform);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public float CalculateRotationSpeed()
		{
			bool flag = this._drill.Enabled && this._workshop.CurrentlyWorking;
			float minimumRotationSpeed = this._drillScrewRotatorSpec.MinimumRotationSpeed;
			float rotationSpeedPerWorker = this._drillScrewRotatorSpec.RotationSpeedPerWorker;
			return (flag ? (minimumRotationSpeed + (float)this._workshop.NumberOfWorkersWorking * rotationSpeedPerWorker) : 0f) * (float)(this._blockObject.FlipMode.IsUnflipped ? 1 : -1);
		}

		// Token: 0x0400002D RID: 45
		public BlockObject _blockObject;

		// Token: 0x0400002E RID: 46
		public Workshop _workshop;

		// Token: 0x0400002F RID: 47
		public Drill _drill;

		// Token: 0x04000030 RID: 48
		public DrillScrewRotatorSpec _drillScrewRotatorSpec;

		// Token: 0x04000031 RID: 49
		public readonly List<Transform> _screwTransforms = new List<Transform>();
	}
}
