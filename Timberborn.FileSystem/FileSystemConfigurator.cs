using System;
using Bindito.Core;

namespace Timberborn.FileSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Bootstrapper")]
	public class FileSystemConfigurator : Configurator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000026B7 File Offset: 0x000008B7
		public override void Configure()
		{
			base.Bind<FilenameValidator>().AsSingleton().AsExported();
			base.Bind<IFileService>().To<FileService>().AsSingleton().AsExported();
		}
	}
}
