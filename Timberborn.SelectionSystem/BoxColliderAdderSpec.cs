using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000009 RID: 9
	public class BoxColliderAdderSpec : ComponentSpec, IEquatable<BoxColliderAdderSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021FE File Offset: 0x000003FE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BoxColliderAdderSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000220A File Offset: 0x0000040A
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002212 File Offset: 0x00000412
		[Serialize]
		public string TargetName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000221B File Offset: 0x0000041B
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002223 File Offset: 0x00000423
		[Serialize]
		public Vector3 Center { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000222C File Offset: 0x0000042C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002234 File Offset: 0x00000434
		[Serialize]
		public Vector3 Size { get; set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002240 File Offset: 0x00000440
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BoxColliderAdderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000228C File Offset: 0x0000048C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TargetName = ");
			builder.Append(this.TargetName);
			builder.Append(", Center = ");
			builder.Append(this.Center.ToString());
			builder.Append(", Size = ");
			builder.Append(this.Size.ToString());
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002316 File Offset: 0x00000516
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BoxColliderAdderSpec left, BoxColliderAdderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002322 File Offset: 0x00000522
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BoxColliderAdderSpec left, BoxColliderAdderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002338 File Offset: 0x00000538
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TargetName>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Size>k__BackingField);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002390 File Offset: 0x00000590
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BoxColliderAdderSpec);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239E File Offset: 0x0000059E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A8 File Offset: 0x000005A8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BoxColliderAdderSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TargetName>k__BackingField, other.<TargetName>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002414 File Offset: 0x00000614
		[CompilerGenerated]
		protected BoxColliderAdderSpec([Nullable(1)] BoxColliderAdderSpec original) : base(original)
		{
			this.TargetName = original.<TargetName>k__BackingField;
			this.Center = original.<Center>k__BackingField;
			this.Size = original.<Size>k__BackingField;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002441 File Offset: 0x00000641
		public BoxColliderAdderSpec()
		{
		}
	}
}
