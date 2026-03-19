using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000018 RID: 24
	[Context("Game")]
	public class WaterBuildingsUIConfigurator : Configurator
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004804 File Offset: 0x00002A04
		public override void Configure()
		{
			base.Bind<SluiceMarker>().AsTransient();
			base.Bind<FillValveMarker>().AsTransient();
			base.Bind<WaterOutputParticleLength>().AsTransient();
			base.Bind<FloodedBuildingStatus>().AsTransient();
			base.Bind<NeedsWaterBuildingStatus>().AsTransient();
			base.Bind<WaterDirectionPreviewMarker>().AsTransient();
			base.Bind<WaterBuildingDescriber>().AsTransient();
			base.Bind<WaterInputSpecDescriber>().AsTransient();
			base.Bind<WaterOutputParticle>().AsTransient();
			base.Bind<WaterOutputParticleColorer>().AsTransient();
			base.Bind<FloodgateFragment>().AsSingleton();
			base.Bind<ValveFragment>().AsSingleton();
			base.Bind<ValveDebugFragment>().AsSingleton();
			base.Bind<FillValveFragment>().AsSingleton();
			base.Bind<StreamGaugeFragment>().AsSingleton();
			base.Bind<WaterMoverToggleFactory>().AsSingleton();
			base.Bind<WaterMoverFragment>().AsSingleton();
			base.Bind<WaterInputDepthFragment>().AsSingleton();
			base.Bind<SluiceFragment>().AsSingleton();
			base.Bind<SluiceToggleFactory>().AsSingleton();
			base.Bind<WaterOutputParticleColors>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterBuildingsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WaterBuildingsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000493C File Offset: 0x00002B3C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FloodableBuilding, FloodedBuildingStatus>();
			builder.AddDecorator<IWaterNeedingBuilding, NeedsWaterBuildingStatus>();
			builder.AddDecorator<IWaterNeedingBuilding, WaterBuildingDescriber>();
			builder.AddDecorator<WaterInput, WaterBuildingDescriber>();
			builder.AddDecorator<WaterInputSpec, WaterInputSpecDescriber>();
			builder.AddDecorator<StreamGauge, WaterBuildingDescriber>();
			builder.AddDecorator<WaterWheelSpec, WaterBuildingDescriber>();
			builder.AddDecorator<WaterOutputParticleSpec, WaterOutputParticle>();
			builder.AddDecorator<WaterOutputParticle, WaterOutputParticleColorer>();
			builder.AddDecorator<WaterOutputParticle, WaterOutputParticleLength>();
			builder.AddDecorator<Sluice, SluiceMarker>();
			builder.AddDecorator<Sluice, WaterDirectionPreviewMarker>();
			builder.AddDecorator<Valve, WaterDirectionPreviewMarker>();
			builder.AddDecorator<FillValve, WaterDirectionPreviewMarker>();
			builder.AddDecorator<FillValve, FillValveMarker>();
			return builder.Build();
		}

		// Token: 0x02000019 RID: 25
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600009E RID: 158 RVA: 0x000049B8 File Offset: 0x00002BB8
			public EntityPanelModuleProvider(FloodgateFragment floodgateFragment, ValveFragment valveFragment, ValveDebugFragment valveDebugFragment, FillValveFragment fillValveFragment, StreamGaugeFragment streamGaugeFragment, WaterMoverFragment waterMoverFragment, WaterInputDepthFragment waterInputDepthFragment, SluiceFragment sluiceFragment)
			{
				this._floodgateFragment = floodgateFragment;
				this._valveFragment = valveFragment;
				this._valveDebugFragment = valveDebugFragment;
				this._fillValveFragment = fillValveFragment;
				this._streamGaugeFragment = streamGaugeFragment;
				this._waterMoverFragment = waterMoverFragment;
				this._waterInputDepthFragment = waterInputDepthFragment;
				this._sluiceFragment = sluiceFragment;
			}

			// Token: 0x0600009F RID: 159 RVA: 0x00004A08 File Offset: 0x00002C08
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._floodgateFragment, 0);
				builder.AddTopFragment(this._valveFragment, 0);
				builder.AddDiagnosticFragment(this._valveDebugFragment);
				builder.AddTopFragment(this._fillValveFragment, 0);
				builder.AddTopFragment(this._streamGaugeFragment, 0);
				builder.AddTopFragment(this._waterMoverFragment, 0);
				builder.AddTopFragment(this._waterInputDepthFragment, 0);
				builder.AddTopFragment(this._sluiceFragment, 0);
				return builder.Build();
			}

			// Token: 0x040000AD RID: 173
			public readonly FloodgateFragment _floodgateFragment;

			// Token: 0x040000AE RID: 174
			public readonly ValveFragment _valveFragment;

			// Token: 0x040000AF RID: 175
			public readonly ValveDebugFragment _valveDebugFragment;

			// Token: 0x040000B0 RID: 176
			public readonly FillValveFragment _fillValveFragment;

			// Token: 0x040000B1 RID: 177
			public readonly StreamGaugeFragment _streamGaugeFragment;

			// Token: 0x040000B2 RID: 178
			public readonly WaterMoverFragment _waterMoverFragment;

			// Token: 0x040000B3 RID: 179
			public readonly WaterInputDepthFragment _waterInputDepthFragment;

			// Token: 0x040000B4 RID: 180
			public readonly SluiceFragment _sluiceFragment;
		}
	}
}
