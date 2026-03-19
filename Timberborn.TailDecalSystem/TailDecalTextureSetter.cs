using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using UnityEngine;

namespace Timberborn.TailDecalSystem
{
	// Token: 0x0200000C RID: 12
	public class TailDecalTextureSetter : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002574 File Offset: 0x00000774
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002582 File Offset: 0x00000782
		public void SetTexture(Texture texture)
		{
			this._characterMaterialModifier.SetTexture(TailDecalTextureSetter.DecalDiffusePropertyId, texture);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002595 File Offset: 0x00000795
		public void ClearDecalTexture()
		{
			this._characterMaterialModifier.SetTexture(TailDecalTextureSetter.DecalDiffusePropertyId, null);
		}

		// Token: 0x04000014 RID: 20
		public static readonly int DecalDiffusePropertyId = Shader.PropertyToID("_TailDecalDiffuse");

		// Token: 0x04000015 RID: 21
		public CharacterMaterialModifier _characterMaterialModifier;
	}
}
