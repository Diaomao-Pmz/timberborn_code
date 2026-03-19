using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x0200000B RID: 11
	public class ContaminationIncubator : BaseComponent, IAwakableComponent, IPersistentEntity, IPostInitializableEntity, IChildhoodInfluenced, IDeletableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002558 File Offset: 0x00000758
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002590 File Offset: 0x00000790
		public event EventHandler IncubationStateChanged;

		// Token: 0x06000026 RID: 38 RVA: 0x000025C5 File Offset: 0x000007C5
		public ContaminationIncubator(ITimeTriggerFactory timeTriggerFactory)
		{
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025D4 File Offset: 0x000007D4
		public bool IsIncubating
		{
			get
			{
				return this._timeTrigger.InProgress;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025E1 File Offset: 0x000007E1
		public bool IncubationFinished
		{
			get
			{
				return this._timeTrigger.Finished;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025EE File Offset: 0x000007EE
		public void Awake()
		{
			this._contaminable = base.GetComponent<Contaminable>();
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.FinishIncubation), ContaminationIncubator.IncubationTimeInDays);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000261E File Offset: 0x0000081E
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress != 0f)
			{
				entitySaver.GetComponent(ContaminationIncubator.ContaminationIncubatorKey).Set(ContaminationIncubator.IncubationProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002654 File Offset: 0x00000854
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(ContaminationIncubator.ContaminationIncubatorKey, out objectLoader))
			{
				float incubationProgress = objectLoader.Get(ContaminationIncubator.IncubationProgressKey);
				this.FastForwardIncubation(incubationProgress);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002683 File Offset: 0x00000883
		public void PostInitializeEntity()
		{
			this.NotifyContaminationIncubationChanged();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000268B File Offset: 0x0000088B
		public void InfluenceByChildhood(Character child)
		{
			this.FastForwardIncubation(child.GetComponent<ContaminationIncubator>().IncubationProgress);
			this.NotifyContaminationIncubationChanged();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026A4 File Offset: 0x000008A4
		public void DeleteEntity()
		{
			this._timeTrigger.Pause();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026B1 File Offset: 0x000008B1
		public void StartIncubation()
		{
			if (!this._contaminable.IsContaminated && !this.IsIncubating)
			{
				this._timeTrigger.Resume();
				this.NotifyContaminationIncubationChanged();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026D9 File Offset: 0x000008D9
		public void ResetIncubation()
		{
			this._timeTrigger.Reset();
			this.NotifyContaminationIncubationChanged();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026EC File Offset: 0x000008EC
		public float IncubationProgress
		{
			get
			{
				return this._timeTrigger.Progress;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002683 File Offset: 0x00000883
		public void FinishIncubation()
		{
			this.NotifyContaminationIncubationChanged();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026F9 File Offset: 0x000008F9
		public void NotifyContaminationIncubationChanged()
		{
			EventHandler incubationStateChanged = this.IncubationStateChanged;
			if (incubationStateChanged == null)
			{
				return;
			}
			incubationStateChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002711 File Offset: 0x00000911
		public void FastForwardIncubation(float incubationProgress)
		{
			if (incubationProgress > 0f)
			{
				this._timeTrigger.FastForwardProgress(incubationProgress);
				this._timeTrigger.Resume();
			}
		}

		// Token: 0x04000018 RID: 24
		public static readonly ComponentKey ContaminationIncubatorKey = new ComponentKey("ContaminationIncubator");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<float> IncubationProgressKey = new PropertyKey<float>("IncubationProgress");

		// Token: 0x0400001A RID: 26
		public static readonly float IncubationTimeInDays = 3f;

		// Token: 0x0400001C RID: 28
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400001D RID: 29
		public Contaminable _contaminable;

		// Token: 0x0400001E RID: 30
		public ITimeTrigger _timeTrigger;
	}
}
