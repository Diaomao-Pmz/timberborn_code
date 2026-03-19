using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000C RID: 12
	public class BlockableObjectVisualizer : BaseComponent, IAwakableComponent, IFinishedStateListener, IPreviewStateListener
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002658 File Offset: 0x00000858
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			string hideableObjectName = base.GetComponent<BlockableObjectVisualizerSpec>().HideableObjectName;
			this._hideableObject = base.GameObject.FindChild(hideableObjectName);
			base.DisableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000269B File Offset: 0x0000089B
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this.UpdateVisualization();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026D7 File Offset: 0x000008D7
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000270D File Offset: 0x0000090D
		public void OnEnterPreviewState()
		{
			base.DisableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000271B File Offset: 0x0000091B
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000271B File Offset: 0x0000091B
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002723 File Offset: 0x00000923
		public void UpdateVisualization()
		{
			this._hideableObject.SetActive(this._blockableObject.IsUnblocked && base.Enabled);
		}

		// Token: 0x04000012 RID: 18
		public BlockableObject _blockableObject;

		// Token: 0x04000013 RID: 19
		public GameObject _hideableObject;
	}
}
