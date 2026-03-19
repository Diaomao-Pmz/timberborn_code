using System;
using Bindito.Core;
using Timberborn.AutomationBuildings;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class AutomationBuildingsUIConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<ResourceCounterGoodsDropdownProvider>().AsTransient();
			base.Bind<GateConflictStatus>().AsTransient();
			base.Bind<TimerIntervalElement>().AsTransient();
			base.Bind<DepthSensorMarker>().AsTransient();
			base.Bind<LeverFragment>().AsSingleton();
			base.Bind<PinnedLeversPanel>().AsSingleton();
			base.Bind<RelayFragment>().AsSingleton();
			base.Bind<RelayModeDescriptions>().AsSingleton();
			base.Bind<MemoryFragment>().AsSingleton();
			base.Bind<MemoryModeDescriptions>().AsSingleton();
			base.Bind<WeatherStationFragment>().AsSingleton();
			base.Bind<ChronometerFragment>().AsSingleton();
			base.Bind<ScienceCounterFragment>().AsSingleton();
			base.Bind<ResourceCounterFragment>().AsSingleton();
			base.Bind<PopulationCounterFragment>().AsSingleton();
			base.Bind<PowerMeterFragment>().AsSingleton();
			base.Bind<NumericComparisonModeDropdownFactory>().AsSingleton();
			base.Bind<TimerFragment>().AsSingleton();
			base.Bind<GateToggleFactory>().AsSingleton();
			base.Bind<GateFragment>().AsSingleton();
			base.Bind<TimerModeDescriptions>().AsSingleton();
			base.Bind<IndicatorFragment>().AsSingleton();
			base.Bind<PinnedIndicatorsPanel>().AsSingleton();
			base.Bind<SpeakerFragment>().AsSingleton();
			base.Bind<SpeakerSoundDropdownProvider>().AsSingleton();
			base.Bind<DepthSensorFragment>().AsSingleton();
			base.Bind<ContaminationSensorFragment>().AsSingleton();
			base.Bind<FlowSensorFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<AutomationBuildingsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(this.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000228B File Offset: 0x0000048B
		public TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ResourceCounter, ResourceCounterGoodsDropdownProvider>();
			builder.AddDecorator<Gate, GateConflictStatus>();
			builder.AddDecorator<DepthSensor, DepthSensorMarker>();
			return builder.Build();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000A RID: 10 RVA: 0x000022B4 File Offset: 0x000004B4
			public EntityPanelModuleProvider(LeverFragment leverFragment, RelayFragment relayFragment, MemoryFragment memoryFragment, WeatherStationFragment weatherStationFragment, ChronometerFragment chronometerFragment, ScienceCounterFragment scienceCounterFragment, ResourceCounterFragment resourceCounterFragment, PopulationCounterFragment populationCounterFragment, PowerMeterFragment powerMeterFragment, TimerFragment timerFragment, GateFragment gateFragment, IndicatorFragment indicatorFragment, SpeakerFragment speakerFragment, DepthSensorFragment depthSensorFragment, ContaminationSensorFragment contaminationSensorFragment, FlowSensorFragment flowSensorFragment)
			{
				this._leverFragment = leverFragment;
				this._relayFragment = relayFragment;
				this._memoryFragment = memoryFragment;
				this._weatherStationFragment = weatherStationFragment;
				this._chronometerFragment = chronometerFragment;
				this._scienceCounterFragment = scienceCounterFragment;
				this._resourceCounterFragment = resourceCounterFragment;
				this._populationCounterFragment = populationCounterFragment;
				this._powerMeterFragment = powerMeterFragment;
				this._timerFragment = timerFragment;
				this._gateFragment = gateFragment;
				this._indicatorFragment = indicatorFragment;
				this._speakerFragment = speakerFragment;
				this._depthSensorFragment = depthSensorFragment;
				this._contaminationSensorFragment = contaminationSensorFragment;
				this._flowSensorFragment = flowSensorFragment;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002344 File Offset: 0x00000544
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._leverFragment, 0);
				builder.AddTopFragment(this._relayFragment, 0);
				builder.AddTopFragment(this._memoryFragment, 0);
				builder.AddTopFragment(this._weatherStationFragment, 0);
				builder.AddTopFragment(this._chronometerFragment, 0);
				builder.AddMiddleFragment(this._scienceCounterFragment, 0);
				builder.AddMiddleFragment(this._resourceCounterFragment, 0);
				builder.AddMiddleFragment(this._populationCounterFragment, 0);
				builder.AddMiddleFragment(this._powerMeterFragment, 0);
				builder.AddTopFragment(this._timerFragment, 0);
				builder.AddTopFragment(this._gateFragment, 0);
				builder.AddTopFragment(this._indicatorFragment, 0);
				builder.AddTopFragment(this._speakerFragment, 0);
				builder.AddTopFragment(this._depthSensorFragment, 0);
				builder.AddTopFragment(this._contaminationSensorFragment, 0);
				builder.AddTopFragment(this._flowSensorFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000008 RID: 8
			public readonly LeverFragment _leverFragment;

			// Token: 0x04000009 RID: 9
			public readonly RelayFragment _relayFragment;

			// Token: 0x0400000A RID: 10
			public readonly MemoryFragment _memoryFragment;

			// Token: 0x0400000B RID: 11
			public readonly WeatherStationFragment _weatherStationFragment;

			// Token: 0x0400000C RID: 12
			public readonly ChronometerFragment _chronometerFragment;

			// Token: 0x0400000D RID: 13
			public readonly ScienceCounterFragment _scienceCounterFragment;

			// Token: 0x0400000E RID: 14
			public readonly ResourceCounterFragment _resourceCounterFragment;

			// Token: 0x0400000F RID: 15
			public readonly PopulationCounterFragment _populationCounterFragment;

			// Token: 0x04000010 RID: 16
			public readonly PowerMeterFragment _powerMeterFragment;

			// Token: 0x04000011 RID: 17
			public readonly TimerFragment _timerFragment;

			// Token: 0x04000012 RID: 18
			public readonly GateFragment _gateFragment;

			// Token: 0x04000013 RID: 19
			public readonly IndicatorFragment _indicatorFragment;

			// Token: 0x04000014 RID: 20
			public readonly SpeakerFragment _speakerFragment;

			// Token: 0x04000015 RID: 21
			public readonly DepthSensorFragment _depthSensorFragment;

			// Token: 0x04000016 RID: 22
			public readonly ContaminationSensorFragment _contaminationSensorFragment;

			// Token: 0x04000017 RID: 23
			public readonly FlowSensorFragment _flowSensorFragment;
		}
	}
}
