using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000019 RID: 25
	public class TransmitterPickerToolHighlighter : ILoadableSingleton
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000347C File Offset: 0x0000167C
		public TransmitterPickerToolHighlighter(Highlighter highlighter, AutomatorRegistry automatorRegistry, ISpecService specService)
		{
			this._highlighter = highlighter;
			this._automatorRegistry = automatorRegistry;
			this._specService = specService;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000349C File Offset: 0x0000169C
		public void Load()
		{
			this._selectionColor = this._specService.GetSingleSpec<SelectionColorsSpec>().EntitySelection;
			TransmitterPickerColorsSpec singleSpec = this._specService.GetSingleSpec<TransmitterPickerColorsSpec>();
			this._transmitterColor = singleSpec.TransmitterColor;
			this._unfinishedTransmitterColor = singleSpec.UnfinishedTransmitterColor;
			this._hoveredTransmitterColor = singleSpec.HoveredTransmitterColor;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034F0 File Offset: 0x000016F0
		public void Highlight(BaseComponent owner)
		{
			this._owner = owner;
			this._highlighter.UnhighlightAllPrimary();
			foreach (Automator transmitter in this._automatorRegistry.Transmitters)
			{
				this.HighlightTransmitter(transmitter);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003560 File Offset: 0x00001760
		public void UpdateHover(Automator hoveredTransmitter)
		{
			if (hoveredTransmitter != this._hoveredTransmitter)
			{
				if (this._hoveredTransmitter != null)
				{
					this.HighlightTransmitter(this._hoveredTransmitter);
				}
				this._hoveredTransmitter = hoveredTransmitter;
				if (this._hoveredTransmitter != null)
				{
					this._highlighter.HighlightPrimary(this._hoveredTransmitter, this._hoveredTransmitterColor);
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000035B0 File Offset: 0x000017B0
		public void Clear()
		{
			this._highlighter.UnhighlightAllPrimary();
			this._owner = null;
			this._hoveredTransmitter = null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000035CC File Offset: 0x000017CC
		public void HighlightTransmitter(Automator transmitter)
		{
			Color color = (transmitter.GameObject == this._owner.GameObject) ? this._selectionColor : (transmitter.GetComponent<BlockObject>().IsFinished ? this._transmitterColor : this._unfinishedTransmitterColor);
			this._highlighter.HighlightPrimary(transmitter, color);
		}

		// Token: 0x04000060 RID: 96
		public readonly Highlighter _highlighter;

		// Token: 0x04000061 RID: 97
		public readonly AutomatorRegistry _automatorRegistry;

		// Token: 0x04000062 RID: 98
		public readonly ISpecService _specService;

		// Token: 0x04000063 RID: 99
		public Color _selectionColor;

		// Token: 0x04000064 RID: 100
		public Color _transmitterColor;

		// Token: 0x04000065 RID: 101
		public Color _unfinishedTransmitterColor;

		// Token: 0x04000066 RID: 102
		public Color _hoveredTransmitterColor;

		// Token: 0x04000067 RID: 103
		public BaseComponent _owner;

		// Token: 0x04000068 RID: 104
		public Automator _hoveredTransmitter;
	}
}
