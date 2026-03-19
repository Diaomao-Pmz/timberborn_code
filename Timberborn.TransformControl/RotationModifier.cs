using System;
using UnityEngine;

namespace Timberborn.TransformControl
{
	// Token: 0x02000005 RID: 5
	public class RotationModifier
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000210E File Offset: 0x0000030E
		public RotationModifier(TransformController transformController)
		{
			this._transformController = transformController;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002130 File Offset: 0x00000330
		public Quaternion Value { get; private set; } = Quaternion.identity;

		// Token: 0x0600000B RID: 11 RVA: 0x00002139 File Offset: 0x00000339
		public void Set(Quaternion value)
		{
			if (!value.Equals(this.Value))
			{
				this.Value = value;
				this._transformController.ApplyRotation();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000215C File Offset: 0x0000035C
		public void Reset()
		{
			this.Set(Quaternion.identity);
		}

		// Token: 0x04000008 RID: 8
		public readonly TransformController _transformController;
	}
}
