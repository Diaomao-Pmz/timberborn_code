using System;
using Bindito.Core;

namespace Timberborn.WorldSerialization
{
	// Token: 0x02000009 RID: 9
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class WorldSerializationConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023F7 File Offset: 0x000005F7
		public override void Configure()
		{
			base.Bind<WorldSerializer>().AsSingleton();
		}
	}
}
