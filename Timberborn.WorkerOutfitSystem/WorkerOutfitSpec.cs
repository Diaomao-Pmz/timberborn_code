using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000010 RID: 16
	public class WorkerOutfitSpec : ComponentSpec, IEquatable<WorkerOutfitSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C20 File Offset: 0x00000E20
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerOutfitSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C2C File Offset: 0x00000E2C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002C34 File Offset: 0x00000E34
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C3D File Offset: 0x00000E3D
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002C45 File Offset: 0x00000E45
		[Serialize]
		public string FactionId { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C4E File Offset: 0x00000E4E
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002C56 File Offset: 0x00000E56
		[Serialize]
		public string WorkerType { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002C5F File Offset: 0x00000E5F
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002C67 File Offset: 0x00000E67
		[Serialize]
		public AssetRef<Texture2D> DiffuseTexture { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C70 File Offset: 0x00000E70
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002C78 File Offset: 0x00000E78
		[Serialize]
		public AssetRef<Texture2D> NormalTexture { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C81 File Offset: 0x00000E81
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002C89 File Offset: 0x00000E89
		[Serialize]
		public ImmutableArray<string> Attachments { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002C94 File Offset: 0x00000E94
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerOutfitSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CE0 File Offset: 0x00000EE0
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
			builder.Append(", FactionId = ");
			builder.Append(this.FactionId);
			builder.Append(", WorkerType = ");
			builder.Append(this.WorkerType);
			builder.Append(", DiffuseTexture = ");
			builder.Append(this.DiffuseTexture);
			builder.Append(", NormalTexture = ");
			builder.Append(this.NormalTexture);
			builder.Append(", Attachments = ");
			builder.Append(this.Attachments.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DA7 File Offset: 0x00000FA7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerOutfitSpec left, WorkerOutfitSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DB3 File Offset: 0x00000FB3
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerOutfitSpec left, WorkerOutfitSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DC8 File Offset: 0x00000FC8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FactionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerType>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<DiffuseTexture>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<NormalTexture>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Attachments>k__BackingField);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E65 File Offset: 0x00001065
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerOutfitSpec);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000027AB File Offset: 0x000009AB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E74 File Offset: 0x00001074
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerOutfitSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FactionId>k__BackingField, other.<FactionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkerType>k__BackingField, other.<WorkerType>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<DiffuseTexture>k__BackingField, other.<DiffuseTexture>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<NormalTexture>k__BackingField, other.<NormalTexture>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Attachments>k__BackingField, other.<Attachments>k__BackingField));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F30 File Offset: 0x00001130
		[CompilerGenerated]
		protected WorkerOutfitSpec([Nullable(1)] WorkerOutfitSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.FactionId = original.<FactionId>k__BackingField;
			this.WorkerType = original.<WorkerType>k__BackingField;
			this.DiffuseTexture = original.<DiffuseTexture>k__BackingField;
			this.NormalTexture = original.<NormalTexture>k__BackingField;
			this.Attachments = original.<Attachments>k__BackingField;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000027FA File Offset: 0x000009FA
		public WorkerOutfitSpec()
		{
		}
	}
}
