using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesMoisture;

namespace Timberborn.NaturalResourcesMoistureUI
{
	// Token: 0x02000008 RID: 8
	public class WateredNaturalResourceDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000023E2 File Offset: 0x000005E2
		public WateredNaturalResourceDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F1 File Offset: 0x000005F1
		public void Awake()
		{
			this._wateredNaturalResource = base.GetComponent<WateredNaturalResourceSpec>();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023FF File Offset: 0x000005FF
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			float daysToDieDry = this._wateredNaturalResource.DaysToDieDry;
			if (daysToDieDry > 0f)
			{
				string content = SpecialStrings.RowStarter + this._loc.T<string>(WateredNaturalResourceDescriber.DroughtResistanceLocKey, daysToDieDry.ToString("0.#"));
				yield return EntityDescription.CreateTextSection(content, 2050);
			}
			yield break;
		}

		// Token: 0x04000014 RID: 20
		public static readonly string DroughtResistanceLocKey = "NaturalResources.DroughtResistance";

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public WateredNaturalResourceSpec _wateredNaturalResource;
	}
}
