using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.EntityNaming;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Characters
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class CharactersConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000242C File Offset: 0x0000062C
		public override void Configure()
		{
			base.Bind<CharacterTint>().AsTransient();
			base.Bind<Character>().AsTransient();
			base.Bind<CharacterMaterialModifier>().AsTransient();
			base.Bind<CharacterPopulation>().AsSingleton();
			base.Bind<GameSpeedThrottler>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharactersConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002492 File Offset: 0x00000692
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, BlockOccupant>();
			builder.AddDecorator<Character, SelectableObject>();
			builder.AddDecorator<Character, CharacterMaterialModifier>();
			builder.AddDecorator<Character, CharacterTint>();
			builder.AddDecorator<Character, EntityMaterials>();
			builder.AddDecorator<Character, NamedEntityGameObjectSynchronizer>();
			return builder.Build();
		}
	}
}
