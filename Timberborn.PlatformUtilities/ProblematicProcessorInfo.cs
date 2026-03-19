using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x02000009 RID: 9
	public static class ProblematicProcessorInfo
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public static bool IsProblematic()
		{
			bool flag = ProblematicProcessorInfo.isProblematicProcessor.GetValueOrDefault();
			if (ProblematicProcessorInfo.isProblematicProcessor == null)
			{
				flag = ProblematicProcessorInfo.IsProblematicUncached();
				ProblematicProcessorInfo.isProblematicProcessor = new bool?(flag);
				return flag;
			}
			return flag;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000224C File Offset: 0x0000044C
		public static string GetMicrocodeVersion()
		{
			string result;
			if ((result = ProblematicProcessorInfo.microcodeVersion) == null)
			{
				result = (ProblematicProcessorInfo.microcodeVersion = ProblematicProcessorInfo.GetMicrocodeVersionUncached());
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002264 File Offset: 0x00000464
		public static bool IsProblematicUncached()
		{
			bool result;
			try
			{
				string processorType = SystemInfo.processorType;
				result = ProblematicProcessorInfo.ProblematicProcessors.Any((string problematicProcessor) => Regex.IsMatch(processorType, problematicProcessor + "(?![A-Za-z])", RegexOptions.IgnoreCase));
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B8 File Offset: 0x000004B8
		public static string GetMicrocodeVersionUncached()
		{
			string result;
			try
			{
				result = ((ApplicationPlatform.IsWindows() && ProblematicProcessorInfo.IsProblematic()) ? ProblematicProcessorInfo.ReadRegistryKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "Update Revision") : "");
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				result = "";
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000230C File Offset: 0x0000050C
		public static string ReadRegistryKey(string subKey, string valueName)
		{
			IntPtr hKey;
			if (ProblematicProcessorInfo.RegOpenKeyEx((UIntPtr)ProblematicProcessorInfo.HKEY_LOCAL_MACHINE, subKey, 0, ProblematicProcessorInfo.KEY_READ, out hKey) == 0)
			{
				uint num = 1024U;
				byte[] array = new byte[num];
				uint num2;
				if (ProblematicProcessorInfo.RegQueryValueEx(hKey, valueName, 0, out num2, array, ref num) == 0)
				{
					ProblematicProcessorInfo.RegCloseKey(hKey);
					if (num2 == 1U)
					{
						return Encoding.Unicode.GetString(array, 0, (int)(num - 2U));
					}
					if (num2 - 3U <= 1U)
					{
						return string.Format("0x{0:X8}", BitConverter.ToUInt32(array, 0));
					}
				}
				ProblematicProcessorInfo.RegCloseKey(hKey);
			}
			return "";
		}

		// Token: 0x06000011 RID: 17
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		public static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, int ulOptions, int samDesired, out IntPtr phkResult);

		// Token: 0x06000012 RID: 18
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		public static extern int RegQueryValueEx(IntPtr hKey, string lpValueName, int lpReserved, out uint lpType, byte[] lpData, ref uint lpcbData);

		// Token: 0x06000013 RID: 19
		[DllImport("advapi32.dll")]
		public static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x04000006 RID: 6
		public static readonly ImmutableArray<string> ProblematicProcessors = new string[]
		{
			"14900K",
			"14900KF",
			"14900KS",
			"14900F",
			"13900K",
			"13900KF",
			"13900KS",
			"13900F",
			"14700K",
			"14700KF",
			"14700F",
			"14700",
			"13700K",
			"13700KF",
			"13700F",
			"14790F",
			"14600K",
			"14600F",
			"13600K",
			"13600KF"
		}.ToImmutableArray<string>();

		// Token: 0x04000007 RID: 7
		public static readonly uint HKEY_LOCAL_MACHINE = 2147483650U;

		// Token: 0x04000008 RID: 8
		public static readonly int KEY_READ = 131097;

		// Token: 0x04000009 RID: 9
		public static bool? isProblematicProcessor;

		// Token: 0x0400000A RID: 10
		public static string microcodeVersion;
	}
}
