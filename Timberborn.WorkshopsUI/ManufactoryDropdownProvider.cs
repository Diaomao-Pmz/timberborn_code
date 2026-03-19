using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.StatusSystemUI;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200000C RID: 12
	public class ManufactoryDropdownProvider : BaseComponent, IAwakableComponent, IInitializableEntity, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002A6F File Offset: 0x00000C6F
		public ManufactoryDropdownProvider(StatusListFragment statusListFragment)
		{
			this._statusListFragment = statusListFragment;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A89 File Offset: 0x00000C89
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._items.AsReadOnlyList<string>();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A9B File Offset: 0x00000C9B
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void InitializeEntity()
		{
			ImmutableArray<RecipeSpec> productionRecipes = this._manufactory.ProductionRecipes;
			this._items.Add(ManufactoryDropdownProvider.NoRecipeItemLocKey);
			this._items.AddRange(from recipe in productionRecipes
			select recipe.DisplayLocKey);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B05 File Offset: 0x00000D05
		public string GetValue()
		{
			RecipeSpec currentRecipe = this._manufactory.CurrentRecipe;
			return ((currentRecipe != null) ? currentRecipe.DisplayLocKey : null) ?? ManufactoryDropdownProvider.NoRecipeItemLocKey;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B28 File Offset: 0x00000D28
		public void SetValue(string value)
		{
			RecipeSpec recipeSpec = this.GetRecipeSpec(value);
			this._manufactory.SetRecipe(recipeSpec);
			this._statusListFragment.UpdateFragment();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B54 File Offset: 0x00000D54
		public string FormatDisplayText(string value, bool selected)
		{
			return value;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B57 File Offset: 0x00000D57
		public Sprite GetIcon(string value)
		{
			RecipeSpec recipeSpec = this.GetRecipeSpec(value);
			if (recipeSpec == null)
			{
				return null;
			}
			return recipeSpec.UIIcon.Value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B70 File Offset: 0x00000D70
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B78 File Offset: 0x00000D78
		public RecipeSpec GetRecipeSpec(string value)
		{
			return this._manufactory.ProductionRecipes.SingleOrDefault((RecipeSpec recipe) => recipe.DisplayLocKey == value);
		}

		// Token: 0x04000033 RID: 51
		public static readonly string NoRecipeItemLocKey = "Manufactory.NoRecipeOption";

		// Token: 0x04000034 RID: 52
		public readonly StatusListFragment _statusListFragment;

		// Token: 0x04000035 RID: 53
		public Manufactory _manufactory;

		// Token: 0x04000036 RID: 54
		public readonly List<string> _items = new List<string>();
	}
}
