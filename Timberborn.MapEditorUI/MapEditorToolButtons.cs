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
	// Token: 0x0200000A RID: 10
	public class MapEditorToolButtons : IBottomBarElementsProvider
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002A58 File Offset: 0x00000C58
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

		// Token: 0x06000039 RID: 57 RVA: 0x00002AB0 File Offset: 0x00000CB0
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

		// Token: 0x04000034 RID: 52
		public static readonly string AbsoluteTerrainHeightBrushImageKey = "AbsoluteTerrainHeightBrush";

		// Token: 0x04000035 RID: 53
		public static readonly string RelativeTerrainHeightBrushImageKey = "RelativeTerrainHeightBrushIcon";

		// Token: 0x04000036 RID: 54
		public static readonly string SculptingTerrainBrushImageKey = "SculptingTerrainBrushIcon";

		// Token: 0x04000037 RID: 55
		public static readonly string NaturalResourcesSpawningImageKey = "NaturalResourcesIcon";

		// Token: 0x04000038 RID: 56
		public static readonly string NaturalResourcesRemovalImageKey = "RemoveNaturalResourcesIcon";

		// Token: 0x04000039 RID: 57
		public static readonly string DeleteBuildingImageKey = "DeleteObjectIcon";

		// Token: 0x0400003A RID: 58
		public static readonly string ThumbnailCapturingImageKey = "ThumbnailCapturingIcon";

		// Token: 0x0400003B RID: 59
		public static readonly string MapMetadataImageKey = "MapMetadataIcon";

		// Token: 0x0400003C RID: 60
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x0400003D RID: 61
		public readonly AbsoluteTerrainHeightBrushTool _absoluteTerrainHeightBrushTool;

		// Token: 0x0400003E RID: 62
		public readonly RelativeTerrainHeightBrushTool _relativeTerrainHeightBrushTool;

		// Token: 0x0400003F RID: 63
		public readonly SculptingTerrainBrushTool _sculptingTerrainBrushTool;

		// Token: 0x04000040 RID: 64
		public readonly NaturalResourceSpawningBrushTool _naturalResourceSpawningBrushTool;

		// Token: 0x04000041 RID: 65
		public readonly NaturalResourceRemovalBrushTool _naturalResourceRemovalBrushTool;

		// Token: 0x04000042 RID: 66
		public readonly EntityBlockObjectDeletionTool _entityBlockObjectDeletionTool;

		// Token: 0x04000043 RID: 67
		public readonly ThumbnailCapturingTool _thumbnailCapturingTool;

		// Token: 0x04000044 RID: 68
		public readonly MapMetadataTool _mapMetadataTool;
	}
}
