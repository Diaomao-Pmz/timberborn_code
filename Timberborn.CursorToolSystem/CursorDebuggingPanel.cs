using System;
using Timberborn.Common;
using Timberborn.DebuggingUI;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x0200000A RID: 10
	public class CursorDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000278D File Offset: 0x0000098D
		public CursorDebuggingPanel(DebuggingPanel debuggingPanel, CursorDebugger cursorDebugger, MapIndexService mapIndexService)
		{
			this._debuggingPanel = debuggingPanel;
			this._cursorDebugger = cursorDebugger;
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027AA File Offset: 0x000009AA
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Cursor");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027C0 File Offset: 0x000009C0
		public string GetText()
		{
			if (this._cursorDebugger.Active)
			{
				Vector3Int coordinates = this._cursorDebugger.Coordinates;
				Vector3 position = this._cursorDebugger.Position;
				return string.Format("Block coordinates: {0}", coordinates) + string.Format("\nIntersection position: {0}", position) + string.Format("\nMap index: {0}", this._mapIndexService.CellToIndex(coordinates.XY()));
			}
			return null;
		}

		// Token: 0x04000028 RID: 40
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000029 RID: 41
		public readonly CursorDebugger _cursorDebugger;

		// Token: 0x0400002A RID: 42
		public readonly MapIndexService _mapIndexService;
	}
}
