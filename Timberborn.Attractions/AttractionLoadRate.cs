using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EnterableSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Attractions
{
	// Token: 0x0200000C RID: 12
	public class AttractionLoadRate : TickableComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000027C9 File Offset: 0x000009C9
		public AttractionLoadRate(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027F2 File Offset: 0x000009F2
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			base.DisableComponent();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002806 File Offset: 0x00000A06
		public override void Tick()
		{
			this.ResetValuesOnHourChange();
			this.CollectSample();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000224E File Offset: 0x0000044E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002256 File Offset: 0x00000456
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002814 File Offset: 0x00000A14
		public float GetLoadRate(int hour)
		{
			return (float)this._actualLoad[hour] / (float)this._maxLoad[hour];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002829 File Offset: 0x00000A29
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(AttractionLoadRate.AttractionLoadRateKey);
			component.Set(AttractionLoadRate.MaxLoadKey, this._maxLoad);
			component.Set(AttractionLoadRate.ActualLoadKey, this._actualLoad);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002858 File Offset: 0x00000A58
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(AttractionLoadRate.AttractionLoadRateKey, out objectLoader))
			{
				this._maxLoad = objectLoader.Get(AttractionLoadRate.MaxLoadKey).ToArray();
				this._actualLoad = objectLoader.Get(AttractionLoadRate.ActualLoadKey).ToArray();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A0 File Offset: 0x00000AA0
		public void ResetValuesOnHourChange()
		{
			int num = (int)this._dayNightCycle.HoursPassedToday;
			if (num != this._currentHour)
			{
				this._currentHour = num;
				this._maxLoad[this._currentHour] = 0;
				this._actualLoad[this._currentHour] = 0;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void CollectSample()
		{
			this._maxLoad[this._currentHour] += this._enterable.Capacity;
			this._actualLoad[this._currentHour] += this._enterable.NumberOfEnterersInside;
		}

		// Token: 0x0400001E RID: 30
		public static readonly ComponentKey AttractionLoadRateKey = new ComponentKey("AttractionLoadRate");

		// Token: 0x0400001F RID: 31
		public static readonly ListKey<int> MaxLoadKey = new ListKey<int>("MaxLoad");

		// Token: 0x04000020 RID: 32
		public static readonly ListKey<int> ActualLoadKey = new ListKey<int>("ActualLoad");

		// Token: 0x04000021 RID: 33
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000022 RID: 34
		public int[] _maxLoad = new int[24];

		// Token: 0x04000023 RID: 35
		public int[] _actualLoad = new int[24];

		// Token: 0x04000024 RID: 36
		public int _currentHour;

		// Token: 0x04000025 RID: 37
		public Enterable _enterable;
	}
}
