using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200002D RID: 45
	public class NineSliceBackground
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003ABE File Offset: 0x00001CBE
		public bool IsNineSlice
		{
			get
			{
				return this._image != null;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003ACC File Offset: 0x00001CCC
		public void GetDataFromStyle(ICustomStyle customStyle)
		{
			string text;
			this._image = (customStyle.TryGetValue(NineSliceBackground.BackgroundImageProperty, ref text) ? Resources.Load<Sprite>(text) : null);
			Color color;
			this._tint = (customStyle.TryGetValue(NineSliceBackground.BackgroundTintProperty, ref color) ? color : Color.white);
			int num;
			customStyle.TryGetValue(NineSliceBackground.BackgroundSliceProperty, ref num);
			int num2;
			this._sliceTop = (customStyle.TryGetValue(NineSliceBackground.BackgroundSliceTopProperty, ref num2) ? num2 : num);
			int num3;
			this._sliceRight = (customStyle.TryGetValue(NineSliceBackground.BackgroundSliceRightProperty, ref num3) ? num3 : num);
			int num4;
			this._sliceBottom = (customStyle.TryGetValue(NineSliceBackground.BackgroundSliceBottomProperty, ref num4) ? num4 : num);
			int num5;
			this._sliceLeft = (customStyle.TryGetValue(NineSliceBackground.BackgroundSliceLeftProperty, ref num5) ? num5 : num);
			float num6;
			this._sliceScale = (customStyle.TryGetValue(NineSliceBackground.BackgroundSliceScaleProperty, ref num6) ? num6 : 1f);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003BAC File Offset: 0x00001DAC
		public void GenerateVisualContent(MeshGenerationContext mgc, Rect paddingRect)
		{
			if (this.IsNineSlice)
			{
				MeshWriter meshWriter = default(MeshWriter);
				this.WriteMesh(ref meshWriter, paddingRect);
				meshWriter.StartWriting(mgc, this._image.texture);
				this.WriteMesh(ref meshWriter, paddingRect);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public void WriteMesh(ref MeshWriter meshWriter, Rect paddingRect)
		{
			Rect rect = paddingRect;
			float width = rect.width;
			float height = rect.height;
			if (width > 0.01f && height > 0.01f)
			{
				Texture2D texture = this._image.texture;
				int width2 = texture.width;
				int height2 = texture.height;
				float num = (float)this._sliceTop * this._sliceScale;
				float num2 = (float)this._sliceRight * this._sliceScale;
				float num3 = (float)this._sliceBottom * this._sliceScale;
				float num4 = (float)this._sliceLeft * this._sliceScale;
				float num5 = Mathf.Min(width / (num4 + num2), 1f);
				float num6 = Mathf.Min(height / (num + num3), 1f);
				float num7 = num * num6;
				float num8 = num2 * num5;
				float num9 = num3 * num6;
				float num10 = num4 * num5;
				float num11 = (float)this._sliceTop / (float)height2 * num6;
				float num12 = (float)this._sliceRight / (float)width2 * num5;
				float num13 = (float)this._sliceBottom / (float)height2 * num6;
				float num14 = (float)this._sliceLeft / (float)width2 * num5;
				bool flag = num5 >= 1f;
				bool flag2 = num6 >= 1f;
				int num15 = width2 - this._sliceLeft - this._sliceRight;
				int num16 = height2 - this._sliceTop - this._sliceBottom;
				float num17 = width - num4 - num2;
				float num18 = height - num - num3;
				float num19 = num17 / (this._sliceScale * (float)num15);
				float num20 = num18 / (this._sliceScale * (float)num16);
				int num21 = flag ? Math.Max(Mathf.RoundToInt(num19), 1) : 0;
				int num22 = flag2 ? Math.Max(Mathf.RoundToInt(num20), 1) : 0;
				float num23 = flag ? (num17 / (float)num21) : 0f;
				float num24 = flag2 ? (num18 / (float)num22) : 0f;
				this.AddRectangle(ref meshWriter, 0f, 0f, num10, num7, 0f, 1f, num14, 1f - num11);
				this.AddRectangle(ref meshWriter, width - num8, 0f, width, num7, 1f - num12, 1f, 1f, 1f - num11);
				this.AddRectangle(ref meshWriter, 0f, height - num9, num10, height, 0f, num13, num14, 0f);
				this.AddRectangle(ref meshWriter, width - num8, height - num9, width, height, 1f - num12, num13, 1f, 0f);
				for (int i = 0; i < num21; i++)
				{
					this.AddRectangle(ref meshWriter, num10 + (float)i * num23, 0f, num10 + (float)(i + 1) * num23, num7, num14, 1f, 1f - num12, 1f - num11);
				}
				for (int j = 0; j < num21; j++)
				{
					this.AddRectangle(ref meshWriter, num10 + (float)j * num23, height - num9, num10 + (float)(j + 1) * num23, height, num14, num13, 1f - num12, 0f);
				}
				for (int k = 0; k < num22; k++)
				{
					this.AddRectangle(ref meshWriter, 0f, num7 + (float)k * num24, num10, num7 + (float)(k + 1) * num24, 0f, 1f - num11, num14, num13);
				}
				for (int l = 0; l < num22; l++)
				{
					this.AddRectangle(ref meshWriter, width - num8, num7 + (float)l * num24, width, num7 + (float)(l + 1) * num24, 1f - num12, 1f - num11, 1f, num13);
				}
				for (int m = 0; m < num22; m++)
				{
					for (int n = 0; n < num21; n++)
					{
						this.AddRectangle(ref meshWriter, num10 + (float)n * num23, num7 + (float)m * num24, num10 + (float)(n + 1) * num23, num7 + (float)(m + 1) * num24, num14, 1f - num11, 1f - num12, num13);
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003FD8 File Offset: 0x000021D8
		public void AddRectangle(ref MeshWriter meshWriter, float x0, float y0, float x1, float y1, float u0, float v0, float u1, float v1)
		{
			if (x1 - x0 > 0.001f && y1 - y0 > 0.001f)
			{
				Vertex vertex = default(Vertex);
				vertex.position = new Vector3(x0, y0, Vertex.nearZ);
				vertex.uv = new Vector2(u0, v0);
				vertex.tint = this._tint;
				Vertex nextVertex = vertex;
				vertex = default(Vertex);
				vertex.position = new Vector3(x1, y0, Vertex.nearZ);
				vertex.uv = new Vector2(u1, v0);
				vertex.tint = this._tint;
				Vertex nextVertex2 = vertex;
				vertex = default(Vertex);
				vertex.position = new Vector3(x1, y1, Vertex.nearZ);
				vertex.uv = new Vector2(u1, v1);
				vertex.tint = this._tint;
				Vertex nextVertex3 = vertex;
				vertex = default(Vertex);
				vertex.position = new Vector3(x0, y1, Vertex.nearZ);
				vertex.uv = new Vector2(u0, v1);
				vertex.tint = this._tint;
				Vertex nextVertex4 = vertex;
				int vertexCount = meshWriter.VertexCount;
				meshWriter.SetNextVertex(nextVertex);
				meshWriter.SetNextVertex(nextVertex2);
				meshWriter.SetNextVertex(nextVertex3);
				meshWriter.SetNextVertex(nextVertex4);
				meshWriter.SetNextIndex((ushort)vertexCount);
				meshWriter.SetNextIndex((ushort)(vertexCount + 1));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 3));
				meshWriter.SetNextIndex((ushort)vertexCount);
			}
		}

		// Token: 0x04000062 RID: 98
		public static readonly CustomStyleProperty<string> BackgroundImageProperty = new CustomStyleProperty<string>("--background-image");

		// Token: 0x04000063 RID: 99
		public static readonly CustomStyleProperty<Color> BackgroundTintProperty = new CustomStyleProperty<Color>("--background-tint");

		// Token: 0x04000064 RID: 100
		public static readonly CustomStyleProperty<int> BackgroundSliceProperty = new CustomStyleProperty<int>("--background-slice");

		// Token: 0x04000065 RID: 101
		public static readonly CustomStyleProperty<int> BackgroundSliceTopProperty = new CustomStyleProperty<int>("--background-slice-top");

		// Token: 0x04000066 RID: 102
		public static readonly CustomStyleProperty<int> BackgroundSliceRightProperty = new CustomStyleProperty<int>("--background-slice-right");

		// Token: 0x04000067 RID: 103
		public static readonly CustomStyleProperty<int> BackgroundSliceBottomProperty = new CustomStyleProperty<int>("--background-slice-bottom");

		// Token: 0x04000068 RID: 104
		public static readonly CustomStyleProperty<int> BackgroundSliceLeftProperty = new CustomStyleProperty<int>("--background-slice-left");

		// Token: 0x04000069 RID: 105
		public static readonly CustomStyleProperty<float> BackgroundSliceScaleProperty = new CustomStyleProperty<float>("--background-slice-scale");

		// Token: 0x0400006A RID: 106
		public Sprite _image;

		// Token: 0x0400006B RID: 107
		public Color32 _tint;

		// Token: 0x0400006C RID: 108
		public int _sliceBottom;

		// Token: 0x0400006D RID: 109
		public int _sliceLeft;

		// Token: 0x0400006E RID: 110
		public int _sliceRight;

		// Token: 0x0400006F RID: 111
		public float _sliceScale;

		// Token: 0x04000070 RID: 112
		public int _sliceTop;
	}
}
