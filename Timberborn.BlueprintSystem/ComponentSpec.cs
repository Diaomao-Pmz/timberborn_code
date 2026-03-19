using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001B RID: 27
	public abstract class ComponentSpec : IEquatable<ComponentSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000039FA File Offset: 0x00001BFA
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ComponentSpec);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A06 File Offset: 0x00001C06
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003A0E File Offset: 0x00001C0E
		public Blueprint Blueprint { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003A17 File Offset: 0x00001C17
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003A1F File Offset: 0x00001C1F
		internal bool DisableBlueprintCopying { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00003A28 File Offset: 0x00001C28
		public ComponentSpec()
		{
			this.Blueprint = new Blueprint(null, null, this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A3E File Offset: 0x00001C3E
		public ComponentSpec(ComponentSpec original)
		{
			if (original.DisableBlueprintCopying)
			{
				this.Blueprint = null;
				return;
			}
			this.Blueprint = new Blueprint(original.Blueprint, original, this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A69 File Offset: 0x00001C69
		public bool HasSpec<T>()
		{
			return this.Blueprint.HasSpec<T>();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A76 File Offset: 0x00001C76
		public T GetSpec<T>()
		{
			return this.Blueprint.GetSpec<T>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A84 File Offset: 0x00001C84
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ComponentSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003AD0 File Offset: 0x00001CD0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Blueprint = ");
			builder.Append(this.Blueprint);
			return true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003AF1 File Offset: 0x00001CF1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ComponentSpec left, ComponentSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003AFD File Offset: 0x00001CFD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ComponentSpec left, ComponentSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B11 File Offset: 0x00001D11
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Blueprint>.Default.GetHashCode(this.<Blueprint>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DisableBlueprintCopying>k__BackingField);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B51 File Offset: 0x00001D51
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ComponentSpec);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003B60 File Offset: 0x00001D60
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ComponentSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Blueprint>.Default.Equals(this.<Blueprint>k__BackingField, other.<Blueprint>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DisableBlueprintCopying>k__BackingField, other.<DisableBlueprintCopying>k__BackingField));
		}
	}
}
