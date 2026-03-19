using System;
using Bindito.Core;

namespace Timberborn.TerrainQueryingSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainQueryingSystemConfigurator : Configurator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002705 File Offset: 0x00000905
		public override void Configure()
		{
			base.Bind<TerrainPicker>().AsSingleton();
			base.Bind<TerrainAreaService>().AsSingleton();
		}
	}
}
