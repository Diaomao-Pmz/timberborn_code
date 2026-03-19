using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000009 RID: 9
	public class BuildExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000023F2 File Offset: 0x000005F2
		public BuildExecutor(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002401 File Offset: 0x00000601
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._builder = base.GetComponent<Builder>();
			this._worker = base.GetComponent<Worker>();
			this._characterModel = base.GetComponent<CharacterModel>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002434 File Offset: 0x00000634
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._constructionSite || !this._constructionSite.IsOn)
			{
				this.StopBuilding();
				return ExecutorStatus.Failure;
			}
			if (!this._constructionSite.HasMaterialsToResumeBuilding || this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				this.StopBuilding();
				return ExecutorStatus.Success;
			}
			this._constructionSite.IncreaseBuildTime(deltaTimeInHours * this._worker.WorkingSpeedMultiplier);
			return ExecutorStatus.Running;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024A4 File Offset: 0x000006A4
		public void Save(IEntitySaver entitySaver)
		{
			if (this._constructionSite)
			{
				entitySaver.GetComponent(BuildExecutor.BuildExecutorKey).Set(BuildExecutor.FinishTimestampKey, this._finishTimestamp);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024D0 File Offset: 0x000006D0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BuildExecutor.BuildExecutorKey, out objectLoader))
			{
				this._finishTimestamp = objectLoader.Get(BuildExecutor.FinishTimestampKey);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024FD File Offset: 0x000006FD
		public bool Launch(ConstructionSite constructionSite)
		{
			if (constructionSite.HasMaterialsToResumeBuilding)
			{
				this.Initialize(constructionSite);
				this.LookTowardConstructionSite();
				this._finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(BuildExecutor.MaxBuildTimeInHours);
				return true;
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000252D File Offset: 0x0000072D
		public void InitializeAfterLoad(ConstructionSite constructionSite)
		{
			if (constructionSite && this._finishTimestamp > 0f)
			{
				this.Initialize(constructionSite);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000254B File Offset: 0x0000074B
		public void Initialize(ConstructionSite constructionSite)
		{
			this._constructionSite = constructionSite;
			this._constructionSiteHasSlots = constructionSite.GetComponent<ConstructionSiteSlotManager>();
			this.ToggleBuildingAnimation(true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000256C File Offset: 0x0000076C
		public void ToggleBuildingAnimation(bool value)
		{
			if (!this._constructionSiteHasSlots)
			{
				this._characterAnimator.SetBool("Building", value);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002588 File Offset: 0x00000788
		public void LookTowardConstructionSite()
		{
			Vector3 worldCenter = this._constructionSite.GetComponent<BlockObjectCenter>().WorldCenter;
			this._characterModel.LookToward(worldCenter);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025B2 File Offset: 0x000007B2
		public void StopBuilding()
		{
			this._builder.Unreserve();
			this.ToggleBuildingAnimation(false);
		}

		// Token: 0x04000013 RID: 19
		public static readonly float MaxBuildTimeInHours = 0.5f;

		// Token: 0x04000014 RID: 20
		public static readonly ComponentKey BuildExecutorKey = new ComponentKey("BuildExecutor");

		// Token: 0x04000015 RID: 21
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x04000016 RID: 22
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000017 RID: 23
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000018 RID: 24
		public Builder _builder;

		// Token: 0x04000019 RID: 25
		public Worker _worker;

		// Token: 0x0400001A RID: 26
		public CharacterModel _characterModel;

		// Token: 0x0400001B RID: 27
		public ConstructionSite _constructionSite;

		// Token: 0x0400001C RID: 28
		public float _finishTimestamp;

		// Token: 0x0400001D RID: 29
		public bool _constructionSiteHasSlots;
	}
}
