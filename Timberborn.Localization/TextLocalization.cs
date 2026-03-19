using System;
using System.Collections.Generic;

namespace Timberborn.Localization
{
	// Token: 0x0200001D RID: 29
	public class TextLocalization<T1, T2, T3>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003950 File Offset: 0x00001B50
		public string Text { get; private set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003959 File Offset: 0x00001B59
		public TextLocalization(T1 value1, T2 value2, T3 value3, string text)
		{
			this._value1 = value1;
			this._value2 = value2;
			this._value3 = value3;
			this.Text = text;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000397E File Offset: 0x00001B7E
		public void Update(T1 value1, T2 value2, T3 value3, string text)
		{
			this._value1 = value1;
			this._value2 = value2;
			this._value3 = value3;
			this.Text = text;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000399D File Offset: 0x00001B9D
		public bool AreValuesEqual(T1 value1, T2 value2, T3 value3)
		{
			return EqualityComparer<T1>.Default.Equals(this._value1, value1) && EqualityComparer<T2>.Default.Equals(this._value2, value2) && EqualityComparer<T3>.Default.Equals(this._value3, value3);
		}

		// Token: 0x0400006A RID: 106
		public T1 _value1;

		// Token: 0x0400006B RID: 107
		public T2 _value2;

		// Token: 0x0400006C RID: 108
		public T3 _value3;
	}
}
