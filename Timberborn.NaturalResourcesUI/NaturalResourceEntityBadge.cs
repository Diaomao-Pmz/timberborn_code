using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Cutting;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.Growing;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.NaturalResourcesUI
{
	// Token: 0x02000006 RID: 6
	public class NaturalResourceEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021D7 File Offset: 0x000003D7
		public NaturalResourceEntityBadge(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021E6 File Offset: 0x000003E6
		public int EntityBadgePriority
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021ED File Offset: 0x000003ED
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
			this._growable = base.GetComponent<Growable>();
			this._cuttable = base.GetComponent<Cuttable>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002214 File Offset: 0x00000414
		public string GetEntitySubtitle()
		{
			if (this._cuttable && this._cuttable.Yielder.IsYieldRemoved)
			{
				return this._loc.T(NaturalResourceEntityBadge.LeftoverLocKey);
			}
			if (this._growable && !this._growable.IsGrown)
			{
				return this._loc.T(NaturalResourceEntityBadge.SeedlingLocKey);
			}
			return "";
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002281 File Offset: 0x00000481
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002288 File Offset: 0x00000488
		public Sprite GetEntityAvatar()
		{
			return this._labeledEntity.Image;
		}

		// Token: 0x0400000C RID: 12
		public static readonly string SeedlingLocKey = "NaturalResources.Seedling";

		// Token: 0x0400000D RID: 13
		public static readonly string LeftoverLocKey = "NaturalResources.Leftover";

		// Token: 0x0400000E RID: 14
		public readonly ILoc _loc;

		// Token: 0x0400000F RID: 15
		public LabeledEntity _labeledEntity;

		// Token: 0x04000010 RID: 16
		public Growable _growable;

		// Token: 0x04000011 RID: 17
		public Cuttable _cuttable;
	}
}
