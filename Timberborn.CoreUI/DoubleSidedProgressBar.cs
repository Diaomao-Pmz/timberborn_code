using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000012 RID: 18
	[UxmlElement]
	public class DoubleSidedProgressBar : VisualElement
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002946 File Offset: 0x00000B46
		public DoubleSidedProgressBar()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002983 File Offset: 0x00000B83
		public void SetMinimumLength(int minimumLengthPx)
		{
			this._minimumLengthPx = minimumLengthPx;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000298C File Offset: 0x00000B8C
		public void UpdateProgress(float progress, float min, float max)
		{
			if (!Mathf.Approximately(this._progress, progress) || !Mathf.Approximately(this._min, min) || !Mathf.Approximately(this._max, max))
			{
				this._progress = progress;
				this._min = min;
				this._max = max;
				base.MarkDirtyRepaint();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029E0 File Offset: 0x00000BE0
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			string text;
			this._image = (base.customStyle.TryGetValue(DoubleSidedProgressBar.BackgroundImageProperty, ref text) ? Resources.Load<Sprite>(text) : null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A10 File Offset: 0x00000C10
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._image != null)
			{
				MeshWriter meshWriter = new MeshWriter(4, 6);
				meshWriter.StartWriting(mgc, this._image.texture);
				this.WriteMesh(ref meshWriter);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A50 File Offset: 0x00000C50
		public void WriteMesh(ref MeshWriter meshWriter)
		{
			Rect paddingRect = base.paddingRect;
			float width = paddingRect.width;
			float height = paddingRect.height;
			if (width > 0.001f && height > 0.001f)
			{
				float x;
				float x2;
				float uvX;
				this.CalculateBarParameters(width, out x, out x2, out uvX);
				int vertexCount = meshWriter.VertexCount;
				meshWriter.SetNextVertex(DoubleSidedProgressBar.CreateVertex(x, 0f, 0f, 1f));
				meshWriter.SetNextVertex(DoubleSidedProgressBar.CreateVertex(x2, 0f, uvX, 1f));
				meshWriter.SetNextVertex(DoubleSidedProgressBar.CreateVertex(x2, height, uvX, 0f));
				meshWriter.SetNextVertex(DoubleSidedProgressBar.CreateVertex(x, height, 0f, 0f));
				meshWriter.SetNextIndex((ushort)vertexCount);
				meshWriter.SetNextIndex((ushort)(vertexCount + 1));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 3));
				meshWriter.SetNextIndex((ushort)vertexCount);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B40 File Offset: 0x00000D40
		public void CalculateBarParameters(float rectWidth, out float startWidth, out float endWidth, out float progress)
		{
			startWidth = 0f;
			endWidth = 0f;
			progress = 0f;
			if (this._progress != 0f)
			{
				progress = Mathf.Clamp01(Mathf.InverseLerp(this._min, this._max, this._progress));
				if (this.IsDoubleSided())
				{
					this.CalculateDoubleSidedWidths(rectWidth, progress, out startWidth, out endWidth);
					return;
				}
				endWidth = this.CalculateSingleSidedWidth(rectWidth, progress);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public bool IsDoubleSided()
		{
			return this._min != 0f && this._max != 0f && Math.Sign(this._min) != Math.Sign(this._max);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void CalculateDoubleSidedWidths(float rectWidth, float progress, out float startWidth, out float endWidth)
		{
			float num = Mathf.Clamp01(Mathf.InverseLerp(this._min, this._max, 0f));
			bool flag = progress > num;
			float val = rectWidth * Math.Abs(progress - num);
			startWidth = rectWidth * num;
			endWidth = startWidth + Math.Max((float)this._minimumLengthPx, val);
			if (!flag)
			{
				DoubleSidedProgressBar.FlipDoubleSidedWidths(ref startWidth, ref endWidth);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C48 File Offset: 0x00000E48
		public static void FlipDoubleSidedWidths(ref float startWidth, ref float endWidth)
		{
			endWidth = 2f * startWidth - endWidth;
			float num = endWidth;
			float num2 = startWidth;
			startWidth = num;
			endWidth = num2;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C70 File Offset: 0x00000E70
		public float CalculateSingleSidedWidth(float rectWidth, float progress)
		{
			float val = (this._progress < 0f) ? (rectWidth * (1f - progress)) : (rectWidth * progress);
			return Math.Max((float)this._minimumLengthPx, val);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static Vertex CreateVertex(float x, float y, float uvX, float uvY)
		{
			Vertex result = default(Vertex);
			result.position = new Vector3(x, y, Vertex.nearZ);
			result.uv = new Vector2(uvX, uvY);
			result.tint = Color.white;
			return result;
		}

		// Token: 0x04000027 RID: 39
		public static readonly CustomStyleProperty<string> BackgroundImageProperty = new CustomStyleProperty<string>("--background-image");

		// Token: 0x04000028 RID: 40
		public Sprite _image;

		// Token: 0x04000029 RID: 41
		public int _minimumLengthPx;

		// Token: 0x0400002A RID: 42
		public float _min;

		// Token: 0x0400002B RID: 43
		public float _max;

		// Token: 0x0400002C RID: 44
		public float _progress;

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600004C RID: 76 RVA: 0x00002D01 File Offset: 0x00000F01
			public override object CreateInstance()
			{
				return new DoubleSidedProgressBar();
			}
		}
	}
}
