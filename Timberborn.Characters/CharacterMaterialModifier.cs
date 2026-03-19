using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Characters
{
	// Token: 0x0200000A RID: 10
	public class CharacterMaterialModifier : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000235D File Offset: 0x0000055D
		public void Awake()
		{
			this._meshRenderer = base.GetComponentInChildren<Renderer>(true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236C File Offset: 0x0000056C
		public void DeleteEntity()
		{
			Object.Destroy(this._meshRenderer.material);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000237E File Offset: 0x0000057E
		public void SetColor(int propertyId, Color color)
		{
			this._meshRenderer.material.SetColor(propertyId, color);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002392 File Offset: 0x00000592
		public void SetFloat(int propertyId, float value)
		{
			this._meshRenderer.material.SetFloat(propertyId, value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A6 File Offset: 0x000005A6
		public void SetTexture(int propertyId, Texture texture)
		{
			this._meshRenderer.material.SetTexture(propertyId, texture);
		}

		// Token: 0x04000015 RID: 21
		public Renderer _meshRenderer;
	}
}
