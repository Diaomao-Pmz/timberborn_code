using System;
using Timberborn.Modding;

namespace Timberborn.ModManagerScene
{
	// Token: 0x02000009 RID: 9
	public class ModEnvironment : IModEnvironment
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022FF File Offset: 0x000004FF
		public string ModPath { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002307 File Offset: 0x00000507
		public string OriginPath { get; }

		// Token: 0x06000014 RID: 20 RVA: 0x0000230F File Offset: 0x0000050F
		public ModEnvironment(string modPath, string originPath)
		{
			this.ModPath = modPath;
			this.OriginPath = originPath;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002328 File Offset: 0x00000528
		public static ModEnvironment Create(Mod mod)
		{
			return new ModEnvironment(mod.ModDirectory.Directory.FullName, mod.ModDirectory.OriginPath);
		}
	}
}
