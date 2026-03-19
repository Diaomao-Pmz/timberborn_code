using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.MapStateSystem;
using Timberborn.ToolSystemUI;
using UnityEngine.UIElements;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200000E RID: 14
	public class BlockObjectToolDescriber : IBlockObjectToolDescriber
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002C8D File Offset: 0x00000E8D
		public BlockObjectToolDescriber(EntityDescriptionService entityDescriptionService, PreviewFactory previewFactory, VisualElementLoader visualElementLoader, MapEditorMode mapEditorMode)
		{
			this._entityDescriptionService = entityDescriptionService;
			this._previewFactory = previewFactory;
			this._visualElementLoader = visualElementLoader;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public ToolDescription Describe(BlockObjectTool blockObjectTool, IBlockObjectPlacer blockObjectPlacer)
		{
			PlaceableBlockObjectSpec template = blockObjectTool.Template;
			Preview previewFromTemplate = this.GetPreviewFromTemplate(template);
			string elementName = "Game/EntityDescription/DescriptionEmptySection";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			this._entityDescriptionService.DescribeAsSeparateSections(previewFromTemplate, visualElement, "");
			ToolDescription.Builder builder = new ToolDescription.Builder();
			if (visualElement.childCount > 0)
			{
				builder.AddSection(visualElement);
			}
			blockObjectPlacer.Describe(blockObjectTool, builder, previewFromTemplate);
			if (template.DevModeTool && !this._mapEditorMode.IsMapEditor)
			{
				string text = "<color=#ff0000><b>This is a DevModeTool</b></color>";
				builder.AddPrioritizedSection(text.ToUpper());
			}
			return builder.Build();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D58 File Offset: 0x00000F58
		public Preview GetPreviewFromTemplate(PlaceableBlockObjectSpec template)
		{
			return this._previewCache.GetOrAdd(template, () => this._previewFactory.Create(template));
		}

		// Token: 0x04000035 RID: 53
		public readonly EntityDescriptionService _entityDescriptionService;

		// Token: 0x04000036 RID: 54
		public readonly PreviewFactory _previewFactory;

		// Token: 0x04000037 RID: 55
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000038 RID: 56
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000039 RID: 57
		public readonly Dictionary<PlaceableBlockObjectSpec, Preview> _previewCache = new Dictionary<PlaceableBlockObjectSpec, Preview>();
	}
}
