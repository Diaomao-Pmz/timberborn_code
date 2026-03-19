using System;
using Bindito.Core;

namespace Timberborn.FileBrowsing
{
	// Token: 0x02000009 RID: 9
	[Context("MapEditor")]
	public class FileBrowsingConfigurator : Configurator
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002AAA File Offset: 0x00000CAA
		public override void Configure()
		{
			base.Bind<FileBrowser>().AsSingleton();
			base.Bind<DiskSystemEntryElementFactory>().AsSingleton();
			base.Bind<DirectoryListView>().AsSingleton();
			base.Bind<FileFilterProvider>().AsSingleton();
		}
	}
}
