using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200001A RID: 26
	public class KeyBindingGroupSpec : ComponentSpec, IEquatable<KeyBindingGroupSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000038C4 File Offset: 0x00001AC4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(KeyBindingGroupSpec);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000038D0 File Offset: 0x00001AD0
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000038D8 File Offset: 0x00001AD8
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000038E1 File Offset: 0x00001AE1
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000038E9 File Offset: 0x00001AE9
		[Serialize]
		public int Order { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000038F2 File Offset: 0x00001AF2
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000038FA File Offset: 0x00001AFA
		[Serialize("LocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003903 File Offset: 0x00001B03
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000390B File Offset: 0x00001B0B
		[Serialize]
		private string LocKey { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003914 File Offset: 0x00001B14
		public bool IsHiddenGroup
		{
			get
			{
				return base.HasSpec<HiddenKeyBindingGroupSpec>();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000391C File Offset: 0x00001B1C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("KeyBindingGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003968 File Offset: 0x00001B68
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
			builder.Append(", Order = ");
			builder.Append(this.Order.ToString());
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", IsHiddenGroup = ");
			builder.Append(this.IsHiddenGroup.ToString());
			return true;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003A0B File Offset: 0x00001C0B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(KeyBindingGroupSpec left, KeyBindingGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A17 File Offset: 0x00001C17
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(KeyBindingGroupSpec left, KeyBindingGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A2C File Offset: 0x00001C2C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Order>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LocKey>k__BackingField);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003A9B File Offset: 0x00001C9B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as KeyBindingGroupSpec);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000024D7 File Offset: 0x000006D7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003AAC File Offset: 0x00001CAC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(KeyBindingGroupSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Order>k__BackingField, other.<Order>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LocKey>k__BackingField, other.<LocKey>k__BackingField));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003B30 File Offset: 0x00001D30
		[CompilerGenerated]
		protected KeyBindingGroupSpec([Nullable(1)] KeyBindingGroupSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Order = original.<Order>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.LocKey = original.<LocKey>k__BackingField;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002500 File Offset: 0x00000700
		public KeyBindingGroupSpec()
		{
		}
	}
}
