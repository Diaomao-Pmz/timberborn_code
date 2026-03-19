using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000027 RID: 39
	public class NetworkFragmentService
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000040BC File Offset: 0x000022BC
		public NetworkFragmentService(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000411B File Offset: 0x0000231B
		public void Initialize(Label label)
		{
			this._label = label;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004124 File Offset: 0x00002324
		public bool Update(MechanicalNode mechanicalNode)
		{
			MechanicalGraph graph = mechanicalNode.Graph;
			bool flag = graph != null && (graph.NumberOfGenerators > 0 || graph.Batteries.Count > 0);
			if (flag)
			{
				this._label.text = this._loc.T<int, int>(this._powerDemandPhrase, graph.PowerSupply, graph.PowerDemand);
			}
			this._label.ToggleDisplayStyle(flag);
			return flag;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004194 File Offset: 0x00002394
		public void Hide()
		{
			this._label.ToggleDisplayStyle(false);
		}

		// Token: 0x0400007D RID: 125
		public readonly ILoc _loc;

		// Token: 0x0400007E RID: 126
		public Label _label;

		// Token: 0x0400007F RID: 127
		public readonly Phrase _powerDemandPhrase = Phrase.New("Mechanical.NetworkPower").Format<int>((int value) => value.ToString()).Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower));
	}
}
