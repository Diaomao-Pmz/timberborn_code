using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.Workshops
{
	// Token: 0x0200002C RID: 44
	public class Workshop : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600015A RID: 346 RVA: 0x000055E4 File Offset: 0x000037E4
		// (remove) Token: 0x0600015B RID: 347 RVA: 0x0000561C File Offset: 0x0000381C
		public event EventHandler<WorkshopStateChangedEventArgs> WorkshopStateChanged;

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005651 File Offset: 0x00003851
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005659 File Offset: 0x00003859
		public int NumberOfWorkersWorking { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005662 File Offset: 0x00003862
		public bool CurrentlyWorking
		{
			get
			{
				return this.NumberOfWorkersWorking > 0;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000566D File Offset: 0x0000386D
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000567C File Offset: 0x0000387C
		public void InformOfStartedWorking()
		{
			if (this.NumberOfWorkersWorking >= this._workplace.MaxWorkers)
			{
				Debug.LogWarning("Can't have a number of working workers higher than the max number of workers at " + base.Name);
				return;
			}
			int numberOfWorkersWorking = this.NumberOfWorkersWorking + 1;
			this.NumberOfWorkersWorking = numberOfWorkersWorking;
			this.InvokeWorkshopStateChangedEvent();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000056C8 File Offset: 0x000038C8
		public void InformOfStoppedWorking()
		{
			if (this.NumberOfWorkersWorking <= 0)
			{
				Debug.LogWarning("Can't have a negative number of working workers at " + base.Name);
				return;
			}
			int numberOfWorkersWorking = this.NumberOfWorkersWorking - 1;
			this.NumberOfWorkersWorking = numberOfWorkersWorking;
			this.InvokeWorkshopStateChangedEvent();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000570C File Offset: 0x0000390C
		public void InvokeWorkshopStateChangedEvent()
		{
			WorkshopStateChangedEventArgs e = new WorkshopStateChangedEventArgs(this.CurrentlyWorking);
			EventHandler<WorkshopStateChangedEventArgs> workshopStateChanged = this.WorkshopStateChanged;
			if (workshopStateChanged == null)
			{
				return;
			}
			workshopStateChanged(this, e);
		}

		// Token: 0x0400009C RID: 156
		public Workplace _workplace;
	}
}
