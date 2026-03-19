using System;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200000B RID: 11
	public class BuildingDetailTexture : BaseComponent, IStartableComponent
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002528 File Offset: 0x00000728
		public void Start()
		{
			BuildingDetailTextureSpec component = base.GetComponent<BuildingDetailTextureSpec>();
			MeshRenderer[] componentsInChildren = base.GetComponent<BuildingModel>().FinishedModel.GetComponentsInChildren<MeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				foreach (Material material in componentsInChildren[i].materials)
				{
					material.SetTexture(BuildingDetailTexture.TextureProperty, component.Texture.Asset);
					material.SetColor(BuildingDetailTexture.ColorProperty, component.Color);
				}
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly int TextureProperty = Shader.PropertyToID("_DetailAlbedoMap2");

		// Token: 0x04000012 RID: 18
		public static readonly int ColorProperty = Shader.PropertyToID("_DetailAlbedoUV2Color");
	}
}
