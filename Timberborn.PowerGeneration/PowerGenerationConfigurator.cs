using System;
using Bindito.Core;
using Timberborn.LaborSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;
using Timberborn.WorkSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class PowerGenerationConfigurator : Configurator
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000023FC File Offset: 0x000005FC
		public override void Configure()
		{
			base.Bind<GoodPoweredGenerator>().AsTransient();
			base.Bind<PowerGeneratorSounds>().AsTransient();
			base.Bind<WalkerPoweredGenerator>().AsTransient();
			base.Bind<WaterPoweredGenerator>().AsTransient();
			base.Bind<AdjustableStrengthPowerGenerator>().AsTransient();
			base.Bind<RotationMechanicalNodeUpdater>().AsTransient();
			base.Bind<WindPoweredGenerator>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PowerGenerationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000247A File Offset: 0x0000067A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			PowerGenerationConfigurator.InitializeBehaviors(builder);
			builder.AddDecorator<WaterPoweredGeneratorSpec, WaterPoweredGenerator>();
			builder.AddDecorator<WaterPoweredGenerator, RotationMechanicalNodeUpdater>();
			builder.AddDecorator<WindPoweredGeneratorSpec, WindPoweredGenerator>();
			builder.AddDecorator<GoodPoweredGeneratorSpec, GoodPoweredGenerator>();
			builder.AddDecorator<PowerGeneratorSoundsSpec, PowerGeneratorSounds>();
			builder.AddDecorator<WalkerPoweredGeneratorSpec, WalkerPoweredGenerator>();
			builder.AddDecorator<AdjustableStrengthPowerGeneratorSpec, AdjustableStrengthPowerGenerator>();
			return builder.Build();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024B6 File Offset: 0x000006B6
		public static void InitializeBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<WalkerPoweredGenerator, WorkWorkplaceBehavior>();
			builder.AddDecorator<WalkerPoweredGenerator, LaborWorkplaceBehavior>();
			builder.AddDecorator<WalkerPoweredGenerator, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
