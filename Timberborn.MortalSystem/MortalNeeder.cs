using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000010 RID: 16
	public class MortalNeeder : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002CEC File Offset: 0x00000EEC
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._mortal = base.GetComponent<Mortal>();
			this._needManager.NeedChangedIsAtMinimumState += this.OnNeedChangedIsAtMinimumState;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void OnNeedChangedIsAtMinimumState(object sender, NeedChangedIsAtMinimumStateEventArgs e)
		{
			this.NeedDeathUpdate(e.NeedSpec, e.IsAtMinimum);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D34 File Offset: 0x00000F34
		public void NeedDeathUpdate(NeedSpec needSpec, bool isAtMinimum)
		{
			if (isAtMinimum)
			{
				LethalNeedSpec spec = needSpec.GetSpec<LethalNeedSpec>();
				if (spec != null)
				{
					string firstName = this._needManager.GetComponent<Character>().FirstName;
					string value = spec.DeathMessage.Value;
					this._mortal.DiePubliclyAsSoonAsPossible(firstName + " " + value);
				}
			}
		}

		// Token: 0x04000034 RID: 52
		public NeedManager _needManager;

		// Token: 0x04000035 RID: 53
		public Mortal _mortal;
	}
}
