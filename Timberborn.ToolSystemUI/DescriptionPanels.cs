using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x02000006 RID: 6
	public class DescriptionPanels
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002275 File Offset: 0x00000475
		public DescriptionPanels(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002290 File Offset: 0x00000490
		public DescriptionPanel GetDescriptionPanel(IToolDescriptor toolDescriptor)
		{
			DescriptionPanel descriptionPanel;
			if (!this._descriptionPanels.TryGetValue(toolDescriptor, out descriptionPanel))
			{
				descriptionPanel = this.CreateDescriptionPanel(toolDescriptor);
				this._descriptionPanels.Add(toolDescriptor, descriptionPanel);
			}
			return descriptionPanel;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C4 File Offset: 0x000004C4
		public DescriptionPanel CreateDescriptionPanel(IToolDescriptor toolDescriptor)
		{
			ToolDescription toolDescription = toolDescriptor.DescribeTool();
			VisualElement root = this._visualElementLoader.LoadVisualElement("Common/ToolPanel/DescriptionPanel");
			DescriptionPanel descriptionPanel = new DescriptionPanel(root);
			DescriptionPanels.SetBasicInfo(toolDescription, root);
			this.AddSections(toolDescription, descriptionPanel);
			return descriptionPanel;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002300 File Offset: 0x00000500
		public static void SetBasicInfo(ToolDescription toolDescription, VisualElement root)
		{
			Label label = UQueryExtensions.Q<Label>(root, "Title", null);
			if (toolDescription.HasTitle)
			{
				label.text = toolDescription.Title;
			}
			label.parent.ToggleDisplayStyle(toolDescription.HasTitle);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002340 File Offset: 0x00000540
		public void AddSections(ToolDescription toolDescription, DescriptionPanel panel)
		{
			VisualElement root = panel.Root;
			VisualElement internalSections = UQueryExtensions.Q<VisualElement>(root, "InternalSections", null);
			VisualElement externalSections = UQueryExtensions.Q<VisualElement>(root, "ExternalSections", null);
			foreach (ToolDescriptionSection toolDescriptionSection in toolDescription.Sections)
			{
				this.AddSection(toolDescriptionSection, internalSections, externalSections);
				if (toolDescriptionSection.UpdateCallback != null)
				{
					panel.AddUpdateCallback(toolDescriptionSection.UpdateCallback);
				}
			}
			panel.AddUpdateCallback(delegate
			{
				internalSections.ToggleDisplayStyle(internalSections.childCount > 0);
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023F4 File Offset: 0x000005F4
		public void AddSection(ToolDescriptionSection toolDescriptionSection, VisualElement internalSections, VisualElement externalSections)
		{
			VisualElement sectionRoot = this.GetSectionRoot(toolDescriptionSection);
			if (toolDescriptionSection.Prioritized)
			{
				sectionRoot.AddToClassList(DescriptionPanels.PrioritizedClass);
				sectionRoot.AddToClassList(DescriptionPanels.BackgroundClass);
			}
			if (toolDescriptionSection.Section == null)
			{
				if (!string.IsNullOrEmpty(toolDescriptionSection.Content))
				{
					UQueryExtensions.Q<Label>(sectionRoot, "SectionText", null).text = toolDescriptionSection.Content;
					sectionRoot.AddToClassList(DescriptionPanels.BackgroundClass);
					internalSections.Add(sectionRoot);
				}
				return;
			}
			if (toolDescriptionSection.External)
			{
				externalSections.Add(sectionRoot);
				return;
			}
			internalSections.Add(toolDescriptionSection.Section);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002484 File Offset: 0x00000684
		public VisualElement GetSectionRoot(ToolDescriptionSection toolDescriptionSection)
		{
			if (toolDescriptionSection.External)
			{
				return toolDescriptionSection.Section;
			}
			string elementName = "Common/ToolPanel/DescriptionPanelSection";
			return this._visualElementLoader.LoadVisualElement(elementName);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string BackgroundClass = "bg-sub-box--blue";

		// Token: 0x0400000E RID: 14
		public static readonly string PrioritizedClass = "description-panel-section--prioritized";

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly Dictionary<IToolDescriptor, DescriptionPanel> _descriptionPanels = new Dictionary<IToolDescriptor, DescriptionPanel>();
	}
}
