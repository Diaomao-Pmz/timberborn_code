using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.NaturalResources;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x0200000B RID: 11
	internal class NaturalResourceSpawningBrushPanel : IToolFragment
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000027F0 File Offset: 0x000009F0
		public NaturalResourceSpawningBrushPanel(EventBus eventBus, TemplateService templateService, ILoc loc, VisualElementLoader visualElementLoader)
		{
			this._eventBus = eventBus;
			this._templateService = templateService;
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002820 File Offset: 0x00000A20
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/NaturalResourceSpawningBrushPanel");
			this._togglesContainer = this._root.Q("Toggles", null);
			this._densitySlider = this._root.Q("Slider", null);
			this._sliderValue = this._root.Q("SliderValue", null);
			this._randomizeYieldGrowthToggle = this._root.Q("RandomizeYieldGrowthToggle", null);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			this.InitializeTypeToggles();
			this._densitySlider.highValue = 1f;
			this._densitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.SetDensity));
			this._randomizeYieldGrowthToggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this._tool.SwitchRandomizeYieldGrowth(evt.newValue);
			});
			return this._root;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002904 File Offset: 0x00000B04
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._tool = (toolEnteredEvent.Tool as NaturalResourceSpawningBrushTool);
			if (this._tool != null)
			{
				this._root.ToggleDisplayStyle(true);
				foreach (SpawnableResource spawnableResource in this._toggles.Keys)
				{
					bool valueWithoutNotify = this._tool.IsNaturalResourceEnabled(spawnableResource);
					this._toggles[spawnableResource].SetValueWithoutNotify(valueWithoutNotify);
				}
				this._densitySlider.SetValueWithoutNotify(this._tool.Density);
				this.UpdateSliderValue(this._tool.Density);
				this._randomizeYieldGrowthToggle.SetValueWithoutNotify(this._tool.RandomizeYieldGrowth);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029DC File Offset: 0x00000BDC
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029EC File Offset: 0x00000BEC
		private void InitializeTypeToggles()
		{
			foreach (NaturalResourceSpec naturalResourceSpec in from naturalResource in this._templateService.GetAll<NaturalResourceSpec>()
			where naturalResource.UsableWithCurrentFeatureToggles
			orderby naturalResource.Order
			select naturalResource)
			{
				string templateName = naturalResourceSpec.GetSpec<TemplateSpec>().TemplateName;
				string spawnableName = this._loc.T(naturalResourceSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey);
				this.AddToggle(new SpawnableResource(templateName, false), spawnableName);
				this.AddToggle(new SpawnableResource(templateName, true), spawnableName);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AC0 File Offset: 0x00000CC0
		private void AddToggle(SpawnableResource spawnable, string spawnableName)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/ToolPanelToggle");
			Toggle toggle = visualElement.Q("ToolPanelToggle", null);
			toggle.text = this.GetFullName(spawnable, spawnableName);
			toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.SetSpawnableResourceEnabled(spawnable, evt.newValue);
			});
			this._togglesContainer.Add(visualElement);
			this._toggles.Add(spawnable, toggle);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B43 File Offset: 0x00000D43
		private void SetSpawnableResourceEnabled(SpawnableResource spawnableResource, bool value)
		{
			if (value)
			{
				this._tool.EnableSpawnableResource(spawnableResource);
				return;
			}
			this._tool.DisableSpawnableResource(spawnableResource);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B61 File Offset: 0x00000D61
		private void UpdateSliderValue(float value)
		{
			this._sliderValue.text = value.ToString("P0");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B7C File Offset: 0x00000D7C
		private string GetFullName(SpawnableResource matureSpawnable, string spawnableName)
		{
			string str = matureSpawnable.IsSeedling ? this._loc.T(NaturalResourceSpawningBrushPanel.SeedlingLocKey) : this._loc.T<string>(NaturalResourceSpawningBrushPanel.MatureLocKey, spawnableName);
			return spawnableName + " " + str;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BC2 File Offset: 0x00000DC2
		private void SetDensity(ChangeEvent<float> changeEvent)
		{
			this._tool.Density = changeEvent.newValue;
			this.UpdateSliderValue(changeEvent.newValue);
		}

		// Token: 0x04000029 RID: 41
		private static readonly string SeedlingLocKey = "NaturalResources.Seedling";

		// Token: 0x0400002A RID: 42
		private static readonly string MatureLocKey = "NaturalResources.Mature";

		// Token: 0x0400002B RID: 43
		private readonly EventBus _eventBus;

		// Token: 0x0400002C RID: 44
		private readonly TemplateService _templateService;

		// Token: 0x0400002D RID: 45
		private readonly ILoc _loc;

		// Token: 0x0400002E RID: 46
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		private VisualElement _root;

		// Token: 0x04000030 RID: 48
		private VisualElement _togglesContainer;

		// Token: 0x04000031 RID: 49
		private Label _sliderValue;

		// Token: 0x04000032 RID: 50
		private Slider _densitySlider;

		// Token: 0x04000033 RID: 51
		private Toggle _randomizeYieldGrowthToggle;

		// Token: 0x04000034 RID: 52
		private readonly Dictionary<SpawnableResource, Toggle> _toggles = new Dictionary<SpawnableResource, Toggle>();

		// Token: 0x04000035 RID: 53
		private NaturalResourceSpawningBrushTool _tool;
	}
}
