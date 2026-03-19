using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000021 RID: 33
	public class SluiceState : BaseComponent, IUnfinishedStateListener, IPersistentEntity
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000044D6 File Offset: 0x000026D6
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000044DE File Offset: 0x000026DE
		public bool AutoMode { get; private set; } = true;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000044E7 File Offset: 0x000026E7
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000044EF File Offset: 0x000026EF
		public bool IsOpen { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000044F8 File Offset: 0x000026F8
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00004500 File Offset: 0x00002700
		public bool AutoCloseOnOutflow { get; private set; } = true;

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004509 File Offset: 0x00002709
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00004511 File Offset: 0x00002711
		public float OutflowLimit { get; private set; } = -0.5f;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000451A File Offset: 0x0000271A
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00004522 File Offset: 0x00002722
		public bool AutoCloseOnAbove { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000452B File Offset: 0x0000272B
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004533 File Offset: 0x00002733
		public bool AutoCloseOnBelow { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004544 File Offset: 0x00002744
		public float OnAboveLimit { get; private set; } = 0.05f;

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000454D File Offset: 0x0000274D
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004555 File Offset: 0x00002755
		public float OnBelowLimit { get; private set; } = 0.05f;

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000455E File Offset: 0x0000275E
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004566 File Offset: 0x00002766
		public bool IsSynchronized { get; private set; } = true;

		// Token: 0x06000141 RID: 321 RVA: 0x00004570 File Offset: 0x00002770
		public SluiceState(SluiceSynchronizer sluiceSynchronizer)
		{
			this._sluiceSynchronizer = sluiceSynchronizer;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000045C0 File Offset: 0x000027C0
		public void SetAuto()
		{
			this.AutoMode = true;
			this.Synchronize();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000045CF File Offset: 0x000027CF
		public void Open()
		{
			this.IsOpen = true;
			this.AutoMode = false;
			this.Synchronize();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000045E5 File Offset: 0x000027E5
		public void Close()
		{
			this.IsOpen = false;
			this.AutoMode = false;
			this.Synchronize();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000045FB File Offset: 0x000027FB
		public void EnableAutoCloseOnOutflow()
		{
			this.AutoCloseOnOutflow = true;
			this.Synchronize();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000460A File Offset: 0x0000280A
		public void DisableAutoCloseOnOutflow()
		{
			this.AutoCloseOnOutflow = false;
			this.Synchronize();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004619 File Offset: 0x00002819
		public void SetOutflowLimit(float outflowLimit)
		{
			this.OutflowLimit = outflowLimit;
			this.Synchronize();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004628 File Offset: 0x00002828
		public void EnableAutoCloseOnAbove()
		{
			this.AutoCloseOnBelow = false;
			this.AutoCloseOnAbove = true;
			this.Synchronize();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000463E File Offset: 0x0000283E
		public void DisableAutoCloseOnAbove()
		{
			this.AutoCloseOnAbove = false;
			this.Synchronize();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000464D File Offset: 0x0000284D
		public void SetAboveContaminationLimit(float contaminationLimit)
		{
			this.OnAboveLimit = contaminationLimit;
			this.Synchronize();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000465C File Offset: 0x0000285C
		public void EnableAutoCloseOnBelow()
		{
			this.AutoCloseOnBelow = true;
			this.AutoCloseOnAbove = false;
			this.Synchronize();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004672 File Offset: 0x00002872
		public void DisableAutoCloseOnBelow()
		{
			this.AutoCloseOnBelow = false;
			this.Synchronize();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004681 File Offset: 0x00002881
		public void SetBelowContaminationLimit(float contaminationLimit)
		{
			this.OnBelowLimit = contaminationLimit;
			this.Synchronize();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004690 File Offset: 0x00002890
		public void ToggleSynchronization(bool newValue)
		{
			this.IsSynchronized = newValue;
			this.SynchronizeWithNeighbors();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000469F File Offset: 0x0000289F
		public void OnEnterUnfinishedState()
		{
			this.SynchronizeWithNeighbors();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000256E File Offset: 0x0000076E
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000046A8 File Offset: 0x000028A8
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(SluiceState.SluiceStateKey);
			component.Set(SluiceState.AutoModeKey, this.AutoMode);
			component.Set(SluiceState.IsOpenKey, this.IsOpen);
			component.Set(SluiceState.OutflowLimitKey, this.OutflowLimit);
			component.Set(SluiceState.AutoCloseOnOutflowKey, this.AutoCloseOnOutflow);
			component.Set(SluiceState.AutoCloseOnAboveKey, this.AutoCloseOnAbove);
			component.Set(SluiceState.AutoCloseOnBelowKey, this.AutoCloseOnBelow);
			component.Set(SluiceState.OnAboveLimitKey, this.OnAboveLimit);
			component.Set(SluiceState.OnBelowLimitKey, this.OnBelowLimit);
			component.Set(SluiceState.IsSynchronizedKey, this.IsSynchronized);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004758 File Offset: 0x00002958
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(SluiceState.SluiceStateKey);
			this.AutoMode = component.Get(SluiceState.AutoModeKey);
			this.IsOpen = component.Get(SluiceState.IsOpenKey);
			this.AutoCloseOnOutflow = component.Get(SluiceState.AutoCloseOnOutflowKey);
			this.OutflowLimit = component.Get(SluiceState.OutflowLimitKey);
			this.AutoCloseOnAbove = component.Get(SluiceState.AutoCloseOnAboveKey);
			this.AutoCloseOnBelow = component.Get(SluiceState.AutoCloseOnBelowKey);
			this.OnAboveLimit = component.Get(SluiceState.OnAboveLimitKey);
			this.OnBelowLimit = component.Get(SluiceState.OnBelowLimitKey);
			this.IsSynchronized = component.Get(SluiceState.IsSynchronizedKey);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000480C File Offset: 0x00002A0C
		public void SetState(SluiceState neighbor, int minLimit)
		{
			this.AutoMode = neighbor.AutoMode;
			this.IsOpen = neighbor.IsOpen;
			this.AutoCloseOnOutflow = neighbor.AutoCloseOnOutflow;
			this.OutflowLimit = Math.Max(neighbor.OutflowLimit, (float)minLimit);
			this.AutoCloseOnAbove = neighbor.AutoCloseOnAbove;
			this.AutoCloseOnBelow = neighbor.AutoCloseOnBelow;
			this.OnAboveLimit = neighbor.OnAboveLimit;
			this.OnBelowLimit = neighbor.OnBelowLimit;
			this.IsSynchronized = neighbor.IsSynchronized;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000488C File Offset: 0x00002A8C
		public void SetStateAndSynchronize(SluiceState neighbor, int minLimit)
		{
			this.SetState(neighbor, minLimit);
			this.Synchronize();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000489C File Offset: 0x00002A9C
		public void SynchronizeWithNeighbors()
		{
			if (this.IsSynchronized)
			{
				this._sluiceSynchronizer.SynchronizeWithNeighbors(this);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000048B2 File Offset: 0x00002AB2
		public void Synchronize()
		{
			if (this.IsSynchronized)
			{
				this._sluiceSynchronizer.SynchronizeNeighbors(this);
			}
		}

		// Token: 0x04000067 RID: 103
		public static readonly ComponentKey SluiceStateKey = new ComponentKey("SluiceState");

		// Token: 0x04000068 RID: 104
		public static readonly PropertyKey<bool> AutoModeKey = new PropertyKey<bool>("AutoMode");

		// Token: 0x04000069 RID: 105
		public static readonly PropertyKey<bool> IsOpenKey = new PropertyKey<bool>("IsOpen");

		// Token: 0x0400006A RID: 106
		public static readonly PropertyKey<bool> IsSynchronizedKey = new PropertyKey<bool>("IsSynchronized");

		// Token: 0x0400006B RID: 107
		public static readonly PropertyKey<bool> AutoCloseOnOutflowKey = new PropertyKey<bool>("AutoCloseOnOutflow");

		// Token: 0x0400006C RID: 108
		public static readonly PropertyKey<float> OutflowLimitKey = new PropertyKey<float>("OutflowLimit");

		// Token: 0x0400006D RID: 109
		public static readonly PropertyKey<bool> AutoCloseOnAboveKey = new PropertyKey<bool>("AutoCloseOnAbove");

		// Token: 0x0400006E RID: 110
		public static readonly PropertyKey<bool> AutoCloseOnBelowKey = new PropertyKey<bool>("AutoCloseOnBelow");

		// Token: 0x0400006F RID: 111
		public static readonly PropertyKey<float> OnAboveLimitKey = new PropertyKey<float>("OnAboveLimit");

		// Token: 0x04000070 RID: 112
		public static readonly PropertyKey<float> OnBelowLimitKey = new PropertyKey<float>("OnBelowLimit");

		// Token: 0x0400007A RID: 122
		public readonly SluiceSynchronizer _sluiceSynchronizer;
	}
}
