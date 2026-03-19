using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;
using Timberborn.WindSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000013 RID: 19
	public class WindPoweredGenerator : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002EC1 File Offset: 0x000010C1
		public WindPoweredGenerator(WindService windService)
		{
			this._windService = windService;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void Awake()
		{
			this._windPoweredGeneratorSpec = base.GetComponent<WindPoweredGeneratorSpec>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002EEA File Offset: 0x000010EA
		public void InitializeEntity()
		{
			this.UpdateOutput();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002EEA File Offset: 0x000010EA
		public override void Tick()
		{
			this.UpdateOutput();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002EF2 File Offset: 0x000010F2
		public void UpdateOutput()
		{
			this._mechanicalNode.SetOutputMultiplier((this._windService.WindStrength > this._windPoweredGeneratorSpec.MinRequiredWindStrength) ? this._windService.WindStrength : 0f);
		}

		// Token: 0x04000028 RID: 40
		public readonly WindService _windService;

		// Token: 0x04000029 RID: 41
		public WindPoweredGeneratorSpec _windPoweredGeneratorSpec;

		// Token: 0x0400002A RID: 42
		public MechanicalNode _mechanicalNode;
	}
}
