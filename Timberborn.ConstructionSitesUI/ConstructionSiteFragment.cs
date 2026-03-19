using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.BuilderPrioritySystemUI;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.PrioritySystemUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000006 RID: 6
	public class ConstructionSiteFragment : IEntityPanelFragment
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000226E File Offset: 0x0000046E
		public ConstructionSiteFragment(BuilderPriorityToggleGroupFactory builderPriorityToggleGroupFactory, ConstructionSiteFragmentInventory constructionSiteFragmentInventory, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._builderPriorityToggleGroupFactory = builderPriorityToggleGroupFactory;
			this._constructionSiteFragmentInventory = constructionSiteFragmentInventory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002294 File Offset: 0x00000494
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ConstructionSiteFragment");
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._description = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._constructionSiteFragmentInventory.InitializeFragment(this._root);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._root, "HeaderWrapper", null);
			this._priorityToggleGroup = this._builderPriorityToggleGroupFactory.Create(visualElement, ConstructionSiteFragment.PriorityLabelLocKey);
			this._tooltipRegistrar.RegisterLocalizable(UQueryExtensions.Q<VisualElement>(visualElement, "TogglesWrapper", null), ConstructionSiteFragment.PriorityLocKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002350 File Offset: 0x00000550
		public void ShowFragment(BaseComponent entity)
		{
			this._constructionSite = entity.GetComponent<ConstructionSite>();
			if (this._constructionSite)
			{
				this._constructionSiteDescriber = this._constructionSite.GetComponent<ConstructionSiteDescriber>();
				this._builderPrioritizable = entity.GetComponent<BuilderPrioritizable>();
				this._constructionSiteFragmentInventory.ShowFragment(this._constructionSite.Inventory);
				this._priorityToggleGroup.Enable(this._builderPrioritizable);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023BA File Offset: 0x000005BA
		public void ClearFragment()
		{
			this._constructionSite = null;
			this._constructionSiteDescriber = null;
			this._builderPrioritizable = null;
			this._constructionSiteFragmentInventory.ClearFragment();
			this._priorityToggleGroup.Disable();
			this.UpdateFragment();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023F0 File Offset: 0x000005F0
		public void UpdateFragment()
		{
			if (this._constructionSite && this._constructionSite.Enabled)
			{
				this._description.text = this._constructionSiteDescriber.GetProgressInfoShort();
				this._constructionSiteFragmentInventory.UpdateFragment();
				this._progressBar.SetProgress(this._constructionSite.BuildTimeProgress);
				this._priorityToggleGroup.UpdateGroup();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string PriorityLabelLocKey = "ConstructionSites.DisplayName";

		// Token: 0x0400000E RID: 14
		public static readonly string PriorityLocKey = "ConstructionSites.Priority";

		// Token: 0x0400000F RID: 15
		public readonly BuilderPriorityToggleGroupFactory _builderPriorityToggleGroupFactory;

		// Token: 0x04000010 RID: 16
		public readonly ConstructionSiteFragmentInventory _constructionSiteFragmentInventory;

		// Token: 0x04000011 RID: 17
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public ConstructionSite _constructionSite;

		// Token: 0x04000014 RID: 20
		public ConstructionSiteDescriber _constructionSiteDescriber;

		// Token: 0x04000015 RID: 21
		public BuilderPrioritizable _builderPrioritizable;

		// Token: 0x04000016 RID: 22
		public Label _description;

		// Token: 0x04000017 RID: 23
		public VisualElement _root;

		// Token: 0x04000018 RID: 24
		public ProgressBar _progressBar;

		// Token: 0x04000019 RID: 25
		public PriorityToggleGroup _priorityToggleGroup;
	}
}
