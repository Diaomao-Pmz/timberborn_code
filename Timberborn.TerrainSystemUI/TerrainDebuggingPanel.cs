using System;
using System.Text;
using Timberborn.Common;
using Timberborn.CursorToolSystem;
using Timberborn.DebuggingUI;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;
using Timberborn.TerrainSystem;
using Timberborn.TerrainSystemRendering;
using UnityEngine;

namespace Timberborn.TerrainSystemUI
{
	// Token: 0x02000004 RID: 4
	public class TerrainDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public TerrainDebuggingPanel(DebuggingPanel debuggingPanel, CursorDebugger cursorDebugger, IThreadSafeColumnTerrainMap threadSafeColumnTerrainMap, MapIndexService mapIndexService, ISoilMoistureService soilMoistureService, ISoilContaminationService soilContaminationService, TerrainMaterialMap terrainMaterialMap)
		{
			this._debuggingPanel = debuggingPanel;
			this._cursorDebugger = cursorDebugger;
			this._threadSafeColumnTerrainMap = threadSafeColumnTerrainMap;
			this._mapIndexService = mapIndexService;
			this._soilMoistureService = soilMoistureService;
			this._soilContaminationService = soilContaminationService;
			this._terrainMaterialMap = terrainMaterialMap;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000210F File Offset: 0x0000030F
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Terrain columns");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public string GetText()
		{
			if (this._cursorDebugger.Active)
			{
				Vector2Int vector2Int = this._cursorDebugger.Coordinates.XY();
				int num = this._mapIndexService.CellToIndex(vector2Int);
				int columnCount = this._threadSafeColumnTerrainMap.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int num2 = num + i * this._mapIndexService.VerticalStride;
					int columnFloor = this._threadSafeColumnTerrainMap.GetColumnFloor(num2);
					int columnCeiling = this._threadSafeColumnTerrainMap.GetColumnCeiling(num2);
					this._text.AppendLine(string.Format("Column {0} - {1}", columnFloor, columnCeiling));
					this._text.AppendLine(string.Format("  - Soil moisture: {0:0.00}", this._soilMoistureService.SoilMoisture(num2)));
					float desertIntensity = this._terrainMaterialMap.GetDesertIntensity(vector2Int.ToVector3Int(columnCeiling));
					this._text.AppendLine(string.Format("  - Desert intensity: {0:0.00}", desertIntensity));
					this._text.AppendLine(string.Format("  - Soil contamination: {0:0.00}", this._soilContaminationService.Contamination(num2)));
				}
				return this._text.ToStringAndClear();
			}
			return null;
		}

		// Token: 0x04000006 RID: 6
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000007 RID: 7
		public readonly CursorDebugger _cursorDebugger;

		// Token: 0x04000008 RID: 8
		public readonly IThreadSafeColumnTerrainMap _threadSafeColumnTerrainMap;

		// Token: 0x04000009 RID: 9
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400000A RID: 10
		public readonly ISoilMoistureService _soilMoistureService;

		// Token: 0x0400000B RID: 11
		public readonly ISoilContaminationService _soilContaminationService;

		// Token: 0x0400000C RID: 12
		public readonly TerrainMaterialMap _terrainMaterialMap;

		// Token: 0x0400000D RID: 13
		public readonly StringBuilder _text = new StringBuilder();
	}
}
