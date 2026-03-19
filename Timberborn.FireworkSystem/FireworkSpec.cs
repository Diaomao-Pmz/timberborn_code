using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;
using Timberborn.TemplateSystem;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000011 RID: 17
	public class FireworkSpec : ComponentSpec, IEquatable<FireworkSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000031DD File Offset: 0x000013DD
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FireworkSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000031E9 File Offset: 0x000013E9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000031F1 File Offset: 0x000013F1
		[Serialize("DisplayNameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000031FA File Offset: 0x000013FA
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003202 File Offset: 0x00001402
		[Serialize]
		public bool HasBurst { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000320B File Offset: 0x0000140B
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003213 File Offset: 0x00001413
		[Serialize]
		public string TrailSound { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000321C File Offset: 0x0000141C
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003224 File Offset: 0x00001424
		[Serialize]
		public string BurstSound { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000322D File Offset: 0x0000142D
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003235 File Offset: 0x00001435
		[Serialize]
		private string DisplayNameLocKey { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000323E File Offset: 0x0000143E
		public string FireworkId
		{
			get
			{
				return base.Blueprint.GetSpec<TemplateSpec>().TemplateName;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003250 File Offset: 0x00001450
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FireworkSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000329C File Offset: 0x0000149C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", HasBurst = ");
			builder.Append(this.HasBurst.ToString());
			builder.Append(", TrailSound = ");
			builder.Append(this.TrailSound);
			builder.Append(", BurstSound = ");
			builder.Append(this.BurstSound);
			builder.Append(", FireworkId = ");
			builder.Append(this.FireworkId);
			return true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000334A File Offset: 0x0000154A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FireworkSpec left, FireworkSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003356 File Offset: 0x00001556
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FireworkSpec left, FireworkSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000336C File Offset: 0x0000156C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasBurst>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TrailSound>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BurstSound>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayNameLocKey>k__BackingField);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033F2 File Offset: 0x000015F2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FireworkSpec);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F09 File Offset: 0x00001109
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003400 File Offset: 0x00001600
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FireworkSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasBurst>k__BackingField, other.<HasBurst>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TrailSound>k__BackingField, other.<TrailSound>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BurstSound>k__BackingField, other.<BurstSound>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayNameLocKey>k__BackingField, other.<DisplayNameLocKey>k__BackingField));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034A0 File Offset: 0x000016A0
		[CompilerGenerated]
		protected FireworkSpec([Nullable(1)] FireworkSpec original) : base(original)
		{
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.HasBurst = original.<HasBurst>k__BackingField;
			this.TrailSound = original.<TrailSound>k__BackingField;
			this.BurstSound = original.<BurstSound>k__BackingField;
			this.DisplayNameLocKey = original.<DisplayNameLocKey>k__BackingField;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002FD1 File Offset: 0x000011D1
		public FireworkSpec()
		{
		}
	}
}
