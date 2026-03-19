using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Illumination;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001E RID: 30
	public class ZiplineTowerIlluminator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000464C File Offset: 0x0000284C
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._ziplineTowerOperationValidator = base.GetComponent<ZiplineTowerOperationValidator>();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000466B File Offset: 0x0000286B
		public void OnEnterFinishedState()
		{
			this._ziplineTowerOperationValidator.OperativeStateChanged += this.OnOperativeStateChanged;
			this.UpdateIlluminator();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000468A File Offset: 0x0000288A
		public void OnExitFinishedState()
		{
			this._ziplineTowerOperationValidator.OperativeStateChanged -= this.OnOperativeStateChanged;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046A3 File Offset: 0x000028A3
		public void OnOperativeStateChanged(object sender, EventArgs e)
		{
			this.UpdateIlluminator();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000046AB File Offset: 0x000028AB
		public void UpdateIlluminator()
		{
			if (this._ziplineTowerOperationValidator.IsOperative)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x0400005D RID: 93
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400005E RID: 94
		public ZiplineTowerOperationValidator _ziplineTowerOperationValidator;
	}
}
