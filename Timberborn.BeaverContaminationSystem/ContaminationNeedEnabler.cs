using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x0200000C RID: 12
	public class ContaminationNeedEnabler : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000275C File Offset: 0x0000095C
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._contaminable = base.GetComponent<Contaminable>();
			this._contaminable.ContaminationChanged += this.OnContaminationChanged;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000278D File Offset: 0x0000098D
		public void InitializeEntity()
		{
			this.UpdateNeeds();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000278D File Offset: 0x0000098D
		public void OnContaminationChanged(object sender, EventArgs e)
		{
			this.UpdateNeeds();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002798 File Offset: 0x00000998
		public void UpdateNeeds()
		{
			foreach (NeedSpec needSpec in this._needManager.NeedSpecs)
			{
				if (this.ShouldBeEnabled(needSpec))
				{
					this._needManager.EnableNeed(needSpec.Id);
				}
				else
				{
					this._needManager.ResetNeed(needSpec.Id);
					this._needManager.DisableNeed(needSpec.Id);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000280A File Offset: 0x00000A0A
		public bool ShouldBeEnabled(NeedSpec needSpec)
		{
			if (ContaminationNeedEnabler.IsEnabledOnlyWhenContaminated(needSpec))
			{
				return this._contaminable.IsContaminated;
			}
			return !ContaminationNeedEnabler.IsDisabledWhenContaminated(needSpec) || !this._contaminable.IsContaminated;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002838 File Offset: 0x00000A38
		public static bool IsEnabledOnlyWhenContaminated(NeedSpec needSpec)
		{
			return needSpec.Id == ContaminationNeedEnabler.AntidoteNeedId;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000284A File Offset: 0x00000A4A
		public static bool IsDisabledWhenContaminated(NeedSpec needSpec)
		{
			return !needSpec.HasSpec<CriticalNeedSpec>() && needSpec.Id != ContaminationNeedEnabler.ShelterNeedId;
		}

		// Token: 0x0400001F RID: 31
		public static readonly string ShelterNeedId = "Shelter";

		// Token: 0x04000020 RID: 32
		public static readonly string AntidoteNeedId = "Antidote";

		// Token: 0x04000021 RID: 33
		public NeedManager _needManager;

		// Token: 0x04000022 RID: 34
		public Contaminable _contaminable;
	}
}
