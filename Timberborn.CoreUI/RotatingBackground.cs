using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000047 RID: 71
	[UxmlElement]
	public class RotatingBackground : VisualElement
	{
		// Token: 0x06000126 RID: 294 RVA: 0x000051B6 File Offset: 0x000033B6
		public RotatingBackground()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000051F3 File Offset: 0x000033F3
		public void SetRotation(float angle)
		{
			if (this._angle != angle)
			{
				this._angle = angle;
				base.MarkDirtyRepaint();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000520C File Offset: 0x0000340C
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			string text;
			this._image = (base.customStyle.TryGetValue(RotatingBackground.BackgroundImageProperty, ref text) ? Resources.Load<Sprite>(text) : null);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000523C File Offset: 0x0000343C
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._image != null)
			{
				MeshWriter meshWriter = new MeshWriter(4, 6);
				meshWriter.StartWriting(mgc, this._image.texture);
				this.WriteMesh(ref meshWriter);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000527C File Offset: 0x0000347C
		public void WriteMesh(ref MeshWriter meshWriter)
		{
			Rect paddingRect = base.paddingRect;
			float width = paddingRect.width;
			float height = paddingRect.height;
			Vector3 center;
			center..ctor(width / 2f, height / 2f, 0f);
			Quaternion quaternion = Quaternion.AngleAxis(this._angle, Vector3.forward);
			if (width > 0.001f && height > 0.001f)
			{
				Vertex vertex = default(Vertex);
				vertex.position = RotatingBackground.RotateVertex(new Vector3(0f, 0f, Vertex.nearZ), center, quaternion);
				vertex.uv = new Vector2(0f, 1f);
				vertex.tint = RotatingBackground.White;
				Vertex nextVertex = vertex;
				vertex = default(Vertex);
				vertex.position = RotatingBackground.RotateVertex(new Vector3(width, 0f, Vertex.nearZ), center, quaternion);
				vertex.uv = new Vector2(1f, 1f);
				vertex.tint = RotatingBackground.White;
				Vertex nextVertex2 = vertex;
				vertex = default(Vertex);
				vertex.position = RotatingBackground.RotateVertex(new Vector3(width, height, Vertex.nearZ), center, quaternion);
				vertex.uv = new Vector2(1f, 0f);
				vertex.tint = RotatingBackground.White;
				Vertex nextVertex3 = vertex;
				vertex = default(Vertex);
				vertex.position = RotatingBackground.RotateVertex(new Vector3(0f, height, Vertex.nearZ), center, quaternion);
				vertex.uv = new Vector2(0f, 0f);
				vertex.tint = RotatingBackground.White;
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

		// Token: 0x0600012B RID: 299 RVA: 0x0000547E File Offset: 0x0000367E
		public static Vector3 RotateVertex(Vector3 input, Vector3 center, Quaternion quaternion)
		{
			return quaternion * (input - center) + center;
		}

		// Token: 0x04000099 RID: 153
		public static readonly CustomStyleProperty<string> BackgroundImageProperty = new CustomStyleProperty<string>("--background-image");

		// Token: 0x0400009A RID: 154
		public static readonly Color32 White = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400009B RID: 155
		public Sprite _image;

		// Token: 0x0400009C RID: 156
		public float _angle;

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600012D RID: 301 RVA: 0x000054C2 File Offset: 0x000036C2
			public override object CreateInstance()
			{
				return new RotatingBackground();
			}
		}
	}
}
