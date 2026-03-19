using System;
using System.IO;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x02000005 RID: 5
	public static class DocumentsPermissions
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public static bool HasPermissions()
		{
			bool result;
			try
			{
				if (ApplicationPlatform.IsMacOS())
				{
					Directory.CreateDirectory(UserDataFolder.Folder).GetDirectories();
				}
				result = true;
			}
			catch (UnauthorizedAccessException)
			{
				result = false;
			}
			return result;
		}
	}
}
