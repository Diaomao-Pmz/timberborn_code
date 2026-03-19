using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameFactionSystem;
using Timberborn.Localization;
using Timberborn.NeedApplication;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedApplicationUI
{
	// Token: 0x02000005 RID: 5
	public class NeedEffectsSpecDescriber : BaseComponent, IEntityDescriber, IAwakableComponent
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		public NeedEffectsSpecDescriber(EffectProbabilityService effectProbabilityService, FactionNeedService factionNeedService, NeedSpecFormatter needSpecFormatter, ILoc loc)
		{
			this._effectProbabilityService = effectProbabilityService;
			this._factionNeedService = factionNeedService;
			this._needSpecFormatter = needSpecFormatter;
			this._loc = loc;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002133 File Offset: 0x00000333
		public void Awake()
		{
			this._spec = base.GetComponent<INeedEffectsSpec>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002141 File Offset: 0x00000341
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._effectProbabilityService.CanApplyEffects)
			{
				this._descriptionBuilder.Clear();
				foreach (NeedApplierEffectSpec needApplierEffectSpec in this._spec.Effects)
				{
					NeedSpec beaverOrBotNeedById = this._factionNeedService.GetBeaverOrBotNeedById(needApplierEffectSpec.NeedId);
					string param = this._needSpecFormatter.ColorizeNeedByEffect(beaverOrBotNeedById);
					string displayName = ProbabilityDescriptionHelper.GetDisplayName(needApplierEffectSpec.Probability);
					string str = this._loc.T<string>(displayName, param);
					this._descriptionBuilder.AppendLine(SpecialStrings.RowStarter + str);
				}
				yield return EntityDescription.CreateTextSection(this._descriptionBuilder.ToStringWithoutNewLineEndAndClean(), 3000);
			}
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public readonly EffectProbabilityService _effectProbabilityService;

		// Token: 0x04000007 RID: 7
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x04000008 RID: 8
		public readonly NeedSpecFormatter _needSpecFormatter;

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public INeedEffectsSpec _spec;

		// Token: 0x0400000B RID: 11
		public readonly StringBuilder _descriptionBuilder = new StringBuilder();
	}
}
