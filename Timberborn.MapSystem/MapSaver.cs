using System;
using System.IO;
using Timberborn.MapRepositorySystem;
using Timberborn.SaveSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.MapSystem
{
	// Token: 0x02000005 RID: 5
	public class MapSaver
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002135 File Offset: 0x00000335
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000213D File Offset: 0x0000033D
		public MapFileReference? LastSavedMap { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002146 File Offset: 0x00000346
		public MapSaver(MapRepository mapRepository, SaveWriter saveWriter, Ticker ticker)
		{
			this._mapRepository = mapRepository;
			this._saveWriter = saveWriter;
			this._ticker = ticker;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public void Save(MapFileReference mapFileReference)
		{
			string name = mapFileReference.Name;
			Debug.Log(string.Format("Saving map to {0} at {1:u}", name, DateTime.Now));
			this._ticker.FinishFullTick();
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._saveWriter.WriteToSaveStream(memoryStream, true);
					using (Stream stream = this._mapRepository.CreateUserMap(name))
					{
						memoryStream.Position = 0L;
						memoryStream.CopyTo(stream);
						this.LastSavedMap = new MapFileReference?(mapFileReference);
					}
				}
			}
			catch (Exception ex) when (ex is IOException || ex is ArgumentException)
			{
				throw new MapSaverException(ex.Message, ex.InnerException);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002254 File Offset: 0x00000454
		public void Save(Stream stream)
		{
			this._saveWriter.WriteToSaveStream(stream, false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002264 File Offset: 0x00000464
		public bool MapExists(string mapName)
		{
			bool result;
			try
			{
				result = this._mapRepository.UserMapExists(mapName);
			}
			catch (IOException ex)
			{
				throw new MapSaverException(ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		public readonly MapRepository _mapRepository;

		// Token: 0x0400000B RID: 11
		public readonly SaveWriter _saveWriter;

		// Token: 0x0400000C RID: 12
		public readonly Ticker _ticker;
	}
}
