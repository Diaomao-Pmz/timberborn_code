using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000007 RID: 7
	public class BlockableFloodableObject : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._floodableObject = base.GetComponent<FloodableObject>();
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public void OnEnterFinishedState()
		{
			if (this._floodableObject.IsFlooded)
			{
				this.BlockBuilding();
			}
			this._floodableObject.Flooded += this.OnFlooded;
			this._floodableObject.Unflooded += this.OnUnflooded;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216A File Offset: 0x0000036A
		public void OnExitFinishedState()
		{
			this._floodableObject.Flooded -= this.OnFlooded;
			this._floodableObject.Unflooded -= this.OnUnflooded;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219A File Offset: 0x0000039A
		public void OnFlooded(object sender, EventArgs e)
		{
			this.BlockBuilding();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A2 File Offset: 0x000003A2
		public void OnUnflooded(object sender, EventArgs e)
		{
			this.UnblockBuilding();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021AA File Offset: 0x000003AA
		public void BlockBuilding()
		{
			this._blockableObject.Block(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B8 File Offset: 0x000003B8
		public void UnblockBuilding()
		{
			this._blockableObject.Unblock(this);
		}

		// Token: 0x04000008 RID: 8
		public FloodableObject _floodableObject;

		// Token: 0x04000009 RID: 9
		public BlockableObject _blockableObject;
	}
}
