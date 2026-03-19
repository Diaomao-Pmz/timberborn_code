using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WalkingSystem;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000D RID: 13
	public class ZiplineSwimmingBlocker : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002E93 File Offset: 0x00001093
		public void Awake()
		{
			this._swimmingAnimator = base.GetComponent<SwimmingAnimator>();
			ZiplineVisitor component = base.GetComponent<ZiplineVisitor>();
			component.EnteredZipline += this.OnEnteredZipline;
			component.ExitedZipline += this.OnExitedZipline;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ECA File Offset: 0x000010CA
		public void OnEnteredZipline(object sender, EventArgs e)
		{
			this._swimmingAnimator.BlockSwimmingMovement();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002ED7 File Offset: 0x000010D7
		public void OnExitedZipline(object sender, EventArgs e)
		{
			this._swimmingAnimator.UnblockSwimmingMovement();
		}

		// Token: 0x04000029 RID: 41
		public SwimmingAnimator _swimmingAnimator;
	}
}
