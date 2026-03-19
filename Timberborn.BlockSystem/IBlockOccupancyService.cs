using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200003F RID: 63
	public interface IBlockOccupancyService
	{
		// Token: 0x060001BA RID: 442
		bool OccupantPresentOnArea(BlockObject blockObject, float minDistanceFromArea);
	}
}
