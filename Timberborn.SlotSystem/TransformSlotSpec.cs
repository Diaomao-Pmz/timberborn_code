using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000022 RID: 34
	public class TransformSlotSpec : IEquatable<TransformSlotSpec>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003F7D File Offset: 0x0000217D
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TransformSlotSpec);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F89 File Offset: 0x00002189
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003F91 File Offset: 0x00002191
		[Serialize]
		public string SlotKeyword { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003F9A File Offset: 0x0000219A
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003FA2 File Offset: 0x000021A2
		[Serialize]
		public string Animation { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003FAB File Offset: 0x000021AB
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003FB3 File Offset: 0x000021B3
		[Serialize]
		public bool Inanimate { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003FBC File Offset: 0x000021BC
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003FC4 File Offset: 0x000021C4
		[Serialize]
		public bool RandomizeYRotation { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003FCD File Offset: 0x000021CD
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003FD5 File Offset: 0x000021D5
		[Serialize]
		public bool WaterSlot { get; set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00003FE0 File Offset: 0x000021E0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransformSlotSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000402C File Offset: 0x0000222C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SlotKeyword = ");
			builder.Append(this.SlotKeyword);
			builder.Append(", Animation = ");
			builder.Append(this.Animation);
			builder.Append(", Inanimate = ");
			builder.Append(this.Inanimate.ToString());
			builder.Append(", RandomizeYRotation = ");
			builder.Append(this.RandomizeYRotation.ToString());
			builder.Append(", WaterSlot = ");
			builder.Append(this.WaterSlot.ToString());
			return true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000040E6 File Offset: 0x000022E6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransformSlotSpec left, TransformSlotSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000040F2 File Offset: 0x000022F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransformSlotSpec left, TransformSlotSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004108 File Offset: 0x00002308
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SlotKeyword>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Animation>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Inanimate>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RandomizeYRotation>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WaterSlot>k__BackingField);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004198 File Offset: 0x00002398
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransformSlotSpec);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000041A8 File Offset: 0x000023A8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransformSlotSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SlotKeyword>k__BackingField, other.<SlotKeyword>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Animation>k__BackingField, other.<Animation>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Inanimate>k__BackingField, other.<Inanimate>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<RandomizeYRotation>k__BackingField, other.<RandomizeYRotation>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<WaterSlot>k__BackingField, other.<WaterSlot>k__BackingField));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004258 File Offset: 0x00002458
		[CompilerGenerated]
		protected TransformSlotSpec([Nullable(1)] TransformSlotSpec original)
		{
			this.SlotKeyword = original.<SlotKeyword>k__BackingField;
			this.Animation = original.<Animation>k__BackingField;
			this.Inanimate = original.<Inanimate>k__BackingField;
			this.RandomizeYRotation = original.<RandomizeYRotation>k__BackingField;
			this.WaterSlot = original.<WaterSlot>k__BackingField;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000020F8 File Offset: 0x000002F8
		public TransformSlotSpec()
		{
		}
	}
}
