using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200000E RID: 14
	public class FinishedStateLightingEnforcer : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002A23 File Offset: 0x00000C23
		public FinishedStateLightingEnforcer(MaterialColorer materialColorer)
		{
			this._materialColorer = materialColorer;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A32 File Offset: 0x00000C32
		public void Awake()
		{
			this._finishedStateLightingEnforcerSpec = base.GetComponent<FinishedStateLightingEnforcerSpec>();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A40 File Offset: 0x00000C40
		public void OnEnterFinishedState()
		{
			foreach (string childName in this._finishedStateLightingEnforcerSpec.ChildrenNames)
			{
				GameObject root = base.GameObject.FindChild(childName);
				this._materialColorer.EnableLightingAndDisableChanges(this, root);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A8E File Offset: 0x00000C8E
		public void OnExitFinishedState()
		{
		}

		// Token: 0x0400001C RID: 28
		public readonly MaterialColorer _materialColorer;

		// Token: 0x0400001D RID: 29
		public FinishedStateLightingEnforcerSpec _finishedStateLightingEnforcerSpec;
	}
}
