using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CameraSystem;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.CharacterControlSystemUI
{
	// Token: 0x02000004 RID: 4
	public class CharacterControlDestinationPicker
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CharacterControlDestinationPicker(SelectableObjectRaycaster selectableObjectRaycaster, TerrainPicker terrainPicker, CameraService cameraService, InputService inputService)
		{
			this._selectableObjectRaycaster = selectableObjectRaycaster;
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._inputService = inputService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public Vector3? PickDestination()
		{
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			SelectableObject hitObject;
			if (this._selectableObjectRaycaster.TryHitSelectableObject(out hitObject))
			{
				Vector3? vector = CharacterControlDestinationPicker.PickDestination(hitObject);
				if (vector != null)
				{
					Vector3 valueOrDefault = vector.GetValueOrDefault();
					return new Vector3?(valueOrDefault);
				}
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				return new Vector3?(traversedCoordinates.GetValueOrDefault().Intersection);
			}
			return null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002174 File Offset: 0x00000374
		public static Vector3? PickDestination(BaseComponent hitObject)
		{
			BlockObject component = hitObject.GetComponent<BlockObject>();
			if (component != null)
			{
				ImmutableArray<Block> allBlocks = component.PositionedBlocks.GetAllBlocks();
				if (allBlocks.Any((Block block) => block.Stackable > BlockStackable.None))
				{
					int num = allBlocks.Max((Block block) => block.Coordinates.z);
					Vector3 gridCenterGrounded = component.GetComponent<BlockObjectCenter>().GridCenterGrounded;
					return new Vector3?(new Vector3(gridCenterGrounded.x, gridCenterGrounded.y, (float)(num + 1)));
				}
				if (component.HasEntrance)
				{
					return new Vector3?(component.PositionedEntrance.DoorstepCoordinates);
				}
			}
			return null;
		}

		// Token: 0x04000006 RID: 6
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000007 RID: 7
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000008 RID: 8
		public readonly CameraService _cameraService;

		// Token: 0x04000009 RID: 9
		public readonly InputService _inputService;
	}
}
