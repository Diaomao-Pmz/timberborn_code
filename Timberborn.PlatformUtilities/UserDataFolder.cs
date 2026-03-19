using System;
using System.IO;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x0200000E RID: 14
	public static class UserDataFolder
	{
		// Token: 0x04000013 RID: 19
		public static readonly string Folder = ApplicationPlatform.IsMacOS() ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents", "Timberborn") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Timberborn");
	}
}
