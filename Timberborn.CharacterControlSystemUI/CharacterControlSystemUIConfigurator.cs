using System;
using Bindito.Core;
using Timberborn.CharacterControlSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterControlSystemUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class CharacterControlSystemUIConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026A4 File Offset: 0x000008A4
		public override void Configure()
		{
			base.Bind<ControllableCharacterDropdownProvider>().AsTransient();
			base.Bind<CharacterControlFragment>().AsSingleton();
			base.Bind<CharacterControlDestinationPicker>().AsSingleton();
			base.Bind<ControllableCharacterAnimations>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<CharacterControlSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterControlSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000270F File Offset: 0x0000090F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ControllableCharacter, ControllableCharacterDropdownProvider>();
			return builder.Build();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00002729 File Offset: 0x00000929
			public EntityPanelModuleProvider(CharacterControlFragment characterControlFragment)
			{
				this._characterControlFragment = characterControlFragment;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002738 File Offset: 0x00000938
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._characterControlFragment);
				return builder.Build();
			}

			// Token: 0x04000020 RID: 32
			public readonly CharacterControlFragment _characterControlFragment;
		}
	}
}
