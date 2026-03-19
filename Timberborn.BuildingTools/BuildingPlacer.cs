using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.ConstructionSites;
using Timberborn.Coordinates;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.ScienceSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.BuildingTools
{
	// Token: 0x02000005 RID: 5
	public class BuildingPlacer : IBlockObjectPlacer
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002180 File Offset: 0x00000380
		public BuildingPlacer(BuildingCostSectionProvider buildingCostSectionProvider, ConstructionFactory constructionFactory, InputService inputService, VisualElementLoader visualElementLoader, ScienceService scienceService, ToolUnlockingService toolUnlockingService, UnlockSectionController unlockSectionController, IEnumerable<ISectionProvider> sectionProviders)
		{
			this._buildingCostSectionProvider = buildingCostSectionProvider;
			this._constructionFactory = constructionFactory;
			this._inputService = inputService;
			this._visualElementLoader = visualElementLoader;
			this._scienceService = scienceService;
			this._toolUnlockingService = toolUnlockingService;
			this._unlockSectionController = unlockSectionController;
			this._sectionProviders = sectionProviders.ToImmutableArray<ISectionProvider>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D5 File Offset: 0x000003D5
		public bool CanHandle(BlockObjectSpec template)
		{
			return template.HasSpec<BuildingSpec>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Place(BlockObjectSpec template, Placement placement, Action<BaseComponent> placedCallback)
		{
			BuildingSpec spec = template.GetSpec<BuildingSpec>();
			BaseComponent obj = this.ShouldBePlacedFinished(spec) ? this._constructionFactory.CreateAsFinished(spec, placement) : this._constructionFactory.CreateAsUnfinished(spec, placement);
			placedCallback(obj);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002221 File Offset: 0x00000421
		public void Describe(BlockObjectTool tool, ToolDescription.Builder builder, Preview preview)
		{
			this.AddBuildingCostSection(builder, preview);
			this.AddSections(builder, preview);
			this.AddUnlockSection(tool, builder);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000223B File Offset: 0x0000043B
		public bool ShouldBePlacedFinished(BuildingSpec buildingSpec)
		{
			return this._inputService.IsKeyHeld(BuildingPlacer.PlaceFinishedKey) || buildingSpec.PlaceFinished;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
		public void AddBuildingCostSection(ToolDescription.Builder builder, Preview preview)
		{
			VisualElement content;
			if (this._buildingCostSectionProvider.TryGetSection(preview, out content))
			{
				builder.AddExternalSection(content);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002280 File Offset: 0x00000480
		public void AddSections(ToolDescription.Builder builder, Preview preview)
		{
			ImmutableArray<ISectionProvider>.Enumerator enumerator = this._sectionProviders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				VisualElement content;
				if (enumerator.Current.TryGetSection(preview, out content))
				{
					builder.AddSection(content);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C0 File Offset: 0x000004C0
		public void AddUnlockSection(BlockObjectTool tool, ToolDescription.Builder builder)
		{
			if (this._toolUnlockingService.IsLocked(tool))
			{
				VisualElement root = this._visualElementLoader.LoadVisualElement("Game/ToolPanel/UnlockSection");
				BuildingSpec spec = tool.Template.GetSpec<BuildingSpec>();
				this.InitializeUnlockButton(tool, spec, UQueryExtensions.Q<VisualElement>(root, "UnlockButton", null));
				builder.AddUpdatableSection(root, delegate
				{
					this._unlockSectionController.UpdateSection(root, tool);
				});
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002354 File Offset: 0x00000554
		public void InitializeUnlockButton(BlockObjectTool tool, BuildingSpec buildingSpec, VisualElement unlockButton)
		{
			int buildingScienceCost = buildingSpec.ScienceCost;
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(unlockButton, "ScienceCostSection", null);
			UQueryExtensions.Q<Label>(visualElement, "ScienceCost", null).text = NumberFormatter.Format(buildingScienceCost);
			visualElement.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				unlockButton.EnableInClassList(BuildingPlacer.UnlockableClass, this._scienceService.SciencePoints >= buildingScienceCost);
			}, 0);
			visualElement.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._toolUnlockingService.TryToUnlock(tool);
			}, 0);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string PlaceFinishedKey = "PlaceFinished";

		// Token: 0x04000009 RID: 9
		public static readonly string UnlockableClass = "unlockable";

		// Token: 0x0400000A RID: 10
		public readonly BuildingCostSectionProvider _buildingCostSectionProvider;

		// Token: 0x0400000B RID: 11
		public readonly ConstructionFactory _constructionFactory;

		// Token: 0x0400000C RID: 12
		public readonly InputService _inputService;

		// Token: 0x0400000D RID: 13
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000E RID: 14
		public readonly ScienceService _scienceService;

		// Token: 0x0400000F RID: 15
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000010 RID: 16
		public readonly UnlockSectionController _unlockSectionController;

		// Token: 0x04000011 RID: 17
		public readonly ImmutableArray<ISectionProvider> _sectionProviders;
	}
}
