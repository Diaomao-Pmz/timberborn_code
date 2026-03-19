using System;
using Timberborn.Debugging;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x0200000F RID: 15
	public class PotentialSpotsToggler : IDevModule, IUpdatableSingleton
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000271D File Offset: 0x0000091D
		public PotentialSpotsToggler(NaturalResourceReproducer naturalResourceReproducer, AreaHighlightingService areaHighlightingService)
		{
			this._naturalResourceReproducer = naturalResourceReproducer;
			this._areaHighlightingService = areaHighlightingService;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002733 File Offset: 0x00000933
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Highlight resource reproduction spots", new Action(this.TogglePotentialSpots))).Build();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000275A File Offset: 0x0000095A
		public void UpdateSingleton()
		{
			if (this._showingSpots)
			{
				this.DrawPotentialSpots();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000276A File Offset: 0x0000096A
		public void TogglePotentialSpots()
		{
			this._showingSpots = !this._showingSpots;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000277C File Offset: 0x0000097C
		public void DrawPotentialSpots()
		{
			foreach (Vector3Int coordinates in this._naturalResourceReproducer.PotentialSpots)
			{
				this._areaHighlightingService.DrawTile(coordinates, Color.magenta);
			}
		}

		// Token: 0x04000018 RID: 24
		public readonly NaturalResourceReproducer _naturalResourceReproducer;

		// Token: 0x04000019 RID: 25
		public readonly AreaHighlightingService _areaHighlightingService;

		// Token: 0x0400001A RID: 26
		public bool _showingSpots;
	}
}
