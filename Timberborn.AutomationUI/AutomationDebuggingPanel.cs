using System;
using System.Text;
using Timberborn.Automation;
using Timberborn.Common;
using Timberborn.DebuggingUI;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200000B RID: 11
	public class AutomationDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000023E7 File Offset: 0x000005E7
		public AutomationDebuggingPanel(AutomationDebugger automationDebugger, IAutomationRunnerDebugger automationRunnerDebugger, EntitySelectionService entitySelectionService, DebuggingPanel debuggingPanel)
		{
			this._automationDebugger = automationDebugger;
			this._automationRunnerDebugger = automationRunnerDebugger;
			this._entitySelectionService = entitySelectionService;
			this._debuggingPanel = debuggingPanel;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002417 File Offset: 0x00000617
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Automation");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000242C File Offset: 0x0000062C
		public string GetText()
		{
			this._stringBuilder.Clear();
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Partitioning", this._automationDebugger.PartitioningTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Adding", this._automationDebugger.AddingTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Removing", this._automationDebugger.RemovingTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Merging", this._automationDebugger.MergingTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Planning", this._automationDebugger.PlanningTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Evaluation", this._automationDebugger.EvaluationTimeMs));
			this._stringBuilder.AppendLine(AutomationDebuggingPanel.FormatMetric("Tick evaluation", this._automationDebugger.TickEvaluationTimeMs));
			this._stringBuilder.AppendLine(string.Format("Total partitions: {0}", this._automationRunnerDebugger.PartitionCount));
			Automator automator;
			if (this._entitySelectionService.IsAnythingSelected && this._entitySelectionService.SelectedObject.TryGetComponent<Automator>(out automator))
			{
				AutomatorPartition partition = automator.Partition;
				this._stringBuilder.AppendLine();
				this._stringBuilder.AppendLine("<b>Selected</b>");
				this._stringBuilder.AppendLine((partition != null) ? string.Format("Partition: {0} ({1} nodes)", partition.DebuggingId, partition.Size) : "No partition!");
				if (automator.IsCyclicOrBlocked)
				{
					this._stringBuilder.AppendLine("IsCyclicOrBlocked");
				}
				this._stringBuilder.AppendLine(string.Format("Evaluations: {0}", automator.Evaluations));
			}
			return this._stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002613 File Offset: 0x00000813
		public static string FormatMetric(string name, AutomationDebuggerMetric metric)
		{
			return string.Format("{0}: {1:0.0000}ms (max {2:0.0000}ms)", name, metric.Total, metric.Max);
		}

		// Token: 0x04000014 RID: 20
		public readonly AutomationDebugger _automationDebugger;

		// Token: 0x04000015 RID: 21
		public readonly IAutomationRunnerDebugger _automationRunnerDebugger;

		// Token: 0x04000016 RID: 22
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000017 RID: 23
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000018 RID: 24
		public readonly StringBuilder _stringBuilder = new StringBuilder();
	}
}
