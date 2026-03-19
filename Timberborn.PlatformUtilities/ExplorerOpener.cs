using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x02000006 RID: 6
	public class ExplorerOpener : IExplorerOpener
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		public void OpenDirectory(string directory)
		{
			if (Directory.Exists(directory))
			{
				if (ApplicationPlatform.IsWindows())
				{
					this.StartProcessIgnoringExceptions("explorer.exe", directory.Replace("/", "\\"));
					return;
				}
				if (ApplicationPlatform.IsMacOS())
				{
					this.StartProcessIgnoringExceptions("open", "\"" + directory.Replace("\\", "/") + "\"");
					return;
				}
			}
			else
			{
				Debug.LogWarning("Directory " + directory + " does not exist.");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C8 File Offset: 0x000003C8
		public void StartProcessIgnoringExceptions(string fileName, string arguments)
		{
			try
			{
				Process.Start(fileName, arguments);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}
	}
}
