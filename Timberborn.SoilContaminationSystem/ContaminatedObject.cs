using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x02000007 RID: 7
	public class ContaminatedObject : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler EnteredContaminatedState;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler ExitedContaminatedState;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021E5 File Offset: 0x000003E5
		public bool IsContaminated { get; private set; }

		// Token: 0x0600000D RID: 13 RVA: 0x000021EE File Offset: 0x000003EE
		public ContaminatedObject(ISoilContaminationService soilContaminationService)
		{
			this._soilContaminationService = soilContaminationService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021FD File Offset: 0x000003FD
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000220B File Offset: 0x0000040B
		public void InitializeEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				if (this.IsOnContaminatedSoil)
				{
					this.InternalEnterContaminatedState();
					return;
				}
				this.InternalExitContaminatedState();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222F File Offset: 0x0000042F
		public void EnterContaminatedState()
		{
			if (!this.IsContaminated)
			{
				this.InternalEnterContaminatedState();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000223F File Offset: 0x0000043F
		public void ExitContaminatedState()
		{
			if (this.IsContaminated)
			{
				this.InternalExitContaminatedState();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224F File Offset: 0x0000044F
		public bool IsOnContaminatedSoil
		{
			get
			{
				return this._soilContaminationService.SoilIsContaminated(this._blockObject.Coordinates);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002267 File Offset: 0x00000467
		public void InternalEnterContaminatedState()
		{
			this.IsContaminated = true;
			EventHandler enteredContaminatedState = this.EnteredContaminatedState;
			if (enteredContaminatedState == null)
			{
				return;
			}
			enteredContaminatedState(this, EventArgs.Empty);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002286 File Offset: 0x00000486
		public void InternalExitContaminatedState()
		{
			this.IsContaminated = false;
			EventHandler exitedContaminatedState = this.ExitedContaminatedState;
			if (exitedContaminatedState == null)
			{
				return;
			}
			exitedContaminatedState(this, EventArgs.Empty);
		}

		// Token: 0x0400000B RID: 11
		public readonly ISoilContaminationService _soilContaminationService;

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;
	}
}
