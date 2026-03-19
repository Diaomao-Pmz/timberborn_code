using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200000C RID: 12
	public class EntityDescriptionService
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002841 File Offset: 0x00000A41
		public EntityDescriptionService(VisualElementLoader visualElementLoader, RowShader rowShader)
		{
			this._visualElementLoader = visualElementLoader;
			this._rowShader = rowShader;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000286D File Offset: 0x00000A6D
		public void DescribeAsSingleSection(BaseComponent subject, VisualElement root)
		{
			this.Describe(subject, root, true, "");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000287D File Offset: 0x00000A7D
		public void DescribeAsSeparateSections(BaseComponent subject, VisualElement root, string startingDescription = "")
		{
			this.Describe(subject, root, false, startingDescription);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000288C File Offset: 0x00000A8C
		public void Describe(BaseComponent subject, VisualElement root, bool singleSection, string startingDescription)
		{
			subject.GetComponents<IEntityDescriber>(this._entityDescribersCache);
			IOrderedEnumerable<EntityDescription> collection = from description in this._entityDescribersCache.SelectMany((IEntityDescriber describer) => describer.DescribeEntity())
			orderby description.Order
			select description;
			this._entityDescriptions.AddRange(collection);
			this.AddSections(subject, root, singleSection, startingDescription);
			this._entityDescribersCache.Clear();
			this._entityDescriptions.Clear();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002921 File Offset: 0x00000B21
		public void AddSections(BaseComponent subject, VisualElement root, bool singleSection, string startingDescription)
		{
			this.DescribeHeader(subject, root, singleSection);
			this.DescribeProduction(root);
			this.DescribeText(root, singleSection, startingDescription);
			this.DescribeBottomSections(root, singleSection);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002948 File Offset: 0x00000B48
		public void DescribeHeader(BaseComponent subject, VisualElement root, bool singleSection)
		{
			if (!singleSection)
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescriptionHeader");
				LabeledEntity component = subject.GetComponent<LabeledEntity>();
				UQueryExtensions.Q<Label>(visualElement, "Title", null).text = component.DisplayName;
				UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = component.Image;
				this.AddMiddleSections(UQueryExtensions.Q<VisualElement>(visualElement, "AdditionalInfo", null));
				root.Add(visualElement);
				return;
			}
			string elementName = "Game/EntityDescription/DescriptionEmptySection";
			VisualElement visualElement2 = this._visualElementLoader.LoadVisualElement(elementName);
			visualElement2.AddToClassList(EntityDescriptionService.MiddleSectionRootClass);
			this.AddMiddleSections(visualElement2);
			if (visualElement2.childCount > 0)
			{
				root.Add(visualElement2);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029F0 File Offset: 0x00000BF0
		public void AddMiddleSections(VisualElement middleSectionRoot)
		{
			foreach (EntityDescription entityDescription in from description in this._entityDescriptions
			where description.MiddleSection
			select description)
			{
				middleSectionRoot.Add(entityDescription.Section);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A68 File Offset: 0x00000C68
		public void DescribeProduction(VisualElement root)
		{
			if (this._entityDescriptions.Any((EntityDescription description) => description.ProductionSection))
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/ProductionItems");
				EntityDescriptionService.AddElements(from description in this._entityDescriptions
				where description.Input && description.Output
				select description, visualElement, "InputAndOutput");
				this._rowShader.ShadeRows(UQueryExtensions.Q<VisualElement>(visualElement, "InputAndOutput", null));
				bool flag = EntityDescriptionService.AddElements(from description in this._entityDescriptions
				where description.Input && !description.Output
				select description, visualElement, "Inputs");
				bool flag2 = EntityDescriptionService.AddElements(from description in this._entityDescriptions
				where !description.Input && description.Output
				select description, visualElement, "Outputs");
				UQueryExtensions.Q<Image>(visualElement, "InputOrOutputIcon", null).ToggleDisplayStyle(flag || flag2);
				this.SetTime(visualElement);
				root.Add(visualElement);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B90 File Offset: 0x00000D90
		public static bool AddElements(IEnumerable<EntityDescription> descriptions, VisualElement root, string name)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, name, null);
			bool result = false;
			foreach (EntityDescription entityDescription in descriptions)
			{
				visualElement.Add(entityDescription.Section);
				result = true;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void SetTime(VisualElement productionItems)
		{
			EntityDescription entityDescription = this._entityDescriptions.FirstOrDefault((EntityDescription description) => description.Time != null);
			Label label = UQueryExtensions.Q<Label>(productionItems, "Time", null);
			label.ToggleDisplayStyle(entityDescription != null);
			if (entityDescription != null)
			{
				label.text = entityDescription.Time;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void DescribeBottomSections(VisualElement root, bool singleSection)
		{
			if (this._entityDescriptions.Any((EntityDescription description) => description.BottomSection))
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescriptionBottomSection");
				EntityDescriptionService.AddElements(from description in this._entityDescriptions
				where description.BottomSection
				select description, visualElement, "BottomSection");
				visualElement.EnableInClassList(EntityDescriptionService.DescriptionBackgroundClass, !singleSection);
				root.Add(visualElement);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void DescribeText(VisualElement root, bool singleSection, string startingDescription)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescriptionText");
			visualElement.EnableInClassList(EntityDescriptionService.SingleSectionClass, singleSection);
			visualElement.EnableInClassList(EntityDescriptionService.DescriptionBackgroundClass, !singleSection);
			IEnumerable<EntityDescription> entityDescriptions = from description in this._entityDescriptions
			where description.TextSection
			select description;
			Label label = UQueryExtensions.Q<Label>(visualElement, "Description", null);
			bool flag = this.Describe(entityDescriptions, label, startingDescription);
			label.ToggleDisplayStyle(flag);
			IEnumerable<EntityDescription> entityDescriptions2 = from description in this._entityDescriptions
			where description.FlavorSection
			select description;
			Label label2 = UQueryExtensions.Q<Label>(visualElement, "Flavor", null);
			bool flag2 = this.Describe(entityDescriptions2, label2, "");
			label2.ToggleDisplayStyle(flag2);
			if (flag2 || flag)
			{
				root.Add(visualElement);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public bool Describe(IEnumerable<EntityDescription> entityDescriptions, Label textLabel, string startingDescription)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(startingDescription))
			{
				stringBuilder.AppendLine(startingDescription);
			}
			foreach (EntityDescription entityDescription in entityDescriptions)
			{
				stringBuilder.AppendLine(entityDescription.Content);
			}
			string text = stringBuilder.ToStringWithoutNewLineEnd();
			textLabel.text = text;
			return !string.IsNullOrEmpty(text);
		}

		// Token: 0x0400001F RID: 31
		public static readonly string DescriptionBackgroundClass = "bg-sub-box--blue";

		// Token: 0x04000020 RID: 32
		public static readonly string SingleSectionClass = "description-text--single-section";

		// Token: 0x04000021 RID: 33
		public static readonly string MiddleSectionRootClass = "content-row-centered";

		// Token: 0x04000022 RID: 34
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000023 RID: 35
		public readonly RowShader _rowShader;

		// Token: 0x04000024 RID: 36
		public readonly List<EntityDescription> _entityDescriptions = new List<EntityDescription>();

		// Token: 0x04000025 RID: 37
		public readonly List<IEntityDescriber> _entityDescribersCache = new List<IEntityDescriber>();
	}
}
