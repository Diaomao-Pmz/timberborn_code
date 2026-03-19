using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000C RID: 12
	public class PowerGeneratorSounds : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000024D2 File Offset: 0x000006D2
		public void Awake()
		{
			this._buildingSounds = base.GetComponent<BuildingSounds>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			base.DisableComponent();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024F2 File Offset: 0x000006F2
		public override void Tick()
		{
			this.UpdateSound();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022BF File Offset: 0x000004BF
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000022C7 File Offset: 0x000004C7
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024FA File Offset: 0x000006FA
		public void UpdateSound()
		{
			this._buildingSounds.ToggleSound(this._mechanicalNode.ActiveAndPowered && this._mechanicalNode.OutputMultiplier > 0f);
		}

		// Token: 0x0400000E RID: 14
		public BuildingSounds _buildingSounds;

		// Token: 0x0400000F RID: 15
		public MechanicalNode _mechanicalNode;
	}
}
