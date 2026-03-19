using System;
using Bindito.Core;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x0200000F RID: 15
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class KeyBindingSystemUIConfigurator : Configurator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002B18 File Offset: 0x00000D18
		public override void Configure()
		{
			base.Bind<KeyBindingRowFactory>().AsSingleton();
			base.Bind<KeyBindingsBox>().AsSingleton();
			base.Bind<KeyBindingShortcutService>().AsSingleton();
			base.Bind<KeyBindingShortcutUpdater>().AsSingleton();
			base.Bind<KeyBindingTooltipFactory>().AsSingleton();
			base.Bind<KeyRebinder>().AsSingleton();
			base.Bind<InputBindingDescriber>().AsSingleton();
			base.Bind<KeyBindingDescriber>().AsSingleton();
			base.Bind<FixedKeyBindingElementFactory>().AsSingleton();
		}
	}
}
