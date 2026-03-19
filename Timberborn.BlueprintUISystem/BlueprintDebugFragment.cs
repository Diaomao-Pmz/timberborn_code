using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.SerializationSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BlueprintUISystem
{
	// Token: 0x02000004 RID: 4
	public class BlueprintDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlueprintDebugFragment(DebugFragmentFactory debugFragmentFactory, DialogBoxShower dialogBoxShower, VisualElementLoader visualElementLoader, BlueprintSourceService blueprintSourceService, SerializedObjectReaderWriter serializedObjectReaderWriter)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._dialogBoxShower = dialogBoxShower;
			this._visualElementLoader = visualElementLoader;
			this._blueprintSourceService = blueprintSourceService;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.ShowBlueprint), "Show Blueprint");
			this._root = this._debugFragmentFactory.Create(debugFragmentButton);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000212C File Offset: 0x0000032C
		public void ShowFragment(BaseComponent entity)
		{
			ComponentSpec componentSpec = entity.AllComponents.First((object component) => component is ComponentSpec) as ComponentSpec;
			this._blueprint = componentSpec.Blueprint;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217A File Offset: 0x0000037A
		public void UpdateFragment()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000217C File Offset: 0x0000037C
		public void ClearFragment()
		{
			this._blueprint = null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
		public void ShowBlueprint()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/BlueprintDebugWindow");
			BlueprintFileBundle blueprintFileBundle = this._blueprintSourceService.Get(this._blueprint);
			UQueryExtensions.Q<Label>(visualElement, "Title", null).text = blueprintFileBundle.Name;
			UQueryExtensions.Q<Label>(visualElement, "Path", null).text = blueprintFileBundle.Path;
			TabView tabView = UQueryExtensions.Q<TabView>(visualElement, "Jsons", null);
			this.AddTabs(tabView, blueprintFileBundle);
			this._dialogBoxShower.Create().AddContent(visualElement).SetInfoButton(delegate
			{
				GUIUtility.systemCopyBuffer = UQueryExtensions.Q<TextField>(tabView.activeTab, null, null).text;
			}, "Copy to clipboard").SetConfirmButton(delegate()
			{
			}, "Close").Show();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002264 File Offset: 0x00000464
		public void AddTabs(TabView tabView, BlueprintFileBundle source)
		{
			SerializedObject serializedObject = this._serializedObjectReaderWriter.ReadJsons(source.Jsons);
			tabView.Add(this.CreateTab("Merged", this._serializedObjectReaderWriter.WriteJson(serializedObject), null));
			if (source.Jsons.Length > 1)
			{
				for (int i = 0; i < source.Jsons.Length; i++)
				{
					tabView.Add(this.CreateTab(string.Format("Part {0}", i + 1), source.Jsons[i], source.Sources[i]));
				}
				return;
			}
			UQueryExtensions.Q<VisualElement>(tabView, "unity-tab__header", null).SetEnabled(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002320 File Offset: 0x00000520
		public VisualElement CreateTab(string name, string content, string source = null)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/BlueprintDebugTab");
			UQueryExtensions.Q<Tab>(visualElement, null, null).label = name;
			UQueryExtensions.Q<Label>(visualElement, "Source", null).text = "Source: " + (source ?? "All");
			TextField textField = UQueryExtensions.Q<TextField>(visualElement, "Json", null);
			textField.selectAllOnFocus = false;
			textField.selectAllOnMouseUp = false;
			textField.SetValueWithoutNotify(content);
			return visualElement;
		}

		// Token: 0x04000006 RID: 6
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000007 RID: 7
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly BlueprintSourceService _blueprintSourceService;

		// Token: 0x0400000A RID: 10
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x0400000B RID: 11
		public Blueprint _blueprint;

		// Token: 0x0400000C RID: 12
		public VisualElement _root;
	}
}
