using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using Timberborn.TextureOperations;
using Timberborn.Timbermesh;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200001C RID: 28
	public class VertexAnimationTextureGenerator : IUnloadableSingleton
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003D84 File Offset: 0x00001F84
		public VertexAnimationTextureGenerator(TextureFactory textureFactory)
		{
			this._textureFactory = textureFactory;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public ValueTuple<Texture, Texture> CreateAnimationTextures(VertexAnimation animation)
		{
			List<VertexAnimationFrame> frames = animation.Frames;
			int animatedVertexCount = animation.AnimatedVertexCount;
			int count = frames.Count;
			byte[] array = new byte[animatedVertexCount * count * VertexAnimationTextureGenerator.PixelByteSize];
			byte[] array2 = new byte[animatedVertexCount * count * VertexAnimationTextureGenerator.PixelByteSize];
			for (int i = 0; i < count; i++)
			{
				VertexAnimationFrame vertexAnimationFrame = frames[i];
				int num = i * animatedVertexCount * VertexAnimationTextureGenerator.PixelByteSize;
				byte[] data = vertexAnimationFrame.VertexProperties.Get(VertexAnimationTextureGenerator.OffsetProperty).Data;
				byte[] data2 = vertexAnimationFrame.VertexProperties.Get(VertexAnimationTextureGenerator.RotationProperty).Data;
				for (int j = 0; j < animatedVertexCount; j++)
				{
					int offset = num + j * VertexAnimationTextureGenerator.PixelByteSize;
					VertexAnimationTextureGenerator.ComputeOffsetBytes(data, j, array, offset);
					VertexAnimationTextureGenerator.ComputeRotationBytes(data2, j, array2, offset);
				}
			}
			Texture item = this.CreateTexture(animatedVertexCount, count, array);
			Texture2D item2 = this.CreateTexture(animatedVertexCount, count, array2);
			return new ValueTuple<Texture, Texture>(item, item2);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003E87 File Offset: 0x00002087
		public void Unload()
		{
			this.CleanupGeneratedTextures();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003E90 File Offset: 0x00002090
		public static void ComputeOffsetBytes(byte[] positions, int index, byte[] offsetBytes, int offset)
		{
			int num = index * 12;
			ushort num2 = VertexAnimationTextureGenerator.FloatToHalf(positions, num);
			ushort num3 = VertexAnimationTextureGenerator.FloatToHalf(positions, num + 4);
			ushort num4 = VertexAnimationTextureGenerator.FloatToHalf(positions, num + 8);
			offsetBytes[offset] = (byte)num2;
			offsetBytes[offset + 1] = (byte)(num2 >> 8);
			offsetBytes[offset + 2] = (byte)num3;
			offsetBytes[offset + 3] = (byte)(num3 >> 8);
			offsetBytes[offset + 4] = (byte)num4;
			offsetBytes[offset + 5] = (byte)(num4 >> 8);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003EEC File Offset: 0x000020EC
		public static void ComputeRotationBytes(byte[] rotations, int index, byte[] rotationBytes, int offset)
		{
			int num = index * 16;
			ushort num2 = VertexAnimationTextureGenerator.FloatToHalf(rotations, num);
			ushort num3 = VertexAnimationTextureGenerator.FloatToHalf(rotations, num + 4);
			ushort num4 = VertexAnimationTextureGenerator.FloatToHalf(rotations, num + 8);
			ushort num5 = VertexAnimationTextureGenerator.FloatToHalf(rotations, num + 12);
			rotationBytes[offset] = (byte)num2;
			rotationBytes[offset + 1] = (byte)(num2 >> 8);
			rotationBytes[offset + 2] = (byte)num3;
			rotationBytes[offset + 3] = (byte)(num3 >> 8);
			rotationBytes[offset + 4] = (byte)num4;
			rotationBytes[offset + 5] = (byte)(num4 >> 8);
			rotationBytes[offset + 6] = (byte)num5;
			rotationBytes[offset + 7] = (byte)(num5 >> 8);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F68 File Offset: 0x00002168
		public static ushort FloatToHalf(byte[] floatBytes, int dataOffset)
		{
			int num = (int)floatBytes[3 + dataOffset] << 24 | (int)floatBytes[2 + dataOffset] << 16 | (int)floatBytes[1 + dataOffset] << 8 | (int)floatBytes[dataOffset];
			int num2 = num >> 16 & 32768;
			int num3 = (num >> 23 & 255) - 112;
			int num4 = num & 8388607;
			if (num3 <= 0)
			{
				if (num3 < -10)
				{
					return 0;
				}
				num4 = (num4 | 8388608) >> 1 - num3;
				if ((num4 & 4096) == 4096)
				{
					num4 += 8192;
				}
				return (ushort)(num2 | num4 >> 13);
			}
			else if (num3 == 143)
			{
				if (num4 == 0)
				{
					return (ushort)(num2 | 31744);
				}
				num4 >>= 13;
				return (ushort)(num2 | 31744 | num4 | ((num4 == 0) ? 0 : 1));
			}
			else
			{
				if ((num4 & 4096) == 4096)
				{
					num4 += 8192;
					if ((num4 & 8388608) == 8388608)
					{
						num4 = 0;
						num3++;
					}
				}
				if (num3 > 30)
				{
					return (ushort)(num2 | 31744);
				}
				return (ushort)(num2 | num3 << 10 | num4 >> 13);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000405C File Offset: 0x0000225C
		public Texture2D CreateTexture(int width, int height, byte[] rawData)
		{
			TextureSettings textureSettings = new TextureSettings.Builder().SetSize(width, height).SetTextureFormat(17).SetGenerateMipmap(false).SetLinear(true).Build();
			Texture2D texture2D = this._textureFactory.CreateTexture(textureSettings);
			texture2D.LoadRawTextureData(rawData);
			texture2D.Apply(false, true);
			this._generatedTextures.Add(texture2D);
			return texture2D;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000040B8 File Offset: 0x000022B8
		public void CleanupGeneratedTextures()
		{
			for (int i = 0; i < this._generatedTextures.Count; i++)
			{
				Object.Destroy(this._generatedTextures[i]);
			}
			this._generatedTextures.Clear();
		}

		// Token: 0x04000057 RID: 87
		public static readonly int PixelByteSize = 8;

		// Token: 0x04000058 RID: 88
		public static readonly string OffsetProperty = "offset";

		// Token: 0x04000059 RID: 89
		public static readonly string RotationProperty = "rotation";

		// Token: 0x0400005A RID: 90
		public readonly TextureFactory _textureFactory;

		// Token: 0x0400005B RID: 91
		public readonly List<Texture> _generatedTextures = new List<Texture>();
	}
}
