using System;
using Bindito.Core;
using Timberborn.ReservableSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Yielding
{
	// Token: 0x02000016 RID: 22
	[Context("Game")]
	[Context("MapEditor")]
	public class YieldingConfigurator : Configurator
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000037C8 File Offset: 0x000019C8
		public override void Configure()
		{
			base.Bind<YieldRemoverBehavior>().AsTransient();
			base.Bind<RemoveYieldExecutor>().AsTransient();
			base.Bind<InRangeYielderGoodAllower>().AsTransient();
			base.Bind<InRangeYielders>().AsTransient();
			base.Bind<Yielder>().AsTransient();
			base.Bind<YielderRemover>().AsTransient();
			base.Bind<YieldRemovalSuccessValidator>().AsTransient();
			base.Bind<YieldRemovingBuilding>().AsTransient();
			base.Bind<YielderInitializer>().AsSingleton();
			base.Bind<YieldRemovalChanceBonusService>().AsSingleton();
			base.Bind<RemoveYieldStrategySpecService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<YieldingConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000017 RID: 23
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x0600009F RID: 159 RVA: 0x00003872 File Offset: 0x00001A72
			public TemplateModuleProvider(YielderInitializer yielderInitializer)
			{
				this._yielderInitializer = yielderInitializer;
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x00003884 File Offset: 0x00001A84
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDedicatedDecorator<IYielderDecorable, Yielder>(this._yielderInitializer);
				builder.AddDecorator<Yielder, Reservable>();
				builder.AddDecorator<Worker, YielderRemover>();
				builder.AddDecorator<Worker, YieldRemoverBehavior>();
				builder.AddDecorator<Worker, YieldRemovalSuccessValidator>();
				builder.AddDecorator<IYielderRetriever, InRangeYielders>();
				builder.AddDecorator<InRangeYielders, InRangeYielderGoodAllower>();
				builder.AddDecorator<YieldRemovingBuildingSpec, YieldRemovingBuilding>();
				return builder.Build();
			}

			// Token: 0x04000043 RID: 67
			public readonly YielderInitializer _yielderInitializer;
		}
	}
}
