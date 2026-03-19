using System;
using UnityEngine;

namespace Timberborn.TransformControl
{
	// Token: 0x02000006 RID: 6
	public class ScaleModifier
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002169 File Offset: 0x00000369
		public ScaleModifier(TransformController transformController)
		{
			this._transformController = transformController;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002183 File Offset: 0x00000383
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000218B File Offset: 0x0000038B
		public Vector3 Value { get; private set; } = Vector3.one;

		// Token: 0x06000010 RID: 16 RVA: 0x00002194 File Offset: 0x00000394
		public void Set(Vector3 value)
		{
			if (!value.Equals(this.Value))
			{
				this.Value = value;
				this._transformController.ApplyScale();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021B7 File Offset: 0x000003B7
		public void Reset()
		{
			this.Set(Vector3.one);
		}

		// Token: 0x0400000A RID: 10
		public readonly TransformController _transformController;
	}
}
