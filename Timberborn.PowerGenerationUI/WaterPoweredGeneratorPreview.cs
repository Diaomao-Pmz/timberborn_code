using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.PowerGeneration;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x0200000F RID: 15
	public class WaterPoweredGeneratorPreview : BaseComponent, IAwakableComponent, IPreviewSelectionListener
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000027C2 File Offset: 0x000009C2
		public WaterPoweredGeneratorPreview(WaterPoweredGeneratorPreviewPanel waterPoweredGeneratorPreviewPanel)
		{
			this._waterPoweredGeneratorPreviewPanel = waterPoweredGeneratorPreviewPanel;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027D1 File Offset: 0x000009D1
		public void Awake()
		{
			this._waterPoweredGenerator = base.GetComponent<WaterPoweredGenerator>();
			this._mechanicalNodeSpec = base.GetComponent<MechanicalNodeSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002804 File Offset: 0x00000A04
		public void OnPreviewSelect()
		{
			if (this._blockObject.Positioned && this._blockObject.IsValid())
			{
				int powerOutput = (int)Math.Abs(this._waterPoweredGenerator.CalculateGeneratedRotation() * (float)this._mechanicalNodeSpec.PowerOutput);
				this._waterPoweredGeneratorPreviewPanel.ShowPreview(powerOutput, this._blockObjectCenter.WorldCenterGrounded);
				return;
			}
			this._waterPoweredGeneratorPreviewPanel.HidePreview();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000286D File Offset: 0x00000A6D
		public void OnPreviewUnselect()
		{
			this._waterPoweredGeneratorPreviewPanel.HidePreview();
		}

		// Token: 0x0400001A RID: 26
		public readonly WaterPoweredGeneratorPreviewPanel _waterPoweredGeneratorPreviewPanel;

		// Token: 0x0400001B RID: 27
		public WaterPoweredGenerator _waterPoweredGenerator;

		// Token: 0x0400001C RID: 28
		public MechanicalNodeSpec _mechanicalNodeSpec;

		// Token: 0x0400001D RID: 29
		public BlockObject _blockObject;

		// Token: 0x0400001E RID: 30
		public BlockObjectCenter _blockObjectCenter;
	}
}
