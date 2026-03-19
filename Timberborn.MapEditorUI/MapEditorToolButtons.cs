using System;
using System.Collections.Generic;
using Timberborn.BlockObjectTools;
using Timberborn.BottomBarSystem;
using Timberborn.MapEditorBrushesUI;
using Timberborn.MapEditorNaturalResourcesUI;
using Timberborn.MapMetadataSystemUI;
using Timberborn.MapThumbnailCapturingUI;
using Timberborn.ToolButtonSystem;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000007 RID: 7
	internal class MapEditorToolButtons : IBottomBarElementsProvider
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000027F8 File Offset: 0x000009F8
		public MapEditorToolButtons(ToolButtonFactory toolButtonFactory, AbsoluteTerrainHeightBrushTool absoluteTerrainHeightBrushTool, RelativeTerrainHeightBrushTool relativeTerrainHeightBrushTool, SculptingTerrainBrushTool sculptingTerrainBrushTool, NaturalResourceSpawningBrushTool naturalResourceSpawningBrushTool, NaturalResourceRemovalBrushTool naturalResourceRemovalBrushTool, EntityBlockObjectDeletionTool entityBlockObjectDeletionTool, ThumbnailCapturingTool thumbnailCapturingTool, MapMetadataTool mapMetadataTool)
		{
			this._toolButtonFactory = toolButtonFactory;
			this._absoluteTerrainHeightBrushTool = absoluteTerrainHeightBrushTool;
			this._relativeTerrainHeightBrushTool = relativeTerrainHeightBrushTool;
			this._sculptingTerrainBrushTool = sculptingTerrainBrushTool;
			this._naturalResourceSpawningBrushTool = naturalResourceSpawningBrushTool;
			this._naturalResourceRemovalBrushTool = naturalResourceRemovalBrushTool;
			this._entityBlockObjectDeletionTool = entityBlockObjectDeletionTool;
			this._thumbnailCapturingTool = thumbnailCapturingTool;
			this._mapMetadataTool = mapMetadataTool;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002850 File Offset: 0x00000A50
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolButton toolButton = this._toolButtonFactory.CreateGrouplessBlue(this._absoluteTerrainHeightBrushTool, MapEditorToolButtons.AbsoluteTerrainHeightBrushImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			ToolButton toolButton2 = this._toolButtonFactory.CreateGrouplessBlue(this._relativeTerrainHeightBrushTool, MapEditorToolButtons.RelativeTerrainHeightBrushImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton2.Root);
			ToolButton toolButton3 = this._toolButtonFactory.CreateGrouplessBlue(this._sculptingTerrainBrushTool, MapEditorToolButtons.SculptingTerrainBrushImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton3.Root);
			ToolButton toolButton4 = this._toolButtonFactory.CreateGrouplessBlue(this._naturalResourceSpawningBrushTool, MapEditorToolButtons.NaturalResourcesSpawningImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton4.Root);
			ToolButton toolButton5 = this._toolButtonFactory.CreateGrouplessBlue(this._naturalResourceRemovalBrushTool, MapEditorToolButtons.NaturalResourcesRemovalImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton5.Root);
			ToolButton toolButton6 = this._toolButtonFactory.CreateGrouplessBlue(this._entityBlockObjectDeletionTool, MapEditorToolButtons.DeleteBuildingImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton6.Root);
			ToolButton toolButton7 = this._toolButtonFactory.CreateGrouplessBlue(this._thumbnailCapturingTool, MapEditorToolButtons.ThumbnailCapturingImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton7.Root);
			ToolButton toolButton8 = this._toolButtonFactory.CreateGrouplessBlue(this._mapMetadataTool, MapEditorToolButtons.MapMetadataImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton8.Root);
			yield break;
		}

		// Token: 0x04000027 RID: 39
		private static readonly string AbsoluteTerrainHeightBrushImageKey = "AbsoluteTerrainHeightBrush";

		// Token: 0x04000028 RID: 40
		private static readonly string RelativeTerrainHeightBrushImageKey = "RelativeTerrainHeightBrushIcon";

		// Token: 0x04000029 RID: 41
		private static readonly string SculptingTerrainBrushImageKey = "SculptingTerrainBrushIcon";

		// Token: 0x0400002A RID: 42
		private static readonly string NaturalResourcesSpawningImageKey = "NaturalResourcesIcon";

		// Token: 0x0400002B RID: 43
		private static readonly string NaturalResourcesRemovalImageKey = "RemoveNaturalResourcesIcon";

		// Token: 0x0400002C RID: 44
		private static readonly string DeleteBuildingImageKey = "DeleteObjectIcon";

		// Token: 0x0400002D RID: 45
		private static readonly string ThumbnailCapturingImageKey = "ThumbnailCapturingIcon";

		// Token: 0x0400002E RID: 46
		private static readonly string MapMetadataImageKey = "MapMetadataIcon";

		// Token: 0x0400002F RID: 47
		private readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000030 RID: 48
		private readonly AbsoluteTerrainHeightBrushTool _absoluteTerrainHeightBrushTool;

		// Token: 0x04000031 RID: 49
		private readonly RelativeTerrainHeightBrushTool _relativeTerrainHeightBrushTool;

		// Token: 0x04000032 RID: 50
		private readonly SculptingTerrainBrushTool _sculptingTerrainBrushTool;

		// Token: 0x04000033 RID: 51
		private readonly NaturalResourceSpawningBrushTool _naturalResourceSpawningBrushTool;

		// Token: 0x04000034 RID: 52
		private readonly NaturalResourceRemovalBrushTool _naturalResourceRemovalBrushTool;

		// Token: 0x04000035 RID: 53
		private readonly EntityBlockObjectDeletionTool _entityBlockObjectDeletionTool;

		// Token: 0x04000036 RID: 54
		private readonly ThumbnailCapturingTool _thumbnailCapturingTool;

		// Token: 0x04000037 RID: 55
		private readonly MapMetadataTool _mapMetadataTool;
	}
}
