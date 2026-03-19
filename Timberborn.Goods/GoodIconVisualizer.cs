using System;
using UnityEngine;

namespace Timberborn.Goods
{
	// Token: 0x0200000C RID: 12
	public class GoodIconVisualizer
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002744 File Offset: 0x00000944
		public void ShowIcon(Material material, GoodSpec goodSpec)
		{
			this.ShowIcon(material, goodSpec, goodSpec.ContainerColor);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002754 File Offset: 0x00000954
		public void ShowIcon(Material material, GoodSpec goodSpec, Color color)
		{
			GoodIconVisualizer.UpdateIconParameters(material, goodSpec, false, GoodIconVisualizer.ColorProperty, color);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002764 File Offset: 0x00000964
		public void ShowColoredIcon(Material material, GoodSpec goodSpec, bool flipped, Color color)
		{
			GoodIconVisualizer.UpdateIconParameters(material, goodSpec, flipped, GoodIconVisualizer.IconColorProperty, color);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002775 File Offset: 0x00000975
		public void HideColoredIcon(Material material)
		{
			GoodIconVisualizer.ClearIconParameters(material, GoodIconVisualizer.IconColorProperty);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002784 File Offset: 0x00000984
		public static void UpdateIconParameters(Material material, GoodSpec goodSpec, bool flipped, int colorProperty, Color color)
		{
			if (color.a > 0f)
			{
				Sprite sprite = flipped ? goodSpec.IconFlipped.Value : goodSpec.Icon.Asset;
				GoodIconVisualizer.SetIconParameters(material, sprite.texture, colorProperty, color);
				return;
			}
			GoodIconVisualizer.ClearIconParameters(material, colorProperty);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027D2 File Offset: 0x000009D2
		public static void SetIconParameters(Material material, Texture texture, int colorProperty, Color color)
		{
			material.SetTexture(GoodIconVisualizer.TextureProperty, texture);
			material.SetColor(colorProperty, color);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027E8 File Offset: 0x000009E8
		public static void ClearIconParameters(Material material, int colorProperty)
		{
			GoodIconVisualizer.SetIconParameters(material, Texture2D.blackTexture, colorProperty, Color.white);
		}

		// Token: 0x04000015 RID: 21
		public static readonly int ColorProperty = Shader.PropertyToID("_Color");

		// Token: 0x04000016 RID: 22
		public static readonly int IconColorProperty = Shader.PropertyToID("_DetailAlbedoUV2Color");

		// Token: 0x04000017 RID: 23
		public static readonly int TextureProperty = Shader.PropertyToID("_DetailAlbedoMap2");
	}
}
