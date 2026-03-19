using System;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000E RID: 14
	public static class Directions3DExtensions
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public static Direction3DEnumerator GetEnumerator(this Directions3D directions)
		{
			return new Direction3DEnumerator(directions);
		}
	}
}
