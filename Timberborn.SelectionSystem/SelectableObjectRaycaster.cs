using System;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000016 RID: 22
	public class SelectableObjectRaycaster
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000034FF File Offset: 0x000016FF
		public SelectableObjectRaycaster(TerrainPicker terrainPicker, CameraService cameraService, InputService inputService, SelectableObjectRetriever selectableObjectRetriever, ITerrainService terrainService)
		{
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._inputService = inputService;
			this._selectableObjectRetriever = selectableObjectRetriever;
			this._terrainService = terrainService;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000352C File Offset: 0x0000172C
		public bool TryHitSelectableObjectIncludeTerrainStump(Ray worldSpaceRay, out SelectableObject hitObject, out RaycastHit raycastHit)
		{
			return this.TryHitSelectableObject(worldSpaceRay, true, out hitObject, out raycastHit);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003538 File Offset: 0x00001738
		public bool TryHitSelectableObjectIncludeTerrainStump(out SelectableObject hitObject)
		{
			Ray worldSpaceRay = this._cameraService.ScreenPointToRayInWorldSpace(this._inputService.MousePosition);
			RaycastHit raycastHit;
			return this.TryHitSelectableObject(worldSpaceRay, true, out hitObject, out raycastHit);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000356C File Offset: 0x0000176C
		public bool TryHitSelectableObject(out SelectableObject hitObject)
		{
			Ray worldSpaceRay = this._cameraService.ScreenPointToRayInWorldSpace(this._inputService.MousePosition);
			RaycastHit raycastHit;
			return this.TryHitSelectableObject(worldSpaceRay, false, out hitObject, out raycastHit);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035A0 File Offset: 0x000017A0
		public bool TryHitSelectableObject(Ray worldSpaceRay, bool includeTerrainStump, out SelectableObject hitObject, out RaycastHit raycastHit)
		{
			if (Physics.Raycast(worldSpaceRay, ref raycastHit) && this.HitIsCloserThanTerrain(worldSpaceRay, includeTerrainStump, raycastHit))
			{
				GameObject gameObject = raycastHit.collider.gameObject;
				if (gameObject && this._selectableObjectRetriever.TryGetSelectableObject(gameObject, out hitObject))
				{
					return true;
				}
			}
			hitObject = null;
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035F4 File Offset: 0x000017F4
		public bool HitIsCloserThanTerrain(Ray ray, bool includeTerrainStump, RaycastHit hit)
		{
			float num;
			return !this.HitTerrain(ray, includeTerrainStump, out num) || hit.distance < num;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000361C File Offset: 0x0000181C
		public bool HitTerrain(Ray ray, bool includeTerrainStump, out float distance)
		{
			Ray gridRay = CoordinateSystem.WorldToGrid(ray);
			TraversedCoordinates? traversedCoordinates = this.PickTerrainCoordinates(gridRay, includeTerrainStump);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				distance = Vector3.Distance(gridRay.origin, valueOrDefault.Intersection);
				return !this.WasCutoutHit(valueOrDefault);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003672 File Offset: 0x00001872
		public TraversedCoordinates? PickTerrainCoordinates(Ray gridRay, bool includeTerrainStump)
		{
			if (!includeTerrainStump)
			{
				return this._terrainPicker.PickTerrainCoordinates(gridRay);
			}
			return this._terrainPicker.PickTerrainCoordinatesWithStump(gridRay);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003690 File Offset: 0x00001890
		public bool WasCutoutHit(TraversedCoordinates terrainCoordinates)
		{
			return terrainCoordinates.Face.z == 1 && this._terrainService.CellIsCutout(terrainCoordinates.Coordinates.Above());
		}

		// Token: 0x0400003A RID: 58
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400003B RID: 59
		public readonly CameraService _cameraService;

		// Token: 0x0400003C RID: 60
		public readonly InputService _inputService;

		// Token: 0x0400003D RID: 61
		public readonly SelectableObjectRetriever _selectableObjectRetriever;

		// Token: 0x0400003E RID: 62
		public readonly ITerrainService _terrainService;
	}
}
