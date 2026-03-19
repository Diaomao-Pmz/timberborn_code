using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000015 RID: 21
	public class SelectableObject : BaseComponent, IAwakableComponent
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000033F0 File Offset: 0x000015F0
		public Vector3 CameraTargetPosition
		{
			get
			{
				ICameraTarget cameraTarget = this._cameraTarget;
				if (cameraTarget == null)
				{
					return base.Transform.position;
				}
				return cameraTarget.CameraTargetPosition;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000340D File Offset: 0x0000160D
		public void Awake()
		{
			this._cameraTarget = base.GetComponent<ICameraTarget>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000341C File Offset: 0x0000161C
		public void OnSelect()
		{
			base.GetComponents<ISelectionListener>(this._selectionListenersCache);
			foreach (ISelectionListener selectionListener in this._selectionListenersCache)
			{
				selectionListener.OnSelect();
			}
			this._selectionListenersCache.Clear();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003484 File Offset: 0x00001684
		public void OnUnselect()
		{
			base.GetComponents<ISelectionListener>(this._selectionListenersCache);
			foreach (ISelectionListener selectionListener in this._selectionListenersCache)
			{
				selectionListener.OnUnselect();
			}
			this._selectionListenersCache.Clear();
		}

		// Token: 0x04000038 RID: 56
		public ICameraTarget _cameraTarget;

		// Token: 0x04000039 RID: 57
		public readonly List<ISelectionListener> _selectionListenersCache = new List<ISelectionListener>();
	}
}
