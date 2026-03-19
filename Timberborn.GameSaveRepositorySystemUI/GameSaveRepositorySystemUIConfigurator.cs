using System;
using Bindito.Core;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000009 RID: 9
	[Context("MainMenu")]
	[Context("Game")]
	public class GameSaveRepositorySystemUIConfigurator : Configurator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024DC File Offset: 0x000006DC
		public override void Configure()
		{
			base.Bind<GameSaveItemElementFactory>().AsSingleton();
			base.Bind<GameSaveItemFactory>().AsSingleton();
			base.Bind<SaveThumbnailCache>().AsSingleton();
			base.Bind<SimpleModItemFactory>().AsSingleton();
			base.Bind<LoadGameBox>().AsSingleton();
			base.Bind<GameSaveModBox>().AsSingleton();
			base.Bind<SaveVersionCompatibilityService>().AsSingleton();
			base.Bind<ValidatingGameLoader>().AsSingleton();
			base.Bind<SettlementList>().AsSingleton();
			base.Bind<SaveList>().AsSingleton();
			base.MultiBind<IGameLoadValidator>().To<SaveFileValidator>().AsSingleton();
			base.MultiBind<IGameLoadValidator>().To<SaveVersionValidator>().AsSingleton();
			base.MultiBind<IGameLoadValidator>().To<SaveModsValidator>().AsSingleton();
		}
	}
}
