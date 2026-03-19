using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Ruins
{
	// Token: 0x0200000F RID: 15
	public class RuinModelVariantSpec : IEquatable<RuinModelVariantSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A0D File Offset: 0x00000C0D
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RuinModelVariantSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A19 File Offset: 0x00000C19
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002A21 File Offset: 0x00000C21
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A2A File Offset: 0x00000C2A
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A32 File Offset: 0x00000C32
		[Serialize]
		public AssetRef<GameObject> Model { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00002A3C File Offset: 0x00000C3C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RuinModelVariantSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A88 File Offset: 0x00000C88
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Model = ");
			builder.Append(this.Model);
			return true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AC2 File Offset: 0x00000CC2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RuinModelVariantSpec left, RuinModelVariantSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002ACE File Offset: 0x00000CCE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RuinModelVariantSpec left, RuinModelVariantSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002AE2 File Offset: 0x00000CE2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<Model>k__BackingField);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B22 File Offset: 0x00000D22
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RuinModelVariantSpec);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B30 File Offset: 0x00000D30
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RuinModelVariantSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B91 File Offset: 0x00000D91
		[CompilerGenerated]
		protected RuinModelVariantSpec([Nullable(1)] RuinModelVariantSpec original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Model = original.<Model>k__BackingField;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000020F8 File Offset: 0x000002F8
		public RuinModelVariantSpec()
		{
		}
	}
}
