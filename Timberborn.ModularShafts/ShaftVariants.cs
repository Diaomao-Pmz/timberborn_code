using System;
using System.Collections.Generic;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200001B RID: 27
	public static class ShaftVariants
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00004FA4 File Offset: 0x000031A4
		public static IEnumerable<ShaftVariant> GetAllVariants()
		{
			foreach (ShaftVariant horizontalVariant in ShaftVariants.HorizontalVariants)
			{
				yield return horizontalVariant;
				yield return horizontalVariant.ToFacingTop();
				yield return horizontalVariant.ToFacingTopReversed();
				yield return horizontalVariant.ToFacingBottom();
				yield return horizontalVariant.ToFacingBottomReversed();
				yield return horizontalVariant.ToFacingTopAndBottom(false, false);
				yield return horizontalVariant.ToFacingTopAndBottom(true, false);
				yield return horizontalVariant.ToFacingTopAndBottom(false, true);
				yield return horizontalVariant.ToFacingTopAndBottom(true, true);
			}
			List<ShaftVariant>.Enumerator enumerator = default(List<ShaftVariant>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400007C RID: 124
		public static readonly List<ShaftVariant> HorizontalVariants = new List<ShaftVariant>
		{
			ShaftVariant.CreateHorizontal(0, 0, 0, 0),
			ShaftVariant.CreateHorizontal(0, 0, 0, 1),
			ShaftVariant.CreateHorizontal(0, 0, 0, 2),
			ShaftVariant.CreateHorizontal(0, 1, 0, 1),
			ShaftVariant.CreateHorizontal(0, 1, 0, 2),
			ShaftVariant.CreateHorizontal(0, 2, 0, 1),
			ShaftVariant.CreateHorizontal(0, 2, 0, 2),
			ShaftVariant.CreateHorizontal(1, 0, 0, 1),
			ShaftVariant.CreateHorizontal(1, 0, 0, 2),
			ShaftVariant.CreateHorizontal(2, 0, 0, 1),
			ShaftVariant.CreateHorizontal(2, 0, 0, 2),
			ShaftVariant.CreateHorizontal(1, 1, 0, 1),
			ShaftVariant.CreateHorizontal(1, 1, 0, 2),
			ShaftVariant.CreateHorizontal(1, 2, 0, 1),
			ShaftVariant.CreateHorizontal(2, 1, 0, 1),
			ShaftVariant.CreateHorizontal(2, 2, 0, 1),
			ShaftVariant.CreateHorizontal(1, 2, 0, 2),
			ShaftVariant.CreateHorizontal(2, 1, 0, 2),
			ShaftVariant.CreateHorizontal(2, 2, 0, 2),
			ShaftVariant.CreateHorizontal(1, 1, 1, 1),
			ShaftVariant.CreateHorizontal(1, 1, 1, 2),
			ShaftVariant.CreateHorizontal(1, 2, 1, 1),
			ShaftVariant.CreateHorizontal(1, 2, 1, 2),
			ShaftVariant.CreateHorizontal(2, 1, 1, 1),
			ShaftVariant.CreateHorizontal(2, 1, 1, 2),
			ShaftVariant.CreateHorizontal(2, 2, 1, 1),
			ShaftVariant.CreateHorizontal(2, 2, 1, 2),
			ShaftVariant.CreateHorizontal(1, 1, 2, 1),
			ShaftVariant.CreateHorizontal(1, 1, 2, 2),
			ShaftVariant.CreateHorizontal(1, 2, 2, 1),
			ShaftVariant.CreateHorizontal(1, 2, 2, 2),
			ShaftVariant.CreateHorizontal(2, 1, 2, 1),
			ShaftVariant.CreateHorizontal(2, 1, 2, 2),
			ShaftVariant.CreateHorizontal(2, 2, 2, 1),
			ShaftVariant.CreateHorizontal(2, 2, 2, 2)
		};
	}
}
