using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Gathering
{
	// Token: 0x02000008 RID: 8
	public class GatherableModel : BaseComponent, IInitializableEntity
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002303 File Offset: 0x00000503
		public void InitializeEntity()
		{
			base.GameObject.FindChild("Mature").GetComponentsInChildren<MeshRenderer>(true, this._meshRenderers);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002324 File Offset: 0x00000524
		public void UpdateMaterial(bool showYield)
		{
			foreach (MeshRenderer meshRenderer in this._meshRenderers)
			{
				meshRenderer.material.SetFloat(GatherableModel.EnableDetailId, showYield ? 1f : 0f);
			}
		}

		// Token: 0x0400000F RID: 15
		public static readonly int EnableDetailId = Shader.PropertyToID("_EnableDetail");

		// Token: 0x04000010 RID: 16
		public readonly List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();
	}
}
