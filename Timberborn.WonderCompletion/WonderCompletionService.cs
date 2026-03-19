using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.PlayerDataSystem;

namespace Timberborn.WonderCompletion
{
	// Token: 0x02000005 RID: 5
	public class WonderCompletionService
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public WonderCompletionService(IPlayerDataService playerDataService)
		{
			this._playerDataService = playerDataService;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EB File Offset: 0x000002EB
		public bool IsWonderCompletedWithAnyFaction(string mapName, bool isResource)
		{
			return this._playerDataService.HasKey(WonderCompletionService.GetKey(mapName, isResource));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public IEnumerable<string> GetWonderCompletionFactionIds(string mapName, bool isResource)
		{
			string key = WonderCompletionService.GetKey(mapName, isResource);
			return this._playerDataService.GetString(key, string.Empty).Split(WonderCompletionService.Separator, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public void CompleteWonder(string mapName, bool isResource, string factionId)
		{
			string key = WonderCompletionService.GetKey(mapName, isResource);
			string @string = this._playerDataService.GetString(key, string.Empty);
			this._factionIdCache.AddRange(@string.Split(WonderCompletionService.Separator, StringSplitOptions.RemoveEmptyEntries));
			this._factionIdCache.Add(factionId);
			this._playerDataService.SetString(key, string.Join(WonderCompletionService.Separator, this._factionIdCache));
			this._factionIdCache.Clear();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A6 File Offset: 0x000003A6
		public void RevokeWonderCompletionForAllFactions(string mapName, bool isResource)
		{
			this._playerDataService.Remove(WonderCompletionService.GetKey(mapName, isResource));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public static string GetKey(string mapName, bool isResource)
		{
			string str = isResource ? WonderCompletionService.ResourceMapPrefix : string.Empty;
			return WonderCompletionService.DataPrefix + str + mapName;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ResourceMapPrefix = "Resource_";

		// Token: 0x04000007 RID: 7
		public static readonly string DataPrefix = "CompletedWonders_";

		// Token: 0x04000008 RID: 8
		public static readonly string Separator = ";";

		// Token: 0x04000009 RID: 9
		public readonly IPlayerDataService _playerDataService;

		// Token: 0x0400000A RID: 10
		public readonly HashSet<string> _factionIdCache = new HashSet<string>();
	}
}
