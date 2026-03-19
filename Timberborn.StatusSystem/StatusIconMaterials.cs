using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000016 RID: 22
	public class StatusIconMaterials
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002E48 File Offset: 0x00001048
		public void SetMaterial(MeshRenderer renderer, Sprite sprite)
		{
			if (!this._materials.ContainsKey(sprite))
			{
				Material material = new Material(renderer.sharedMaterial);
				material.SetTexture(StatusIconMaterials.BaseMapProperty, sprite.texture);
				this._materials[sprite] = material;
			}
			renderer.sharedMaterial = this._materials[sprite];
		}

		// Token: 0x0400003E RID: 62
		public static readonly int BaseMapProperty = Shader.PropertyToID("_BaseMap");

		// Token: 0x0400003F RID: 63
		public readonly Dictionary<Sprite, Material> _materials = new Dictionary<Sprite, Material>();
	}
}
