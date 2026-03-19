using System;
using Timberborn.CoreUI;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200000C RID: 12
	public class ConsumerFragmentService
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000024C3 File Offset: 0x000006C3
		public ConsumerFragmentService(MechanicalNodeTextFormatter mechanicalNodeTextFormatter)
		{
			this._mechanicalNodeTextFormatter = mechanicalNodeTextFormatter;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D2 File Offset: 0x000006D2
		public void Initialize(Label label)
		{
			this._label = label;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024DB File Offset: 0x000006DB
		public bool Update(MechanicalNode mechanicalNode)
		{
			if (mechanicalNode.IsConsumer)
			{
				this.UpdateText(this._label, mechanicalNode);
			}
			this._label.ToggleDisplayStyle(mechanicalNode.IsConsumer);
			return mechanicalNode.IsConsumer;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002509 File Offset: 0x00000709
		public void Hide()
		{
			this._label.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002517 File Offset: 0x00000717
		public void UpdateText(Label label, MechanicalNode mechanicalNode)
		{
			label.text = this._mechanicalNodeTextFormatter.FormatConsumerText(mechanicalNode);
		}

		// Token: 0x0400001D RID: 29
		public readonly MechanicalNodeTextFormatter _mechanicalNodeTextFormatter;

		// Token: 0x0400001E RID: 30
		public Label _label;
	}
}
