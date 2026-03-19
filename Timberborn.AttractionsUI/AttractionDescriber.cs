using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.Attractions;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.AttractionsUI
{
	// Token: 0x02000006 RID: 6
	public class AttractionDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021AB File Offset: 0x000003AB
		public AttractionDescriber(EffectDescriber effectDescriber, ILoc loc)
		{
			this._effectDescriber = effectDescriber;
			this._loc = loc;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
		public void Awake()
		{
			this._attraction = base.GetComponent<Attraction>();
			this._enterable = base.GetComponent<Enterable>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E6 File Offset: 0x000003E6
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!this._enterable.Enabled)
			{
				string str = this._loc.T<int>(AttractionDescriber.VisitorsLimitLocKey, this._enterable.EnterableSpec.CapacityFinished);
				yield return EntityDescription.CreateTextSection(SpecialStrings.RowStarter + str, 10);
			}
			if (this._attraction.Effects.Count > 0)
			{
				this._effectDescriber.DescribeEffects(this._attraction.Effects, this._description);
				yield return EntityDescription.CreateTextSection(this._description.ToStringWithoutNewLineEndAndClean(), 1010);
			}
			yield break;
		}

		// Token: 0x0400000C RID: 12
		public static readonly string VisitorsLimitLocKey = "Attractions.VisitorsLimit";

		// Token: 0x0400000D RID: 13
		public readonly EffectDescriber _effectDescriber;

		// Token: 0x0400000E RID: 14
		public readonly ILoc _loc;

		// Token: 0x0400000F RID: 15
		public Attraction _attraction;

		// Token: 0x04000010 RID: 16
		public Enterable _enterable;

		// Token: 0x04000011 RID: 17
		public readonly StringBuilder _description = new StringBuilder();
	}
}
