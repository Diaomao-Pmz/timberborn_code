using System;

namespace Timberborn.Workshops
{
	// Token: 0x0200000F RID: 15
	public interface IRecipeSelector
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002F RID: 47
		// (remove) Token: 0x06000030 RID: 48
		event EventHandler RecipeChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49
		bool HasCurrentRecipe { get; }
	}
}
