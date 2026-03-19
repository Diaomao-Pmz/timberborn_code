using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.FireworkSystem
{
	// Token: 0x0200000B RID: 11
	public class FireworkLauncherSpec : ComponentSpec, IEquatable<FireworkLauncherSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002D35 File Offset: 0x00000F35
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FireworkLauncherSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D41 File Offset: 0x00000F41
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002D49 File Offset: 0x00000F49
		[Serialize]
		public string Turret { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D52 File Offset: 0x00000F52
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D5A File Offset: 0x00000F5A
		[Serialize]
		public string Barrel { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002D6B File Offset: 0x00000F6B
		[Serialize]
		public string GoodId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D74 File Offset: 0x00000F74
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002D7C File Offset: 0x00000F7C
		[Serialize]
		public int GoodAmount { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002D88 File Offset: 0x00000F88
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FireworkLauncherSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DD4 File Offset: 0x00000FD4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Turret = ");
			builder.Append(this.Turret);
			builder.Append(", Barrel = ");
			builder.Append(this.Barrel);
			builder.Append(", GoodId = ");
			builder.Append(this.GoodId);
			builder.Append(", GoodAmount = ");
			builder.Append(this.GoodAmount.ToString());
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E69 File Offset: 0x00001069
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FireworkLauncherSpec left, FireworkLauncherSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E75 File Offset: 0x00001075
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FireworkLauncherSpec left, FireworkLauncherSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8C File Offset: 0x0000108C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Turret>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Barrel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GoodId>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<GoodAmount>k__BackingField);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EFB File Offset: 0x000010FB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FireworkLauncherSpec);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F09 File Offset: 0x00001109
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F14 File Offset: 0x00001114
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FireworkLauncherSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Turret>k__BackingField, other.<Turret>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Barrel>k__BackingField, other.<Barrel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GoodId>k__BackingField, other.<GoodId>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<GoodAmount>k__BackingField, other.<GoodAmount>k__BackingField));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F98 File Offset: 0x00001198
		[CompilerGenerated]
		protected FireworkLauncherSpec([Nullable(1)] FireworkLauncherSpec original) : base(original)
		{
			this.Turret = original.<Turret>k__BackingField;
			this.Barrel = original.<Barrel>k__BackingField;
			this.GoodId = original.<GoodId>k__BackingField;
			this.GoodAmount = original.<GoodAmount>k__BackingField;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FD1 File Offset: 0x000011D1
		public FireworkLauncherSpec()
		{
		}
	}
}
