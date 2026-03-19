using System;
using Timberborn.DebuggingUI;
using Timberborn.Diagnostics;
using Timberborn.SingletonSystem;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x0200000A RID: 10
	public class MeshMetricsDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023B4 File Offset: 0x000005B4
		public MeshMetricsDebuggingPanel(DebuggingPanel debuggingPanel, SelectedMeshMetrics selectedMeshMetrics)
		{
			this._debuggingPanel = debuggingPanel;
			this._selectedMeshMetrics = selectedMeshMetrics;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023CA File Offset: 0x000005CA
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Mesh metrics");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023E0 File Offset: 0x000005E0
		public string GetText()
		{
			MeshMetrics meshMetrics = this._selectedMeshMetrics.MeshMetrics;
			if (meshMetrics != null)
			{
				return string.Format("Verts: {0:N0}", meshMetrics.NumberOfVertices) + string.Format("\nTris: {0:N0}", meshMetrics.NumberOfTriangles) + string.Format("\nTris/tile: {0:N0}", meshMetrics.NumberOfTrianglesPerTile) + string.Format("\nSubmeshes: {0:N0}", meshMetrics.NumberOfSubmeshes);
			}
			return "Nothing selected";
		}

		// Token: 0x04000010 RID: 16
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000011 RID: 17
		public readonly SelectedMeshMetrics _selectedMeshMetrics;
	}
}
