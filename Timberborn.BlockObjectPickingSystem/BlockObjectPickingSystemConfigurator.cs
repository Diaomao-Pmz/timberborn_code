using System;
using Bindito.Core;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockObjectPickingSystemConfigurator : Configurator
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002A4C File Offset: 0x00000C4C
		public override void Configure()
		{
			base.Bind<BlockObjectModelBlockadeIgnorer>().AsTransient();
			base.Bind<BlockObjectRaycaster>().AsSingleton();
			base.Bind<BlockObjectPicker>().AsSingleton();
			base.Bind<BlockObjectPreviewPicker>().AsSingleton();
			base.Bind<StackedBlockObjectPicker>().AsSingleton();
		}
	}
}
