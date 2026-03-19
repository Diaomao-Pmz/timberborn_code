using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200000E RID: 14
	public class DistrictCenterEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025ED File Offset: 0x000007ED
		public int EntityBadgePriority
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025F1 File Offset: 0x000007F1
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025FF File Offset: 0x000007FF
		public string GetEntitySubtitle()
		{
			return this._labeledEntity.DisplayName;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000260C File Offset: 0x0000080C
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002613 File Offset: 0x00000813
		public Sprite GetEntityAvatar()
		{
			return this._labeledEntity.Image;
		}

		// Token: 0x04000018 RID: 24
		public LabeledEntity _labeledEntity;
	}
}
