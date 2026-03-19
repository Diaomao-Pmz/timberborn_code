using System;
using Bindito.Core;
using Timberborn.Automation;
using Timberborn.Buildings;
using Timberborn.EntityNaming;
using Timberborn.Illumination;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class AutomationBuildingsConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<DepthSensor>().AsTransient();
			base.Bind<ContaminationSensor>().AsTransient();
			base.Bind<FlowSensor>().AsTransient();
			base.Bind<ScienceCounter>().AsTransient();
			base.Bind<ResourceCounter>().AsTransient();
			base.Bind<ResourceCounterBannerSetter>().AsTransient();
			base.Bind<PopulationCounter>().AsTransient();
			base.Bind<PowerMeter>().AsTransient();
			base.Bind<Lever>().AsTransient();
			base.Bind<LeverModel>().AsTransient();
			base.Bind<Relay>().AsTransient();
			base.Bind<Memory>().AsTransient();
			base.Bind<WeatherStation>().AsTransient();
			base.Bind<PausableBuildingTerminal>().AsTransient();
			base.Bind<Chronometer>().AsTransient();
			base.Bind<AutoAutomatableNeeder>().AsTransient();
			base.Bind<Gate>().AsTransient();
			base.Bind<GatePlacement>().AsTransient();
			base.Bind<GateNavMeshBlocker>().AsTransient();
			base.Bind<GateModel>().AsTransient();
			base.Bind<Timer>().AsTransient();
			base.Bind<Detonator>().AsTransient();
			base.Bind<Indicator>().AsTransient();
			base.Bind<Speaker>().AsTransient();
			base.Bind<SpeakerAnimationController>().AsTransient();
			base.Bind<TimerModel>().AsTransient();
			base.Bind<SamplingPopulationService>().AsSingleton();
			base.Bind<SamplingResourcesService>().AsSingleton();
			base.Bind<SpringReturnService>().AsSingleton();
			base.Bind<GateUpdater>().AsSingleton();
			base.Bind<SpeakerPlayer>().AsSingleton();
			base.Bind<SpeakerSoundService>().AsSingleton();
			base.Bind<SpeakerBuiltinSounds>().AsSingleton();
			base.Bind<SpeakerCustomSoundLoader>().AsSingleton();
			base.Bind<TimerIntervalSerializer>().AsSingleton();
			base.Bind<TimerIntervalFactory>().AsSingleton();
			base.MultiBind<IPathTransformer>().To<GatePathTransformer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AutomationBuildingsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022EC File Offset: 0x000004EC
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DepthSensorSpec, DepthSensor>();
			builder.AddDecorator<DepthSensor, AutomatorIlluminator>();
			builder.AddDecorator<ContaminationSensorSpec, ContaminationSensor>();
			builder.AddDecorator<ContaminationSensor, AutomatorIlluminator>();
			builder.AddDecorator<FlowSensorSpec, FlowSensor>();
			builder.AddDecorator<FlowSensor, AutomatorIlluminator>();
			builder.AddDecorator<LeverSpec, Lever>();
			builder.AddDecorator<Lever, AutomatorIlluminator>();
			builder.AddDecorator<LeverModelSpec, LeverModel>();
			builder.AddDecorator<RelaySpec, Relay>();
			builder.AddDecorator<Relay, AutomatorIlluminator>();
			builder.AddDecorator<MemorySpec, Memory>();
			builder.AddDecorator<Memory, AutomatorIlluminator>();
			builder.AddDecorator<WeatherStationSpec, WeatherStation>();
			builder.AddDecorator<WeatherStation, AutomatorIlluminator>();
			builder.AddDecorator<IFinishedPausable, PausableBuildingTerminal>();
			builder.AddDecorator<PausableBuildingTerminal, AutoAutomatableNeeder>();
			builder.AddDecorator<ChronometerSpec, Chronometer>();
			builder.AddDecorator<Chronometer, AutomatorIlluminator>();
			builder.AddDecorator<ScienceCounterSpec, ScienceCounter>();
			builder.AddDecorator<ScienceCounter, AutomatorIlluminator>();
			builder.AddDecorator<ResourceCounterSpec, ResourceCounter>();
			builder.AddDecorator<ResourceCounter, AutomatorIlluminator>();
			builder.AddDecorator<ResourceCounter, ResourceCounterBannerSetter>();
			builder.AddDecorator<PopulationCounterSpec, PopulationCounter>();
			builder.AddDecorator<PopulationCounter, AutomatorIlluminator>();
			builder.AddDecorator<PowerMeterSpec, PowerMeter>();
			builder.AddDecorator<PowerMeter, AutomatorIlluminator>();
			builder.AddDecorator<GateSpec, Gate>();
			builder.AddDecorator<Gate, GatePlacement>();
			builder.AddDecorator<Gate, GateNavMeshBlocker>();
			builder.AddDecorator<Gate, Illuminator>();
			builder.AddDecorator<GateModelSpec, GateModel>();
			builder.AddDecorator<TimerSpec, Timer>();
			builder.AddDecorator<Timer, AutomatorIlluminator>();
			builder.AddDecorator<Timer, TimerModel>();
			builder.AddDecorator<DetonatorSpec, Detonator>();
			builder.AddDecorator<Detonator, Illuminator>();
			builder.AddDecorator<IndicatorSpec, Indicator>();
			builder.AddDecorator<Indicator, Illuminator>();
			builder.AddDecorator<Indicator, CustomizableIlluminator>();
			builder.AddDecorator<Indicator, NumberedEntityNamer>();
			builder.AddDecorator<SpeakerSpec, Speaker>();
			builder.AddDecorator<Speaker, Illuminator>();
			builder.AddDecorator<Speaker, SpeakerAnimationController>();
			return builder.Build();
		}
	}
}
