using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000054 RID: 84
	public class PlaceableBlockObjectSpec : ComponentSpec, IEquatable<PlaceableBlockObjectSpec>
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000684B File Offset: 0x00004A4B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlaceableBlockObjectSpec);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006857 File Offset: 0x00004A57
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000685F File Offset: 0x00004A5F
		[Serialize]
		public string ToolGroupId { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006868 File Offset: 0x00004A68
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00006870 File Offset: 0x00004A70
		[Serialize]
		public int ToolOrder { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006879 File Offset: 0x00004A79
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00006881 File Offset: 0x00004A81
		[Serialize]
		public ToolShapes ToolShape { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000688A File Offset: 0x00004A8A
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00006892 File Offset: 0x00004A92
		[Serialize]
		public BlockObjectLayout Layout { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000689B File Offset: 0x00004A9B
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000068A3 File Offset: 0x00004AA3
		[Serialize]
		public CustomPivotSpec CustomPivot { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000068AC File Offset: 0x00004AAC
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000068B4 File Offset: 0x00004AB4
		[Serialize]
		public bool CanBeAttachedToTerrainSide { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000068BD File Offset: 0x00004ABD
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000068C5 File Offset: 0x00004AC5
		[Serialize]
		public bool DevModeTool { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000068CE File Offset: 0x00004ACE
		public bool UsableWithCurrentFeatureToggles
		{
			get
			{
				return base.GetSpec<TemplateSpec>().UsableWithCurrentFeatureToggles;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000068DC File Offset: 0x00004ADC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaceableBlockObjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006928 File Offset: 0x00004B28
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ToolGroupId = ");
			builder.Append(this.ToolGroupId);
			builder.Append(", ToolOrder = ");
			builder.Append(this.ToolOrder.ToString());
			builder.Append(", ToolShape = ");
			builder.Append(this.ToolShape.ToString());
			builder.Append(", Layout = ");
			builder.Append(this.Layout.ToString());
			builder.Append(", CustomPivot = ");
			builder.Append(this.CustomPivot);
			builder.Append(", CanBeAttachedToTerrainSide = ");
			builder.Append(this.CanBeAttachedToTerrainSide.ToString());
			builder.Append(", DevModeTool = ");
			builder.Append(this.DevModeTool.ToString());
			builder.Append(", UsableWithCurrentFeatureToggles = ");
			builder.Append(this.UsableWithCurrentFeatureToggles.ToString());
			return true;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006A67 File Offset: 0x00004C67
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaceableBlockObjectSpec left, PlaceableBlockObjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006A73 File Offset: 0x00004C73
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaceableBlockObjectSpec left, PlaceableBlockObjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006A88 File Offset: 0x00004C88
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ToolGroupId>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ToolOrder>k__BackingField)) * -1521134295 + EqualityComparer<ToolShapes>.Default.GetHashCode(this.<ToolShape>k__BackingField)) * -1521134295 + EqualityComparer<BlockObjectLayout>.Default.GetHashCode(this.<Layout>k__BackingField)) * -1521134295 + EqualityComparer<CustomPivotSpec>.Default.GetHashCode(this.<CustomPivot>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CanBeAttachedToTerrainSide>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DevModeTool>k__BackingField);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006B3C File Offset: 0x00004D3C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaceableBlockObjectSpec);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00003CFB File Offset: 0x00001EFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006B4C File Offset: 0x00004D4C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaceableBlockObjectSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ToolGroupId>k__BackingField, other.<ToolGroupId>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ToolOrder>k__BackingField, other.<ToolOrder>k__BackingField) && EqualityComparer<ToolShapes>.Default.Equals(this.<ToolShape>k__BackingField, other.<ToolShape>k__BackingField) && EqualityComparer<BlockObjectLayout>.Default.Equals(this.<Layout>k__BackingField, other.<Layout>k__BackingField) && EqualityComparer<CustomPivotSpec>.Default.Equals(this.<CustomPivot>k__BackingField, other.<CustomPivot>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CanBeAttachedToTerrainSide>k__BackingField, other.<CanBeAttachedToTerrainSide>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DevModeTool>k__BackingField, other.<DevModeTool>k__BackingField));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006C24 File Offset: 0x00004E24
		[CompilerGenerated]
		protected PlaceableBlockObjectSpec([Nullable(1)] PlaceableBlockObjectSpec original) : base(original)
		{
			this.ToolGroupId = original.<ToolGroupId>k__BackingField;
			this.ToolOrder = original.<ToolOrder>k__BackingField;
			this.ToolShape = original.<ToolShape>k__BackingField;
			this.Layout = original.<Layout>k__BackingField;
			this.CustomPivot = original.<CustomPivot>k__BackingField;
			this.CanBeAttachedToTerrainSide = original.<CanBeAttachedToTerrainSide>k__BackingField;
			this.DevModeTool = original.<DevModeTool>k__BackingField;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00003E1C File Offset: 0x0000201C
		public PlaceableBlockObjectSpec()
		{
		}
	}
}
