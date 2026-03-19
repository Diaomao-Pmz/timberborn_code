using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.Persistence;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Workshops
{
	// Token: 0x02000021 RID: 33
	public class ProductionResetter : TickableComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000406A File Offset: 0x0000226A
		public ProductionResetter(IDayNightCycle dayNightCycle, ILoc loc)
		{
			this._dayNightCycle = dayNightCycle;
			this._loc = loc;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004080 File Offset: 0x00002280
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._productionResetterSpec = base.GetComponent<ProductionResetterSpec>();
			this._manufactory = base.GetComponent<Manufactory>();
			this._manufactory.ProductionProgressed += this.OnProductionProgressed;
			this._productionStoppedStatus = StatusToggle.CreatePriorityStatusWithAlertAndFloatingIcon(this._productionResetterSpec.StatusIcon, this._loc.T(this._productionResetterSpec.StatusLocKey), this._loc.T(this._productionResetterSpec.AlertLocKey), 0f);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000410F File Offset: 0x0000230F
		public override void StartTickable()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._productionStoppedStatus);
			this.UpdateStatus();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004128 File Offset: 0x00002328
		public override void Tick()
		{
			this.EvaluateTriggerConditions();
			this.UpdateTimer();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004136 File Offset: 0x00002336
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ProductionResetter.ProductionResetterKey);
			component.Set(ProductionResetter.ResetTimerKey, this._resetTimer);
			component.Set(ProductionResetter.DeactivationTriggeredKey, this._deactivationTriggered);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004164 File Offset: 0x00002364
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ProductionResetter.ProductionResetterKey);
			this._resetTimer = component.Get(ProductionResetter.ResetTimerKey);
			this._deactivationTriggered = component.Get(ProductionResetter.DeactivationTriggeredKey);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000419F File Offset: 0x0000239F
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			if (e.ProductionProgressChange > 0f)
			{
				this._deactivationTriggered = true;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000041B5 File Offset: 0x000023B5
		public void UpdateStatus()
		{
			if (this._resetTimer > 0f)
			{
				this._productionStoppedStatus.Activate();
				return;
			}
			this._productionStoppedStatus.Deactivate();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000041DC File Offset: 0x000023DC
		public void EvaluateTriggerConditions()
		{
			bool flag = this._workplace && this._workplace.NumberOfAssignedWorkers == 0;
			bool flag2 = this._manufactory.ProductionProgress > 0f && (!this._manufactory.IsReadyToProduce || flag);
			if (flag2 && this._resetTimer == 0f)
			{
				this.ActivateTimer();
				return;
			}
			if (!flag2 && this._deactivationTriggered && this._resetTimer > 0f)
			{
				this.DeactivateTimer();
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004264 File Offset: 0x00002464
		public void UpdateTimer()
		{
			if (this._resetTimer > 0f)
			{
				this._resetTimer -= this._dayNightCycle.FixedDeltaTimeInHours;
				if (this._resetTimer <= 0f)
				{
					this._manufactory.ResetProductionProgress();
					this.DeactivateTimer();
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000042B4 File Offset: 0x000024B4
		public void ActivateTimer()
		{
			this._resetTimer = this._productionResetterSpec.HoursToResetProgress;
			this._deactivationTriggered = false;
			this.UpdateStatus();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042D4 File Offset: 0x000024D4
		public void DeactivateTimer()
		{
			this._resetTimer = 0f;
			this._deactivationTriggered = false;
			this.UpdateStatus();
		}

		// Token: 0x04000068 RID: 104
		public static readonly ComponentKey ProductionResetterKey = new ComponentKey("ProductionResetter");

		// Token: 0x04000069 RID: 105
		public static readonly PropertyKey<float> ResetTimerKey = new PropertyKey<float>("ResetTimer");

		// Token: 0x0400006A RID: 106
		public static readonly PropertyKey<bool> DeactivationTriggeredKey = new PropertyKey<bool>("DeactivationTriggered");

		// Token: 0x0400006B RID: 107
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400006C RID: 108
		public readonly ILoc _loc;

		// Token: 0x0400006D RID: 109
		public Workplace _workplace;

		// Token: 0x0400006E RID: 110
		public ProductionResetterSpec _productionResetterSpec;

		// Token: 0x0400006F RID: 111
		public Manufactory _manufactory;

		// Token: 0x04000070 RID: 112
		public StatusToggle _productionStoppedStatus;

		// Token: 0x04000071 RID: 113
		public float _resetTimer;

		// Token: 0x04000072 RID: 114
		public bool _deactivationTriggered;
	}
}
