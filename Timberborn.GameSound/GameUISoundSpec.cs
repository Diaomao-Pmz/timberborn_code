using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameSound
{
	// Token: 0x0200000C RID: 12
	public class GameUISoundSpec : ComponentSpec, IEquatable<GameUISoundSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002820 File Offset: 0x00000A20
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GameUISoundSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002834 File Offset: 0x00000A34
		[Serialize]
		public string WellbeingHighscore { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000283D File Offset: 0x00000A3D
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002845 File Offset: 0x00000A45
		[Serialize]
		public string FieldPlaced { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000284E File Offset: 0x00000A4E
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002856 File Offset: 0x00000A56
		[Serialize]
		public string BlinkingSoundKey { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000285F File Offset: 0x00000A5F
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002867 File Offset: 0x00000A67
		[Serialize]
		public string BadtideStartedSoundKey { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002870 File Offset: 0x00000A70
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002878 File Offset: 0x00000A78
		[Serialize]
		public string DroughtStartedSoundKey { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002881 File Offset: 0x00000A81
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002889 File Offset: 0x00000A89
		[Serialize]
		public string TemperateWeatherStartedSoundKey { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000289A File Offset: 0x00000A9A
		[Serialize]
		public string WonderCongratulationSoundKey { get; set; }

		// Token: 0x06000048 RID: 72 RVA: 0x000028A4 File Offset: 0x00000AA4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameUISoundSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028F0 File Offset: 0x00000AF0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WellbeingHighscore = ");
			builder.Append(this.WellbeingHighscore);
			builder.Append(", FieldPlaced = ");
			builder.Append(this.FieldPlaced);
			builder.Append(", BlinkingSoundKey = ");
			builder.Append(this.BlinkingSoundKey);
			builder.Append(", BadtideStartedSoundKey = ");
			builder.Append(this.BadtideStartedSoundKey);
			builder.Append(", DroughtStartedSoundKey = ");
			builder.Append(this.DroughtStartedSoundKey);
			builder.Append(", TemperateWeatherStartedSoundKey = ");
			builder.Append(this.TemperateWeatherStartedSoundKey);
			builder.Append(", WonderCongratulationSoundKey = ");
			builder.Append(this.WonderCongratulationSoundKey);
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029C2 File Offset: 0x00000BC2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameUISoundSpec left, GameUISoundSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029CE File Offset: 0x00000BCE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameUISoundSpec left, GameUISoundSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029E4 File Offset: 0x00000BE4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WellbeingHighscore>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FieldPlaced>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BlinkingSoundKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BadtideStartedSoundKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DroughtStartedSoundKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemperateWeatherStartedSoundKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WonderCongratulationSoundKey>k__BackingField);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A98 File Offset: 0x00000C98
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameUISoundSpec);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002282 File Offset: 0x00000482
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AA8 File Offset: 0x00000CA8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameUISoundSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WellbeingHighscore>k__BackingField, other.<WellbeingHighscore>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FieldPlaced>k__BackingField, other.<FieldPlaced>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BlinkingSoundKey>k__BackingField, other.<BlinkingSoundKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BadtideStartedSoundKey>k__BackingField, other.<BadtideStartedSoundKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DroughtStartedSoundKey>k__BackingField, other.<DroughtStartedSoundKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TemperateWeatherStartedSoundKey>k__BackingField, other.<TemperateWeatherStartedSoundKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WonderCongratulationSoundKey>k__BackingField, other.<WonderCongratulationSoundKey>k__BackingField));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B80 File Offset: 0x00000D80
		[CompilerGenerated]
		protected GameUISoundSpec([Nullable(1)] GameUISoundSpec original) : base(original)
		{
			this.WellbeingHighscore = original.<WellbeingHighscore>k__BackingField;
			this.FieldPlaced = original.<FieldPlaced>k__BackingField;
			this.BlinkingSoundKey = original.<BlinkingSoundKey>k__BackingField;
			this.BadtideStartedSoundKey = original.<BadtideStartedSoundKey>k__BackingField;
			this.DroughtStartedSoundKey = original.<DroughtStartedSoundKey>k__BackingField;
			this.TemperateWeatherStartedSoundKey = original.<TemperateWeatherStartedSoundKey>k__BackingField;
			this.WonderCongratulationSoundKey = original.<WonderCongratulationSoundKey>k__BackingField;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002325 File Offset: 0x00000525
		public GameUISoundSpec()
		{
		}
	}
}
