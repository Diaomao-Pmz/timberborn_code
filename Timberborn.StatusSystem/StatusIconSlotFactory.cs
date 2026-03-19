using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000019 RID: 25
	public class StatusIconSlotFactory
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003734 File Offset: 0x00001934
		public StatusSlot CreateBounded(SlotConstraints slotConstraints, TopBoundForLayer topBoundForLayer, float statusZCoordinate, byte baseZ)
		{
			if ((slotConstraints.IsOccupied && !slotConstraints.InvalidInConstructionMode) || statusZCoordinate < topBoundForLayer.NormalModeTopBound)
			{
				return StatusSlot.CreateValidAboveMaxVisibilityLevel(statusZCoordinate, baseZ);
			}
			if ((!slotConstraints.InvalidInConstructionMode && statusZCoordinate >= topBoundForLayer.ConstructionModeTopBound) || slotConstraints.ForceValidInConstructionMode)
			{
				return StatusSlot.CreateAlwaysValid(statusZCoordinate);
			}
			return StatusSlot.CreateInvalidInConstructionMode(statusZCoordinate, baseZ);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003791 File Offset: 0x00001991
		public StatusSlot CreateUnbounded(SlotConstraints slotConstraints, float statusZCoordinate, byte baseZ)
		{
			if (slotConstraints.IsOccupied && !slotConstraints.InvalidInConstructionMode)
			{
				return StatusSlot.CreateValidAboveMaxVisibilityLevel(statusZCoordinate, baseZ);
			}
			if (!slotConstraints.InvalidInConstructionMode)
			{
				return StatusSlot.CreateAlwaysValid(statusZCoordinate);
			}
			return StatusSlot.CreateInvalidInConstructionMode(statusZCoordinate, baseZ);
		}
	}
}
