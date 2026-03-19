using System;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x02000014 RID: 20
	public class IlluminatorColorizer
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000335E File Offset: 0x0000155E
		public IlluminatorColorizer(Illuminator illuminator, int priority)
		{
			this._illuminator = illuminator;
			this._priority = priority;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003374 File Offset: 0x00001574
		public void SetColor(Color value)
		{
			this._illuminator.SetColor(this._priority, value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003388 File Offset: 0x00001588
		public void ClearColor()
		{
			this._illuminator.ClearColor(this._priority);
		}

		// Token: 0x04000036 RID: 54
		public readonly Illuminator _illuminator;

		// Token: 0x04000037 RID: 55
		public readonly int _priority;
	}
}
