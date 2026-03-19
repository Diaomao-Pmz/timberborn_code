using System;
using Bindito.Core;

namespace Timberborn.TextureOperations
{
	// Token: 0x02000005 RID: 5
	[Context("Bootstrapper")]
	public class TextureOperationsConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021FD File Offset: 0x000003FD
		public override void Configure()
		{
			base.Bind<TextureFactory>().AsSingleton().AsExported();
		}
	}
}
