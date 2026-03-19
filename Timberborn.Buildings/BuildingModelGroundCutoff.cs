using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200000E RID: 14
	public class BuildingModelGroundCutoff : BaseComponent, IAwakableComponent, IInitializableEntity, IPostPlacementChangeListener
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002A5D File Offset: 0x00000C5D
		public BuildingModelGroundCutoff(MaterialHeightCutoffSetter materialHeightCutoffSetter)
		{
			this._materialHeightCutoffSetter = materialHeightCutoffSetter;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A6C File Offset: 0x00000C6C
		public void Awake()
		{
			this._buildingModelGroundCutoffSpec = base.GetComponent<BuildingModelGroundCutoffSpec>();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002A7A File Offset: 0x00000C7A
		public void InitializeEntity()
		{
			this.UpdateCutoff();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002A7A File Offset: 0x00000C7A
		public void OnPostPlacementChanged()
		{
			this.UpdateCutoff();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002A84 File Offset: 0x00000C84
		public void UpdateCutoff()
		{
			BlockObjectCenter component = base.GetComponent<BlockObjectCenter>();
			foreach (string childName in this._buildingModelGroundCutoffSpec.Targets)
			{
				foreach (Material material in base.GameObject.FindChild(childName).GetComponent<MeshRenderer>().materials)
				{
					this._materialHeightCutoffSetter.SetCutoff(material, component.WorldCenterGrounded.y + this._buildingModelGroundCutoffSpec.Offset);
				}
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly MaterialHeightCutoffSetter _materialHeightCutoffSetter;

		// Token: 0x04000020 RID: 32
		public BuildingModelGroundCutoffSpec _buildingModelGroundCutoffSpec;
	}
}
