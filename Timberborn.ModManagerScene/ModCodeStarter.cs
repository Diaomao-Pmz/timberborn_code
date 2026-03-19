using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Timberborn.Modding;

namespace Timberborn.ModManagerScene
{
	// Token: 0x02000006 RID: 6
	public class ModCodeStarter
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020BE File Offset: 0x000002BE
		public ModCodeStarter(ModRepository modRepository)
		{
			this._modRepository = modRepository;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		public void Start()
		{
			this.LoadAssemblies();
			this.StartMods();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public void LoadAssemblies()
		{
			foreach (Mod mod in this._modRepository.EnabledMods)
			{
				this.LoadAssemblies(mod);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void LoadAssemblies(Mod mod)
		{
			this._loadedAssemblies[mod] = new List<Assembly>();
			FileInfo[] files = mod.ModDirectory.Directory.GetFiles("*.dll", SearchOption.AllDirectories);
			for (int i = 0; i < files.Length; i++)
			{
				Assembly item = Assembly.Load(File.ReadAllBytes(files[i].FullName));
				this._loadedAssemblies[mod].Add(item);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public void StartMods()
		{
			foreach (Mod mod in this._modRepository.EnabledMods)
			{
				this.StartMod(mod);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		public void StartMod(Mod mod)
		{
			ModEnvironment modEnvironment = ModEnvironment.Create(mod);
			foreach (Type type in this.GetModStarters(mod))
			{
				((IModStarter)Activator.CreateInstance(type)).StartMod(modEnvironment);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		public IEnumerable<Type> GetModStarters(Mod mod)
		{
			Type modStarterType = typeof(IModStarter);
			return from type in this._loadedAssemblies[mod].SelectMany((Assembly assembly) => assembly.GetTypes())
			where modStarterType.IsAssignableFrom(type) && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null
			select type;
		}

		// Token: 0x04000006 RID: 6
		public readonly ModRepository _modRepository;

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<Mod, List<Assembly>> _loadedAssemblies = new Dictionary<Mod, List<Assembly>>();
	}
}
