using System;
using Bindito.Core;

namespace Timberborn.BlueprintPrefabSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class BlueprintPrefabSystemConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002189 File Offset: 0x00000389
		public override void Configure()
		{
			base.Bind<BlueprintPrefabConverter>().AsSingleton();
		}
	}
}
