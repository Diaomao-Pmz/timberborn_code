using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.ErrorReporting
{
	// Token: 0x0200000C RID: 12
	public class LoadingIssueService : ILoadingIssueService
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029C8 File Offset: 0x00000BC8
		public bool HasAnyIssues
		{
			get
			{
				return this._issues.Count > 0;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029D8 File Offset: 0x00000BD8
		public IEnumerable<ValueTuple<LoadingIssueMessage, int>> GetIssues()
		{
			foreach (KeyValuePair<LoadingIssueMessage, int> keyValuePair in this._issues)
			{
				yield return new ValueTuple<LoadingIssueMessage, int>(keyValuePair.Key, keyValuePair.Value);
			}
			Dictionary<LoadingIssueMessage, int>.Enumerator enumerator = default(Dictionary<LoadingIssueMessage, int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E8 File Offset: 0x00000BE8
		public void AddIssue(string warningText, string messageLocKey, string messageParam = null, bool paramIsLocKey = false)
		{
			Debug.LogWarning(warningText);
			LoadingIssueMessage key = new LoadingIssueMessage(messageLocKey, messageParam, paramIsLocKey);
			int num;
			if (this._issues.TryGetValue(key, out num))
			{
				this._issues[key] = num + 1;
				return;
			}
			this._issues[key] = 1;
		}

		// Token: 0x04000017 RID: 23
		public readonly Dictionary<LoadingIssueMessage, int> _issues = new Dictionary<LoadingIssueMessage, int>();
	}
}
