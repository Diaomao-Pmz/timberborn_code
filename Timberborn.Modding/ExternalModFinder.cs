using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Timberborn.Modding
{
	// Token: 0x02000006 RID: 6
	public static class ExternalModFinder
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022F4 File Offset: 0x000004F4
		public static void CheckForMods()
		{
			if (ExternalModFinder.GetMods().Any<string>())
			{
				ModdedState.SetUnofficialMods();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002307 File Offset: 0x00000507
		public static IEnumerable<string> GetMods()
		{
			Assembly[] loadedAssemblies = ExternalModFinder.GetAssemblies();
			using (List<string>.Enumerator enumerator = ExternalModFinder.Assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string modAssembly = enumerator.Current;
					if (loadedAssemblies.Any((Assembly loadedAssembly) => ExternalModFinder.AssemblyIsMod(loadedAssembly, modAssembly)))
					{
						yield return modAssembly;
					}
				}
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			Process[] activeProcesses = Process.GetProcesses();
			using (List<string>.Enumerator enumerator = ExternalModFinder.Processes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string modProcess = enumerator.Current;
					if (activeProcesses.Any((Process activeProcess) => ExternalModFinder.ProcessIsMod(activeProcess, modProcess)))
					{
						yield return modProcess;
					}
				}
			}
			enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
		public static Assembly[] GetAssemblies()
		{
			Assembly[] result;
			try
			{
				result = AppDomain.CurrentDomain.GetAssemblies();
			}
			catch (AppDomainUnloadedException)
			{
				result = Array.Empty<Assembly>();
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002344 File Offset: 0x00000544
		public static bool AssemblyIsMod(Assembly loadedAssembly, string modAssembly)
		{
			return loadedAssembly.FullName.Contains(modAssembly, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002354 File Offset: 0x00000554
		public static bool ProcessIsMod(Process process, string modProcess)
		{
			bool result;
			try
			{
				result = (!process.HasExited && process.ProcessName.Contains(modProcess, StringComparison.OrdinalIgnoreCase));
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			catch (NotSupportedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		public static readonly List<string> Assemblies = new List<string>
		{
			"BepInEx"
		};

		// Token: 0x0400000B RID: 11
		public static readonly List<string> Processes = new List<string>
		{
			"WeMod",
			"Plitch",
			"TrainerManager"
		};
	}
}
