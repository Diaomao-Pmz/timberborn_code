using System;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000022 RID: 34
	public class MechanicalNodeTextFormatter
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public MechanicalNodeTextFormatter(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D7C File Offset: 0x00001F7C
		public string FormatGeneratorText(MechanicalNode mechanicalNode)
		{
			return this._loc.T<int>(this._powerOutputPhrase, mechanicalNode.Actuals.PowerOutput);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D9C File Offset: 0x00001F9C
		public string FormatConsumerText(MechanicalNode mechanicalNode)
		{
			float num = mechanicalNode.Active ? mechanicalNode.PowerEfficiency : 0f;
			int param = Mathf.RoundToInt(num * 100f);
			int val = Mathf.RoundToInt(num * (float)mechanicalNode.Actuals.PowerInput);
			int param2 = Math.Min(mechanicalNode.Actuals.PowerInput, val);
			int powerInput = mechanicalNode.Actuals.PowerInput;
			return this._loc.T<int, int, int>(this._powerInputPhrase, param2, powerInput, param);
		}

		// Token: 0x04000073 RID: 115
		public readonly ILoc _loc;

		// Token: 0x04000074 RID: 116
		public readonly Phrase _powerOutputPhrase = Phrase.New("Mechanical.PowerOutput").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower));

		// Token: 0x04000075 RID: 117
		public readonly Phrase _powerInputPhrase = Phrase.New("Mechanical.PowerInputMaximum").Format<int>((int value) => value.ToString()).Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower)).Format<int>((int value) => value.ToString());
	}
}
