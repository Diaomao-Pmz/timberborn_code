using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000026 RID: 38
	public class RecipeSpecService : ILoadableSingleton
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000051BC File Offset: 0x000033BC
		public RecipeSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000051CC File Offset: 0x000033CC
		public void Load()
		{
			this._recipeSpecs = this._specService.GetSpecs<RecipeSpec>().ToFrozenDictionary((RecipeSpec spec) => spec.Id, (RecipeSpec spec) => spec, null);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000522E File Offset: 0x0000342E
		public unsafe RecipeSpec GetRecipe(string recipeId)
		{
			return *this._recipeSpecs[recipeId];
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000523D File Offset: 0x0000343D
		public IEnumerable<RecipeSpec> GetRecipes()
		{
			return this._recipeSpecs.Values;
		}

		// Token: 0x04000088 RID: 136
		public readonly ISpecService _specService;

		// Token: 0x04000089 RID: 137
		public FrozenDictionary<string, RecipeSpec> _recipeSpecs;
	}
}
