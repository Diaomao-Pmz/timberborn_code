using System;
using UnityEngine;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000015 RID: 21
	public interface IEntityBadge
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003CBF File Offset: 0x00001EBF
		bool EntityBadgeEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A1 RID: 161
		int EntityBadgePriority { get; }

		// Token: 0x060000A2 RID: 162
		string GetEntitySubtitle();

		// Token: 0x060000A3 RID: 163
		ClickableSubtitle GetEntityClickableSubtitle();

		// Token: 0x060000A4 RID: 164
		Sprite GetEntityAvatar();
	}
}
