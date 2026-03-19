using System;
using Bindito.Core;
using Timberborn.Automation;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class AutomationUIConfigurator : Configurator
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002880 File Offset: 0x00000A80
		public override void Configure()
		{
			base.Bind<AutomationLoopStatus>().AsTransient();
			base.Bind<SequentialTransmitterDescriber>().AsTransient();
			base.Bind<TransmitterFragment>().AsSingleton();
			base.Bind<AutomatableFragment>().AsSingleton();
			base.Bind<AutomatableBatchControlRowItemFactory>().AsSingleton();
			base.Bind<AutomationDebuggingPanel>().AsSingleton();
			base.Bind<TransmitterSelectorInitializer>().AsSingleton();
			base.Bind<SequentialTransmitterResetFragment>().AsSingleton();
			base.Bind<AutomationStateIconBuilder>().AsSingleton();
			base.Bind<TransmitterPickerTool>().AsSingleton();
			base.Bind<TransmitterPickerToolHighlighter>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AutomationUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<AutomationUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000293F File Offset: 0x00000B3F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Automator, AutomationLoopStatus>();
			builder.AddDecorator<ISequentialTransmitter, SequentialTransmitterDescriber>();
			return builder.Build();
		}

		// Token: 0x02000011 RID: 17
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000032 RID: 50 RVA: 0x0000295F File Offset: 0x00000B5F
			public EntityPanelModuleProvider(TransmitterFragment transmitterFragment, AutomatableFragment automatableFragment, SequentialTransmitterResetFragment sequentialTransmitterResetFragment)
			{
				this._transmitterFragment = transmitterFragment;
				this._automatableFragment = automatableFragment;
				this._sequentialTransmitterResetFragment = sequentialTransmitterResetFragment;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x0000297C File Offset: 0x00000B7C
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._sequentialTransmitterResetFragment, 50);
				builder.AddMiddleFragment(this._transmitterFragment, 100);
				builder.AddBottomFragment(this._automatableFragment, 100);
				return builder.Build();
			}

			// Token: 0x04000028 RID: 40
			public readonly TransmitterFragment _transmitterFragment;

			// Token: 0x04000029 RID: 41
			public readonly AutomatableFragment _automatableFragment;

			// Token: 0x0400002A RID: 42
			public readonly SequentialTransmitterResetFragment _sequentialTransmitterResetFragment;
		}
	}
}
