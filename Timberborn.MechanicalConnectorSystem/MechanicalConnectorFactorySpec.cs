using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x02000009 RID: 9
	public class MechanicalConnectorFactorySpec : ComponentSpec, IEquatable<MechanicalConnectorFactorySpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000244A File Offset: 0x0000064A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalConnectorFactorySpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002456 File Offset: 0x00000656
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000245E File Offset: 0x0000065E
		[Serialize]
		public string MechanicalConnectorPrefabPath { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x00002468 File Offset: 0x00000668
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalConnectorFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024B4 File Offset: 0x000006B4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MechanicalConnectorPrefabPath = ");
			builder.Append(this.MechanicalConnectorPrefabPath);
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024E5 File Offset: 0x000006E5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalConnectorFactorySpec left, MechanicalConnectorFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024F1 File Offset: 0x000006F1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalConnectorFactorySpec left, MechanicalConnectorFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002505 File Offset: 0x00000705
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MechanicalConnectorPrefabPath>k__BackingField);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002524 File Offset: 0x00000724
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalConnectorFactorySpec);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002532 File Offset: 0x00000732
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000253B File Offset: 0x0000073B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalConnectorFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<MechanicalConnectorPrefabPath>k__BackingField, other.<MechanicalConnectorPrefabPath>k__BackingField));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000256C File Offset: 0x0000076C
		[CompilerGenerated]
		protected MechanicalConnectorFactorySpec([Nullable(1)] MechanicalConnectorFactorySpec original) : base(original)
		{
			this.MechanicalConnectorPrefabPath = original.<MechanicalConnectorPrefabPath>k__BackingField;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002581 File Offset: 0x00000781
		public MechanicalConnectorFactorySpec()
		{
		}
	}
}
