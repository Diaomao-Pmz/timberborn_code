using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.NeedSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000006 RID: 6
	public class Contaminable : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002250 File Offset: 0x00000450
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		public event EventHandler ContaminationChanged;

		// Token: 0x06000010 RID: 16 RVA: 0x000022BD File Offset: 0x000004BD
		public Contaminable(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CC File Offset: 0x000004CC
		public bool IsContaminated
		{
			get
			{
				return this._needManager.NeedIsActive(Contaminable.ContaminationNeedId);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022DE File Offset: 0x000004DE
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._needManager.NeedChangedActiveState += this.OnNeedChangedActiveState;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002304 File Offset: 0x00000504
		public void Contaminate()
		{
			NeedManager needManager = this._needManager;
			InstantEffect instantEffect = new InstantEffect(Contaminable.ContaminationNeedId, float.MinValue, 1);
			needManager.ApplyEffect(instantEffect);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002330 File Offset: 0x00000530
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			if (e.NeedSpec.Id == Contaminable.ContaminationNeedId)
			{
				EventHandler contaminationChanged = this.ContaminationChanged;
				if (contaminationChanged != null)
				{
					contaminationChanged(this, EventArgs.Empty);
				}
				this._eventBus.Post(new ContaminableContaminationChangedEvent(this));
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ContaminationNeedId = "BadwaterContamination";

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public NeedManager _needManager;
	}
}
