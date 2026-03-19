using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x02000007 RID: 7
	public class AttachmentDefinition : IEquatable<AttachmentDefinition>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AttachmentDefinition);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		[Serialize]
		public AssetRef<GameObject> Prefab { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		[Serialize]
		public string Parent { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		[Serialize]
		public Vector3 Position { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002156 File Offset: 0x00000356
		[Serialize]
		public Vector3 Rotation { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002167 File Offset: 0x00000367
		[Serialize]
		public Vector3 Scale { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002178 File Offset: 0x00000378
		[Serialize]
		public bool CreateInstantly { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002184 File Offset: 0x00000384
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AttachmentDefinition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021D0 File Offset: 0x000003D0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("AttachmentId = ");
			builder.Append(this.AttachmentId);
			builder.Append(", Prefab = ");
			builder.Append(this.Prefab);
			builder.Append(", Parent = ");
			builder.Append(this.Parent);
			builder.Append(", Position = ");
			builder.Append(this.Position.ToString());
			builder.Append(", Rotation = ");
			builder.Append(this.Rotation.ToString());
			builder.Append(", Scale = ");
			builder.Append(this.Scale.ToString());
			builder.Append(", CreateInstantly = ");
			builder.Append(this.CreateInstantly.ToString());
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022CA File Offset: 0x000004CA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AttachmentDefinition left, AttachmentDefinition right)
		{
			return !(left == right);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D6 File Offset: 0x000004D6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AttachmentDefinition left, AttachmentDefinition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022EC File Offset: 0x000004EC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<Prefab>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Parent>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Position>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Rotation>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CreateInstantly>k__BackingField);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023AA File Offset: 0x000005AA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AttachmentDefinition);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023B8 File Offset: 0x000005B8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AttachmentDefinition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<Prefab>k__BackingField, other.<Prefab>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Parent>k__BackingField, other.<Parent>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Position>k__BackingField, other.<Position>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Rotation>k__BackingField, other.<Rotation>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CreateInstantly>k__BackingField, other.<CreateInstantly>k__BackingField));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A0 File Offset: 0x000006A0
		[CompilerGenerated]
		protected AttachmentDefinition([Nullable(1)] AttachmentDefinition original)
		{
			this.AttachmentId = original.<AttachmentId>k__BackingField;
			this.Prefab = original.<Prefab>k__BackingField;
			this.Parent = original.<Parent>k__BackingField;
			this.Position = original.<Position>k__BackingField;
			this.Rotation = original.<Rotation>k__BackingField;
			this.Scale = original.<Scale>k__BackingField;
			this.CreateInstantly = original.<CreateInstantly>k__BackingField;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002507 File Offset: 0x00000707
		public AttachmentDefinition()
		{
			this.Scale = Vector3.one;
			this.CreateInstantly = true;
			base..ctor();
		}
	}
}
