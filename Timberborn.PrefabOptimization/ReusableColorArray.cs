using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000021 RID: 33
	public class ReusableColorArray
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005120 File Offset: 0x00003320
		public Color32[] Get(int minLength, Color32 color)
		{
			if (this._array == null || this._array.Length < minLength)
			{
				this._array = new Color32[minLength];
				this._filledColor = default(Color32);
				this._filledLength = this._array.Length;
			}
			if (!ReusableColorArray.ColorsAreEqual(this._filledColor, color) || this._filledLength < minLength)
			{
				Array.Fill<Color32>(this._array, color, 0, minLength);
				this._filledColor = color;
				this._filledLength = minLength;
			}
			return this._array;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000519F File Offset: 0x0000339F
		public void Clear()
		{
			this._array = null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000051A8 File Offset: 0x000033A8
		public static bool ColorsAreEqual(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x0400008B RID: 139
		public Color32[] _array;

		// Token: 0x0400008C RID: 140
		public Color32 _filledColor;

		// Token: 0x0400008D RID: 141
		public int _filledLength;
	}
}
