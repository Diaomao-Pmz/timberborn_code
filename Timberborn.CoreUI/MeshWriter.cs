using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200002C RID: 44
	public struct MeshWriter
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000039D4 File Offset: 0x00001BD4
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000039DC File Offset: 0x00001BDC
		public int VertexCount { readonly get; private set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x000039E5 File Offset: 0x00001BE5
		public MeshWriter(int vertexCount, int indexCount)
		{
			this.VertexCount = vertexCount;
			this._indexCount = indexCount;
			this._meshWriteData = null;
			this._writing = false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A04 File Offset: 0x00001C04
		public void StartWriting(MeshGenerationContext meshGenerationContext, Texture texture = null)
		{
			if (this._writing)
			{
				throw new InvalidOperationException("This MeshWriter is already writing.");
			}
			this._writing = true;
			this._meshWriteData = ((this._indexCount > 0) ? meshGenerationContext.Allocate(this.VertexCount, this._indexCount, texture) : null);
			this.VertexCount = 0;
			this._indexCount = 0;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A5E File Offset: 0x00001C5E
		public void SetNextIndex(ushort index)
		{
			this._indexCount++;
			if (this._writing)
			{
				MeshWriteData meshWriteData = this._meshWriteData;
				if (meshWriteData == null)
				{
					return;
				}
				meshWriteData.SetNextIndex(index);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A88 File Offset: 0x00001C88
		public void SetNextVertex(Vertex vertex)
		{
			int vertexCount = this.VertexCount;
			this.VertexCount = vertexCount + 1;
			if (this._writing)
			{
				MeshWriteData meshWriteData = this._meshWriteData;
				if (meshWriteData == null)
				{
					return;
				}
				meshWriteData.SetNextVertex(vertex);
			}
		}

		// Token: 0x0400005F RID: 95
		public bool _writing;

		// Token: 0x04000060 RID: 96
		public MeshWriteData _meshWriteData;

		// Token: 0x04000061 RID: 97
		public int _indexCount;
	}
}
