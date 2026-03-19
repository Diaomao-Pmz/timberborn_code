using System;
using Timberborn.BlockSystem;
using Timberborn.Planting;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200000E RID: 14
	public class PlantablePreviewService : ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002825 File Offset: 0x00000A25
		public PlantablePreviewService(EventBus eventBus, ITerrainService terrainService, IBlockService blockService, PlantablePreviewFactory plantablePreviewFactory, PlantingService plantingService)
		{
			this._eventBus = eventBus;
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._plantablePreviewFactory = plantablePreviewFactory;
			this._plantingService = plantingService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002854 File Offset: 0x00000A54
		public void Load()
		{
			this._eventBus.Register(this);
			this._previews = new PlantablePreview[this._terrainService.Size.x, this._terrainService.Size.y, this._terrainService.Size.z];
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028B4 File Offset: 0x00000AB4
		public void PostLoad()
		{
			foreach (Vector3Int vector3Int in this._plantingService.PlantingCoordinates)
			{
				string resourceAt = this._plantingService.GetResourceAt(vector3Int);
				this.CreatePreview(resourceAt, vector3Int).Hide();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000291C File Offset: 0x00000B1C
		public PlantablePreview GetPreview(Vector3Int coordinates)
		{
			return this._previews[coordinates.x, coordinates.y, coordinates.z];
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000293E File Offset: 0x00000B3E
		public void ShowPreview(Vector3Int coordinates)
		{
			this._previews[coordinates.x, coordinates.y, coordinates.z].Show();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002968 File Offset: 0x00000B68
		public void HidePreviews()
		{
			PlantablePreview[,,] previews = this._previews;
			int upperBound = previews.GetUpperBound(0);
			int upperBound2 = previews.GetUpperBound(1);
			int upperBound3 = previews.GetUpperBound(2);
			for (int i = previews.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = previews.GetLowerBound(1); j <= upperBound2; j++)
				{
					for (int k = previews.GetLowerBound(2); k <= upperBound3; k++)
					{
						PlantablePreviewService.HidePreview(previews[i, j, k]);
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029E7 File Offset: 0x00000BE7
		public void HidePreview(Vector3Int coordinates)
		{
			if (this._terrainService.Contains(coordinates))
			{
				PlantablePreviewService.HidePreview(this._previews[coordinates.x, coordinates.y, coordinates.z]);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A1C File Offset: 0x00000C1C
		[OnEvent]
		public void OnPlantingCoordinatesSet(PlantingCoordinatesSetEvent plantingCoordinatesSetEvent)
		{
			this.CreatePreview(plantingCoordinatesSetEvent.Resource, plantingCoordinatesSetEvent.Coordinates);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A34 File Offset: 0x00000C34
		[OnEvent]
		public void OnPlantingCoordinatesUnset(PlantingCoordinatesUnsetEvent plantingCoordinatesUnsetEvent)
		{
			Vector3Int coordinates = plantingCoordinatesUnsetEvent.Coordinates;
			PlantablePreview plantablePreview = this._previews[coordinates.x, coordinates.y, coordinates.z];
			Object.Destroy((plantablePreview != null) ? plantablePreview.GameObject : null);
			this._previews[coordinates.x, coordinates.y, coordinates.z] = null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A9C File Offset: 0x00000C9C
		public bool ShouldShowPreview(Vector3Int coordinates)
		{
			Plantable bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Plantable>(coordinates);
			if (bottomObjectComponentAt != null && !bottomObjectComponentAt.GetComponent<BlockObject>().Overridable)
			{
				string resourceAt = this._plantingService.GetResourceAt(coordinates);
				return bottomObjectComponentAt.GetComponent<TemplateSpec>().TemplateName != resourceAt;
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public static void HidePreview(PlantablePreview preview)
		{
			if (preview)
			{
				preview.Hide();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public PlantablePreview CreatePreview(string resource, Vector3Int coords)
		{
			PlantablePreview plantablePreview = this._plantablePreviewFactory.CreatePreview(resource, coords);
			this._previews[coords.x, coords.y, coords.z] = plantablePreview;
			if (this.ShouldShowPreview(coords))
			{
				plantablePreview.Show();
			}
			else
			{
				plantablePreview.Hide();
			}
			return plantablePreview;
		}

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly ITerrainService _terrainService;

		// Token: 0x0400002B RID: 43
		public readonly IBlockService _blockService;

		// Token: 0x0400002C RID: 44
		public readonly PlantablePreviewFactory _plantablePreviewFactory;

		// Token: 0x0400002D RID: 45
		public readonly PlantingService _plantingService;

		// Token: 0x0400002E RID: 46
		public PlantablePreview[,,] _previews;
	}
}
