using System;
using System.Text;
using Timberborn.Common;
using Timberborn.CursorToolSystem;
using Timberborn.Debugging;
using Timberborn.DebuggingUI;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSystemUI
{
	// Token: 0x02000004 RID: 4
	public class WaterColumnDebuggingPanel : ILoadableSingleton, ITickableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public WaterColumnDebuggingPanel(DebuggingPanel debuggingPanel, CursorDebugger cursorDebugger, INonThreadSafeWaterService nonThreadSafeWaterService, MapIndexService mapIndexService, DebugModeManager debugModeManager, EventBus eventBus)
		{
			this._debuggingPanel = debuggingPanel;
			this._cursorDebugger = cursorDebugger;
			this._nonThreadSafeWaterService = nonThreadSafeWaterService;
			this._mapIndexService = mapIndexService;
			this._debugModeManager = debugModeManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FE File Offset: 0x000002FE
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Water columns");
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211D File Offset: 0x0000031D
		public void Tick()
		{
			if (this._debugModeManager.Enabled)
			{
				this._dataUpdated = true;
				this._nonThreadSafeWaterService.UpdateOutflowsData();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213E File Offset: 0x0000033E
		[OnEvent]
		public void OnDebugModeToggled(DebugModeToggledEvent debugModeToggledEvent)
		{
			this._dataUpdated = false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
		public string GetText()
		{
			if (this._cursorDebugger.Active)
			{
				this._text.Clear();
				Vector2Int coordinates = this._cursorDebugger.Coordinates.XY();
				int num = this._mapIndexService.CellToIndex(coordinates);
				this._text.AppendLine(string.Format("Coords: {0}, {1} (index: {2})", coordinates.x, coordinates.y, num));
				for (int i = 0; i < this._nonThreadSafeWaterService.GetColumnCount(num); i++)
				{
					int index3D = num + i * this._mapIndexService.VerticalStride;
					ReadOnlyWaterColumn columnByIndex = this._nonThreadSafeWaterService.GetColumnByIndex(index3D);
					this._text.AppendLine(string.Format("Column {0} - {1}:", columnByIndex.Floor, columnByIndex.Ceiling));
					this._text.AppendLine(string.Format(" - Water depth: {0}", columnByIndex.WaterDepth));
					this._text.AppendLine(string.Format(" - Water height: {0}", (float)columnByIndex.Floor + columnByIndex.WaterDepth));
					this._text.AppendLine(string.Format(" - Contamination: {0:F5}", columnByIndex.Contamination));
					this._text.AppendLine(string.Format(" - Overflow: {0}", columnByIndex.Overflow));
					this._text.AppendLine(" - Outflows:");
					this.AppendOutflows(index3D);
				}
			}
			return this._text.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E0 File Offset: 0x000004E0
		public void AppendOutflows(int index3D)
		{
			if (!this._dataUpdated)
			{
				this._text.AppendLine("   - Waiting for tick...");
				return;
			}
			ReadOnlyColumnOutflows outflows = this._nonThreadSafeWaterService.ColumnOutflows(index3D);
			if (outflows.Outflows != null)
			{
				this.AppendAllOutflows(outflows);
				return;
			}
			string str = string.Concat(new string[]
			{
				"B=",
				WaterColumnDebuggingPanel.FormatOutflow(outflows.BottomFlow),
				", L=",
				WaterColumnDebuggingPanel.FormatOutflow(outflows.LeftFlow),
				", T=",
				WaterColumnDebuggingPanel.FormatOutflow(outflows.TopFlow),
				", R=",
				WaterColumnDebuggingPanel.FormatOutflow(outflows.RightFlow)
			});
			this._text.AppendLine("  - " + str);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023A4 File Offset: 0x000005A4
		public void AppendAllOutflows(ReadOnlyColumnOutflows outflows)
		{
			if (outflows.BottomFlow.Flow > 0f)
			{
				this.AppendFormattedOutflow(outflows.BottomFlow);
			}
			if (outflows.LeftFlow.Flow > 0f)
			{
				this.AppendFormattedOutflow(outflows.LeftFlow);
			}
			if (outflows.TopFlow.Flow > 0f)
			{
				this.AppendFormattedOutflow(outflows.TopFlow);
			}
			if (outflows.RightFlow.Flow > 0f)
			{
				this.AppendFormattedOutflow(outflows.RightFlow);
			}
			foreach (TargetedFlow outflow in outflows.Outflows)
			{
				this.AppendFormattedOutflow(outflow);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002478 File Offset: 0x00000678
		public void AppendFormattedOutflow(TargetedFlow outflow)
		{
			ReadOnlyWaterColumn columnByIndex = this._nonThreadSafeWaterService.GetColumnByIndex(outflow.Index3D);
			this._text.AppendLine(string.Format("  - {0}: {1}", columnByIndex.Floor, WaterColumnDebuggingPanel.FormatOutflow(outflow)));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024BF File Offset: 0x000006BF
		public static string FormatOutflow(TargetedFlow outflow)
		{
			return string.Format("{0:0.000}", outflow.Flow);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000007 RID: 7
		public readonly CursorDebugger _cursorDebugger;

		// Token: 0x04000008 RID: 8
		public readonly INonThreadSafeWaterService _nonThreadSafeWaterService;

		// Token: 0x04000009 RID: 9
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400000A RID: 10
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly StringBuilder _text = new StringBuilder();

		// Token: 0x0400000D RID: 13
		public bool _dataUpdated;
	}
}
