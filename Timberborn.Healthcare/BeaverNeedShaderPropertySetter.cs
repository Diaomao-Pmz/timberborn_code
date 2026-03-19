using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.NeedSystem;
using UnityEngine;

namespace Timberborn.Healthcare
{
	// Token: 0x0200000B RID: 11
	public class BeaverNeedShaderPropertySetter : BaseComponent, IAwakableComponent, IDeadNeededComponent, IInitializableEntity
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
			this._beaverNeedShaderPropertySetterSpec = base.GetComponent<BeaverNeedShaderPropertySetterSpec>();
			this._propertyIds = this._beaverNeedShaderPropertySetterSpec.PropertySets.ToDictionary((BeaverNeedShaderPropertySet s) => s, (BeaverNeedShaderPropertySet s) => Shader.PropertyToID(s.PropertyName));
			base.GetComponent<NeedManager>().NeedChangedActiveState += this.OnNeedChangedActiveState;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002960 File Offset: 0x00000B60
		public void InitializeEntity()
		{
			this.UpdateAllParameters();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002968 File Offset: 0x00000B68
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			foreach (BeaverNeedShaderPropertySet beaverNeedShaderPropertySet in this._beaverNeedShaderPropertySetterSpec.PropertySets)
			{
				if (e.NeedSpec.Id == beaverNeedShaderPropertySet.NeedId)
				{
					this.UpdateParameter(beaverNeedShaderPropertySet, e.IsActive);
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void UpdateAllParameters()
		{
			foreach (BeaverNeedShaderPropertySet beaverNeedShaderPropertySet in this._beaverNeedShaderPropertySetterSpec.PropertySets)
			{
				bool isNeedActive = base.GetComponent<NeedManager>().NeedIsActive(beaverNeedShaderPropertySet.NeedId);
				this.UpdateParameter(beaverNeedShaderPropertySet, isNeedActive);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A12 File Offset: 0x00000C12
		public void UpdateParameter(BeaverNeedShaderPropertySet propertySet, bool isNeedActive)
		{
			this._characterMaterialModifier.SetFloat(this._propertyIds[propertySet], (float)(isNeedActive ? 1 : 0));
		}

		// Token: 0x0400001C RID: 28
		public CharacterMaterialModifier _characterMaterialModifier;

		// Token: 0x0400001D RID: 29
		public BeaverNeedShaderPropertySetterSpec _beaverNeedShaderPropertySetterSpec;

		// Token: 0x0400001E RID: 30
		public Dictionary<BeaverNeedShaderPropertySet, int> _propertyIds;
	}
}
