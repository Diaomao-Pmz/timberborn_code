using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Workshops
{
	// Token: 0x0200002D RID: 45
	public class WorkshopProductivityCounter : TickableComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005737 File Offset: 0x00003937
		public WorkshopProductivityCounter(IDayNightCycle dayNightCycle, DailyProductivitySerializer dailyProductivitySerializer)
		{
			this._dayNightCycle = dayNightCycle;
			this._dailyProductivitySerializer = dailyProductivitySerializer;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000574D File Offset: 0x0000394D
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._workshop = base.GetComponent<Workshop>();
			this._workplaceWorkingHours = base.GetComponent<WorkplaceWorkingHours>();
			this._dailyProductivity = DailyProductivity.CreateDefault();
			base.DisableComponent();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005784 File Offset: 0x00003984
		public override void StartTickable()
		{
			this._dailyProductivity.SetCurrentHour((int)this._dayNightCycle.HoursPassedToday);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000579D File Offset: 0x0000399D
		public override void Tick()
		{
			this.CheckAndUpdateCurrentHour();
			this.CollectSample();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000057AB File Offset: 0x000039AB
		public float CalculateProductivity()
		{
			return this._dailyProductivity.CalculateProductivity();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003F91 File Offset: 0x00002191
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002711 File Offset: 0x00000911
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000057B8 File Offset: 0x000039B8
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WorkshopProductivityCounter.WorkshopProductivityCounterKey).Set<DailyProductivity>(WorkshopProductivityCounter.DailyProductivityKey, this._dailyProductivity, this._dailyProductivitySerializer);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000057DC File Offset: 0x000039DC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WorkshopProductivityCounter.WorkshopProductivityCounterKey);
			this._dailyProductivity = component.Get<DailyProductivity>(WorkshopProductivityCounter.DailyProductivityKey, this._dailyProductivitySerializer);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000580C File Offset: 0x00003A0C
		public void CheckAndUpdateCurrentHour()
		{
			int num = (int)this._dayNightCycle.HoursPassedToday;
			if (num != this._dailyProductivity.CurrentHour)
			{
				this._dailyProductivity.UpdateAndSetCurrentHour(num);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005840 File Offset: 0x00003A40
		public void CollectSample()
		{
			if (this._workplaceWorkingHours.AreWorkingHours || this._workshop.NumberOfWorkersWorking > 0)
			{
				this._dailyProductivity.AddSample(this._workplace.NumberOfAssignedWorkers, this._workshop.NumberOfWorkersWorking);
			}
		}

		// Token: 0x0400009D RID: 157
		public static readonly ComponentKey WorkshopProductivityCounterKey = new ComponentKey("WorkshopProductivityCounter");

		// Token: 0x0400009E RID: 158
		public static readonly PropertyKey<DailyProductivity> DailyProductivityKey = new PropertyKey<DailyProductivity>("DailyProductivity");

		// Token: 0x0400009F RID: 159
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x040000A0 RID: 160
		public readonly DailyProductivitySerializer _dailyProductivitySerializer;

		// Token: 0x040000A1 RID: 161
		public DailyProductivity _dailyProductivity;

		// Token: 0x040000A2 RID: 162
		public Workplace _workplace;

		// Token: 0x040000A3 RID: 163
		public Workshop _workshop;

		// Token: 0x040000A4 RID: 164
		public WorkplaceWorkingHours _workplaceWorkingHours;
	}
}
