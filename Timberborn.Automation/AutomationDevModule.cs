using System;
using System.Text;
using Timberborn.Debugging;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.Automation
{
	// Token: 0x02000009 RID: 9
	public class AutomationDevModule : IDevModule
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000266A File Offset: 0x0000086A
		public AutomationDevModule(AutomationRunner automationRunner)
		{
			this._automationRunner = automationRunner;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002679 File Offset: 0x00000879
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Automation: Log partitions", new Action(this.LogPartitions))).Build();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026A0 File Offset: 0x000008A0
		public void LogPartitions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AutomatorPartition automatorPartition in this._automationRunner.GetPartitionsSnapshot())
			{
				stringBuilder.AppendLine("Partition " + automatorPartition.DebuggingId + ":");
				foreach (Automator automator in automatorPartition.GetPlanSnapshot())
				{
					stringBuilder.Append("  - " + AutomationDevModule.GetTemplateName(automator));
					if (!string.IsNullOrEmpty(automator.AutomatorName))
					{
						stringBuilder.Append(" - " + automator.AutomatorName);
					}
					if (automator.IsTransmitter)
					{
						stringBuilder.Append(string.Format(" [{0}]", automator.UnfinishedState));
					}
					stringBuilder.AppendLine();
				}
			}
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002798 File Offset: 0x00000998
		public static string GetTemplateName(Automator automator)
		{
			TemplateSpec templateSpec;
			if (!automator.TryGetComponent<TemplateSpec>(out templateSpec))
			{
				return automator.Name;
			}
			return templateSpec.TemplateName;
		}

		// Token: 0x04000019 RID: 25
		public readonly AutomationRunner _automationRunner;
	}
}
