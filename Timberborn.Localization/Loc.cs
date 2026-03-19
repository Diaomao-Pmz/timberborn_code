using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x02000008 RID: 8
	public class Loc : ILoc
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000020EF File Offset: 0x000002EF
		public void Initialize(Dictionary<string, string> localization)
		{
			this._localizationCache.Clear();
			this._localization = localization;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002103 File Offset: 0x00000303
		public IEnumerable<string> GetRawTexts()
		{
			return this._localization.Values;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002110 File Offset: 0x00000310
		public string T(string key)
		{
			string result;
			if (key != null && this._localization.TryGetValue(key, out result))
			{
				return result;
			}
			Debug.LogError("No localization for " + key);
			return key;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002143 File Offset: 0x00000343
		public string T<T1>(string key, T1 param1)
		{
			return this.T<T1, object, object>(key, param1, null, null);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000214F File Offset: 0x0000034F
		public string T<T1, T2>(string key, T1 param1, T2 param2)
		{
			return this.T<T1, T2, object>(key, param1, param2, null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000215C File Offset: 0x0000035C
		public string T<T1, T2, T3>(string key, T1 param1, T2 param2, T3 param3)
		{
			TextLocalizationWrapper textLocalizationWrapper;
			if (this._localizationCache.TryGetValue(key, out textLocalizationWrapper))
			{
				return textLocalizationWrapper.GetText<T1, T2, T3>(this, param1, param2, param3);
			}
			TextLocalizationWrapper textLocalizationWrapper2 = new TextLocalizationWrapper(this.T(key));
			this._localizationCache.Add(key, textLocalizationWrapper2);
			return textLocalizationWrapper2.GetText<T1, T2, T3>(this, param1, param2, param3);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021AA File Offset: 0x000003AA
		public string T(Phrase phrase)
		{
			return this.T(phrase.Key);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021B8 File Offset: 0x000003B8
		public string T<T1>(Phrase phrase, T1 param1)
		{
			return phrase.GetText<T1, object, object>(this, param1, null, null);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021C4 File Offset: 0x000003C4
		public string T<T1, T2>(Phrase phrase, T1 param1, T2 param2)
		{
			return phrase.GetText<T1, T2, object>(this, param1, param2, null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021D0 File Offset: 0x000003D0
		public string T<T1, T2, T3>(Phrase phrase, T1 param1, T2 param2, T3 param3)
		{
			return phrase.GetText<T1, T2, T3>(this, param1, param2, param3);
		}

		// Token: 0x04000009 RID: 9
		public Dictionary<string, string> _localization;

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, TextLocalizationWrapper> _localizationCache = new Dictionary<string, TextLocalizationWrapper>();
	}
}
