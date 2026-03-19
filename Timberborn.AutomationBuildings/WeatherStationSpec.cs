using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	public class WeatherStationSpec : ComponentSpec, IEquatable<WeatherStationSpec>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00009BEF File Offset: 0x00007DEF
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WeatherStationSpec);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00009BFB File Offset: 0x00007DFB
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00009C03 File Offset: 0x00007E03
		[Serialize]
		public int MaxEarlyActivationHours { get; set; }

		// Token: 0x0600038D RID: 909 RVA: 0x00009C0C File Offset: 0x00007E0C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WeatherStationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009C58 File Offset: 0x00007E58
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxEarlyActivationHours = ");
			builder.Append(this.MaxEarlyActivationHours.ToString());
			return true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00009CA2 File Offset: 0x00007EA2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WeatherStationSpec left, WeatherStationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00009CAE File Offset: 0x00007EAE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WeatherStationSpec left, WeatherStationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009CC2 File Offset: 0x00007EC2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxEarlyActivationHours>k__BackingField);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009CE1 File Offset: 0x00007EE1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WeatherStationSpec);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00009CEF File Offset: 0x00007EEF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WeatherStationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxEarlyActivationHours>k__BackingField, other.<MaxEarlyActivationHours>k__BackingField));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009D20 File Offset: 0x00007F20
		[CompilerGenerated]
		protected WeatherStationSpec(WeatherStationSpec original) : base(original)
		{
			this.MaxEarlyActivationHours = original.<MaxEarlyActivationHours>k__BackingField;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00002778 File Offset: 0x00000978
		public WeatherStationSpec()
		{
		}
	}
}
