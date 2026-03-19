using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.MainMenuSceneLoading;
using Timberborn.SettlementNameSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Autosaving
{
	// Token: 0x02000009 RID: 9
	public class Autosaver : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		public Autosaver(AutosaveNameService autosaveNameService, GameSaver gameSaver, EventBus eventBus, GameSaveRepository gameSaveRepository, SettlementReferenceService settlementReferenceService, ISpecService specService, IEnumerable<IAutosaveBlocker> autosaveBlockers)
		{
			this._autosaveNameService = autosaveNameService;
			this._gameSaver = gameSaver;
			this._eventBus = eventBus;
			this._gameSaveRepository = gameSaveRepository;
			this._settlementReferenceService = settlementReferenceService;
			this._specService = specService;
			this._autosaveBlockers = autosaveBlockers.ToImmutableArray<IAutosaveBlocker>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002225 File Offset: 0x00000425
		public void Load()
		{
			this._autosaverSpec = this._specService.GetSingleSpec<AutosaverSpec>();
			this._eventBus.Register(this);
			this.ScheduleNextSave();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000224A File Offset: 0x0000044A
		public void UpdateSingleton()
		{
			if (this.TimeToSave() && this.IsNotBlocked())
			{
				this.Save(false);
				this.ScheduleNextSave();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002269 File Offset: 0x00000469
		[OnEvent]
		public void OnPreMainMenuStarted(PreMainMenuStartedEvent preMainMenuStartedEvent)
		{
			if (!preMainMenuStartedEvent.SkipAutoSave)
			{
				this.CreateExitSave();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002279 File Offset: 0x00000479
		public void CreateExitSave()
		{
			if (!Application.isEditor && !this._exitSaveCreated && this._settlementReferenceService.SettlementReference != null)
			{
				this.Save(true);
				this._exitSaveCreated = true;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AB File Offset: 0x000004AB
		public void Suspend()
		{
			this._suspended = true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B4 File Offset: 0x000004B4
		public bool TimeToSave()
		{
			return Time.unscaledTime > this._nextSaveTime;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022C4 File Offset: 0x000004C4
		public bool IsNotBlocked()
		{
			ImmutableArray<IAutosaveBlocker>.Enumerator enumerator = this._autosaveBlockers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsBlocking)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022FC File Offset: 0x000004FC
		public void Save(bool instant)
		{
			if (!this._suspended)
			{
				SaveReference saveReference = new SaveReference(this._autosaveNameService.GetAutosaveName(), this._settlementReferenceService.SettlementReference);
				try
				{
					if (instant)
					{
						this._gameSaver.SaveInstantlySkippingNameValidation(saveReference, new Action(this.AutosaveCompleted));
					}
					else
					{
						this._gameSaver.QueueSaveSkippingNameValidation(saveReference, new Action(this.AutosaveCompleted));
					}
				}
				catch (GameSaverException ex)
				{
					Debug.LogError(string.Format("Error occured while saving: {0}", ex.InnerException));
					this._eventBus.Post(AutosaveEvent.CreateFailure(ex));
					this._gameSaveRepository.DeleteSaveSafely(saveReference);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023B0 File Offset: 0x000005B0
		public void AutosaveCompleted()
		{
			this._eventBus.Post(AutosaveEvent.CreateSuccess());
			this.DeleteExcessAutosaves();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C8 File Offset: 0x000005C8
		public void ScheduleNextSave()
		{
			this._nextSaveTime = Time.unscaledTime + this._autosaverSpec.FrequencyInMinutes * 60f;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023E8 File Offset: 0x000005E8
		public void DeleteExcessAutosaves()
		{
			ImmutableArray<SaveReference> autosaves = (from save in this._gameSaveRepository.GetSaves(this._settlementReferenceService.SettlementReference)
			where this._autosaveNameService.IsAutosaveName(save.SaveName)
			select save).ToImmutableArray<SaveReference>();
			this.DeleteExcessAutosaves(autosaves);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000242C File Offset: 0x0000062C
		public void DeleteExcessAutosaves(ImmutableArray<SaveReference> autosaves)
		{
			int num = 0;
			for (int i = autosaves.Length - 1; i >= this._autosaverSpec.AutosavesPerSettlement; i--)
			{
				SaveReference saveReference = autosaves[i];
				if (this._gameSaveRepository.DeleteSaveSafely(saveReference))
				{
					Debug.Log(string.Format("Deleted excess autosave {0}", saveReference));
				}
				else
				{
					Debug.LogError(string.Format("Failed to delete excess autosave {0}", saveReference));
					num++;
				}
			}
			if (num >= 3)
			{
				throw new InvalidOperationException(string.Format("Failed to delete {0} autosaves.", num));
			}
		}

		// Token: 0x0400000D RID: 13
		public readonly AutosaveNameService _autosaveNameService;

		// Token: 0x0400000E RID: 14
		public readonly GameSaver _gameSaver;

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000011 RID: 17
		public readonly SettlementReferenceService _settlementReferenceService;

		// Token: 0x04000012 RID: 18
		public readonly ISpecService _specService;

		// Token: 0x04000013 RID: 19
		public readonly ImmutableArray<IAutosaveBlocker> _autosaveBlockers;

		// Token: 0x04000014 RID: 20
		public AutosaverSpec _autosaverSpec;

		// Token: 0x04000015 RID: 21
		public float _nextSaveTime;

		// Token: 0x04000016 RID: 22
		public bool _suspended;

		// Token: 0x04000017 RID: 23
		public bool _exitSaveCreated;
	}
}
