using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.Effects;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x0200000B RID: 11
	public class DwellingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000025E4 File Offset: 0x000007E4
		public DwellingDescriber(EffectDescriber effectDescriber, ILoc loc)
		{
			this._effectDescriber = effectDescriber;
			this._loc = loc;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002605 File Offset: 0x00000805
		public void Awake()
		{
			this._dwelling = base.GetComponent<Dwelling>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002613 File Offset: 0x00000813
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!this._dwelling.Enabled)
			{
				string str = this._loc.T<int>(DwellingDescriber.InhabitantsLocKey, this._dwelling.MaxBeavers);
				string content = SpecialStrings.RowStarter + str;
				yield return EntityDescription.CreateTextSection(content, 30);
			}
			if (this._dwelling.SleepEffects.Count > 0)
			{
				this._effectDescriber.DescribeEffects(this._dwelling.SleepEffects, this._description);
				yield return EntityDescription.CreateTextSection(this._description.ToStringWithoutNewLineEndAndClean(), 1000);
			}
			yield break;
		}

		// Token: 0x04000021 RID: 33
		public static readonly string InhabitantsLocKey = "Dwelling.Inhabitants";

		// Token: 0x04000022 RID: 34
		public readonly EffectDescriber _effectDescriber;

		// Token: 0x04000023 RID: 35
		public readonly ILoc _loc;

		// Token: 0x04000024 RID: 36
		public Dwelling _dwelling;

		// Token: 0x04000025 RID: 37
		public readonly StringBuilder _description = new StringBuilder();
	}
}
