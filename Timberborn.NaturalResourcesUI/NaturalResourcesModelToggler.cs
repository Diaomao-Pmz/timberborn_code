using System;
using Timberborn.Debugging;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesModelSystem;

namespace Timberborn.NaturalResourcesUI
{
	// Token: 0x02000008 RID: 8
	public class NaturalResourcesModelToggler : IDevModule
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000231A File Offset: 0x0000051A
		public NaturalResourcesModelToggler(EntityComponentRegistry entityComponentRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002329 File Offset: 0x00000529
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle models: Natural resources", new Action(this.ToggleNaturalResourceModels))).Build();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002350 File Offset: 0x00000550
		public void ToggleNaturalResourceModels()
		{
			this._naturalResourcesHidden = !this._naturalResourcesHidden;
			foreach (NaturalResourceModel model in this._entityComponentRegistry.GetEnabled<NaturalResourceModel>())
			{
				this.ToggleNaturalResource(model);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B4 File Offset: 0x000005B4
		public void ToggleNaturalResource(NaturalResourceModel model)
		{
			if (this._naturalResourcesHidden)
			{
				model.Hide();
				return;
			}
			model.Show();
		}

		// Token: 0x04000015 RID: 21
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000016 RID: 22
		public bool _naturalResourcesHidden;
	}
}
