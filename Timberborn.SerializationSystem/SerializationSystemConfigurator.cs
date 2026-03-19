using System;
using Bindito.Core;

namespace Timberborn.SerializationSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Bootstrapper")]
	public class SerializationSystemConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000316D File Offset: 0x0000136D
		public override void Configure()
		{
			base.Bind<SerializedObjectReaderWriter>().AsSingleton().AsExported();
			base.Bind<JsonMerger>().AsSingleton();
		}
	}
}
