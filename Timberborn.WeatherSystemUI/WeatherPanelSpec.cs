using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class WeatherPanelSpec : ComponentSpec, IEquatable<WeatherPanelSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002856 File Offset: 0x00000A56
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WeatherPanelSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002862 File Offset: 0x00000A62
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000286A File Offset: 0x00000A6A
		[Serialize]
		public int NumberOfBlinks { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002873 File Offset: 0x00000A73
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000287B File Offset: 0x00000A7B
		[Serialize]
		public float SecondsBetweenBlinks { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002884 File Offset: 0x00000A84
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WeatherPanelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028D0 File Offset: 0x00000AD0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NumberOfBlinks = ");
			builder.Append(this.NumberOfBlinks.ToString());
			builder.Append(", SecondsBetweenBlinks = ");
			builder.Append(this.SecondsBetweenBlinks.ToString());
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002941 File Offset: 0x00000B41
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WeatherPanelSpec left, WeatherPanelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000294D File Offset: 0x00000B4D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WeatherPanelSpec left, WeatherPanelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002961 File Offset: 0x00000B61
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NumberOfBlinks>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<SecondsBetweenBlinks>k__BackingField);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002997 File Offset: 0x00000B97
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WeatherPanelSpec);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029A5 File Offset: 0x00000BA5
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029B0 File Offset: 0x00000BB0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WeatherPanelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<NumberOfBlinks>k__BackingField, other.<NumberOfBlinks>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<SecondsBetweenBlinks>k__BackingField, other.<SecondsBetweenBlinks>k__BackingField));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A04 File Offset: 0x00000C04
		[CompilerGenerated]
		protected WeatherPanelSpec(WeatherPanelSpec original) : base(original)
		{
			this.NumberOfBlinks = original.<NumberOfBlinks>k__BackingField;
			this.SecondsBetweenBlinks = original.<SecondsBetweenBlinks>k__BackingField;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A25 File Offset: 0x00000C25
		public WeatherPanelSpec()
		{
		}
	}
}
