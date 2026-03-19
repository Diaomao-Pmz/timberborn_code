using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000019 RID: 25
	public class MechanicalNodeDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000307E File Offset: 0x0000127E
		public MechanicalNodeDescriber(ILoc loc, DescribedAmountFactory describedAmountFactory, ProductionItemFactory productionItemFactory)
		{
			this._loc = loc;
			this._describedAmountFactory = describedAmountFactory;
			this._productionItemFactory = productionItemFactory;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000309B File Offset: 0x0000129B
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNodeSpec = base.GetComponent<MechanicalNodeSpec>();
			this._mechanicalNodeDescriptionSpec = base.GetComponent<MechanicalNodeDescriptionSpec>();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030C1 File Offset: 0x000012C1
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._mechanicalNodeSpec != null && !this._mechanicalNode.Enabled)
			{
				int powerOutput = this._mechanicalNodeSpec.PowerOutput;
				int powerInput = this._mechanicalNodeSpec.PowerInput;
				if (powerOutput > 0)
				{
					string tooltipText = this.GetTooltipText(powerOutput, MechanicalNodeDescriber.PowerOutputLocKey);
					string amount = this.FormatPower(powerOutput) ?? "";
					VisualElement output = this._describedAmountFactory.CreatePlain(MechanicalNodeDescriber.PowerClass, amount, tooltipText);
					VisualElement content = this._productionItemFactory.CreateOutput(output);
					yield return EntityDescription.CreateOutputSection(content, 2147483646);
				}
				if (powerInput > 0)
				{
					string tooltipText2 = this.GetTooltipText(powerInput, MechanicalNodeDescriber.PowerInputKey);
					string amount2 = this.FormatPower(powerInput) ?? "";
					VisualElement content2 = this._describedAmountFactory.CreatePlain(MechanicalNodeDescriber.PowerClass, amount2, tooltipText2);
					yield return EntityDescription.CreateMiddleSection(content2, 4);
				}
			}
			yield break;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030D1 File Offset: 0x000012D1
		public string GetTooltipText(int power, string typeLocKey)
		{
			return this._loc.T<string>(typeLocKey, this.FormatPower(power));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030E8 File Offset: 0x000012E8
		public string FormatPower(int power)
		{
			string key = (this._mechanicalNodeDescriptionSpec != null) ? this._mechanicalNodeDescriptionSpec.AlternativePowerUnitLocKey : MechanicalNodeDescriber.PowerUnitLocKey;
			return this._loc.T<int>(key, power);
		}

		// Token: 0x04000043 RID: 67
		public static readonly string PowerClass = "described-amount--power";

		// Token: 0x04000044 RID: 68
		public static readonly string PowerInputKey = "Mechanical.PowerInput";

		// Token: 0x04000045 RID: 69
		public static readonly string PowerOutputLocKey = "Mechanical.PowerOutput";

		// Token: 0x04000046 RID: 70
		public static readonly string PowerUnitLocKey = "Unit.HorsePower.NumberAndUnit";

		// Token: 0x04000047 RID: 71
		public readonly ILoc _loc;

		// Token: 0x04000048 RID: 72
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x04000049 RID: 73
		public readonly ProductionItemFactory _productionItemFactory;

		// Token: 0x0400004A RID: 74
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400004B RID: 75
		public MechanicalNodeSpec _mechanicalNodeSpec;

		// Token: 0x0400004C RID: 76
		public MechanicalNodeDescriptionSpec _mechanicalNodeDescriptionSpec;
	}
}
