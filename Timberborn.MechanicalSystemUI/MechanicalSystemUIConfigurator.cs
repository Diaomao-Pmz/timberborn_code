using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using Timberborn.MechanicalSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000025 RID: 37
	[Context("Game")]
	public class MechanicalSystemUIConfigurator : Configurator
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003F28 File Offset: 0x00002128
		public override void Configure()
		{
			base.Bind<MechanicalNodeAnimator>().AsTransient();
			base.Bind<MechanicalNodeIlluminator>().AsTransient();
			base.Bind<NoPowerStatus>().AsTransient();
			base.Bind<MechanicalModel>().AsTransient();
			base.Bind<MechanicalNodeDescriber>().AsTransient();
			base.Bind<MechanicalNodeFacingMarkerDrawer>().AsTransient();
			base.Bind<MechanicalNodeSelfMarkerDrawer>().AsTransient();
			base.Bind<MechanicalNodeFragment>().AsSingleton();
			base.Bind<ConsumerFragmentService>().AsSingleton();
			base.Bind<NetworkFragmentService>().AsSingleton();
			base.Bind<GeneratorFragmentService>().AsSingleton();
			base.Bind<MarkerMatrix4x4Calculator>().AsSingleton();
			base.Bind<BatteryFragment>().AsSingleton();
			base.Bind<BatteryBatchControlRowItemFactory>().AsSingleton();
			base.Bind<MechanicalBatchControlRowItemFactory>().AsSingleton();
			base.Bind<MechanicalGraphModelUpdater>().AsSingleton();
			base.Bind<MechanicalSystemDebuggingPanel>().AsSingleton();
			base.Bind<MechanicalNodeTextFormatter>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MechanicalSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MechanicalSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000403B File Offset: 0x0000223B
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MechanicalNode, MechanicalModel>();
			builder.AddDecorator<MechanicalNode, MechanicalNodeDescriber>();
			builder.AddDecorator<MechanicalNode, MechanicalNodeSelfMarkerDrawer>();
			builder.AddDecorator<MechanicalNode, MechanicalNodeFacingMarkerDrawer>();
			builder.AddDecorator<MechanicalNode, NoPowerStatus>();
			builder.AddDecorator<MechanicalNodeIlluminatorSpec, MechanicalNodeIlluminator>();
			builder.AddDecorator<MechanicalNodeIlluminator, Illuminator>();
			builder.AddDecorator<MechanicalNodeAnimatorSpec, MechanicalNodeAnimator>();
			return builder.Build();
		}

		// Token: 0x02000026 RID: 38
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x0000407F File Offset: 0x0000227F
			public EntityPanelModuleProvider(MechanicalNodeFragment mechanicalNodeFragment, BatteryFragment batteryFragment)
			{
				this._mechanicalNodeFragment = mechanicalNodeFragment;
				this._batteryFragment = batteryFragment;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00004095 File Offset: 0x00002295
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._batteryFragment, 0);
				builder.AddMiddleFragment(this._mechanicalNodeFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400007B RID: 123
			public readonly MechanicalNodeFragment _mechanicalNodeFragment;

			// Token: 0x0400007C RID: 124
			public readonly BatteryFragment _batteryFragment;
		}
	}
}
