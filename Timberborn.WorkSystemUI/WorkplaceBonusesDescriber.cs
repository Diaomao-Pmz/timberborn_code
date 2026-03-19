using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000012 RID: 18
	public class WorkplaceBonusesDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002DBF File Offset: 0x00000FBF
		public WorkplaceBonusesDescriber(BonusDescriber bonusDescriber)
		{
			this._bonusDescriber = bonusDescriber;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DCE File Offset: 0x00000FCE
		public void Awake()
		{
			this._workplaceBonuses = base.GetComponent<WorkplaceBonuses>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DDC File Offset: 0x00000FDC
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (BonusSpec bonus in this._workplaceBonuses.WorkerBonuses)
			{
				this.AppendBonusDescription(stringBuilder, bonus);
			}
			if (stringBuilder.Length > 0)
			{
				yield return EntityDescription.CreateTextSection(stringBuilder.ToString(), 90);
			}
			yield break;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DEC File Offset: 0x00000FEC
		public void AppendBonusDescription(StringBuilder description, BonusSpec bonus)
		{
			description.AppendLine(SpecialStrings.RowStarter + this._bonusDescriber.Describe(bonus));
		}

		// Token: 0x0400004D RID: 77
		public readonly BonusDescriber _bonusDescriber;

		// Token: 0x0400004E RID: 78
		public WorkplaceBonuses _workplaceBonuses;
	}
}
