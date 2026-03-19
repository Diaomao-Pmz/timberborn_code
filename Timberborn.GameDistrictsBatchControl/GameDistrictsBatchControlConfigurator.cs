using System;
using Bindito.Core;

namespace Timberborn.GameDistrictsBatchControl
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class GameDistrictsBatchControlConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021B9 File Offset: 0x000003B9
		public override void Configure()
		{
			base.Bind<DistrictCenterRowItemFactory>().AsSingleton();
		}
	}
}
