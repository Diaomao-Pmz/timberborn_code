using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class ConnectionAnchorPointSpec : ComponentSpec, IEquatable<ConnectionAnchorPointSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002335 File Offset: 0x00000535
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ConnectionAnchorPointSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002341 File Offset: 0x00000541
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002349 File Offset: 0x00000549
		[Serialize]
		public Vector3 Position { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002354 File Offset: 0x00000554
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConnectionAnchorPointSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A0 File Offset: 0x000005A0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Position = ");
			builder.Append(this.Position.ToString());
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023EA File Offset: 0x000005EA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConnectionAnchorPointSpec left, ConnectionAnchorPointSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023F6 File Offset: 0x000005F6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConnectionAnchorPointSpec left, ConnectionAnchorPointSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000240A File Offset: 0x0000060A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Position>k__BackingField);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002429 File Offset: 0x00000629
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConnectionAnchorPointSpec);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002437 File Offset: 0x00000637
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002440 File Offset: 0x00000640
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConnectionAnchorPointSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<Position>k__BackingField, other.<Position>k__BackingField));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002471 File Offset: 0x00000671
		[CompilerGenerated]
		protected ConnectionAnchorPointSpec(ConnectionAnchorPointSpec original) : base(original)
		{
			this.Position = original.<Position>k__BackingField;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002486 File Offset: 0x00000686
		public ConnectionAnchorPointSpec()
		{
		}
	}
}
