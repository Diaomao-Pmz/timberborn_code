using System;
using Timberborn.BlockSystem;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000B RID: 11
	public class NaturalResourcePlantedEvent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023C7 File Offset: 0x000005C7
		public BlockObjectSpec PlantedResource { get; }

		// Token: 0x06000019 RID: 25 RVA: 0x000023CF File Offset: 0x000005CF
		public NaturalResourcePlantedEvent(BlockObjectSpec plantedResource)
		{
			this.PlantedResource = plantedResource;
		}
	}
}
