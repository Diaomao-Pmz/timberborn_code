using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x0200000F RID: 15
	public class GoodStackModelSpec : ComponentSpec, IEquatable<GoodStackModelSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026BC File Offset: 0x000008BC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodStackModelSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026D0 File Offset: 0x000008D0
		[Serialize]
		public string LogObjectName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026D9 File Offset: 0x000008D9
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026E1 File Offset: 0x000008E1
		[Serialize]
		public string BarrelObjectName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026EA File Offset: 0x000008EA
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026F2 File Offset: 0x000008F2
		[Serialize]
		public string BoxObjectName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026FB File Offset: 0x000008FB
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002703 File Offset: 0x00000903
		[Serialize]
		public string BagObjectName { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x0000270C File Offset: 0x0000090C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodStackModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LogObjectName = ");
			builder.Append(this.LogObjectName);
			builder.Append(", BarrelObjectName = ");
			builder.Append(this.BarrelObjectName);
			builder.Append(", BoxObjectName = ");
			builder.Append(this.BoxObjectName);
			builder.Append(", BagObjectName = ");
			builder.Append(this.BagObjectName);
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027DF File Offset: 0x000009DF
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodStackModelSpec left, GoodStackModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027EB File Offset: 0x000009EB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodStackModelSpec left, GoodStackModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002800 File Offset: 0x00000A00
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogObjectName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BarrelObjectName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BoxObjectName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BagObjectName>k__BackingField);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000286F File Offset: 0x00000A6F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodStackModelSpec);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000287D File Offset: 0x00000A7D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002888 File Offset: 0x00000A88
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodStackModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<LogObjectName>k__BackingField, other.<LogObjectName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BarrelObjectName>k__BackingField, other.<BarrelObjectName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BoxObjectName>k__BackingField, other.<BoxObjectName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BagObjectName>k__BackingField, other.<BagObjectName>k__BackingField));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000290C File Offset: 0x00000B0C
		[CompilerGenerated]
		protected GoodStackModelSpec([Nullable(1)] GoodStackModelSpec original) : base(original)
		{
			this.LogObjectName = original.<LogObjectName>k__BackingField;
			this.BarrelObjectName = original.<BarrelObjectName>k__BackingField;
			this.BoxObjectName = original.<BoxObjectName>k__BackingField;
			this.BagObjectName = original.<BagObjectName>k__BackingField;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002945 File Offset: 0x00000B45
		public GoodStackModelSpec()
		{
		}
	}
}
