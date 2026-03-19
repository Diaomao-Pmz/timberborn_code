using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000019 RID: 25
	public class ManufactoryTogglableRecipes : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000038AE File Offset: 0x00001AAE
		public string LabelLocKey
		{
			get
			{
				return this._manufactoryTogglableRecipesSpec.LabelLocKey;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000038BB File Offset: 0x00001ABB
		public void Awake()
		{
			this._manufactoryTogglableRecipesSpec = base.GetComponent<ManufactoryTogglableRecipesSpec>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038CC File Offset: 0x00001ACC
		public void Start()
		{
			Manufactory component = base.GetComponent<Manufactory>();
			if (component.CurrentRecipe == null)
			{
				component.SetRecipe(component.ProductionRecipes[0]);
			}
		}

		// Token: 0x0400004D RID: 77
		public ManufactoryTogglableRecipesSpec _manufactoryTogglableRecipesSpec;
	}
}
