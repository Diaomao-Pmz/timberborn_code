using System;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200001C RID: 28
	public readonly struct MaterialProperties : IEquatable<MaterialProperties>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003FFD File Offset: 0x000021FD
		public Color Color { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004005 File Offset: 0x00002205
		public float Grayscale { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000400D File Offset: 0x0000220D
		public Color LightingColor { get; }

		// Token: 0x060000D1 RID: 209 RVA: 0x00004015 File Offset: 0x00002215
		public MaterialProperties(Color color, float grayscale, Color lightingColor)
		{
			this.Color = color;
			this.Grayscale = grayscale;
			this.LightingColor = lightingColor;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000402C File Offset: 0x0000222C
		public bool Equals(MaterialProperties other)
		{
			return this.Color.Equals(other.Color) && this.Grayscale.Equals(other.Grayscale) && this.LightingColor.Equals(other.LightingColor);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004080 File Offset: 0x00002280
		public override bool Equals(object obj)
		{
			if (obj is MaterialProperties)
			{
				MaterialProperties other = (MaterialProperties)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000040A5 File Offset: 0x000022A5
		public override int GetHashCode()
		{
			return HashCode.Combine<Color, float, Color>(this.Color, this.Grayscale, this.LightingColor);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000040BE File Offset: 0x000022BE
		public static bool operator ==(MaterialProperties left, MaterialProperties right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000040C8 File Offset: 0x000022C8
		public static bool operator !=(MaterialProperties left, MaterialProperties right)
		{
			return !left.Equals(right);
		}
	}
}
