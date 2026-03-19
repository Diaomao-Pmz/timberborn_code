using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.Modding
{
	// Token: 0x0200001A RID: 26
	public class ModSorter
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003648 File Offset: 0x00001848
		public IEnumerable<Mod> Sort(IEnumerable<Mod> mods)
		{
			return (from x in ModSorter.SortByDependencies(from mod in mods
			orderby mod.DisplayName
			select mod).Select((Mod x, int i) => new
			{
				Value = x,
				OriginalIndex = i
			})
			orderby x.OriginalIndex - ModPlayerPrefsHelper.GetModPriority(x.Value)
			select x).Select(delegate(x, int i)
			{
				int modPriority = ModPlayerPrefsHelper.GetModPriority(x.Value);
				if (modPriority != 0 && i != x.OriginalIndex - modPriority)
				{
					ModPlayerPrefsHelper.SetModPriority(x.Value, x.OriginalIndex - i);
				}
				return x.Value;
			});
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036EB File Offset: 0x000018EB
		public static IEnumerable<Mod> SortByDependencies(IEnumerable<Mod> mods)
		{
			Dictionary<Mod, List<VersionedMod>> modsDependencies = mods.ToDictionary((Mod mod) => mod, (Mod mod) => mod.Manifest.RequiredMods.Concat(mod.Manifest.OptionalMods).ToList<VersionedMod>());
			while (modsDependencies.Count > 0)
			{
				int minDependenciesCount = modsDependencies.Min((KeyValuePair<Mod, List<VersionedMod>> x) => x.Value.Count);
				KeyValuePair<Mod, List<VersionedMod>> currentMod = modsDependencies.First((KeyValuePair<Mod, List<VersionedMod>> x) => x.Value.Count == minDependenciesCount);
				modsDependencies.Remove(currentMod.Key);
				if (modsDependencies.All((KeyValuePair<Mod, List<VersionedMod>> x) => x.Key.Manifest.Id != currentMod.Key.Manifest.Id))
				{
					Predicate<VersionedMod> <>9__5;
					foreach (KeyValuePair<Mod, List<VersionedMod>> keyValuePair in modsDependencies)
					{
						List<VersionedMod> value = keyValuePair.Value;
						Predicate<VersionedMod> match;
						if ((match = <>9__5) == null)
						{
							match = (<>9__5 = ((VersionedMod mod) => mod.Id == currentMod.Key.Manifest.Id));
						}
						value.RemoveAll(match);
					}
				}
				yield return currentMod.Key;
			}
			yield break;
		}
	}
}
