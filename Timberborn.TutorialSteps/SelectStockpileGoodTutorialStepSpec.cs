using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200004B RID: 75
	public class SelectStockpileGoodTutorialStepSpec : ComponentSpec, IEquatable<SelectStockpileGoodTutorialStepSpec>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000063A1 File Offset: 0x000045A1
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SelectStockpileGoodTutorialStepSpec);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000063AD File Offset: 0x000045AD
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000063B5 File Offset: 0x000045B5
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000063BE File Offset: 0x000045BE
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000063C6 File Offset: 0x000045C6
		[Serialize]
		public int RequiredAmount { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000063CF File Offset: 0x000045CF
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000063D7 File Offset: 0x000045D7
		[Serialize]
		public string GoodId { get; set; }

		// Token: 0x06000201 RID: 513 RVA: 0x000063E0 File Offset: 0x000045E0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SelectStockpileGoodTutorialStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000642C File Offset: 0x0000462C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateName = ");
			builder.Append(this.TemplateName);
			builder.Append(", RequiredAmount = ");
			builder.Append(this.RequiredAmount.ToString());
			builder.Append(", GoodId = ");
			builder.Append(this.GoodId);
			return true;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000064A8 File Offset: 0x000046A8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SelectStockpileGoodTutorialStepSpec left, SelectStockpileGoodTutorialStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000064B4 File Offset: 0x000046B4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SelectStockpileGoodTutorialStepSpec left, SelectStockpileGoodTutorialStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000064C8 File Offset: 0x000046C8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RequiredAmount>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodId>k__BackingField);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006520 File Offset: 0x00004720
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SelectStockpileGoodTutorialStepSpec);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006530 File Offset: 0x00004730
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SelectStockpileGoodTutorialStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RequiredAmount>k__BackingField, other.<RequiredAmount>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodId>k__BackingField, other.<GoodId>k__BackingField));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000659C File Offset: 0x0000479C
		[CompilerGenerated]
		protected SelectStockpileGoodTutorialStepSpec([Nullable(1)] SelectStockpileGoodTutorialStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
			this.RequiredAmount = original.<RequiredAmount>k__BackingField;
			this.GoodId = original.<GoodId>k__BackingField;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000239D File Offset: 0x0000059D
		public SelectStockpileGoodTutorialStepSpec()
		{
		}
	}
}
