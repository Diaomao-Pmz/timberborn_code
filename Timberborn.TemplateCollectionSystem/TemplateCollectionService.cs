using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.TemplateCollectionSystem
{
	// Token: 0x0200000A RID: 10
	public class TemplateCollectionService : ILoadableSingleton
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021BF File Offset: 0x000003BF
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021C7 File Offset: 0x000003C7
		public ImmutableArray<Blueprint> AllTemplates { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x000021D0 File Offset: 0x000003D0
		public TemplateCollectionService(ISpecService specService, IEnumerable<ITemplateCollectionIdProvider> templateCollectionIdProviders)
		{
			this._specService = specService;
			this._templateCollectionIdProviders = templateCollectionIdProviders.ToImmutableArray<ITemplateCollectionIdProvider>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021EC File Offset: 0x000003EC
		public void Load()
		{
			List<Blueprint> list = new List<Blueprint>();
			foreach (ITemplateCollectionIdProvider templateCollectionIdProvider in this._templateCollectionIdProviders)
			{
				using (IEnumerator<string> enumerator2 = templateCollectionIdProvider.GetTemplateCollectionIds().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string collectionId = enumerator2.Current;
						list.AddRange(from asset in (from spec in this._specService.GetSpecs<TemplateCollectionSpec>()
						where spec.CollectionId == collectionId
						select spec).SelectMany((TemplateCollectionSpec spec) => spec.Blueprints)
						select this._specService.GetBlueprint(asset.Path));
					}
				}
			}
			this.AllTemplates = list.ToImmutableArray<Blueprint>();
		}

		// Token: 0x0400000D RID: 13
		public readonly ISpecService _specService;

		// Token: 0x0400000E RID: 14
		public readonly ImmutableArray<ITemplateCollectionIdProvider> _templateCollectionIdProviders;
	}
}
