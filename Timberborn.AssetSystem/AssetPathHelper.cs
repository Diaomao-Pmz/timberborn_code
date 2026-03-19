using System;
using System.IO;

namespace Timberborn.AssetSystem
{
	// Token: 0x02000008 RID: 8
	public static class AssetPathHelper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022AB File Offset: 0x000004AB
		public static string NormalizeAssetPath(string assetPath)
		{
			return AssetPathHelper.NormalizeAssetPath(assetPath, AssetPathHelper.ResourcesDirectory);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022B8 File Offset: 0x000004B8
		public static string NormalizeAssetPath(string assetPath, string rootDirectory)
		{
			return AssetPathHelper.NormalizePath(AssetPathHelper.GetRelativePath(assetPath, rootDirectory));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C6 File Offset: 0x000004C6
		public static string GetRelativePath(string assetPath)
		{
			return AssetPathHelper.GetRelativePath(assetPath, AssetPathHelper.ResourcesDirectory);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D3 File Offset: 0x000004D3
		public static string NormalizePath(string path)
		{
			return path.Replace('\\', '/').ToLower();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E4 File Offset: 0x000004E4
		public static string GetRelativePath(string assetPath, string rootDirectory)
		{
			assetPath = AssetPathHelper.TruncatePath(assetPath, rootDirectory);
			string extension = Path.GetExtension(assetPath);
			string text;
			if (extension == null)
			{
				text = assetPath;
			}
			else
			{
				string text2 = assetPath;
				int length = extension.Length;
				text = text2.Substring(0, text2.Length - length);
			}
			assetPath = text;
			if (assetPath.StartsWith("/") || assetPath.StartsWith("\\"))
			{
				string text2 = assetPath;
				assetPath = text2.Substring(1, text2.Length - 1);
			}
			return assetPath;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002350 File Offset: 0x00000550
		public static string TruncatePath(string assetPath, string rootDirectory)
		{
			if (assetPath.StartsWith(rootDirectory))
			{
				string text = assetPath;
				int num = rootDirectory.Length;
				assetPath = text.Substring(num, text.Length - num);
			}
			else
			{
				string text2 = "/" + rootDirectory;
				int num2 = assetPath.IndexOf(text2, 5);
				if (num2 != -1)
				{
					string text = assetPath;
					int num = num2 + text2.Length;
					assetPath = text.Substring(num, text.Length - num);
				}
				else if (assetPath.StartsWith(AssetPathHelper.AssetPathPrefix, 5))
				{
					string text = assetPath;
					int num = AssetPathHelper.AssetPathPrefix.Length;
					assetPath = text.Substring(num, text.Length - num);
				}
			}
			return assetPath;
		}

		// Token: 0x0400000B RID: 11
		public static readonly string AssetPathPrefix = "assets/";

		// Token: 0x0400000C RID: 12
		public static readonly string ResourcesDirectory = "resources/";
	}
}
