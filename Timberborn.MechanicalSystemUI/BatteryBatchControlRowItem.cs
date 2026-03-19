using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000007 RID: 7
	public class BatteryBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public VisualElement Root { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public BatteryBatchControlRowItem(ILoc loc, VisualElement root, ProgressBar progressBar, Label chargeLabel, MechanicalNode mechanicalNode)
		{
			this._loc = loc;
			this.Root = root;
			this._progressBar = progressBar;
			this._chargeLabel = chargeLabel;
			this._mechanicalNode = mechanicalNode;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002188 File Offset: 0x00000388
		public void UpdateRowItem()
		{
			this._chargeLabel.text = this._loc.T<int, int>(this._chargePhrase, this._mechanicalNode.NominalBatteryCharge, this._mechanicalNode.NominalBatteryCapacity);
			this._progressBar.SetProgress(this._mechanicalNode.NominalBatteryChargeLevel);
		}

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public readonly ProgressBar _progressBar;

		// Token: 0x0400000B RID: 11
		public readonly Label _chargeLabel;

		// Token: 0x0400000C RID: 12
		public readonly MechanicalNode _mechanicalNode;

		// Token: 0x0400000D RID: 13
		public readonly Phrase _chargePhrase = Phrase.New("Mechanical.BatteryCharge").Format<int>((int value) => value.ToString()).Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPowerCapacity));
	}
}
