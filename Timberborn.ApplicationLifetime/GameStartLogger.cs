using System;
using System.Text;
using Timberborn.Modding;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.ApplicationLifetime
{
	// Token: 0x02000006 RID: 6
	public static class GameStartLogger
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
		[RuntimeInitializeOnLoadMethod(1)]
		public static void Log()
		{
			if (!Application.isEditor)
			{
				Debug.Log("Starting game version " + Application.version);
				Debug.Log(GameStartLogger.GetSystemInfo());
				Debug.Log(GameStartLogger.MachineIdKey + ": " + GameStartLogger.GetMachineId());
				ExternalModFinder.CheckForMods();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public static string GetSystemInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("System info:");
			stringBuilder.AppendLine("  System: " + SystemInfo.operatingSystem);
			stringBuilder.AppendLine("  CPU: " + SystemInfo.processorType);
			stringBuilder.AppendLine("  CPU manufacturer: " + SystemInfo.processorManufacturer);
			stringBuilder.AppendLine("  CPU model: " + SystemInfo.processorModel);
			stringBuilder.AppendLine(string.Format("  CPU count: {0}", SystemInfo.processorCount));
			stringBuilder.AppendLine(string.Format("  CPU frequency: {0}", SystemInfo.processorFrequency));
			stringBuilder.AppendLine(string.Format("  CPU problematic: {0}", ProblematicProcessorInfo.IsProblematic()));
			stringBuilder.AppendLine("  CPU microcode: " + ProblematicProcessorInfo.GetMicrocodeVersion());
			stringBuilder.AppendLine("  GPU: " + SystemInfo.graphicsDeviceName);
			stringBuilder.AppendLine(string.Format("  GPU memory: {0}MB", SystemInfo.graphicsMemorySize));
			stringBuilder.AppendLine(string.Format("  RAM: {0}MB", SystemInfo.systemMemorySize));
			return stringBuilder.ToString();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002264 File Offset: 0x00000464
		public static string GetMachineId()
		{
			string result;
			try
			{
				if (!PlayerPrefs.HasKey(GameStartLogger.MachineIdKey))
				{
					PlayerPrefs.SetString(GameStartLogger.MachineIdKey, Guid.NewGuid().ToString());
					PlayerPrefs.Save();
				}
				result = PlayerPrefs.GetString(GameStartLogger.MachineIdKey);
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
				result = "error";
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string MachineIdKey = "MachineId";
	}
}
