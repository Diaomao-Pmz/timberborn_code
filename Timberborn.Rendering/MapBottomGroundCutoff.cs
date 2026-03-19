using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000013 RID: 19
	public class MapBottomGroundCutoff : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002D1F File Offset: 0x00000F1F
		public MapBottomGroundCutoff(MaterialHeightCutoffSetter materialHeightCutoffSetter)
		{
			this._materialHeightCutoffSetter = materialHeightCutoffSetter;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D30 File Offset: 0x00000F30
		public void Awake()
		{
			foreach (string childName in base.GetComponent<MapBottomGroundCutoffSpec>().Targets)
			{
				MeshRenderer component = base.GameObject.FindChild(childName).GetComponent<MeshRenderer>();
				this._materialHeightCutoffSetter.SetCutoff(component.material, -1f);
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly MaterialHeightCutoffSetter _materialHeightCutoffSetter;
	}
}
