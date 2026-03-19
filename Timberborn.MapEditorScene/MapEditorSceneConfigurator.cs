using System;
using Bindito.Core;
using Timberborn.Demolishing;
using Timberborn.MapStateSystem;
using Timberborn.Navigation;
using Timberborn.WorldPersistence;

namespace Timberborn.MapEditorScene
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorSceneConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		protected override void Configure()
		{
			base.Bind<Accessible>().AsTransient();
			base.Bind<Demolishable>().AsTransient();
			base.Bind<AccessibleDemolishableReacher>().AsTransient();
			base.Bind<MapEditorCameraCenterer>().AsSingleton();
			base.Bind<ISerializedWorldSupplier>().To<MapEditorSerializedWorldSupplier>().AsSingleton();
			base.Bind<INavMeshService>().To<DummyNavMeshService>().AsSingleton();
			base.Bind<INavigationService>().To<DummyNavigationService>().AsSingleton();
			base.Bind<INavigationCachingService>().To<DummyNavigationCachingService>().AsSingleton();
			base.Bind<INavigationDebuggingService>().To<DummyNavigationDebuggingService>().AsSingleton();
			base.Bind<INavMeshListenerEntityRegistry>().To<DummyNavMeshListenerEntityRegistry>().AsSingleton();
			base.Bind<MapEditorMode>().ToInstance(MapEditorMode.MapEditorInstance());
		}
	}
}
