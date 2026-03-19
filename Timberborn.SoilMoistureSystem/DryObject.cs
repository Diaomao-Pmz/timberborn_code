using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000008 RID: 8
	public class DryObject : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002328 File Offset: 0x00000528
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002360 File Offset: 0x00000560
		public event EventHandler EnteredDryState;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x000023D0 File Offset: 0x000005D0
		public event EventHandler ExitedDryState;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002405 File Offset: 0x00000605
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000240D File Offset: 0x0000060D
		public bool IsDry { get; private set; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002416 File Offset: 0x00000616
		public DryObject(ISoilMoistureService soilMoistureService)
		{
			this._soilMoistureService = soilMoistureService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002425 File Offset: 0x00000625
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002433 File Offset: 0x00000633
		public void InitializeEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				if (this.IsOnMoistSoil)
				{
					this.InternalExitDryState();
					return;
				}
				this.InternalEnterDryState();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002457 File Offset: 0x00000657
		public void EnterDryState()
		{
			if (!this.IsDry)
			{
				this.InternalEnterDryState();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002467 File Offset: 0x00000667
		public void ExitDryState()
		{
			if (this.IsDry)
			{
				this.InternalExitDryState();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002477 File Offset: 0x00000677
		public bool IsOnMoistSoil
		{
			get
			{
				return this._soilMoistureService.SoilIsMoist(this._blockObject.CoordinatesAtBaseZ);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000248F File Offset: 0x0000068F
		public void InternalEnterDryState()
		{
			this.IsDry = true;
			EventHandler enteredDryState = this.EnteredDryState;
			if (enteredDryState == null)
			{
				return;
			}
			enteredDryState(this, EventArgs.Empty);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024AE File Offset: 0x000006AE
		public void InternalExitDryState()
		{
			this.IsDry = false;
			EventHandler exitedDryState = this.ExitedDryState;
			if (exitedDryState == null)
			{
				return;
			}
			exitedDryState(this, EventArgs.Empty);
		}

		// Token: 0x04000013 RID: 19
		public readonly ISoilMoistureService _soilMoistureService;

		// Token: 0x04000014 RID: 20
		public BlockObject _blockObject;
	}
}
