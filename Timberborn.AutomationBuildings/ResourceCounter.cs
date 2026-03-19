using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.ResourceCountingSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000036 RID: 54
	public class ResourceCounter : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<ResourceCounter>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000253 RID: 595 RVA: 0x00006F48 File Offset: 0x00005148
		// (remove) Token: 0x06000254 RID: 596 RVA: 0x00006F80 File Offset: 0x00005180
		public event EventHandler<string> GoodChanged;

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006FB5 File Offset: 0x000051B5
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00006FBD File Offset: 0x000051BD
		public int SampledResourceCount { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00006FC6 File Offset: 0x000051C6
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00006FCE File Offset: 0x000051CE
		public float SampledFillRate { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006FD7 File Offset: 0x000051D7
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00006FDF File Offset: 0x000051DF
		public int Threshold { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00006FE8 File Offset: 0x000051E8
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00006FF0 File Offset: 0x000051F0
		public string GoodId { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00006FF9 File Offset: 0x000051F9
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00007001 File Offset: 0x00005201
		public float FillRateThreshold { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000700A File Offset: 0x0000520A
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00007012 File Offset: 0x00005212
		public NumericComparisonMode ComparisonMode { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000701B File Offset: 0x0000521B
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00007023 File Offset: 0x00005223
		public ResourceCounterMode Mode { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00007034 File Offset: 0x00005234
		public bool IncludeInputs { get; private set; }

		// Token: 0x06000265 RID: 613 RVA: 0x0000703D File Offset: 0x0000523D
		public ResourceCounter(IGoodService goodService, SamplingResourcesService samplingResourcesService)
		{
			this._goodService = goodService;
			this._samplingResourcesService = samplingResourcesService;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007054 File Offset: 0x00005254
		public void Awake()
		{
			this.GoodId = this._goodService.Goods[0];
			this._automator = base.GetComponent<Automator>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedDistrict;
			this._districtBuilding.ReassignedConstructionDistrict += this.OnReassignedDistrict;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000070C4 File Offset: 0x000052C4
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ResourceCounter.ComponentKey);
			component.Set(ResourceCounter.ThresholdKey, this.Threshold);
			component.Set(ResourceCounter.FillRateThresholdKey, this.FillRateThreshold);
			component.Set(ResourceCounter.GoodIdKey, this.GoodId);
			component.Set<ResourceCounterMode>(ResourceCounter.ModeKey, this.Mode);
			component.Set<NumericComparisonMode>(ResourceCounter.ComparisonModeKey, this.ComparisonMode);
			component.Set(ResourceCounter.IncludeInputsKey, this.IncludeInputs);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007144 File Offset: 0x00005344
		[BackwardCompatible(2026, 2, 24, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ResourceCounter.ComponentKey);
			this.Threshold = component.Get(ResourceCounter.ThresholdKey);
			this.FillRateThreshold = component.Get(ResourceCounter.FillRateThresholdKey);
			this.GoodId = component.Get(ResourceCounter.GoodIdKey);
			this.Mode = component.Get<ResourceCounterMode>(ResourceCounter.ModeKey);
			this.ComparisonMode = component.Get<NumericComparisonMode>(ResourceCounter.ComparisonModeKey);
			if (component.Has<bool>(ResourceCounter.IncludeInputsKey))
			{
				this.IncludeInputs = component.Get(ResourceCounter.IncludeInputsKey);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000071D0 File Offset: 0x000053D0
		public void DuplicateFrom(ResourceCounter source)
		{
			this.Mode = source.Mode;
			this.GoodId = source.GoodId;
			this.InvokeGoodChangeEvent(source.GoodId);
			this.Threshold = source.Threshold;
			this.FillRateThreshold = source.FillRateThreshold;
			this.ComparisonMode = source.ComparisonMode;
			this.IncludeInputs = source.IncludeInputs;
			this.Sample();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007237 File Offset: 0x00005437
		public void SetGoodId(string goodId)
		{
			this.GoodId = goodId;
			this.InvokeGoodChangeEvent(goodId);
			this.Sample();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000724D File Offset: 0x0000544D
		public void SetThreshold(int threshold)
		{
			this.Threshold = threshold;
			this.UpdateOutputState();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000725C File Offset: 0x0000545C
		public void SetFillRateThreshold(float fillRateThreshold)
		{
			this.FillRateThreshold = fillRateThreshold;
			this.UpdateOutputState();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000726B File Offset: 0x0000546B
		public void SetMode(ResourceCounterMode mode)
		{
			this.Mode = mode;
			this.Sample();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000727A File Offset: 0x0000547A
		public void SetComparisonMode(NumericComparisonMode comparisionMode)
		{
			this.ComparisonMode = comparisionMode;
			this.UpdateOutputState();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007289 File Offset: 0x00005489
		public void SetIncludeInputs(bool includeInputs)
		{
			this.IncludeInputs = includeInputs;
			this.Sample();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007298 File Offset: 0x00005498
		public void Sample()
		{
			DistrictCenter instantOrConstructionDistrict = this._districtBuilding.GetInstantOrConstructionDistrict();
			DistrictResourceCounter districtCounter = this._samplingResourcesService.GetDistrictCounter(instantOrConstructionDistrict);
			ResourceCounterMode mode = this.Mode;
			if (mode != ResourceCounterMode.FillRate)
			{
				if (mode == ResourceCounterMode.StockLevel)
				{
					this.SampledResourceCount = (this.IncludeInputs ? districtCounter.GetResourceCount(this.GoodId).AllStock : districtCounter.GetResourceCount(this.GoodId).AvailableStock);
				}
			}
			else
			{
				this.SampledFillRate = districtCounter.GetResourceCount(this.GoodId).FillRate;
			}
			this.UpdateOutputState();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007328 File Offset: 0x00005528
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.Sample();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007330 File Offset: 0x00005530
		public void UpdateOutputState()
		{
			if (this._districtBuilding.GetInstantOrConstructionDistrict() == null)
			{
				this._automator.SetState(false);
				return;
			}
			Automator automator = this._automator;
			ResourceCounterMode mode = this.Mode;
			bool state;
			if (mode != ResourceCounterMode.FillRate)
			{
				if (mode != ResourceCounterMode.StockLevel)
				{
					throw new ArgumentOutOfRangeException();
				}
				state = this.ComparisonMode.Evaluate(this.SampledResourceCount, this.Threshold);
			}
			else
			{
				state = this.ComparisonMode.Evaluate(this.SampledFillRate, this.FillRateThreshold);
			}
			automator.SetState(state);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000073AD File Offset: 0x000055AD
		public void InvokeGoodChangeEvent(string goodId)
		{
			EventHandler<string> goodChanged = this.GoodChanged;
			if (goodChanged == null)
			{
				return;
			}
			goodChanged(this, goodId);
		}

		// Token: 0x04000118 RID: 280
		public static readonly ComponentKey ComponentKey = new ComponentKey("ResourceCounter");

		// Token: 0x04000119 RID: 281
		public static readonly PropertyKey<int> ThresholdKey = new PropertyKey<int>("Threshold");

		// Token: 0x0400011A RID: 282
		public static readonly PropertyKey<float> FillRateThresholdKey = new PropertyKey<float>("FillRateThreshold");

		// Token: 0x0400011B RID: 283
		public static readonly PropertyKey<string> GoodIdKey = new PropertyKey<string>("GoodId");

		// Token: 0x0400011C RID: 284
		public static readonly PropertyKey<ResourceCounterMode> ModeKey = new PropertyKey<ResourceCounterMode>("Mode");

		// Token: 0x0400011D RID: 285
		public static readonly PropertyKey<NumericComparisonMode> ComparisonModeKey = new PropertyKey<NumericComparisonMode>("ComparisonMode");

		// Token: 0x0400011E RID: 286
		public static readonly PropertyKey<bool> IncludeInputsKey = new PropertyKey<bool>("IncludeInputs");

		// Token: 0x04000128 RID: 296
		public readonly IGoodService _goodService;

		// Token: 0x04000129 RID: 297
		public readonly SamplingResourcesService _samplingResourcesService;

		// Token: 0x0400012A RID: 298
		public Automator _automator;

		// Token: 0x0400012B RID: 299
		public DistrictBuilding _districtBuilding;
	}
}
