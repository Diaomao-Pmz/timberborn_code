using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000D RID: 13
	public class HazardousWeatherFogSettingsSpec : IEquatable<HazardousWeatherFogSettingsSpec>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BD1 File Offset: 0x00000DD1
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(HazardousWeatherFogSettingsSpec);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002BDD File Offset: 0x00000DDD
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002BE5 File Offset: 0x00000DE5
		[Serialize]
		public string HazardousWeatherId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BEE File Offset: 0x00000DEE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002BF6 File Offset: 0x00000DF6
		[Serialize]
		public FogSettingsSpec FogSettings { get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002C00 File Offset: 0x00000E00
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("HazardousWeatherFogSettingsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C4C File Offset: 0x00000E4C
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("HazardousWeatherId = ");
			builder.Append(this.HazardousWeatherId);
			builder.Append(", FogSettings = ");
			builder.Append(this.FogSettings);
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C86 File Offset: 0x00000E86
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(HazardousWeatherFogSettingsSpec left, HazardousWeatherFogSettingsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C92 File Offset: 0x00000E92
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(HazardousWeatherFogSettingsSpec left, HazardousWeatherFogSettingsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002CA6 File Offset: 0x00000EA6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HazardousWeatherId>k__BackingField)) * -1521134295 + EqualityComparer<FogSettingsSpec>.Default.GetHashCode(this.<FogSettings>k__BackingField);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CE6 File Offset: 0x00000EE6
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HazardousWeatherFogSettingsSpec);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002CF4 File Offset: 0x00000EF4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(HazardousWeatherFogSettingsSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<HazardousWeatherId>k__BackingField, other.<HazardousWeatherId>k__BackingField) && EqualityComparer<FogSettingsSpec>.Default.Equals(this.<FogSettings>k__BackingField, other.<FogSettings>k__BackingField));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D55 File Offset: 0x00000F55
		[CompilerGenerated]
		protected HazardousWeatherFogSettingsSpec([Nullable(1)] HazardousWeatherFogSettingsSpec original)
		{
			this.HazardousWeatherId = original.<HazardousWeatherId>k__BackingField;
			this.FogSettings = original.<FogSettings>k__BackingField;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000020F6 File Offset: 0x000002F6
		public HazardousWeatherFogSettingsSpec()
		{
		}
	}
}
