using System;
using Bindito.Core;
using Timberborn.SaveSystem;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200001D RID: 29
	[Context("Game")]
	[Context("MapEditor")]
	public class WorldPersistenceConfigurator : Configurator
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public override void Configure()
		{
			base.Bind<ReferenceSerializer>().AsSingleton();
			base.Bind<WorldEntitiesLoader>().AsSingleton();
			base.Bind<EntitiesLoader>().AsSingleton();
			base.Bind<SerializedWorldFactory>().AsSingleton();
			base.Bind<ISingletonLoader>().To<WorldSingletonLoader>().AsSingleton();
			base.MultiBind<ISaveEntryWriter>().To<WorldEntryWriter>().AsSingleton();
		}
	}
}
