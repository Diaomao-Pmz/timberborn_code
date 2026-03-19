using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.GameSaveRepositorySystem
{
	// Token: 0x0200000D RID: 13
	public class SettlementReference : IEquatable<SettlementReference>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A3B File Offset: 0x00000C3B
		public SettlementReference(string SettlementName, string SaveDirectory)
		{
			this.SettlementName = SettlementName;
			this.SaveDirectory = SaveDirectory;
			base..ctor();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A51 File Offset: 0x00000C51
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SettlementReference);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A5D File Offset: 0x00000C5D
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A65 File Offset: 0x00000C65
		public string SettlementName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A76 File Offset: 0x00000C76
		public string SaveDirectory { get; set; }

		// Token: 0x06000048 RID: 72 RVA: 0x00002A80 File Offset: 0x00000C80
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SettlementReference");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002ACC File Offset: 0x00000CCC
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SettlementName = ");
			builder.Append(this.SettlementName);
			builder.Append(", SaveDirectory = ");
			builder.Append(this.SaveDirectory);
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B06 File Offset: 0x00000D06
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SettlementReference left, SettlementReference right)
		{
			return !(left == right);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B12 File Offset: 0x00000D12
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SettlementReference left, SettlementReference right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B26 File Offset: 0x00000D26
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SettlementName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SaveDirectory>k__BackingField);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B66 File Offset: 0x00000D66
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SettlementReference);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B74 File Offset: 0x00000D74
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SettlementReference other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SettlementName>k__BackingField, other.<SettlementName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SaveDirectory>k__BackingField, other.<SaveDirectory>k__BackingField));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BD5 File Offset: 0x00000DD5
		[CompilerGenerated]
		protected SettlementReference([Nullable(1)] SettlementReference original)
		{
			this.SettlementName = original.<SettlementName>k__BackingField;
			this.SaveDirectory = original.<SaveDirectory>k__BackingField;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BF5 File Offset: 0x00000DF5
		[CompilerGenerated]
		public void Deconstruct(out string SettlementName, out string SaveDirectory)
		{
			SettlementName = this.SettlementName;
			SaveDirectory = this.SaveDirectory;
		}
	}
}
