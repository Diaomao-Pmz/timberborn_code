using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200001A RID: 26
	public class MaterialLightingEnabler : ILoadableSingleton
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003E65 File Offset: 0x00002065
		public void Load()
		{
			Shader.SetGlobalFloat(MaterialLightingEnabler.LightingStrengthMultiplierId, 1f / MaterialLightingEnabler.LightingStrengthMultiplier);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E7C File Offset: 0x0000207C
		public void EnableLighting(GameObject target, float? strength = 1f)
		{
			MaterialLightingEnabler.SetStrengthInRenderers(target.GetComponentsInChildren<MeshRenderer>(true), strength);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E8B File Offset: 0x0000208B
		public void EnableLighting(BaseComponent entity, float? strength = 1f)
		{
			MaterialLightingEnabler.SetStrengthInRenderers(entity.GetComponent<MaterialLightingRenderers>().Renderers, strength);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EA3 File Offset: 0x000020A3
		public void DisableLighting(BaseComponent entity)
		{
			this.EnableLighting(entity, new float?(0f));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003EB8 File Offset: 0x000020B8
		public static void SetStrengthInRenderers(IReadOnlyList<MeshRenderer> renderers, float? strength)
		{
			uint shaderUserValue = (uint)((strength ?? 1f) * MaterialLightingEnabler.LightingStrengthMultiplier);
			for (int i = 0; i < renderers.Count; i++)
			{
				renderers[i].SetShaderUserValue(shaderUserValue);
			}
		}

		// Token: 0x0400004A RID: 74
		public static readonly int LightingStrengthMultiplierId = Shader.PropertyToID("LightingStrengthMultiplier");

		// Token: 0x0400004B RID: 75
		public static readonly float MaxStrength = 4f;

		// Token: 0x0400004C RID: 76
		public static readonly float LightingStrengthMultiplier = 1f / MaterialLightingEnabler.MaxStrength * 255f;
	}
}
