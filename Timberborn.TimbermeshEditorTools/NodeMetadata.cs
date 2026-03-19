using System;
using UnityEngine;

namespace Timberborn.TimbermeshEditorTools
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class NodeMetadata
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022D5 File Offset: 0x000004D5
		public NodeMetadata(string name, int treeDepth, int vertexCount)
		{
			this._name = name;
			this._treeDepth = treeDepth;
			this._vertexCount = vertexCount;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022F2 File Offset: 0x000004F2
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022FA File Offset: 0x000004FA
		public int TreeDepth
		{
			get
			{
				return this._treeDepth;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002302 File Offset: 0x00000502
		public int VertexCount
		{
			get
			{
				return this._vertexCount;
			}
		}

		// Token: 0x0400000F RID: 15
		[SerializeField]
		public string _name;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		public int _treeDepth;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		public int _vertexCount;
	}
}
