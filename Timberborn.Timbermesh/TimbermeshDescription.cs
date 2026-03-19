using System;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x0200000C RID: 12
	public class TimbermeshDescription : MonoBehaviour
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000249B File Offset: 0x0000069B
		public string ModelName
		{
			get
			{
				return this._modelName;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024A3 File Offset: 0x000006A3
		public void SetModelName(string modelName)
		{
			this._modelName = modelName;
		}

		// Token: 0x04000011 RID: 17
		[SerializeField]
		public string _modelName;
	}
}
