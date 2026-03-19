using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001C RID: 28
	public class PreviewBlockingCoordinatesChangedEvent
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003D9D File Offset: 0x00001F9D
		public ReadOnlyList<Vector3Int> ChangedCoordinates { get; }

		// Token: 0x060000FC RID: 252 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public PreviewBlockingCoordinatesChangedEvent(ReadOnlyList<Vector3Int> changedCoordinates)
		{
			this.ChangedCoordinates = changedCoordinates;
		}
	}
}
