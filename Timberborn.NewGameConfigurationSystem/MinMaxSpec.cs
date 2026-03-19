using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NewGameConfigurationSystem
{
	// Token: 0x0200000B RID: 11
	public class MinMaxSpec<T> : ComponentSpec, IEquatable<MinMaxSpec<T>>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F35 File Offset: 0x00001135
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MinMaxSpec<T>);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F41 File Offset: 0x00001141
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002F49 File Offset: 0x00001149
		[Serialize]
		public T Min { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F52 File Offset: 0x00001152
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002F5A File Offset: 0x0000115A
		[Serialize]
		public T Max { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002F63 File Offset: 0x00001163
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.Min, this.Max);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F88 File Offset: 0x00001188
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Min = ");
			builder.Append(this.Min);
			builder.Append(", Max = ");
			builder.Append(this.Max);
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FE7 File Offset: 0x000011E7
		[CompilerGenerated]
		public static bool operator !=([Nullable(new byte[]
		{
			2,
			0
		})] MinMaxSpec<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] MinMaxSpec<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FF3 File Offset: 0x000011F3
		[CompilerGenerated]
		public static bool operator ==([Nullable(new byte[]
		{
			2,
			0
		})] MinMaxSpec<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] MinMaxSpec<T> right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003007 File Offset: 0x00001207
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.<Max>k__BackingField);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000303D File Offset: 0x0000123D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MinMaxSpec<T>);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AAA File Offset: 0x00000CAA
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000304C File Offset: 0x0000124C
		[CompilerGenerated]
		public virtual bool Equals([Nullable(new byte[]
		{
			2,
			0
		})] MinMaxSpec<T> other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<T>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<T>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030A0 File Offset: 0x000012A0
		[CompilerGenerated]
		protected MinMaxSpec([Nullable(new byte[]
		{
			1,
			0
		})] MinMaxSpec<T> original) : base(original)
		{
			this.Min = original.<Min>k__BackingField;
			this.Max = original.<Max>k__BackingField;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E64 File Offset: 0x00001064
		public MinMaxSpec()
		{
		}
	}
}
