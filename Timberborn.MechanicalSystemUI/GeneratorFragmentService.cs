using System;
using Timberborn.CoreUI;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200000D RID: 13
	public class GeneratorFragmentService
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000252B File Offset: 0x0000072B
		public GeneratorFragmentService(MechanicalNodeTextFormatter mechanicalNodeTextFormatter)
		{
			this._mechanicalNodeTextFormatter = mechanicalNodeTextFormatter;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000253A File Offset: 0x0000073A
		public void Initialize(Label label)
		{
			this._label = label;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002543 File Offset: 0x00000743
		public bool Update(MechanicalNode mechanicalNode)
		{
			if (mechanicalNode.IsGenerator)
			{
				this._label.text = this._mechanicalNodeTextFormatter.FormatGeneratorText(mechanicalNode);
			}
			this._label.ToggleDisplayStyle(mechanicalNode.IsGenerator);
			return mechanicalNode.IsGenerator;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000257B File Offset: 0x0000077B
		public void Hide()
		{
			this._label.ToggleDisplayStyle(false);
		}

		// Token: 0x0400001F RID: 31
		public readonly MechanicalNodeTextFormatter _mechanicalNodeTextFormatter;

		// Token: 0x04000020 RID: 32
		public Label _label;
	}
}
