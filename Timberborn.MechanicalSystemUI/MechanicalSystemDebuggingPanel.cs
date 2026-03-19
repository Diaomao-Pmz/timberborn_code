using System;
using System.Text;
using Timberborn.DebuggingUI;
using Timberborn.MechanicalSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000024 RID: 36
	public class MechanicalSystemDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003E1C File Offset: 0x0000201C
		public MechanicalSystemDebuggingPanel(DebuggingPanel debuggingPanel, EntitySelectionService entitySelectionService)
		{
			this._debuggingPanel = debuggingPanel;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003E32 File Offset: 0x00002032
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Mechanical system");
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E48 File Offset: 0x00002048
		public string GetText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			MechanicalNode mechanicalNode;
			if (this._entitySelectionService.IsAnythingSelected && this._entitySelectionService.SelectedObject.TryGetComponent<MechanicalNode>(out mechanicalNode))
			{
				MechanicalGraph graph = mechanicalNode.Graph;
				if (graph != null)
				{
					stringBuilder.AppendLine(string.Format("Graph power supply: {0} hp", graph.PowerSupply));
					stringBuilder.AppendLine(string.Format("Graph power demand: {0} hp", graph.PowerDemand));
					stringBuilder.AppendLine(string.Format("Graph battery charge: {0} hph", graph.BatteryCharge));
					stringBuilder.AppendLine(string.Format("Graph battery capacity: {0} hph", graph.BatteryCapacity));
					stringBuilder.AppendLine(string.Format("Graph power efficiency: {0:0.0}%", graph.PowerEfficiency * 100f));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000079 RID: 121
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x0400007A RID: 122
		public readonly EntitySelectionService _entitySelectionService;
	}
}
