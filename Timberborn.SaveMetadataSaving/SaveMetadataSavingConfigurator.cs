using System;
using Bindito.Core;
using Timberborn.SaveSystem;

namespace Timberborn.SaveMetadataSaving
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class SaveMetadataSavingConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021A7 File Offset: 0x000003A7
		public override void Configure()
		{
			base.MultiBind<ISaveEntryWriter>().To<SaveMetadataSaveEntryWriter>().AsSingleton();
		}
	}
}
