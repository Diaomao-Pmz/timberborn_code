using System;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class AnimationMetadata
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002261 File Offset: 0x00000461
		public AnimationMetadata(string name, float length)
		{
			this._name = name;
			this._length = length;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002277 File Offset: 0x00000477
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000227F File Offset: 0x0000047F
		public float Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x0400000C RID: 12
		[SerializeField]
		public string _name;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		public float _length;
	}
}
