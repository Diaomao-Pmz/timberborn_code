using System;
using Bindito.Core;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000023 RID: 35
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class KeyBindingSystemConfigurator : Configurator
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00004568 File Offset: 0x00002768
		public override void Configure()
		{
			base.Bind<InputBindingListener>().AsSingleton();
			base.Bind<InputBindingNameService>().AsSingleton();
			base.Bind<CustomInputBindingSerializer>().AsSingleton();
			base.Bind<InputModifiersService>().AsSingleton();
			base.Bind<InputUpdater>().AsSingleton();
			base.Bind<KeyBindingDeviceUpdater>().AsSingleton();
			base.Bind<KeyBindingGroupSpecService>().AsSingleton();
			base.Bind<KeyBindingRegistry>().AsSingleton();
			base.Bind<KeyBindingSpecService>().AsSingleton();
			base.Bind<CtrlKeyOverwriter>().AsSingleton();
			base.Bind<KeyBindingFactory>().AsSingleton();
		}
	}
}
