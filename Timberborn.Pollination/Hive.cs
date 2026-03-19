using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Pollination
{
	// Token: 0x02000007 RID: 7
	public class Hive : BaseComponent, IAwakableComponent, IFinishedStateListener, IBuildingWithRange, IPersistentEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public Hive(IRandomNumberGenerator randomNumberGenerator, IBlockService blockService, ITimeTriggerFactory timeTriggerFactory)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._blockService = blockService;
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002123 File Offset: 0x00000323
		public string RangeName
		{
			get
			{
				return this._templateSpec.TemplateName;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public void Awake()
		{
			this._blockObjectRange = base.GetComponent<BlockObjectRange>();
			this._templateSpec = base.GetComponent<TemplateSpec>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._hiveSpec = base.GetComponent<HiveSpec>();
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.PollinateNearbyPollinatees), this._hiveSpec.HoursBetweenPollinations / 24f);
			base.DisableComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A1 File Offset: 0x000003A1
		public IEnumerable<BaseComponent> GetObjectsInRange()
		{
			foreach (Vector3Int coordinates in this.GetBlocksInRange())
			{
				Pollinatee bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Pollinatee>(coordinates);
				if (bottomObjectComponentAt != null)
				{
					yield return bottomObjectComponentAt;
				}
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			if (this._blockableObject.IsUnblocked)
			{
				this._timeTrigger.Resume();
			}
			base.EnableComponent();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002210 File Offset: 0x00000410
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._timeTrigger.Pause();
			base.DisableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Hive.HiveKey).Set(Hive.PollinationProgressKey, this._timeTrigger.Progress);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00000480
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Hive.HiveKey);
			this._timeTrigger.FastForwardProgress(component.Get(Hive.PollinationProgressKey));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022AF File Offset: 0x000004AF
		public IEnumerable<Vector3Int> GetBlocksInRange()
		{
			return this._blockObjectRange.GetBlocksOnTerrainInRectangularRadius(this._hiveSpec.PollinationRadius);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C8 File Offset: 0x000004C8
		public void PollinateNearbyPollinatees()
		{
			this.UpdateNearbyPollinatees();
			int num = Mathf.Min(this._hiveSpec.PlantsPerPollination, this._nearbyPollinatees.Count);
			for (int i = 0; i < num; i++)
			{
				Pollinatee listElement = this._randomNumberGenerator.GetListElement<Pollinatee>(this._nearbyPollinatees);
				this._nearbyPollinatees.Remove(listElement);
				listElement.Pollinate(this._hiveSpec.GrowthTimeReduction);
			}
			this._timeTrigger.Reset();
			this._timeTrigger.Resume();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000234C File Offset: 0x0000054C
		public void UpdateNearbyPollinatees()
		{
			this._nearbyPollinatees.Clear();
			foreach (Vector3Int coordinates in this.GetBlocksInRange())
			{
				Pollinatee bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Pollinatee>(coordinates);
				if (bottomObjectComponentAt && bottomObjectComponentAt.CanPollinate)
				{
					this._nearbyPollinatees.Add(bottomObjectComponentAt);
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023C8 File Offset: 0x000005C8
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this._timeTrigger.Pause();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D5 File Offset: 0x000005D5
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this._timeTrigger.Resume();
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey HiveKey = new ComponentKey("Hive");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<float> PollinationProgressKey = new PropertyKey<float>("PollinationProgress");

		// Token: 0x0400000A RID: 10
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		public readonly IBlockService _blockService;

		// Token: 0x0400000C RID: 12
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400000D RID: 13
		public BlockObjectRange _blockObjectRange;

		// Token: 0x0400000E RID: 14
		public TemplateSpec _templateSpec;

		// Token: 0x0400000F RID: 15
		public BlockableObject _blockableObject;

		// Token: 0x04000010 RID: 16
		public HiveSpec _hiveSpec;

		// Token: 0x04000011 RID: 17
		public ITimeTrigger _timeTrigger;

		// Token: 0x04000012 RID: 18
		public readonly List<Pollinatee> _nearbyPollinatees = new List<Pollinatee>();
	}
}
