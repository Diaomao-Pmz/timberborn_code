using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.SaveSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.GameSaveRuntimeSystem
{
	// Token: 0x02000005 RID: 5
	public class GameSaver
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002159 File Offset: 0x00000359
		public GameSaver(GameSaveRepository gameSaveRepository, Ticker ticker, SaveWriter saveWriter)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._ticker = ticker;
			this._saveWriter = saveWriter;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002181 File Offset: 0x00000381
		public void SaveInstantlySkippingNameValidation(SaveReference saveReference, Action onSaveCompleted)
		{
			this.Save(new GameSaver.QueuedSave(saveReference, true, onSaveCompleted));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002191 File Offset: 0x00000391
		public void QueueSaveSkippingNameValidation(SaveReference saveReference, Action onSaveCompleted)
		{
			this._queuedSave = new GameSaver.QueuedSave?(new GameSaver.QueuedSave(saveReference, true, onSaveCompleted));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A6 File Offset: 0x000003A6
		public void QueueSave(SaveReference saveReference, Action onSaveCompleted)
		{
			this._queuedSave = new GameSaver.QueuedSave?(new GameSaver.QueuedSave(saveReference, false, onSaveCompleted));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BB File Offset: 0x000003BB
		public void SaveWithoutFinishingTick(Stream stream)
		{
			this._saveWriter.WriteToSaveStream(stream, false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		public ImmutableArray<TimeSpan> BenchmarkSavingToMemory(int saveCount)
		{
			List<TimeSpan> list = new List<TimeSpan>();
			this._ticker.FinishFullTick();
			Stopwatch stopwatch = new Stopwatch();
			for (int i = 0; i < saveCount; i++)
			{
				stopwatch.Restart();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this.SaveWithoutFinishingTick(memoryStream);
					list.Add(stopwatch.Elapsed);
				}
			}
			return list.ToImmutableArray<TimeSpan>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002240 File Offset: 0x00000440
		public void SaveQueued()
		{
			if (this._queuedSave != null)
			{
				this.Save(this._queuedSave.Value);
				this._queuedSave = null;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		public void Save(GameSaver.QueuedSave queuedSave)
		{
			Debug.Log(string.Format("Saving game to {0} at {1:u}", queuedSave.SaveReference, DateTime.Now));
			this._ticker.FinishFullTick();
			this._stopwatch.Restart();
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._saveWriter.WriteToSaveStream(memoryStream, true);
					using (Stream stream = queuedSave.SkipNameValidation ? this._gameSaveRepository.CreateSaveSkippingNameValidation(queuedSave.SaveReference) : this._gameSaveRepository.CreateSave(queuedSave.SaveReference))
					{
						memoryStream.Position = 0L;
						memoryStream.CopyTo(stream);
						Action onSaveCompleted = queuedSave.OnSaveCompleted;
						if (onSaveCompleted != null)
						{
							onSaveCompleted();
						}
					}
				}
			}
			catch (Exception ex) when (ex is IOException || ex is ArgumentException)
			{
				throw new GameSaverException(ex.Message, ex);
			}
			this._stopwatch.Stop();
			float num = (float)this._stopwatch.ElapsedMilliseconds / 1000f;
			Debug.Log(string.Format("Saved game in {0:0.00}s", num));
		}

		// Token: 0x0400000A RID: 10
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x0400000B RID: 11
		public readonly Ticker _ticker;

		// Token: 0x0400000C RID: 12
		public readonly SaveWriter _saveWriter;

		// Token: 0x0400000D RID: 13
		public readonly Stopwatch _stopwatch = new Stopwatch();

		// Token: 0x0400000E RID: 14
		public GameSaver.QueuedSave? _queuedSave;

		// Token: 0x02000006 RID: 6
		public readonly struct QueuedSave
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000023BC File Offset: 0x000005BC
			public SaveReference SaveReference { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000013 RID: 19 RVA: 0x000023C4 File Offset: 0x000005C4
			public bool SkipNameValidation { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000014 RID: 20 RVA: 0x000023CC File Offset: 0x000005CC
			public Action OnSaveCompleted { get; }

			// Token: 0x06000015 RID: 21 RVA: 0x000023D4 File Offset: 0x000005D4
			public QueuedSave(SaveReference saveReference, bool skipNameValidation, Action onSaveCompleted)
			{
				this.SaveReference = saveReference;
				this.SkipNameValidation = skipNameValidation;
				this.OnSaveCompleted = onSaveCompleted;
			}
		}
	}
}
