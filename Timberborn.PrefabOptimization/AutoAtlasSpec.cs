using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200000F RID: 15
	public class AutoAtlasSpec : IEquatable<AutoAtlasSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000314F File Offset: 0x0000134F
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AutoAtlasSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000315B File Offset: 0x0000135B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003163 File Offset: 0x00001363
		[Serialize]
		public string Name { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000316C File Offset: 0x0000136C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003174 File Offset: 0x00001374
		[Serialize]
		public bool IsUnique { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000317D File Offset: 0x0000137D
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003185 File Offset: 0x00001385
		[Serialize]
		public ImmutableArray<AssetRef<Material>> Fragments { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00003190 File Offset: 0x00001390
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AutoAtlasSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000031DC File Offset: 0x000013DC
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Name = ");
			builder.Append(this.Name);
			builder.Append(", IsUnique = ");
			builder.Append(this.IsUnique.ToString());
			builder.Append(", Fragments = ");
			builder.Append(this.Fragments.ToString());
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003256 File Offset: 0x00001456
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AutoAtlasSpec left, AutoAtlasSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003262 File Offset: 0x00001462
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AutoAtlasSpec left, AutoAtlasSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003278 File Offset: 0x00001478
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsUnique>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<AssetRef<Material>>>.Default.GetHashCode(this.<Fragments>k__BackingField);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000032DA File Offset: 0x000014DA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AutoAtlasSpec);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032E8 File Offset: 0x000014E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AutoAtlasSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsUnique>k__BackingField, other.<IsUnique>k__BackingField) && EqualityComparer<ImmutableArray<AssetRef<Material>>>.Default.Equals(this.<Fragments>k__BackingField, other.<Fragments>k__BackingField));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003361 File Offset: 0x00001561
		[CompilerGenerated]
		protected AutoAtlasSpec([Nullable(1)] AutoAtlasSpec original)
		{
			this.Name = original.<Name>k__BackingField;
			this.IsUnique = original.<IsUnique>k__BackingField;
			this.Fragments = original.<Fragments>k__BackingField;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000020F8 File Offset: 0x000002F8
		public AutoAtlasSpec()
		{
		}
	}
}
