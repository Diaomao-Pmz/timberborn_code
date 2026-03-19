using System;
using Bindito.Core;
using Timberborn.BlueprintPrefabSystem;

namespace Timberborn.Timbermesh
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class TimbermeshConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002468 File Offset: 0x00000668
		public override void Configure()
		{
			base.Bind<TimbermeshImporter>().AsSingleton();
			base.Bind<StaticMeshBuilder>().AsSingleton();
			base.MultiBind<ISpecToPrefabConverter>().To<TimbermeshSpecConverter>().AsSingleton();
		}
	}
}
