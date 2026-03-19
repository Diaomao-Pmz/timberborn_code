using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;

namespace Timberborn.Illumination
{
	// Token: 0x02000007 RID: 7
	public class BlockableIlluminator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			if (this._blockableObject.IsUnblocked)
			{
				this._illuminatorToggle.TurnOn();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002173 File Offset: 0x00000373
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AE File Offset: 0x000003AE
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this._illuminatorToggle.TurnOn();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BB File Offset: 0x000003BB
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000008 RID: 8
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x04000009 RID: 9
		public BlockableObject _blockableObject;
	}
}
