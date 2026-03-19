using System;
using Bindito.Core;
using Timberborn.Timbermesh;

namespace Timberborn.TimbermeshMaterials
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class TimbermeshMaterialsConfigurator : Configurator
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002787 File Offset: 0x00000987
		public override void Configure()
		{
			base.Bind<IMaterialRepository>().To<MaterialRepository>().AsSingleton();
			base.MultiBind<IMaterialCollectionIdsProvider>().To<CommonMaterialCollectionIdsProvider>().AsSingleton();
		}
	}
}
