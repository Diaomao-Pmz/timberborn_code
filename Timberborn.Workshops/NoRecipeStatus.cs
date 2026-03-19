using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200001B RID: 27
	public class NoRecipeStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003A30 File Offset: 0x00001C30
		public NoRecipeStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A40 File Offset: 0x00001C40
		public void Awake()
		{
			this._recipeSelector = base.GetComponent<IRecipeSelector>();
			this._noRecipeStatusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("UnspecifiedRecipe", this._loc.T(NoRecipeStatus.NoRecipeLocKey), this._loc.T(NoRecipeStatus.NoRecipeShortLocKey), 0f);
			this._isAutomaticRecipeManufactory = base.GetComponent<AutomaticRecipeManufactory>();
			base.DisableComponent();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._noRecipeStatusToggle);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateStatusToggle();
			this._recipeSelector.RecipeChanged += this.OnProductionRecipeChanged;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003ADD File Offset: 0x00001CDD
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateStatusToggle();
			this._recipeSelector.RecipeChanged -= this.OnProductionRecipeChanged;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B02 File Offset: 0x00001D02
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B0A File Offset: 0x00001D0A
		public void UpdateStatusToggle()
		{
			if (base.Enabled && !this._recipeSelector.HasCurrentRecipe && !this._isAutomaticRecipeManufactory)
			{
				this._noRecipeStatusToggle.Activate();
				return;
			}
			this._noRecipeStatusToggle.Deactivate();
		}

		// Token: 0x0400004F RID: 79
		public static readonly string NoRecipeLocKey = "Status.Manufactory.NoRecipe";

		// Token: 0x04000050 RID: 80
		public static readonly string NoRecipeShortLocKey = "Status.Manufactory.NoRecipe.Short";

		// Token: 0x04000051 RID: 81
		public readonly ILoc _loc;

		// Token: 0x04000052 RID: 82
		public IRecipeSelector _recipeSelector;

		// Token: 0x04000053 RID: 83
		public StatusToggle _noRecipeStatusToggle;

		// Token: 0x04000054 RID: 84
		public bool _isAutomaticRecipeManufactory;
	}
}
