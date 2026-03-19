using System;
using UnityEngine;

namespace Timberborn.TransformControl
{
	// Token: 0x02000004 RID: 4
	public class PositionModifier
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public PositionModifier(TransformController transformController)
		{
			this._transformController = transformController;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020D5 File Offset: 0x000002D5
		public Vector3 Value { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public void Set(Vector3 value)
		{
			if (!value.Equals(this.Value))
			{
				this.Value = value;
				this._transformController.ApplyPosition();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public void Reset()
		{
			this.Set(Vector3.zero);
		}

		// Token: 0x04000006 RID: 6
		public readonly TransformController _transformController;
	}
}
