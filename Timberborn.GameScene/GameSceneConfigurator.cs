using System;
using Bindito.Core;
using Timberborn.MapStateSystem;
using Timberborn.TickSystem;
using Timberborn.UndoSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameScene
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class GameSceneConfigurator : Configurator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000221C File Offset: 0x0000041C
		public override void Configure()
		{
			base.Bind<DateSalter>().AsSingleton();
			base.Bind<ITickingMode>().To<GameSceneTickingMode>().AsSingleton();
			base.Bind<ISerializedWorldSupplier>().To<GameSceneSerializedWorldSupplier>().AsSingleton();
			base.Bind<MapEditorMode>().ToInstance(MapEditorMode.NonMapEditorInstance());
			base.Bind<IUndoRegistry>().To<DummyUndoRegistry>().AsSingleton();
		}
	}
}
