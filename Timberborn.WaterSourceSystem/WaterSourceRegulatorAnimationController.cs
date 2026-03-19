using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000020 RID: 32
	public class WaterSourceRegulatorAnimationController : BaseComponent, IAwakableComponent, IStartableComponent, IUpdatableComponent
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00003A52 File Offset: 0x00001C52
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterSourceRegulator = base.GetComponent<WaterSourceRegulator>();
			this._waterSourceRegulatorAnimationControllerSpec = base.GetComponent<WaterSourceRegulatorAnimationControllerSpec>();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003A78 File Offset: 0x00001C78
		public void Start()
		{
			foreach (RegulatorTransformSpec spec in this._waterSourceRegulatorAnimationControllerSpec.RegulatorTransforms)
			{
				this._regulatorTransforms.Add(RegulatorTransform.Create(base.GameObject, spec, this._waterSourceRegulator.IsOpen));
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public void Update()
		{
			foreach (RegulatorTransform regulatorTransform in this._regulatorTransforms)
			{
				if (this._blockObject.IsFinished)
				{
					regulatorTransform.UpdateSmoothly(this._waterSourceRegulator.IsOpen);
				}
				else
				{
					regulatorTransform.UpdateInstantly(this._waterSourceRegulator.IsOpen);
				}
			}
		}

		// Token: 0x04000055 RID: 85
		public BlockObject _blockObject;

		// Token: 0x04000056 RID: 86
		public WaterSourceRegulator _waterSourceRegulator;

		// Token: 0x04000057 RID: 87
		public WaterSourceRegulatorAnimationControllerSpec _waterSourceRegulatorAnimationControllerSpec;

		// Token: 0x04000058 RID: 88
		public readonly List<RegulatorTransform> _regulatorTransforms = new List<RegulatorTransform>();
	}
}
