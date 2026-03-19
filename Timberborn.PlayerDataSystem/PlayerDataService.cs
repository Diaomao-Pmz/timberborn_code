using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.PlayerDataSystem
{
	// Token: 0x02000008 RID: 8
	public class PlayerDataService : ILoadableSingleton, IPlayerDataService
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000254A File Offset: 0x0000074A
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002552 File Offset: 0x00000752
		public bool DataLoadSuccessful { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x0000255B File Offset: 0x0000075B
		public PlayerDataService(PlayerDataSerializer playerDataSerializer, PlayerDataFileService playerDataFileService)
		{
			this._playerDataSerializer = playerDataSerializer;
			this._playerDataFileService = playerDataFileService;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002574 File Offset: 0x00000774
		public void Load()
		{
			bool dataLoadSuccessful;
			this._playerData = this._playerDataSerializer.LoadData(out dataLoadSuccessful);
			this.DataLoadSuccessful = dataLoadSuccessful;
			if (this.DataLoadSuccessful)
			{
				this._playerDataFileService.BackupFile();
				return;
			}
			string str = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd-HH\\hmm\\mss\\s");
			this._playerDataFileService.CopyFile("corrupted." + str);
			this._playerDataFileService.RestoreFromBackup();
			bool flag;
			this._playerData = this._playerDataSerializer.LoadData(out flag);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025FE File Offset: 0x000007FE
		public bool HasKey(string key)
		{
			return this._playerData.ContainsKey(key);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000260C File Offset: 0x0000080C
		public bool GetBool(string key, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(this._playerData.GetOrDefault(key), out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002631 File Offset: 0x00000831
		public string GetString(string key, string defaultValue)
		{
			return this._playerData.GetOrDefault(key) ?? defaultValue;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002644 File Offset: 0x00000844
		public void SetBool(string key, bool value)
		{
			this.Set(key, value.ToString());
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002654 File Offset: 0x00000854
		public void SetString(string key, string value)
		{
			this.Set(key, value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000265E File Offset: 0x0000085E
		public void Remove(string key)
		{
			this._playerData.Remove(key);
			this.Save();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002673 File Offset: 0x00000873
		public void RemoveAll()
		{
			this._playerData.Clear();
			this.Save();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002686 File Offset: 0x00000886
		public void Set(string key, string value)
		{
			this._playerData[key] = value;
			this.Save();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000269B File Offset: 0x0000089B
		public void Save()
		{
			this._playerDataSerializer.SaveData(this._playerData);
		}

		// Token: 0x0400000F RID: 15
		public readonly PlayerDataSerializer _playerDataSerializer;

		// Token: 0x04000010 RID: 16
		public readonly PlayerDataFileService _playerDataFileService;

		// Token: 0x04000011 RID: 17
		public Dictionary<string, string> _playerData;
	}
}
