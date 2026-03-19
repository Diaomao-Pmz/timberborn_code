using System;
using Bindito.Core;
using Timberborn.BlueprintSystem;
using Timberborn.GoodCollectionSystem;
using Timberborn.NeedCollectionSystem;
using Timberborn.TemplateCollectionSystem;
using Timberborn.TimbermeshMaterials;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	public class GameFactionSystemConfigurator : Configurator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000029D0 File Offset: 0x00000BD0
		public override void Configure()
		{
			base.Bind<FactionService>().AsSingleton();
			base.Bind<FactionNeedService>().AsSingleton();
			base.Bind<NeedModificationService>().AsSingleton();
			base.Bind<FactionBlueprintModifierProvider>().AsSingleton();
			base.Bind<NeedVerifier>().AsSingleton();
			base.MultiBind<ITemplateCollectionIdProvider>().To<FactionTemplateCollectionIdProvider>().AsSingleton();
			base.MultiBind<IMaterialCollectionIdsProvider>().To<FactionMaterialCollectionIdsProvider>().AsSingleton();
			base.MultiBind<IBlueprintModifierProvider>().ToExisting<FactionBlueprintModifierProvider>();
			base.MultiBind<IGoodCollectionIdsProvider>().To<FactionGoodCollectionIdsProvider>().AsSingleton();
			base.MultiBind<INeedCollectionIdsProvider>().To<FactionNeedCollectionIdsProvider>().AsSingleton();
		}
	}
}
