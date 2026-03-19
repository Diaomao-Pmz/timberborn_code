using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001F RID: 31
	public class NeedPenaltyManager : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000339D File Offset: 0x0000159D
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033AC File Offset: 0x000015AC
		public void Start()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._needManager.NeedChangedIsFavorable += delegate(object _, NeedChangedIsFavorableEventArgs e)
			{
				this.UpdatePenalties(e.NeedSpec);
			};
			foreach (NeedSpec needSpec in this._needManager.NeedSpecs)
			{
				PunitiveNeedSpec spec = needSpec.GetSpec<PunitiveNeedSpec>();
				if (spec != null && !this._needManager.NeedIsFavorable(needSpec.Id))
				{
					this.AddPenalties(spec);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003428 File Offset: 0x00001628
		public void UpdatePenalties(NeedSpec needSpec)
		{
			PunitiveNeedSpec spec = needSpec.GetSpec<PunitiveNeedSpec>();
			if (spec != null)
			{
				if (!this._needManager.NeedIsFavorable(needSpec.Id))
				{
					this.AddPenalties(spec);
					return;
				}
				this.RemovePenalties(spec);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003464 File Offset: 0x00001664
		public void AddPenalties(PunitiveNeedSpec punitiveNeedSpec)
		{
			foreach (BonusSpec bonusSpec in punitiveNeedSpec.Penalties)
			{
				this._bonusManager.AddBonus(bonusSpec.Id, bonusSpec.MultiplierDelta);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000034AC File Offset: 0x000016AC
		public void RemovePenalties(PunitiveNeedSpec punitiveNeedSpec)
		{
			foreach (BonusSpec bonusSpec in punitiveNeedSpec.Penalties)
			{
				this._bonusManager.RemoveBonus(bonusSpec.Id, bonusSpec.MultiplierDelta);
			}
		}

		// Token: 0x0400004B RID: 75
		public BonusManager _bonusManager;

		// Token: 0x0400004C RID: 76
		public NeedManager _needManager;
	}
}
