using System;
using System.Collections.Generic;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.WorkerTypes;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200002A RID: 42
	public class WorkplaceUnlockingService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00004A00 File Offset: 0x00002C00
		public WorkplaceUnlockingService(MapEditorMode mapEditorMode, TemplateService templateService, TemplateNameMapper templateNameMapper, ScienceService scienceService, ISingletonLoader singletonLoader, UnlockableWorkerTypeSerializer unlockableWorkerTypeSerializer, WorkerTypeService workerTypeService)
		{
			this._mapEditorMode = mapEditorMode;
			this._templateService = templateService;
			this._templateNameMapper = templateNameMapper;
			this._scienceService = scienceService;
			this._singletonLoader = singletonLoader;
			this._unlockableWorkerTypeSerializer = unlockableWorkerTypeSerializer;
			this._workerTypeService = workerTypeService;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004A5E File Offset: 0x00002C5E
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(WorkplaceUnlockingService.WorkplaceUnlockingServiceKey).Set<UnlockableWorkerType>(WorkplaceUnlockingService.UnlockedWorkerTypesKey, this._unlockedWorkerTypes, this._unlockableWorkerTypeSerializer);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004A90 File Offset: 0x00002C90
		public void Load()
		{
			this.FillWorkerTypeUnlockCosts();
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WorkplaceUnlockingService.WorkplaceUnlockingServiceKey, out objectLoader))
			{
				foreach (UnlockableWorkerType unlockableWorkerType in objectLoader.Get<UnlockableWorkerType>(WorkplaceUnlockingService.UnlockedWorkerTypesKey, this._unlockableWorkerTypeSerializer))
				{
					TemplateSpec templateSpec;
					if (this._templateNameMapper.TryGetTemplate(unlockableWorkerType.WorkplaceTemplateName, out templateSpec))
					{
						string workerType = this._workerTypeService.GetWorkerType(unlockableWorkerType.WorkerType);
						this._unlockedWorkerTypes.Add(new UnlockableWorkerType(templateSpec.TemplateName, workerType));
					}
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004B44 File Offset: 0x00002D44
		public bool Unlocked(UnlockableWorkerType unlockableWorkerType)
		{
			return this.GetUnlockCost(unlockableWorkerType) <= 0 || this._unlockedWorkerTypes.Contains(unlockableWorkerType);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004B60 File Offset: 0x00002D60
		public void Unlock(UnlockableWorkerType unlockableWorkerType)
		{
			if (!this.Unlockable(unlockableWorkerType))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Can't unlock ",
					unlockableWorkerType.WorkerType,
					" workplace in ",
					unlockableWorkerType.WorkplaceTemplateName,
					", not enough science points!"
				}));
			}
			this._scienceService.SubtractPoints(this.GetUnlockCost(unlockableWorkerType));
			this.UnlockIgnoringCost(unlockableWorkerType);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004BCC File Offset: 0x00002DCC
		public void UnlockIgnoringCost(UnlockableWorkerType unlockableWorkerType)
		{
			this._unlockedWorkerTypes.Add(unlockableWorkerType);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004BDB File Offset: 0x00002DDB
		public bool Unlockable(UnlockableWorkerType unlockableWorkerType)
		{
			return this.GetUnlockCost(unlockableWorkerType) <= this._scienceService.SciencePoints;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public int GetUnlockCost(UnlockableWorkerType unlockableWorkerType)
		{
			int result;
			if (this._unlockableWorkerTypeCosts.TryGetValue(unlockableWorkerType, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004C14 File Offset: 0x00002E14
		public void FillWorkerTypeUnlockCosts()
		{
			foreach (WorkplaceSpec workplaceSpec in this._templateService.GetAll<WorkplaceSpec>())
			{
				string templateName = workplaceSpec.GetSpec<TemplateSpec>().TemplateName;
				foreach (WorkerTypeUnlockCost workerTypeUnlockCost in workplaceSpec.WorkerTypeUnlockCosts)
				{
					UnlockableWorkerType key = new UnlockableWorkerType(templateName, workerTypeUnlockCost.WorkerType);
					this._unlockableWorkerTypeCosts.Add(key, workerTypeUnlockCost.ScienceCost);
				}
			}
		}

		// Token: 0x0400006D RID: 109
		public static readonly SingletonKey WorkplaceUnlockingServiceKey = new SingletonKey("WorkplaceUnlockingService");

		// Token: 0x0400006E RID: 110
		public static readonly ListKey<UnlockableWorkerType> UnlockedWorkerTypesKey = new ListKey<UnlockableWorkerType>("UnlockedWorkerTypes");

		// Token: 0x0400006F RID: 111
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000070 RID: 112
		public readonly TemplateService _templateService;

		// Token: 0x04000071 RID: 113
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000072 RID: 114
		public readonly ScienceService _scienceService;

		// Token: 0x04000073 RID: 115
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000074 RID: 116
		public readonly UnlockableWorkerTypeSerializer _unlockableWorkerTypeSerializer;

		// Token: 0x04000075 RID: 117
		public readonly WorkerTypeService _workerTypeService;

		// Token: 0x04000076 RID: 118
		public readonly Dictionary<UnlockableWorkerType, int> _unlockableWorkerTypeCosts = new Dictionary<UnlockableWorkerType, int>();

		// Token: 0x04000077 RID: 119
		public readonly HashSet<UnlockableWorkerType> _unlockedWorkerTypes = new HashSet<UnlockableWorkerType>();
	}
}
