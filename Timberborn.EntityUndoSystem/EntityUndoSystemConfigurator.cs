using System;
using Bindito.Core;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class EntityUndoSystemConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002363 File Offset: 0x00000563
		public override void Configure()
		{
			base.Bind<EntityLifecycleUndoableRegistrar>().AsSingleton();
			base.Bind<UndoableEntitiesLoader>().AsSingleton();
			base.Bind<EntityChangeRecorderFactory>().AsSingleton();
			base.Bind<UndoableEntityFactory>().AsSingleton();
			base.MultiBind<IUndoPostprocessor>().ToExisting<UndoableEntitiesLoader>();
		}
	}
}
