using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200001F RID: 31
	public class KeyBindingSpec : ComponentSpec, IEquatable<KeyBindingSpec>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003E3C File Offset: 0x0000203C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(KeyBindingSpec);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003E48 File Offset: 0x00002048
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003E50 File Offset: 0x00002050
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003E59 File Offset: 0x00002059
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003E61 File Offset: 0x00002061
		[Serialize]
		public string GroupId { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003E6A File Offset: 0x0000206A
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003E72 File Offset: 0x00002072
		[Serialize]
		public string LocKey { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003E7B File Offset: 0x0000207B
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003E83 File Offset: 0x00002083
		[Serialize]
		public int Order { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003E8C File Offset: 0x0000208C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003E94 File Offset: 0x00002094
		[Serialize]
		public bool AllowOtherModifiers { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003E9D File Offset: 0x0000209D
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003EA5 File Offset: 0x000020A5
		[Serialize]
		public bool DevModeOnly { get; set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x00003EB0 File Offset: 0x000020B0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("KeyBindingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003EFC File Offset: 0x000020FC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", GroupId = ");
			builder.Append(this.GroupId);
			builder.Append(", LocKey = ");
			builder.Append(this.LocKey);
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", AllowOtherModifiers = ");
			builder.Append(this.AllowOtherModifiers.ToString());
			builder.Append(", DevModeOnly = ");
			builder.Append(this.DevModeOnly.ToString());
			return true;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FDF File Offset: 0x000021DF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(KeyBindingSpec left, KeyBindingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003FEB File Offset: 0x000021EB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(KeyBindingSpec left, KeyBindingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004000 File Offset: 0x00002200
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LocKey>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AllowOtherModifiers>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DevModeOnly>k__BackingField);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000409D File Offset: 0x0000229D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as KeyBindingSpec);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000040AC File Offset: 0x000022AC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(KeyBindingSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GroupId>k__BackingField, other.<GroupId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LocKey>k__BackingField, other.<LocKey>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AllowOtherModifiers>k__BackingField, other.<AllowOtherModifiers>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DevModeOnly>k__BackingField, other.<DevModeOnly>k__BackingField));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004168 File Offset: 0x00002368
		[CompilerGenerated]
		protected KeyBindingSpec([Nullable(1)] KeyBindingSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.GroupId = original.<GroupId>k__BackingField;
			this.LocKey = original.<LocKey>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.AllowOtherModifiers = original.<AllowOtherModifiers>k__BackingField;
			this.DevModeOnly = original.<DevModeOnly>k__BackingField;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002500 File Offset: 0x00000700
		public KeyBindingSpec()
		{
		}
	}
}
