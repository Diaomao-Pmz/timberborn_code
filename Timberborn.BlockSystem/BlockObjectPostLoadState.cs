using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000015 RID: 21
	public class BlockObjectPostLoadState : BaseComponent, IAwakableComponent, IPostLoadableEntity, IFinishedStateListener
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000035EE File Offset: 0x000017EE
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000035FC File Offset: 0x000017FC
		public void PostLoadEntity()
		{
			if (this._blockObject.IsFinished)
			{
				this.NotifyEnter();
			}
			this._postLoaded = true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003618 File Offset: 0x00001818
		public void OnEnterFinishedState()
		{
			if (this._postLoaded)
			{
				this.NotifyEnter();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003628 File Offset: 0x00001828
		public void OnExitFinishedState()
		{
			if (this._postLoaded)
			{
				this.NotifyExit();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003638 File Offset: 0x00001838
		public void NotifyEnter()
		{
			foreach (IFinishedPostLoadStateListener finishedPostLoadStateListener in base.GetComponentsAllocating<IFinishedPostLoadStateListener>())
			{
				finishedPostLoadStateListener.OnEnterFinishedPostLoadState();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003688 File Offset: 0x00001888
		public void NotifyExit()
		{
			foreach (IFinishedPostLoadStateListener finishedPostLoadStateListener in base.GetComponentsAllocating<IFinishedPostLoadStateListener>())
			{
				finishedPostLoadStateListener.OnExitFinishedPostLoadState();
			}
		}

		// Token: 0x0400005C RID: 92
		public BlockObject _blockObject;

		// Token: 0x0400005D RID: 93
		public bool _postLoaded;
	}
}
