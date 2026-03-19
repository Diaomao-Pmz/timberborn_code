using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.ZiplineSystem;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000015 RID: 21
	public class ZiplineTowerPreview : BaseComponent, IAwakableComponent, IPostPlacementChangeListener, IPreviewSelectionListener
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000347F File Offset: 0x0000167F
		public ZiplineTowerPreview(ConnectionCandidates connectionCandidates)
		{
			this._connectionCandidates = connectionCandidates;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000348E File Offset: 0x0000168E
		public void Awake()
		{
			this._ziplineTower = base.GetComponent<ZiplineTower>();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000349C File Offset: 0x0000169C
		public void OnPostPlacementChanged()
		{
			if (this._isSelected)
			{
				this._connectionCandidates.UpdateCandidates();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034B1 File Offset: 0x000016B1
		public void OnPreviewSelect()
		{
			if (!this._isSelected)
			{
				this._connectionCandidates.EnableAndDrawMarkers(this._ziplineTower);
				this._isSelected = true;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034D3 File Offset: 0x000016D3
		public void OnPreviewUnselect()
		{
			this._connectionCandidates.Disable();
			this._isSelected = false;
		}

		// Token: 0x04000065 RID: 101
		public readonly ConnectionCandidates _connectionCandidates;

		// Token: 0x04000066 RID: 102
		public ZiplineTower _ziplineTower;

		// Token: 0x04000067 RID: 103
		public bool _isSelected;
	}
}
