using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x02000007 RID: 7
	public class BlockableBuildingWindAnimator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._windRotationAnimator = base.GetComponent<WindRotationAnimator>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectBlocked += this.OnBlocked;
			this._blockableObject.ObjectUnblocked += this.OnUnblocked;
			if (this._blockableObject.IsUnblocked)
			{
				this._windRotationAnimator.UnsuspendAnimation();
				return;
			}
			this._windRotationAnimator.SuspendAnimation();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002177 File Offset: 0x00000377
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectBlocked -= this.OnBlocked;
			this._blockableObject.ObjectUnblocked -= this.OnUnblocked;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A7 File Offset: 0x000003A7
		public void OnUnblocked(object sender, EventArgs e)
		{
			this._windRotationAnimator.UnsuspendAnimation();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnBlocked(object sender, EventArgs e)
		{
			this._windRotationAnimator.SuspendAnimation();
		}

		// Token: 0x04000008 RID: 8
		public BlockableObject _blockableObject;

		// Token: 0x04000009 RID: 9
		public WindRotationAnimator _windRotationAnimator;
	}
}
