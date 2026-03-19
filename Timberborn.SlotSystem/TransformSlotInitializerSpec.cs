using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000021 RID: 33
	public class TransformSlotInitializerSpec : ComponentSpec, IEquatable<TransformSlotInitializerSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003E37 File Offset: 0x00002037
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TransformSlotInitializerSpec);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003E43 File Offset: 0x00002043
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003E4B File Offset: 0x0000204B
		[Serialize]
		public ImmutableArray<TransformSlotSpec> Slots { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00003E54 File Offset: 0x00002054
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransformSlotInitializerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003EA0 File Offset: 0x000020A0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Slots = ");
			builder.Append(this.Slots.ToString());
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003EEA File Offset: 0x000020EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransformSlotInitializerSpec left, TransformSlotInitializerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003EF6 File Offset: 0x000020F6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransformSlotInitializerSpec left, TransformSlotInitializerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003F0A File Offset: 0x0000210A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<TransformSlotSpec>>.Default.GetHashCode(this.<Slots>k__BackingField);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F29 File Offset: 0x00002129
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransformSlotInitializerSpec);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003F37 File Offset: 0x00002137
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransformSlotInitializerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<TransformSlotSpec>>.Default.Equals(this.<Slots>k__BackingField, other.<Slots>k__BackingField));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F68 File Offset: 0x00002168
		[CompilerGenerated]
		protected TransformSlotInitializerSpec([Nullable(1)] TransformSlotInitializerSpec original) : base(original)
		{
			this.Slots = original.<Slots>k__BackingField;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000238C File Offset: 0x0000058C
		public TransformSlotInitializerSpec()
		{
		}
	}
}
