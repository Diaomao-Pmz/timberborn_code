using System;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000008 RID: 8
	public static class RelativeHeightExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public static RelativeHeight FromModelNameCharacter(char modelNameCharacter)
		{
			if (modelNameCharacter <= 'D')
			{
				if (modelNameCharacter == '0')
				{
					return RelativeHeight.Equal;
				}
				if (modelNameCharacter == 'D')
				{
					return RelativeHeight.Lower;
				}
			}
			else
			{
				if (modelNameCharacter == 'E')
				{
					return RelativeHeight.Empty;
				}
				if (modelNameCharacter == 'U')
				{
					return RelativeHeight.Higher;
				}
				if (modelNameCharacter == 'V')
				{
					return RelativeHeight.Overhang;
				}
			}
			throw new ArgumentException(string.Format("Invalid input: {0}", modelNameCharacter), "modelNameCharacter");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public static char ToModelNameCharacter(RelativeHeight relativeHeight)
		{
			switch (relativeHeight)
			{
			case RelativeHeight.Lower:
				return 'D';
			case RelativeHeight.Equal:
				return '0';
			case RelativeHeight.Overhang:
				return 'V';
			case RelativeHeight.Higher:
				return 'U';
			case RelativeHeight.Empty:
				return 'E';
			default:
				throw new ArgumentException(string.Format("Unexpected input: {0}", relativeHeight), "relativeHeight");
			}
		}
	}
}
