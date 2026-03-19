using System;
using Timberborn.BlockObjectTools;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameStartup
{
	// Token: 0x02000013 RID: 19
	public class StartingBuildingToolDescriber : IBlockObjectToolDescriber, ILoadableSingleton
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002862 File Offset: 0x00000A62
		public StartingBuildingToolDescriber(VisualElementLoader visualElementLoader, StartingBuildingSpawner startingBuildingSpawner, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._startingBuildingSpawner = startingBuildingSpawner;
			this._loc = loc;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002880 File Offset: 0x00000A80
		public ToolDescription Describe(BlockObjectTool blockObjectTool, IBlockObjectPlacer blockObjectPlacer)
		{
			string elementName = "Game/EntityDescription/DescriptionEmptySection";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			this.AddHeaderSection(visualElement);
			ToolDescription.Builder builder = new ToolDescription.Builder();
			if (visualElement.childCount > 0)
			{
				builder.AddSection(visualElement);
			}
			return builder.Build();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void Load()
		{
			TemplateSpec startingBuildingTemplateSpec = this._startingBuildingSpawner.StartingBuildingTemplateSpec;
			this._iconSprite = startingBuildingTemplateSpec.GetSpec<LabeledEntitySpec>().Icon.Asset;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028F4 File Offset: 0x00000AF4
		public void AddHeaderSection(VisualElement root)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescriptionHeader");
			UQueryExtensions.Q<Label>(visualElement, "Title", null).text = this._loc.T("FlexibleStart.ChooseStartLocation");
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = this._iconSprite;
			root.Add(visualElement);
		}

		// Token: 0x04000036 RID: 54
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000037 RID: 55
		public readonly StartingBuildingSpawner _startingBuildingSpawner;

		// Token: 0x04000038 RID: 56
		public readonly ILoc _loc;

		// Token: 0x04000039 RID: 57
		public Sprite _iconSprite;
	}
}
