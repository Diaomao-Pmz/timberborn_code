using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.ScienceSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003C RID: 60
	public class ScienceCounter : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<ScienceCounter>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007837 File Offset: 0x00005A37
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000783F File Offset: 0x00005A3F
		public int SampledSciencePoints { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007848 File Offset: 0x00005A48
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00007850 File Offset: 0x00005A50
		public int Threshold { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007859 File Offset: 0x00005A59
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00007861 File Offset: 0x00005A61
		public NumericComparisonMode Mode { get; private set; }

		// Token: 0x06000295 RID: 661 RVA: 0x0000786A File Offset: 0x00005A6A
		public ScienceCounter(ScienceService scienceService)
		{
			this._scienceService = scienceService;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007879 File Offset: 0x00005A79
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007887 File Offset: 0x00005A87
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ScienceCounter.ScienceCounterKey);
			component.Set(ScienceCounter.ThresholdKey, this.Threshold);
			component.Set<NumericComparisonMode>(ScienceCounter.ModeKey, this.Mode);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000078B8 File Offset: 0x00005AB8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ScienceCounter.ScienceCounterKey);
			this.Threshold = component.Get(ScienceCounter.ThresholdKey);
			this.Mode = component.Get<NumericComparisonMode>(ScienceCounter.ModeKey);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000078F3 File Offset: 0x00005AF3
		public void DuplicateFrom(ScienceCounter source)
		{
			this.Threshold = source.Threshold;
			this.Mode = source.Mode;
			this.UpdateOutputState();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007913 File Offset: 0x00005B13
		public void SetThreshold(int threshold)
		{
			this.Threshold = threshold;
			this.UpdateOutputState();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007922 File Offset: 0x00005B22
		public void SetMode(NumericComparisonMode mode)
		{
			this.Mode = mode;
			this.UpdateOutputState();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007931 File Offset: 0x00005B31
		public void Sample()
		{
			this.SampledSciencePoints = this._scienceService.SciencePoints;
			this.UpdateOutputState();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000794A File Offset: 0x00005B4A
		public void UpdateOutputState()
		{
			this._automator.SetState(this.Mode.Evaluate(this.SampledSciencePoints, this.Threshold));
		}

		// Token: 0x04000141 RID: 321
		public static readonly ComponentKey ScienceCounterKey = new ComponentKey("ScienceCounter");

		// Token: 0x04000142 RID: 322
		public static readonly PropertyKey<int> ThresholdKey = new PropertyKey<int>("Threshold");

		// Token: 0x04000143 RID: 323
		public static readonly PropertyKey<NumericComparisonMode> ModeKey = new PropertyKey<NumericComparisonMode>("Mode");

		// Token: 0x04000147 RID: 327
		public readonly ScienceService _scienceService;

		// Token: 0x04000148 RID: 328
		public Automator _automator;
	}
}
