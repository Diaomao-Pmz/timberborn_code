using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.GameDistricts;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200000A RID: 10
	public class CitizenTint : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022AB File Offset: 0x000004AB
		public void Awake()
		{
			this._characterTint = base.GetComponent<CharacterTint>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C8 File Offset: 0x000004C8
		public void UpdateTint()
		{
			string entityName = this._citizen.GetComponent<NamedEntity>().EntityName;
			string text = this._citizen.HasAssignedDistrict ? this._citizen.AssignedDistrict.DistrictName : string.Empty;
			Color tint;
			if (ColorParser.TryGetColorFromText(entityName, out tint) || ColorParser.TryGetColorFromText(text, out tint))
			{
				this._characterTint.SetTint(tint);
				return;
			}
			this._characterTint.DisableTint();
		}

		// Token: 0x0400000D RID: 13
		public CharacterTint _characterTint;

		// Token: 0x0400000E RID: 14
		public Citizen _citizen;
	}
}
