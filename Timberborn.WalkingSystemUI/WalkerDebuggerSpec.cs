using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WalkingSystemUI
{
	// Token: 0x02000008 RID: 8
	public class WalkerDebuggerSpec : ComponentSpec, IEquatable<WalkerDebuggerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000024F8 File Offset: 0x000006F8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WalkerDebuggerSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002504 File Offset: 0x00000704
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000250C File Offset: 0x0000070C
		[Serialize]
		public string WalkerGameObjectMarkerPath { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002515 File Offset: 0x00000715
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000251D File Offset: 0x0000071D
		[Serialize]
		public string WalkerModelMarkerPath { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002526 File Offset: 0x00000726
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000252E File Offset: 0x0000072E
		[Serialize]
		public string DestinationMarkerPath { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002537 File Offset: 0x00000737
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000253F File Offset: 0x0000073F
		[Serialize]
		public string CornerMarkerPath { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002548 File Offset: 0x00000748
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WalkerDebuggerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002594 File Offset: 0x00000794
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WalkerGameObjectMarkerPath = ");
			builder.Append(this.WalkerGameObjectMarkerPath);
			builder.Append(", WalkerModelMarkerPath = ");
			builder.Append(this.WalkerModelMarkerPath);
			builder.Append(", DestinationMarkerPath = ");
			builder.Append(this.DestinationMarkerPath);
			builder.Append(", CornerMarkerPath = ");
			builder.Append(this.CornerMarkerPath);
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000261B File Offset: 0x0000081B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WalkerDebuggerSpec left, WalkerDebuggerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002627 File Offset: 0x00000827
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WalkerDebuggerSpec left, WalkerDebuggerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000263C File Offset: 0x0000083C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WalkerGameObjectMarkerPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WalkerModelMarkerPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DestinationMarkerPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CornerMarkerPath>k__BackingField);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026AB File Offset: 0x000008AB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WalkerDebuggerSpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026B9 File Offset: 0x000008B9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C4 File Offset: 0x000008C4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WalkerDebuggerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WalkerGameObjectMarkerPath>k__BackingField, other.<WalkerGameObjectMarkerPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WalkerModelMarkerPath>k__BackingField, other.<WalkerModelMarkerPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DestinationMarkerPath>k__BackingField, other.<DestinationMarkerPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CornerMarkerPath>k__BackingField, other.<CornerMarkerPath>k__BackingField));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002748 File Offset: 0x00000948
		[CompilerGenerated]
		protected WalkerDebuggerSpec([Nullable(1)] WalkerDebuggerSpec original) : base(original)
		{
			this.WalkerGameObjectMarkerPath = original.<WalkerGameObjectMarkerPath>k__BackingField;
			this.WalkerModelMarkerPath = original.<WalkerModelMarkerPath>k__BackingField;
			this.DestinationMarkerPath = original.<DestinationMarkerPath>k__BackingField;
			this.CornerMarkerPath = original.<CornerMarkerPath>k__BackingField;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002781 File Offset: 0x00000981
		public WalkerDebuggerSpec()
		{
		}
	}
}
