using System;
using Timberborn.BatchControl;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000010 RID: 16
	public class MechanicalBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002629 File Offset: 0x00000829
		public VisualElement Root { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002631 File Offset: 0x00000831
		public MechanicalBatchControlRowItem(MechanicalNodeTextFormatter mechanicalNodeTextFormatter, VisualElement root, Label label, MechanicalNode mechanicalNode)
		{
			this.Root = root;
			this._mechanicalNodeTextFormatter = mechanicalNodeTextFormatter;
			this._label = label;
			this._mechanicalNode = mechanicalNode;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002658 File Offset: 0x00000858
		public void UpdateRowItem()
		{
			if (this._mechanicalNode.IsGenerator)
			{
				this._label.text = this._mechanicalNodeTextFormatter.FormatGeneratorText(this._mechanicalNode);
				return;
			}
			if (this._mechanicalNode.IsConsumer)
			{
				this._label.text = this._mechanicalNodeTextFormatter.FormatConsumerText(this._mechanicalNode);
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly MechanicalNodeTextFormatter _mechanicalNodeTextFormatter;

		// Token: 0x04000023 RID: 35
		public readonly Label _label;

		// Token: 0x04000024 RID: 36
		public readonly MechanicalNode _mechanicalNode;
	}
}
