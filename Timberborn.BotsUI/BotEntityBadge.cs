using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.GameFactionSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BotsUI
{
	// Token: 0x02000007 RID: 7
	public class BotEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BotEntityBadge(ILoc loc, EntitySelectionService entitySelectionService, FactionService factionService)
		{
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
			this._factionService = factionService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000211B File Offset: 0x0000031B
		public int EntityBadgePriority
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211F File Offset: 0x0000031F
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		public string GetEntitySubtitle()
		{
			string text = this.Age();
			if (!this._character.Alive)
			{
				return text + " " + this._loc.T(BotEntityBadge.DeadNameSuffixLocKey);
			}
			return text;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
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

		// Token: 0x0600000C RID: 12 RVA: 0x000021D6 File Offset: 0x000003D6
		public Sprite GetEntityAvatar()
		{
			return this._factionService.Current.BotAvatar.Asset;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021ED File Offset: 0x000003ED
		public string Age()
		{
			return this._loc.T<int>(BotEntityBadge.AgeLocKey, this._character.Age);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string AgeLocKey = "Beaver.Age";

		// Token: 0x04000009 RID: 9
		public static readonly string DeadNameSuffixLocKey = "Bot.DeadNameSuffix";

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000C RID: 12
		public readonly FactionService _factionService;

		// Token: 0x0400000D RID: 13
		public Character _character;

		// Token: 0x0400000E RID: 14
		public Citizen _citizen;
	}
}
