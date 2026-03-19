using System;
using UnityEngine;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x0200000B RID: 11
	public static class GameObjectExtensions
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002868 File Offset: 0x00000A68
		public static void ToggleModelVisibility(this GameObject model, bool showModel, bool showShadows)
		{
			if (model)
			{
				GameObjectExtensions.ToggleRenderers(model, showModel, showShadows);
				GameObjectExtensions.ToggleColliders(model, showModel);
				GameObjectExtensions.ToggleLights(model, showModel);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002888 File Offset: 0x00000A88
		public static void ToggleRenderers(GameObject model, bool showModel, bool showShadows)
		{
			foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
			{
				if (showModel || showShadows)
				{
					renderer.enabled = true;
					renderer.shadowCastingMode = (showShadows ? (showModel ? 1 : 3) : 0);
				}
				else
				{
					renderer.enabled = false;
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static void ToggleColliders(GameObject model, bool showModel)
		{
			Collider[] componentsInChildren = model.GetComponentsInChildren<Collider>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = showModel;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002904 File Offset: 0x00000B04
		public static void ToggleLights(GameObject model, bool showModel)
		{
			Light[] componentsInChildren = model.GetComponentsInChildren<Light>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = showModel;
			}
		}
	}
}
