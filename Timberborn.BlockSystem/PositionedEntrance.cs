using System;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200005F RID: 95
	public class PositionedEntrance
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000077C7 File Offset: 0x000059C7
		public Vector3Int Coordinates { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000077CF File Offset: 0x000059CF
		public Direction2D Direction2D { get; }

		// Token: 0x0600026C RID: 620 RVA: 0x000077D7 File Offset: 0x000059D7
		public PositionedEntrance(Vector3Int coordinates, Direction2D direction2D)
		{
			this.Coordinates = coordinates;
			this.Direction2D = direction2D;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000077F0 File Offset: 0x000059F0
		public static PositionedEntrance From(Blocks blocks, EntranceBlockSpec spec, Placement placement)
		{
			if (spec.HasEntrance)
			{
				Vector3Int coordinates = spec.Coordinates;
				Direction2D direction2D = placement.Orientation.Transform(PositionedEntrance.OnlyAllowedEntranceDirection);
				return new PositionedEntrance(blocks.Transform(coordinates, placement), direction2D);
			}
			return null;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000782E File Offset: 0x00005A2E
		public Vector3Int DoorstepCoordinates
		{
			get
			{
				return this.Coordinates - this.Direction2D.ToOffset();
			}
		}

		// Token: 0x04000127 RID: 295
		public static readonly Direction2D OnlyAllowedEntranceDirection;
	}
}
