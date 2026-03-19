using System;
using Bindito.Core;
using Timberborn.CameraSystem;
using Timberborn.Debugging;

namespace Timberborn.CameraWorldState
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class CameraWorldPositionConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000212F File Offset: 0x0000032F
		public override void Configure()
		{
			base.Bind<ICameraAnchorPicker>().To<CameraAnchorPicker>().AsSingleton();
			base.MultiBind<IDevModule>().To<CameraWorldStateResetter>().AsSingleton();
		}
	}
}
