using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200004C RID: 76
	[UxmlElement]
	public class SimpleProgressBar : VisualElement
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00005612 File Offset: 0x00003812
		public SimpleProgressBar()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000564F File Offset: 0x0000384F
		public void SetProgress(float progress)
		{
			if (this._progress != progress)
			{
				this._progress = Mathf.Clamp01(progress);
				base.MarkDirtyRepaint();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000566C File Offset: 0x0000386C
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			string text;
			this._image = (base.customStyle.TryGetValue(SimpleProgressBar.BackgroundImageProperty, ref text) ? Resources.Load<Sprite>(text) : null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000569C File Offset: 0x0000389C
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._image != null)
			{
				MeshWriter meshWriter = new MeshWriter(4, 6);
				meshWriter.StartWriting(mgc, this._image.texture);
				this.WriteMesh(ref meshWriter);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056DC File Offset: 0x000038DC
		public void WriteMesh(ref MeshWriter meshWriter)
		{
			Rect paddingRect = base.paddingRect;
			float width = paddingRect.width;
			float height = paddingRect.height;
			float x = width * this._progress;
			if (width > 0.001f && height > 0.001f)
			{
				int vertexCount = meshWriter.VertexCount;
				meshWriter.SetNextVertex(SimpleProgressBar.CreateVertex(0f, 0f, 0f, 1f));
				meshWriter.SetNextVertex(SimpleProgressBar.CreateVertex(x, 0f, this._progress, 1f));
				meshWriter.SetNextVertex(SimpleProgressBar.CreateVertex(x, height, this._progress, 0f));
				meshWriter.SetNextVertex(SimpleProgressBar.CreateVertex(0f, height, 0f, 0f));
				meshWriter.SetNextIndex((ushort)vertexCount);
				meshWriter.SetNextIndex((ushort)(vertexCount + 1));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 2));
				meshWriter.SetNextIndex((ushort)(vertexCount + 3));
				meshWriter.SetNextIndex((ushort)vertexCount);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000057CC File Offset: 0x000039CC
		public static Vertex CreateVertex(float x, float y, float uvX, float uvY)
		{
			Vertex result = default(Vertex);
			result.position = new Vector3(x, y, Vertex.nearZ);
			result.uv = new Vector2(uvX, uvY);
			result.tint = Color.white;
			return result;
		}

		// Token: 0x040000A3 RID: 163
		public static readonly CustomStyleProperty<string> BackgroundImageProperty = new CustomStyleProperty<string>("--background-image");

		// Token: 0x040000A4 RID: 164
		public Sprite _image;

		// Token: 0x040000A5 RID: 165
		public float _progress;

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000140 RID: 320 RVA: 0x00005825 File Offset: 0x00003A25
			public override object CreateInstance()
			{
				return new SimpleProgressBar();
			}
		}
	}
}
