using System;
using Timberborn.BatchControl;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000014 RID: 20
	public class MechanicalHeaderBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002BED File Offset: 0x00000DED
		public VisualElement Root { get; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public MechanicalHeaderBatchControlRowItem(ILoc loc, VisualElement root, Label label, MechanicalGraph mechanicalGraph)
		{
			this.Root = root;
			this._loc = loc;
			this._label = label;
			this._mechanicalGraph = mechanicalGraph;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C70 File Offset: 0x00000E70
		public void UpdateRowItem()
		{
			string str = this._loc.T<int, int>(this._networkPowerPhrase, this._mechanicalGraph.PowerSupply, this._mechanicalGraph.PowerDemand);
			int param = Mathf.RoundToInt(this._mechanicalGraph.PowerEfficiency * 100f);
			string str2 = this._loc.T<int>(MechanicalHeaderBatchControlRowItem.EfficiencyLocKey, param) ?? "";
			this._label.text = str + " " + str2;
		}

		// Token: 0x04000031 RID: 49
		public static readonly string EfficiencyLocKey = "Mechanical.Efficiency";

		// Token: 0x04000033 RID: 51
		public readonly ILoc _loc;

		// Token: 0x04000034 RID: 52
		public readonly Label _label;

		// Token: 0x04000035 RID: 53
		public readonly MechanicalGraph _mechanicalGraph;

		// Token: 0x04000036 RID: 54
		public readonly Phrase _networkPowerPhrase = Phrase.New("Mechanical.NetworkPower").Format<int>((int value) => value.ToString()).Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower));
	}
}
