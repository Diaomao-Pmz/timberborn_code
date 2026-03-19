using System;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200001B RID: 27
	public static class PathMeshConnectionKeys
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003D30 File Offset: 0x00001F30
		public static byte ParseCharToByteKey(char modelNameChar)
		{
			if (modelNameChar <= 'B')
			{
				if (modelNameChar == '0')
				{
					return PathMeshConnectionKeys.Nothing;
				}
				if (modelNameChar == 'B')
				{
					return PathMeshConnectionKeys.Building;
				}
			}
			else
			{
				if (modelNameChar == 'P')
				{
					return PathMeshConnectionKeys.Path;
				}
				if (modelNameChar == 'b')
				{
					return PathMeshConnectionKeys.AlternativeBuilding;
				}
				if (modelNameChar == 'p')
				{
					return PathMeshConnectionKeys.AlternativePath;
				}
			}
			throw new ArgumentOutOfRangeException(string.Format("{0} isn't acceptable value", modelNameChar));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D9E File Offset: 0x00001F9E
		public static byte ToAlternativeKey(byte key)
		{
			if (key == PathMeshConnectionKeys.Path)
			{
				return PathMeshConnectionKeys.AlternativePath;
			}
			if (key == PathMeshConnectionKeys.Building)
			{
				return PathMeshConnectionKeys.AlternativeBuilding;
			}
			throw new ArgumentOutOfRangeException(string.Format("Can't find alternative value for {0}", key));
		}

		// Token: 0x04000060 RID: 96
		public static readonly byte Nothing = 0;

		// Token: 0x04000061 RID: 97
		public static readonly byte Path = 1;

		// Token: 0x04000062 RID: 98
		public static readonly byte Building = 3;

		// Token: 0x04000063 RID: 99
		public static readonly byte AlternativePath = 2;

		// Token: 0x04000064 RID: 100
		public static readonly byte AlternativeBuilding = 4;
	}
}
