using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000086 RID: 134
	public class ConfiguratorRunner
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000034F5 File Offset: 0x000016F5
		public ConfiguratorRunner(IContainerDefinition containerDefinition)
		{
			this._containerDefinition = containerDefinition;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003504 File Offset: 0x00001704
		public void RunConfigurators(IEnumerable<IConfigurator> configurators)
		{
			foreach (IConfigurator configurator in configurators)
			{
				configurator.Configure(this._containerDefinition);
			}
		}

		// Token: 0x04000084 RID: 132
		private readonly IContainerDefinition _containerDefinition;
	}
}
