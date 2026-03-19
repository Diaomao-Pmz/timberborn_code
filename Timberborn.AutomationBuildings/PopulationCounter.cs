using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BuildingsReachability;
using Timberborn.DuplicationSystem;
using Timberborn.GameDistricts;
using Timberborn.Persistence;
using Timberborn.Population;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200002D RID: 45
	public class PopulationCounter : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<PopulationCounter>, IDuplicable, ISamplingTransmitter, ITransmitter, IUnconnectedBuildingBlocker
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001E3 RID: 483 RVA: 0x00005D70 File Offset: 0x00003F70
		// (remove) Token: 0x060001E4 RID: 484 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public event EventHandler IsUnconnectedBlockedChanged;

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005DDD File Offset: 0x00003FDD
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00005DE5 File Offset: 0x00003FE5
		public PopulationCounterMode Mode { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005DEE File Offset: 0x00003FEE
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00005DF6 File Offset: 0x00003FF6
		public NumericComparisonMode ComparisonMode { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005DFF File Offset: 0x00003FFF
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00005E07 File Offset: 0x00004007
		public bool GlobalMode { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005E10 File Offset: 0x00004010
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00005E18 File Offset: 0x00004018
		public bool CountBeavers { get; private set; } = true;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00005E21 File Offset: 0x00004021
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00005E29 File Offset: 0x00004029
		public bool CountBots { get; private set; } = true;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005E32 File Offset: 0x00004032
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00005E3A File Offset: 0x0000403A
		public int Threshold { get; private set; }

		// Token: 0x060001F1 RID: 497 RVA: 0x00005E43 File Offset: 0x00004043
		public PopulationCounter(SamplingPopulationService samplingPopulationService)
		{
			this._samplingPopulationService = samplingPopulationService;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00005E6C File Offset: 0x0000406C
		public bool UsesWorkerType
		{
			get
			{
				bool result;
				switch (this.Mode)
				{
				case PopulationCounterMode.TotalPopulation:
					result = false;
					break;
				case PopulationCounterMode.TotalBeavers:
					result = false;
					break;
				case PopulationCounterMode.Adults:
					result = false;
					break;
				case PopulationCounterMode.Children:
					result = false;
					break;
				case PopulationCounterMode.Bots:
					result = false;
					break;
				case PopulationCounterMode.OccupiedBeds:
					result = false;
					break;
				case PopulationCounterMode.FreeBeds:
					result = false;
					break;
				case PopulationCounterMode.Homeless:
					result = false;
					break;
				case PopulationCounterMode.Jobs:
					result = true;
					break;
				case PopulationCounterMode.Employed:
					result = true;
					break;
				case PopulationCounterMode.Unemployed:
					result = true;
					break;
				case PopulationCounterMode.Vacancies:
					result = true;
					break;
				case PopulationCounterMode.TotalWorkers:
					result = true;
					break;
				case PopulationCounterMode.HealthyWorkers:
					result = true;
					break;
				case PopulationCounterMode.UnhealthyWorkers:
					result = true;
					break;
				case PopulationCounterMode.ContaminatedTotal:
					result = false;
					break;
				case PopulationCounterMode.ContaminatedAdults:
					result = false;
					break;
				case PopulationCounterMode.ContaminatedChildren:
					result = false;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005F1F File Offset: 0x0000411F
		public bool IsUnconnectedBlocked
		{
			get
			{
				return this.GlobalMode;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005F28 File Offset: 0x00004128
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedDistrict;
			this._districtBuilding.ReassignedConstructionDistrict += this.OnReassignedDistrict;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005F7C File Offset: 0x0000417C
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PopulationCounter.ComponentKey);
			component.Set<PopulationCounterMode>(PopulationCounter.ModeKey, this.Mode);
			component.Set<NumericComparisonMode>(PopulationCounter.ComparisonModeKey, this.ComparisonMode);
			component.Set(PopulationCounter.GlobalModeKey, this.GlobalMode);
			component.Set(PopulationCounter.CountBeaversKey, this.CountBeavers);
			component.Set(PopulationCounter.CountBotsKey, this.CountBots);
			component.Set(PopulationCounter.ThresholdKey, this.Threshold);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005FFC File Offset: 0x000041FC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PopulationCounter.ComponentKey);
			this.Mode = component.Get<PopulationCounterMode>(PopulationCounter.ModeKey);
			this.ComparisonMode = component.Get<NumericComparisonMode>(PopulationCounter.ComparisonModeKey);
			this.GlobalMode = (component.Has<bool>(PopulationCounter.GlobalModeKey) && component.Get(PopulationCounter.GlobalModeKey));
			this.CountBeavers = (component.Has<bool>(PopulationCounter.CountBeaversKey) && component.Get(PopulationCounter.CountBeaversKey));
			this.CountBots = (component.Has<bool>(PopulationCounter.CountBotsKey) && component.Get(PopulationCounter.CountBotsKey));
			this.Threshold = component.Get(PopulationCounter.ThresholdKey);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000060AC File Offset: 0x000042AC
		public void DuplicateFrom(PopulationCounter source)
		{
			this.Mode = source.Mode;
			this.ComparisonMode = source.ComparisonMode;
			this.SetGlobalModeInternal(source.GlobalMode, false);
			this.CountBeavers = source.CountBeavers;
			this.CountBots = source.CountBots;
			this.Threshold = source.Threshold;
			this.Sample();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006108 File Offset: 0x00004308
		public void SetThreshold(int threshold)
		{
			if (this.Threshold != threshold)
			{
				this.Threshold = threshold;
				this.Sample();
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006120 File Offset: 0x00004320
		public void SetMode(PopulationCounterMode mode)
		{
			if (this.Mode != mode)
			{
				this.Mode = mode;
				this.Sample();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006138 File Offset: 0x00004338
		public void SetGlobalMode(bool value)
		{
			this.SetGlobalModeInternal(value, true);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006142 File Offset: 0x00004342
		public void SetCountBeavers(bool value)
		{
			if (this.CountBeavers != value)
			{
				this.CountBeavers = value;
				this.Sample();
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000615A File Offset: 0x0000435A
		public void SetCountBots(bool value)
		{
			if (this.CountBots != value)
			{
				this.CountBots = value;
				this.Sample();
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006172 File Offset: 0x00004372
		public void SetComparisonMode(NumericComparisonMode comparisionMode)
		{
			this.ComparisonMode = comparisionMode;
			this.Sample();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006184 File Offset: 0x00004384
		public void Sample()
		{
			if (this.GlobalMode)
			{
				this._sampledPopulationData = this._samplingPopulationService.GlobalPopulationData;
			}
			else
			{
				DistrictCenter instantOrConstructionDistrict = this._districtBuilding.GetInstantOrConstructionDistrict();
				this._sampledPopulationData = (instantOrConstructionDistrict ? this._samplingPopulationService.GetDistrictData(instantOrConstructionDistrict) : this._emptyPopulationData);
			}
			this.UpdateOutputState();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000061E0 File Offset: 0x000043E0
		public int GetMeasurement()
		{
			int result;
			switch (this.Mode)
			{
			case PopulationCounterMode.TotalPopulation:
				result = this._sampledPopulationData.TotalPopulation;
				break;
			case PopulationCounterMode.TotalBeavers:
				result = this._sampledPopulationData.NumberOfBeavers;
				break;
			case PopulationCounterMode.Adults:
				result = this._sampledPopulationData.NumberOfAdults;
				break;
			case PopulationCounterMode.Children:
				result = this._sampledPopulationData.NumberOfChildren;
				break;
			case PopulationCounterMode.Bots:
				result = this._sampledPopulationData.NumberOfBots;
				break;
			case PopulationCounterMode.OccupiedBeds:
				result = this._sampledPopulationData.BedData.OccupiedBeds;
				break;
			case PopulationCounterMode.FreeBeds:
				result = this._sampledPopulationData.BedData.FreeBeds;
				break;
			case PopulationCounterMode.Homeless:
				result = this._sampledPopulationData.BedData.Homeless;
				break;
			case PopulationCounterMode.Jobs:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkplaceData.TotalWorkslots : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkplaceData.TotalWorkslots : 0);
				break;
			case PopulationCounterMode.Employed:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkplaceData.OccupiedWorkslots : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkplaceData.OccupiedWorkslots : 0);
				break;
			case PopulationCounterMode.Unemployed:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkplaceData.Unemployed : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkplaceData.Unemployed : 0);
				break;
			case PopulationCounterMode.Vacancies:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkplaceData.FreeWorkslots : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkplaceData.FreeWorkslots : 0);
				break;
			case PopulationCounterMode.TotalWorkers:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkforceData.Total : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkforceData.Total : 0);
				break;
			case PopulationCounterMode.HealthyWorkers:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkforceData.Employable : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkforceData.Employable : 0);
				break;
			case PopulationCounterMode.UnhealthyWorkers:
				result = (this.CountBeavers ? this._sampledPopulationData.BeaverWorkforceData.Unemployable : 0) + (this.CountBots ? this._sampledPopulationData.BotWorkforceData.Unemployable : 0);
				break;
			case PopulationCounterMode.ContaminatedTotal:
				result = this._sampledPopulationData.ContaminationData.ContaminatedTotal;
				break;
			case PopulationCounterMode.ContaminatedAdults:
				result = this._sampledPopulationData.ContaminationData.ContaminatedAdults;
				break;
			case PopulationCounterMode.ContaminatedChildren:
				result = this._sampledPopulationData.ContaminationData.ContaminatedChildren;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000650B File Offset: 0x0000470B
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.Sample();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006513 File Offset: 0x00004713
		public void UpdateOutputState()
		{
			this._automator.SetState(this.ComparisonMode.Evaluate(this.GetMeasurement(), this.Threshold));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006537 File Offset: 0x00004737
		public void SetGlobalModeInternal(bool value, bool sample)
		{
			if (this.GlobalMode != value)
			{
				this.GlobalMode = value;
				EventHandler isUnconnectedBlockedChanged = this.IsUnconnectedBlockedChanged;
				if (isUnconnectedBlockedChanged != null)
				{
					isUnconnectedBlockedChanged(this, EventArgs.Empty);
				}
				if (sample)
				{
					this.Sample();
				}
			}
		}

		// Token: 0x040000CD RID: 205
		public static readonly ComponentKey ComponentKey = new ComponentKey("PopulationCounter");

		// Token: 0x040000CE RID: 206
		public static readonly PropertyKey<PopulationCounterMode> ModeKey = new PropertyKey<PopulationCounterMode>("Mode");

		// Token: 0x040000CF RID: 207
		public static readonly PropertyKey<NumericComparisonMode> ComparisonModeKey = new PropertyKey<NumericComparisonMode>("ComparisonMode");

		// Token: 0x040000D0 RID: 208
		public static readonly PropertyKey<bool> GlobalModeKey = new PropertyKey<bool>("GlobalMode");

		// Token: 0x040000D1 RID: 209
		public static readonly PropertyKey<bool> CountBeaversKey = new PropertyKey<bool>("CountBeavers");

		// Token: 0x040000D2 RID: 210
		public static readonly PropertyKey<bool> CountBotsKey = new PropertyKey<bool>("CountBots");

		// Token: 0x040000D3 RID: 211
		public static readonly PropertyKey<int> ThresholdKey = new PropertyKey<int>("Threshold");

		// Token: 0x040000DB RID: 219
		public readonly SamplingPopulationService _samplingPopulationService;

		// Token: 0x040000DC RID: 220
		public Automator _automator;

		// Token: 0x040000DD RID: 221
		public DistrictBuilding _districtBuilding;

		// Token: 0x040000DE RID: 222
		public PopulationData _sampledPopulationData;

		// Token: 0x040000DF RID: 223
		public readonly PopulationData _emptyPopulationData = new PopulationData();
	}
}
