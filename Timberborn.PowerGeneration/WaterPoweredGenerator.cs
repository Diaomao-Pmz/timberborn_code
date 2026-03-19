using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000011 RID: 17
	public class WaterPoweredGenerator : TickableComponent, IAwakableComponent, IFinishedPostLoadStateListener, IPostPlacementChangeListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000066 RID: 102 RVA: 0x00002980 File Offset: 0x00000B80
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x000029B8 File Offset: 0x00000BB8
		public event EventHandler RotationUpdated;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000029ED File Offset: 0x00000BED
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000029F5 File Offset: 0x00000BF5
		public float GeneratorRotation { get; private set; }

		// Token: 0x0600006A RID: 106 RVA: 0x000029FE File Offset: 0x00000BFE
		public WaterPoweredGenerator(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002A18 File Offset: 0x00000C18
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._waterPoweredGeneratorSpec = base.GetComponent<WaterPoweredGeneratorSpec>();
			base.DisableComponent();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002A44 File Offset: 0x00000C44
		public void OnEnterFinishedPostLoadState()
		{
			this.ValidateWaterDirection();
			this.CalculateCoordinates();
			this.UpdateGenerator();
			base.EnableComponent();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000022C7 File Offset: 0x000004C7
		public void OnExitFinishedPostLoadState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002A5E File Offset: 0x00000C5E
		public void OnPostPlacementChanged()
		{
			this.CalculateCoordinates();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002A66 File Offset: 0x00000C66
		public override void Tick()
		{
			this.UpdateGenerator();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002A70 File Offset: 0x00000C70
		public float CalculateGeneratedRotation()
		{
			float num = 0f;
			for (int i = 0; i < this._groundedCoordinates.Count; i++)
			{
				Vector3Int coordinates = this._groundedCoordinates[i];
				float num2 = this.CalculateGeneratedRotation(coordinates);
				if (Mathf.Abs(num2) > this._waterPoweredGeneratorSpec.MinRequiredOutflow)
				{
					num += num2;
				}
			}
			return num / (float)this._groundedCoordinates.Count;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void CalculateCoordinates()
		{
			this._expectedWaterDirection = this._blockObject.Orientation.Transform(this._waterPoweredGeneratorSpec.ExpectedWaterDirection);
			this._groundedCoordinates.Clear();
			foreach (Vector2Int value in this._waterPoweredGeneratorSpec.Blocks)
			{
				this._groundedCoordinates.Add(this._blockObject.TransformCoordinates(value.XYZ()));
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B5A File Offset: 0x00000D5A
		public void UpdateGenerator()
		{
			this.GeneratorRotation = this.CalculateGeneratedRotation();
			this._mechanicalNode.SetOutputMultiplier(Math.Abs(this.GeneratorRotation));
			EventHandler rotationUpdated = this.RotationUpdated;
			if (rotationUpdated == null)
			{
				return;
			}
			rotationUpdated(this, EventArgs.Empty);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B94 File Offset: 0x00000D94
		public float CalculateGeneratedRotation(Vector3Int coordinates)
		{
			if (this._threadSafeWaterMap.CellIsUnderwater(coordinates))
			{
				Vector2 vector = this._threadSafeWaterMap.WaterFlowDirection(coordinates);
				if (this._expectedWaterDirection.x != 0f)
				{
					return vector.x * (float)Math.Sign(this._expectedWaterDirection.x);
				}
				if (this._expectedWaterDirection.y != 0f)
				{
					return vector.y * (float)Math.Sign(this._expectedWaterDirection.y);
				}
			}
			return 0f;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002C18 File Offset: 0x00000E18
		public void ValidateWaterDirection()
		{
			float x = this._waterPoweredGeneratorSpec.ExpectedWaterDirection.x;
			float y = this._waterPoweredGeneratorSpec.ExpectedWaterDirection.y;
			if ((x != 0f && y != 0f) || (x == 0f && y == 0f))
			{
				throw new InvalidOperationException(base.Name + " should have set either x or y of ExpectedWaterDirection");
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000020 RID: 32
		public BlockObject _blockObject;

		// Token: 0x04000021 RID: 33
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000022 RID: 34
		public WaterPoweredGeneratorSpec _waterPoweredGeneratorSpec;

		// Token: 0x04000023 RID: 35
		public Vector2 _expectedWaterDirection;

		// Token: 0x04000024 RID: 36
		public readonly List<Vector3Int> _groundedCoordinates = new List<Vector3Int>();
	}
}
