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
	// Token: 0x0200000F RID: 15
	public class NaturalResourceSpawningBrushPanel : IToolFragment
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public NaturalResourceSpawningBrushPanel(EventBus eventBus, TemplateService templateService, ILoc loc, VisualElementLoader visualElementLoader)
		{
			this._eventBus = eventBus;
			this._templateService = templateService;
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D14 File Offset: 0x00000F14
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/NaturalResourceSpawningBrushPanel");
			this._togglesContainer = UQueryExtensions.Q<VisualElement>(this._root, "Toggles", null);
			this._densitySlider = UQueryExtensions.Q<Slider>(this._root, "Slider", null);
			this._sliderValue = UQueryExtensions.Q<Label>(this._root, "SliderValue", null);
			this._randomizeYieldGrowthToggle = UQueryExtensions.Q<Toggle>(this._root, "RandomizeYieldGrowthToggle", null);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			this.InitializeTypeToggles();
			this._densitySlider.highValue = 1f;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._densitySlider, new EventCallback<ChangeEvent<float>>(this.SetDensity));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._randomizeYieldGrowthToggle, delegate(ChangeEvent<bool> evt)
			{
				this._tool.SwitchRandomizeYieldGrowth(evt.newValue);
			});
			return this._root;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DF8 File Offset: 0x00000FF8
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

		// Token: 0x0600004C RID: 76 RVA: 0x00002ED0 File Offset: 0x000010D0
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void InitializeTypeToggles()
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

		// Token: 0x0600004E RID: 78 RVA: 0x00002FB4 File Offset: 0x000011B4
		public void AddToggle(SpawnableResource spawnable, string spawnableName)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/ToolPanelToggle");
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "ToolPanelToggle", null);
			toggle.text = this.GetFullName(spawnable, spawnableName);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> evt)
			{
				this.SetSpawnableResourceEnabled(spawnable, evt.newValue);
			});
			this._togglesContainer.Add(visualElement);
			this._toggles.Add(spawnable, toggle);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003037 File Offset: 0x00001237
		public void SetSpawnableResourceEnabled(SpawnableResource spawnableResource, bool value)
		{
			if (value)
			{
				this._tool.EnableSpawnableResource(spawnableResource);
				return;
			}
			this._tool.DisableSpawnableResource(spawnableResource);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003055 File Offset: 0x00001255
		public void UpdateSliderValue(float value)
		{
			this._sliderValue.text = value.ToString("P0");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003070 File Offset: 0x00001270
		public string GetFullName(SpawnableResource matureSpawnable, string spawnableName)
		{
			string str = matureSpawnable.IsSeedling ? this._loc.T(NaturalResourceSpawningBrushPanel.SeedlingLocKey) : this._loc.T<string>(NaturalResourceSpawningBrushPanel.MatureLocKey, spawnableName);
			return spawnableName + " " + str;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030B6 File Offset: 0x000012B6
		public void SetDensity(ChangeEvent<float> changeEvent)
		{
			this._tool.Density = changeEvent.newValue;
			this.UpdateSliderValue(changeEvent.newValue);
		}

		// Token: 0x04000044 RID: 68
		public static readonly string SeedlingLocKey = "NaturalResources.Seedling";

		// Token: 0x04000045 RID: 69
		public static readonly string MatureLocKey = "NaturalResources.Mature";

		// Token: 0x04000046 RID: 70
		public readonly EventBus _eventBus;

		// Token: 0x04000047 RID: 71
		public readonly TemplateService _templateService;

		// Token: 0x04000048 RID: 72
		public readonly ILoc _loc;

		// Token: 0x04000049 RID: 73
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004A RID: 74
		public VisualElement _root;

		// Token: 0x0400004B RID: 75
		public VisualElement _togglesContainer;

		// Token: 0x0400004C RID: 76
		public Label _sliderValue;

		// Token: 0x0400004D RID: 77
		public Slider _densitySlider;

		// Token: 0x0400004E RID: 78
		public Toggle _randomizeYieldGrowthToggle;

		// Token: 0x0400004F RID: 79
		public readonly Dictionary<SpawnableResource, Toggle> _toggles = new Dictionary<SpawnableResource, Toggle>();

		// Token: 0x04000050 RID: 80
		public NaturalResourceSpawningBrushTool _tool;
	}
}
