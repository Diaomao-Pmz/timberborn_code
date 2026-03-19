using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x0200000A RID: 10
	public class ActivationWarningStatusSpec : ComponentSpec, IEquatable<ActivationWarningStatusSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000271E File Offset: 0x0000091E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ActivationWarningStatusSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000272A File Offset: 0x0000092A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002732 File Offset: 0x00000932
		[Serialize]
		public string StatusSpriteName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000273B File Offset: 0x0000093B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002743 File Offset: 0x00000943
		[Serialize]
		public string StatusLocKey { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000274C File Offset: 0x0000094C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002754 File Offset: 0x00000954
		[Serialize]
		public bool UseInfiniteWarning { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000275D File Offset: 0x0000095D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002765 File Offset: 0x00000965
		[Serialize]
		public string WarningSound { get; set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002770 File Offset: 0x00000970
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ActivationWarningStatusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027BC File Offset: 0x000009BC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("StatusSpriteName = ");
			builder.Append(this.StatusSpriteName);
			builder.Append(", StatusLocKey = ");
			builder.Append(this.StatusLocKey);
			builder.Append(", UseInfiniteWarning = ");
			builder.Append(this.UseInfiniteWarning.ToString());
			builder.Append(", WarningSound = ");
			builder.Append(this.WarningSound);
			return true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002851 File Offset: 0x00000A51
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ActivationWarningStatusSpec left, ActivationWarningStatusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000285D File Offset: 0x00000A5D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ActivationWarningStatusSpec left, ActivationWarningStatusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002874 File Offset: 0x00000A74
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StatusSpriteName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StatusLocKey>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UseInfiniteWarning>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WarningSound>k__BackingField);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028E3 File Offset: 0x00000AE3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ActivationWarningStatusSpec);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000023EE File Offset: 0x000005EE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028F4 File Offset: 0x00000AF4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ActivationWarningStatusSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<StatusSpriteName>k__BackingField, other.<StatusSpriteName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StatusLocKey>k__BackingField, other.<StatusLocKey>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UseInfiniteWarning>k__BackingField, other.<UseInfiniteWarning>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WarningSound>k__BackingField, other.<WarningSound>k__BackingField));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002978 File Offset: 0x00000B78
		[CompilerGenerated]
		protected ActivationWarningStatusSpec([Nullable(1)] ActivationWarningStatusSpec original) : base(original)
		{
			this.StatusSpriteName = original.<StatusSpriteName>k__BackingField;
			this.StatusLocKey = original.<StatusLocKey>k__BackingField;
			this.UseInfiniteWarning = original.<UseInfiniteWarning>k__BackingField;
			this.WarningSound = original.<WarningSound>k__BackingField;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002491 File Offset: 0x00000691
		public ActivationWarningStatusSpec()
		{
		}
	}
}
