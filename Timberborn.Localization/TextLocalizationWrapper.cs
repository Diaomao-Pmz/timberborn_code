using System;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x0200001E RID: 30
	public class TextLocalizationWrapper
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000039D8 File Offset: 0x00001BD8
		public TextLocalizationWrapper(string textTemplate)
		{
			this._textTemplate = textTemplate;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039E7 File Offset: 0x00001BE7
		public string GetText<TP1, TP2, TP3>(ILoc loc, TP1 value1, TP2 value2, TP3 value3)
		{
			return this.GetText<TP1, TP2, TP3>(loc, value1, value2, value3, null, null, null);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000039F8 File Offset: 0x00001BF8
		public string GetText<TP1, TP2, TP3>(ILoc loc, TP1 value1, TP2 value2, TP3 value3, object formatter1, object formatter2, object formatter3)
		{
			TextLocalization<TP1, TP2, TP3> textLocalization = this._textLocalization as TextLocalization<TP1, TP2, TP3>;
			if (textLocalization != null)
			{
				return this.GetText<TP1, TP2, TP3>(loc, textLocalization, value1, value2, value3, formatter1, formatter2, formatter3);
			}
			return this.CreateNewTextLocalization<TP1, TP2, TP3>(loc, value1, value2, value3, formatter1, formatter2, formatter3);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003A38 File Offset: 0x00001C38
		public string GetText<TP1, TP2, TP3>(ILoc loc, TextLocalization<TP1, TP2, TP3> textLocalization, TP1 value1, TP2 value2, TP3 value3, object formatter1, object formatter2, object formatter3)
		{
			if (!textLocalization.AreValuesEqual(value1, value2, value3))
			{
				string text = this.Format<TP1, TP2, TP3>(loc, value1, value2, value3, formatter1, formatter2, formatter3);
				textLocalization.Update(value1, value2, value3, text);
			}
			return textLocalization.Text;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003A78 File Offset: 0x00001C78
		public string CreateNewTextLocalization<TP1, TP2, TP3>(ILoc loc, TP1 value1, TP2 value2, TP3 value3, object formatter1, object formatter2, object formatter3)
		{
			this.WarnIfUpdating<TP1, TP2, TP3>(value1, value2, value3);
			string text = this.Format<TP1, TP2, TP3>(loc, value1, value2, value3, formatter1, formatter2, formatter3);
			this._textLocalization = new TextLocalization<TP1, TP2, TP3>(value1, value2, value3, text);
			return text;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public void WarnIfUpdating<TP1, TP2, TP3>(TP1 value1, TP2 value2, TP3 value3)
		{
			if (this._textLocalization != null)
			{
				Debug.LogWarning("TextLocalizationWrapper parameter types have changed. This shouldn't have happened!" + string.Format(" Current types: {0} {1} {2}", (value1 != null) ? value1.GetType() : null, (value2 != null) ? value2.GetType() : null, (value3 != null) ? value3.GetType() : null));
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B2C File Offset: 0x00001D2C
		public string Format<TP1, TP2, TP3>(ILoc loc, TP1 value1, TP2 value2, TP3 value3, object formatter1, object formatter2, object formatter3)
		{
			if (this._textTemplate == null)
			{
				object obj = TextLocalizationWrapper.Format<TP1>(loc, value1, formatter1);
				return ((obj != null) ? obj.ToString() : null) ?? "";
			}
			return string.Format(this._textTemplate, TextLocalizationWrapper.Format<TP1>(loc, value1, formatter1), TextLocalizationWrapper.Format<TP2>(loc, value2, formatter2), TextLocalizationWrapper.Format<TP3>(loc, value3, formatter3));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B88 File Offset: 0x00001D88
		public static object Format<T>(ILoc loc, T value, object formatter)
		{
			object result;
			if (formatter != null)
			{
				Func<T, ILoc, string> func = formatter as Func<T, ILoc, string>;
				if (func == null)
				{
					result = TextLocalizationWrapper.FormatFallback<T>(value, formatter);
				}
				else
				{
					result = func(value, loc);
				}
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public static object FormatFallback<T>(T value, object formatter)
		{
			Debug.LogWarning(string.Format("Argument type {0}", typeof(T)) + string.Format(" does not match formatter type {0}.", formatter.GetType()));
			return value;
		}

		// Token: 0x0400006D RID: 109
		public readonly string _textTemplate;

		// Token: 0x0400006E RID: 110
		public object _textLocalization;
	}
}
