using System;
using Bindito.Core;
using Timberborn.BlueprintPrefabSystem;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class UnityEngineSpecsConfigurator : Configurator
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000035C1 File Offset: 0x000017C1
		public override void Configure()
		{
			base.MultiBind<ISpecToPrefabConverter>().To<TransformSpecPrefabConverter>().AsSingleton();
			base.MultiBind<ISpecToPrefabConverter>().To<CollidersSpecPrefabConverter>().AsSingleton();
		}
	}
}
