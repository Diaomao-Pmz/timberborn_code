using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class SelectionColorsSpec : ComponentSpec, IEquatable<SelectionColorsSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003743 File Offset: 0x00001943
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SelectionColorsSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003757 File Offset: 0x00001957
		[Serialize]
		public Color EntitySelection { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003760 File Offset: 0x00001960
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003768 File Offset: 0x00001968
		[Serialize]
		public Color SelectionToolHighlight { get; set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003774 File Offset: 0x00001974
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SelectionColorsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037C0 File Offset: 0x000019C0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("EntitySelection = ");
			builder.Append(this.EntitySelection.ToString());
			builder.Append(", SelectionToolHighlight = ");
			builder.Append(this.SelectionToolHighlight.ToString());
			return true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003831 File Offset: 0x00001A31
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SelectionColorsSpec left, SelectionColorsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000383D File Offset: 0x00001A3D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SelectionColorsSpec left, SelectionColorsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003851 File Offset: 0x00001A51
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<EntitySelection>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SelectionToolHighlight>k__BackingField);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003887 File Offset: 0x00001A87
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SelectionColorsSpec);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000239E File Offset: 0x0000059E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003898 File Offset: 0x00001A98
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SelectionColorsSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<EntitySelection>k__BackingField, other.<EntitySelection>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SelectionToolHighlight>k__BackingField, other.<SelectionToolHighlight>k__BackingField));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000038EC File Offset: 0x00001AEC
		[CompilerGenerated]
		protected SelectionColorsSpec(SelectionColorsSpec original) : base(original)
		{
			this.EntitySelection = original.<EntitySelection>k__BackingField;
			this.SelectionToolHighlight = original.<SelectionToolHighlight>k__BackingField;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002441 File Offset: 0x00000641
		public SelectionColorsSpec()
		{
		}
	}
}
