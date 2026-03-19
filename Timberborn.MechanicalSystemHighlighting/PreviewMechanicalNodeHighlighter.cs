using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;

namespace Timberborn.MechanicalSystemHighlighting
{
	// Token: 0x0200000C RID: 12
	public class PreviewMechanicalNodeHighlighter : BaseComponent, IAwakableComponent, IPreviewSelectionListener
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027C4 File Offset: 0x000009C4
		public PreviewMechanicalNodeHighlighter(MechanicalGraphHighlightService mechanicalGraphHighlightService)
		{
			this._mechanicalGraphHighlightService = mechanicalGraphHighlightService;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027D3 File Offset: 0x000009D3
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027E1 File Offset: 0x000009E1
		public void OnPreviewSelect()
		{
			this._mechanicalGraphHighlightService.AddNodeToHighlight(this._mechanicalNode);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027F4 File Offset: 0x000009F4
		public void OnPreviewUnselect()
		{
			this._mechanicalGraphHighlightService.RemoveAllNodesFromHighlight();
		}

		// Token: 0x04000016 RID: 22
		public readonly MechanicalGraphHighlightService _mechanicalGraphHighlightService;

		// Token: 0x04000017 RID: 23
		public MechanicalNode _mechanicalNode;
	}
}
