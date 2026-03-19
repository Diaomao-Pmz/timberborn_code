using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000021 RID: 33
	public class PlantingMapSerializer : IValueSerializer<PlantingMap>
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003A03 File Offset: 0x00001C03
		public PlantingMapSerializer(TemplateService templateService, ITerrainService terrainService)
		{
			this._templateService = templateService;
			this._terrainService = terrainService;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A1C File Offset: 0x00001C1C
		public void Serialize(PlantingMap plantingMap, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			foreach (KeyValuePair<string, List<Vector3Int>> keyValuePair in PlantingMapSerializer.GetResourcesToSerialize(plantingMap))
			{
				string text;
				List<Vector3Int> list;
				keyValuePair.Deconstruct(ref text, ref list);
				string name = text;
				List<Vector3Int> values = list;
				objectSaver.Set(new ListKey<Vector3Int>(name), values);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A90 File Offset: 0x00001C90
		public Obsoletable<PlantingMap> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			List<TemplateSpec> list = (from plantable in this._templateService.GetAll<PlantableSpec>()
			select plantable.GetSpec<TemplateSpec>()).ToList<TemplateSpec>();
			PlantingMap plantingMap = new PlantingMap(this._terrainService.Size);
			foreach (TemplateSpec templateSpec in list)
			{
				PlantingMapSerializer.SetPlantingMap(objectLoader, plantingMap, templateSpec);
			}
			foreach (TemplateSpec templateSpec2 in list)
			{
				PlantingMapSerializer.SetBackwardCompatiblePlantingMap(objectLoader, plantingMap, templateSpec2);
			}
			return plantingMap;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003B74 File Offset: 0x00001D74
		public static Dictionary<string, List<Vector3Int>> GetResourcesToSerialize(PlantingMap map)
		{
			Dictionary<string, List<Vector3Int>> dictionary = new Dictionary<string, List<Vector3Int>>();
			foreach (Vector3Int vector3Int in map.GetCoordinatesWithSetResource())
			{
				string resource = map.GetResource(vector3Int);
				dictionary.GetOrAdd(resource, () => new List<Vector3Int>()).Add(vector3Int);
			}
			return dictionary;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public static void SetPlantingMap(IObjectLoader objectLoader, PlantingMap plantingMap, TemplateSpec templateSpec)
		{
			IEnumerable<Vector3Int> resourceCoordinates = PlantingMapSerializer.GetResourceCoordinates(objectLoader, templateSpec.TemplateName);
			plantingMap.SetResource(resourceCoordinates, templateSpec.TemplateName);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003C20 File Offset: 0x00001E20
		public static void SetBackwardCompatiblePlantingMap(IObjectLoader objectLoader, PlantingMap plantingMap, TemplateSpec templateSpec)
		{
			foreach (string nameToCheck in templateSpec.BackwardCompatibleTemplateNames)
			{
				IEnumerable<Vector3Int> resourceCoordinates = PlantingMapSerializer.GetResourceCoordinates(objectLoader, nameToCheck);
				plantingMap.SetResourceIfEmpty(resourceCoordinates, templateSpec.TemplateName);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003C64 File Offset: 0x00001E64
		public static IEnumerable<Vector3Int> GetResourceCoordinates(IObjectLoader objectLoader, string nameToCheck)
		{
			ListKey<Vector3Int> key = new ListKey<Vector3Int>(nameToCheck);
			if (objectLoader.Has<Vector3Int>(key))
			{
				List<Vector3Int> list = objectLoader.Get(key);
				foreach (Vector3Int vector3Int in list)
				{
					yield return vector3Int;
				}
				List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x0400005C RID: 92
		public readonly TemplateService _templateService;

		// Token: 0x0400005D RID: 93
		public readonly ITerrainService _terrainService;
	}
}
