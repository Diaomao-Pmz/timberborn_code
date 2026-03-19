using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000019 RID: 25
	public class LabeledEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public int EntityBadgePriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003CC5 File Offset: 0x00001EC5
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
			this._namedEntity = base.GetComponent<NamedEntity>();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public string GetEntitySubtitle()
		{
			NamedEntity namedEntity = this._namedEntity;
			if (namedEntity == null || !namedEntity.IsEditable)
			{
				return "";
			}
			return this._labeledEntity.DisplayName;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D10 File Offset: 0x00001F10
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D17 File Offset: 0x00001F17
		public Sprite GetEntityAvatar()
		{
			return this._labeledEntity.Image;
		}

		// Token: 0x04000079 RID: 121
		public LabeledEntity _labeledEntity;

		// Token: 0x0400007A RID: 122
		public NamedEntity _namedEntity;

		// Token: 0x0400007B RID: 123
		public Sprite _image;
	}
}
