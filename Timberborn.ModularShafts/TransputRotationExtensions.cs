using System;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200001E RID: 30
	public static class TransputRotationExtensions
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000054CC File Offset: 0x000036CC
		public static TransputRotation ReverseOrSetNormal(this TransputRotation rotation)
		{
			TransputRotation result;
			switch (rotation)
			{
			case TransputRotation.None:
				result = TransputRotation.Normal;
				break;
			case TransputRotation.Normal:
				result = TransputRotation.Reversed;
				break;
			case TransputRotation.Reversed:
				result = TransputRotation.Normal;
				break;
			case TransputRotation.Ignored:
				result = TransputRotation.Normal;
				break;
			default:
				throw new ArgumentOutOfRangeException("rotation", rotation, null);
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005514 File Offset: 0x00003714
		public static byte AsByte(this TransputRotation rotation)
		{
			byte result;
			switch (rotation)
			{
			case TransputRotation.None:
				result = 0;
				break;
			case TransputRotation.Normal:
				result = 1;
				break;
			case TransputRotation.Reversed:
				result = 2;
				break;
			case TransputRotation.Ignored:
				result = 0;
				break;
			default:
				throw new ArgumentOutOfRangeException("rotation", rotation, null);
			}
			return result;
		}
	}
}
