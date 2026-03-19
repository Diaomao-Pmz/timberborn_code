using System;
using System.Collections.Generic;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x02000009 RID: 9
	public class ImportDetails
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002100 File Offset: 0x00000300
		public Transform Root { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
		public ImportDetails(Transform root)
		{
			this.Root = root;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002122 File Offset: 0x00000322
		public IReadOnlyDictionary<Node, GameObject> CreatedObjectsMap
		{
			get
			{
				return this._createdObjectsMap;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000212A File Offset: 0x0000032A
		public void AddObject(GameObject createdObject, Node sourceNode)
		{
			this._createdObjectsMap.Add(sourceNode, createdObject);
		}

		// Token: 0x04000009 RID: 9
		public readonly Dictionary<Node, GameObject> _createdObjectsMap = new Dictionary<Node, GameObject>();
	}
}
