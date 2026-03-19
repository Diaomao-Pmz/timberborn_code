using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.Characters;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.GameFactionSystem;
using Timberborn.Localization;
using Timberborn.MortalSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BeaversUI
{
	// Token: 0x0200000D RID: 13
	public class BeaverEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002B39 File Offset: 0x00000D39
		public BeaverEntityBadge(FactionService factionService, ILoc loc, EntitySelectionService entitySelectionService)
		{
			this._factionService = factionService;
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002B56 File Offset: 0x00000D56
		public int EntityBadgePriority
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B5A File Offset: 0x00000D5A
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
			this._child = base.GetComponent<Child>();
			this._character = base.GetComponent<Character>();
			this._citizen = base.GetComponent<Citizen>();
			this._contaminable = base.GetComponent<Contaminable>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B98 File Offset: 0x00000D98
		public string GetEntitySubtitle()
		{
			string text = this.Age();
			if (!this._mortal.Dead)
			{
				return text;
			}
			return text + " " + this._loc.T(BeaverEntityBadge.DeadNameSuffixLocKey);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			if (this._citizen.HasAssignedDistrict)
			{
				DistrictCenter district = this._citizen.AssignedDistrict;
				return ClickableSubtitle.Create(delegate()
				{
					this._entitySelectionService.SelectAndFocusOn(district);
				}, district.DistrictName);
			}
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C34 File Offset: 0x00000E34
		public Sprite GetEntityAvatar()
		{
			if (this._contaminable && this._contaminable.IsContaminated)
			{
				if (!this._child)
				{
					return this._factionService.Current.ContaminatedAdultAvatar.Asset;
				}
				return this._factionService.Current.ContaminatedChildAvatar.Asset;
			}
			else
			{
				if (!this._child)
				{
					return this._factionService.Current.Avatar.Asset;
				}
				return this._factionService.Current.ChildAvatar.Asset;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002CCC File Offset: 0x00000ECC
		public string Age()
		{
			return this._loc.T<int>(BeaverEntityBadge.AgeLocKey, this._character.Age);
		}

		// Token: 0x0400003A RID: 58
		public static readonly string AgeLocKey = "Beaver.Age";

		// Token: 0x0400003B RID: 59
		public static readonly string DeadNameSuffixLocKey = "Beaver.DeadNameSuffix";

		// Token: 0x0400003C RID: 60
		public readonly FactionService _factionService;

		// Token: 0x0400003D RID: 61
		public readonly ILoc _loc;

		// Token: 0x0400003E RID: 62
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400003F RID: 63
		public Mortal _mortal;

		// Token: 0x04000040 RID: 64
		public Child _child;

		// Token: 0x04000041 RID: 65
		public Character _character;

		// Token: 0x04000042 RID: 66
		public Citizen _citizen;

		// Token: 0x04000043 RID: 67
		public Contaminable _contaminable;
	}
}
