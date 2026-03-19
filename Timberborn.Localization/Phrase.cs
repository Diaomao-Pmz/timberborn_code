using System;

namespace Timberborn.Localization
{
	// Token: 0x02000019 RID: 25
	public class Phrase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003647 File Offset: 0x00001847
		internal string Key { get; }

		// Token: 0x06000086 RID: 134 RVA: 0x0000364F File Offset: 0x0000184F
		public Phrase(string key)
		{
			this.Key = key;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000365E File Offset: 0x0000185E
		public Phrase(string key, object formatter1)
		{
			this.Key = key;
			this._formatter1 = formatter1;
			this._customFormatters = 1;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000367B File Offset: 0x0000187B
		public Phrase(string key, object formatter1, object formatter2)
		{
			Phrase.ValidateKeyExists(key);
			this.Key = key;
			this._formatter1 = formatter1;
			this._formatter2 = formatter2;
			this._customFormatters = 2;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036A5 File Offset: 0x000018A5
		public Phrase(string key, object formatter1, object formatter2, object formatter3)
		{
			Phrase.ValidateKeyExists(key);
			this.Key = key;
			this._formatter1 = formatter1;
			this._formatter2 = formatter2;
			this._formatter3 = formatter3;
			this._customFormatters = 3;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036D7 File Offset: 0x000018D7
		public static Phrase New()
		{
			return new Phrase(null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036DF File Offset: 0x000018DF
		public static Phrase New(string key)
		{
			return new Phrase(key);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036E7 File Offset: 0x000018E7
		public Phrase Format()
		{
			return this.Format<object>(null);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036F0 File Offset: 0x000018F0
		public Phrase Format<T>(string format) where T : IFormattable
		{
			return this.Format<T>((T value) => value.ToString(format, null));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000371C File Offset: 0x0000191C
		public Phrase Format<T>(Func<T, string> formatter)
		{
			return this.Format<T>((T value, ILoc _) => formatter(value));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003748 File Offset: 0x00001948
		public Phrase Format<T>(Func<T, ILoc, string> formatter)
		{
			Phrase result;
			switch (this._customFormatters)
			{
			case 0:
				result = new Phrase(this.Key, formatter);
				break;
			case 1:
				result = new Phrase(this.Key, this._formatter1, formatter);
				break;
			case 2:
				result = new Phrase(this.Key, this._formatter1, this._formatter2, formatter);
				break;
			default:
				throw new InvalidOperationException("Trying to add too many formatters.");
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037BC File Offset: 0x000019BC
		public string GetText<TP1, TP2, TP3>(ILoc loc, TP1 value1, TP2 value2, TP3 value3)
		{
			if (this._textLocalizationWrapper == null)
			{
				this._textLocalizationWrapper = new TextLocalizationWrapper((this.Key == null) ? null : loc.T(this.Key));
			}
			return this._textLocalizationWrapper.GetText<TP1, TP2, TP3>(loc, value1, value2, value3, this._formatter1, this._formatter2, this._formatter3);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003815 File Offset: 0x00001A15
		public static void ValidateKeyExists(string key)
		{
			if (key == null)
			{
				throw new InvalidOperationException("A non-localized Phrase can only have one formatter.");
			}
		}

		// Token: 0x0400005F RID: 95
		public TextLocalizationWrapper _textLocalizationWrapper;

		// Token: 0x04000060 RID: 96
		public readonly int _customFormatters;

		// Token: 0x04000061 RID: 97
		public readonly object _formatter1;

		// Token: 0x04000062 RID: 98
		public readonly object _formatter2;

		// Token: 0x04000063 RID: 99
		public readonly object _formatter3;
	}
}
