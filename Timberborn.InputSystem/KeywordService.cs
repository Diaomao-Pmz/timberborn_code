using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;

namespace Timberborn.InputSystem
{
	// Token: 0x02000015 RID: 21
	public class KeywordService : ILoadableSingleton
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003037 File Offset: 0x00001237
		public KeywordService(EventBus eventBus, KeyboardListener keyboardListener)
		{
			this._eventBus = eventBus;
			this._keyboardListener = keyboardListener;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003058 File Offset: 0x00001258
		public void Load()
		{
			this._keyboardListener.KeyPressed += this.OnKeyPressed;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003071 File Offset: 0x00001271
		public void AddKeyword(string keyword, string keywordNotification, Action onMatch)
		{
			this._keywordItems.Add(new KeywordService.KeywordItem(keywordNotification, keyword.ToUpper(), onMatch));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000308B File Offset: 0x0000128B
		public void OnKeyPressed(object sender, KeyPressedEvent e)
		{
			this.CheckKeywords(e.Key);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000309C File Offset: 0x0000129C
		public void CheckKeywords(string key)
		{
			for (int i = 0; i < this._keywordItems.Count; i++)
			{
				KeywordService.KeywordItem keywordItem = this._keywordItems[i];
				this.CheckKeyword(key, keywordItem);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000030D4 File Offset: 0x000012D4
		public void CheckKeyword(string key, KeywordService.KeywordItem keywordItem)
		{
			if (!keywordItem.IsNextCharMatching(key))
			{
				keywordItem.Reset();
			}
			if (keywordItem.IsNextCharMatching(key))
			{
				keywordItem.Increase();
				if (keywordItem.IsKeywordMatching())
				{
					keywordItem.Match();
					this._eventBus.Post(new KeywordMatchedEvent(keywordItem.KeywordNotification));
				}
			}
		}

		// Token: 0x04000038 RID: 56
		public readonly EventBus _eventBus;

		// Token: 0x04000039 RID: 57
		public readonly KeyboardListener _keyboardListener;

		// Token: 0x0400003A RID: 58
		public readonly List<KeywordService.KeywordItem> _keywordItems = new List<KeywordService.KeywordItem>();

		// Token: 0x02000016 RID: 22
		public class KeywordItem
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00003123 File Offset: 0x00001323
			public string KeywordNotification { get; }

			// Token: 0x06000094 RID: 148 RVA: 0x0000312B File Offset: 0x0000132B
			public KeywordItem(string keywordNotification, string keyword, Action onMatch)
			{
				this.KeywordNotification = keywordNotification;
				this._keyword = keyword;
				this._onMatch = onMatch;
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00003148 File Offset: 0x00001348
			public bool IsNextCharMatching(string key)
			{
				return key.Length == 1 && this._keyword[this._position] == key[0];
			}

			// Token: 0x06000096 RID: 150 RVA: 0x0000316F File Offset: 0x0000136F
			public void Increase()
			{
				this._position++;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x0000317F File Offset: 0x0000137F
			public bool IsKeywordMatching()
			{
				return this._position == this._keyword.Length;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003194 File Offset: 0x00001394
			public void Match()
			{
				this.Reset();
				this._onMatch();
			}

			// Token: 0x06000099 RID: 153 RVA: 0x000031A7 File Offset: 0x000013A7
			public void Reset()
			{
				this._position = 0;
			}

			// Token: 0x0400003C RID: 60
			public readonly string _keyword;

			// Token: 0x0400003D RID: 61
			public readonly Action _onMatch;

			// Token: 0x0400003E RID: 62
			public int _position;
		}
	}
}
