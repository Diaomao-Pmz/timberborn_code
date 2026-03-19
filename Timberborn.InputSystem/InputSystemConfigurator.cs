using System;
using Bindito.Core;

namespace Timberborn.InputSystem
{
	// Token: 0x02000010 RID: 16
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class InputSystemConfigurator : Configurator
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002E60 File Offset: 0x00001060
		public override void Configure()
		{
			base.Bind<CursorService>().AsSingleton();
			base.Bind<InputService>().AsSingleton();
			base.Bind<InputSettings>().AsSingleton();
			base.Bind<IInputStateResetter>().To<InputStateResetter>().AsSingleton();
			base.Bind<KeyboardListener>().AsSingleton();
			base.Bind<KeywordService>().AsSingleton();
			base.Bind<MouseController>().AsSingleton();
			base.Bind<InputBlocker>().AsSingleton();
		}
	}
}
