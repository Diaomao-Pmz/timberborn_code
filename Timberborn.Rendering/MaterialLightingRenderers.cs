using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200001B RID: 27
	public class MaterialLightingRenderers : BaseComponent, IAwakableComponent
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003F35 File Offset: 0x00002135
		public ReadOnlyList<MeshRenderer> Renderers
		{
			get
			{
				return this._renderers.AsReadOnlyList<MeshRenderer>();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F42 File Offset: 0x00002142
		public void Awake()
		{
			this.CollectRenderers();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003F4C File Offset: 0x0000214C
		public void CollectRenderers()
		{
			this._renderers.Clear();
			base.GameObject.GetComponentsInChildren<MeshRenderer>(true, this._renderers);
			foreach (MeshRenderer item in this._disabledRenderers)
			{
				this._renderers.Remove(item);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003FC4 File Offset: 0x000021C4
		public void DisableRendering(MeshRenderer renderer)
		{
			this._disabledRenderers.Add(renderer);
			this._renderers.Remove(renderer);
		}

		// Token: 0x0400004D RID: 77
		public readonly List<MeshRenderer> _renderers = new List<MeshRenderer>();

		// Token: 0x0400004E RID: 78
		public readonly List<MeshRenderer> _disabledRenderers = new List<MeshRenderer>();
	}
}
