using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000013 RID: 19
	public class FloodgateGateCutoff : BaseComponent, IInitializableEntity, IPostPlacementChangeListener
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000379C File Offset: 0x0000199C
		public FloodgateGateCutoff(MaterialHeightCutoffSetter materialHeightCutoffSetter)
		{
			this._materialHeightCutoffSetter = materialHeightCutoffSetter;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000037AB File Offset: 0x000019AB
		public void InitializeEntity()
		{
			this.UpdateCutoff();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000037AB File Offset: 0x000019AB
		public void OnPostPlacementChanged()
		{
			this.UpdateCutoff();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000037B4 File Offset: 0x000019B4
		public void UpdateCutoff()
		{
			BlockObjectCenter component = base.GetComponent<BlockObjectCenter>();
			MeshRenderer componentInChildren = base.GetComponent<FloodgateAnimationController>().Gate.GetComponentInChildren<MeshRenderer>();
			this._materialHeightCutoffSetter.SetCutoff(componentInChildren.material, component.WorldCenterGrounded.y);
		}

		// Token: 0x04000043 RID: 67
		public readonly MaterialHeightCutoffSetter _materialHeightCutoffSetter;
	}
}
