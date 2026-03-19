using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.GameSaveRepositorySystem
{
	// Token: 0x0200000C RID: 12
	public class SaveReference : IEquatable<SaveReference>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000289C File Offset: 0x00000A9C
		public SaveReference(string SaveName, SettlementReference SettlementReference)
		{
			this.SaveName = SaveName;
			this.SettlementReference = SettlementReference;
			base..ctor();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000028B2 File Offset: 0x00000AB2
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SaveReference);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028BE File Offset: 0x00000ABE
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000028C6 File Offset: 0x00000AC6
		public string SaveName { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028CF File Offset: 0x00000ACF
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000028D7 File Offset: 0x00000AD7
		public SettlementReference SettlementReference { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000028E0 File Offset: 0x00000AE0
		public override string ToString()
		{
			return this.SettlementReference.SettlementName + " - " + this.SaveName;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028FD File Offset: 0x00000AFD
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SaveName = ");
			builder.Append(this.SaveName);
			builder.Append(", SettlementReference = ");
			builder.Append(this.SettlementReference);
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002937 File Offset: 0x00000B37
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SaveReference left, SaveReference right)
		{
			return !(left == right);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002943 File Offset: 0x00000B43
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SaveReference left, SaveReference right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002957 File Offset: 0x00000B57
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SaveName>k__BackingField)) * -1521134295 + EqualityComparer<SettlementReference>.Default.GetHashCode(this.<SettlementReference>k__BackingField);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002997 File Offset: 0x00000B97
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SaveReference);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029A8 File Offset: 0x00000BA8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SaveReference other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SaveName>k__BackingField, other.<SaveName>k__BackingField) && EqualityComparer<SettlementReference>.Default.Equals(this.<SettlementReference>k__BackingField, other.<SettlementReference>k__BackingField));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A09 File Offset: 0x00000C09
		[CompilerGenerated]
		protected SaveReference([Nullable(1)] SaveReference original)
		{
			this.SaveName = original.<SaveName>k__BackingField;
			this.SettlementReference = original.<SettlementReference>k__BackingField;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A29 File Offset: 0x00000C29
		[CompilerGenerated]
		public void Deconstruct(out string SaveName, out SettlementReference SettlementReference)
		{
			SaveName = this.SaveName;
			SettlementReference = this.SettlementReference;
		}
	}
}
