using System;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000A RID: 10
	public class BinaryData : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] Bytes
		{
			get
			{
				return this._bytes;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002434 File Offset: 0x00000634
		public void SetData(byte[] bytes)
		{
			this._bytes = bytes;
		}

		// Token: 0x0400000D RID: 13
		[HideInInspector]
		[SerializeField]
		public byte[] _bytes;
	}
}
