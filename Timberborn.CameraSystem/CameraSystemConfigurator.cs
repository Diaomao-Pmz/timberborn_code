using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class CameraSystemConfigurator : Configurator
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003B88 File Offset: 0x00001D88
		public override void Configure()
		{
			base.Bind<FacingCamera>().AsTransient();
			base.Bind<CameraFactory>().AsSingleton();
			base.Bind<CameraService>().AsSingleton();
			base.Bind<ShadowDistanceUpdater>().AsSingleton();
			base.Bind<CameraActionMarker>().AsSingleton();
			base.Bind<CameraStateRestorer>().AsSingleton();
			base.Bind<CameraStateSerializer>().AsSingleton();
			base.Bind<CameraHorizontalShifter>().AsSingleton();
			base.Bind<CameraMovementInput>().AsSingleton();
			base.Bind<GrabbingCameraTargetPicker>().AsSingleton();
			base.Bind<KeyboardCameraController>().AsSingleton();
			base.Bind<MouseCameraController>().AsSingleton();
			base.Bind<EdgePanningCameraTargetPicker>().AsSingleton();
			base.Bind<DraggingCameraTargetPicker>().AsSingleton();
			base.Bind<CameraAntiAliasing>().AsSingleton();
			base.MultiBind<IDevModule>().To<CameraSystemDevModule>().AsSingleton();
		}
	}
}
