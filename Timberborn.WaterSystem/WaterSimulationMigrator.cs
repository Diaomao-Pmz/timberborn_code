using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000035 RID: 53
	[BackwardCompatible(2025, 7, 16, Compatibility.Map)]
	public class WaterSimulationMigrator : ILoadableSingleton, ISaveableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000056C6 File Offset: 0x000038C6
		public WaterSimulationMigrator(ISingletonLoader singletonLoader, EventBus eventBus)
		{
			this._singletonLoader = singletonLoader;
			this._eventBus = eventBus;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000056E7 File Offset: 0x000038E7
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(WaterSimulationMigrator.WaterSimulationMigratorKey).Set(WaterSimulationMigrator.IsMigratedKey, this._isMigrated);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005704 File Offset: 0x00003904
		public void Load()
		{
			IObjectLoader objectLoader;
			this._isMigrated = (this._singletonLoader.TryGetSingleton(WaterSimulationMigrator.WaterSimulationMigratorKey, out objectLoader) && objectLoader.Get(WaterSimulationMigrator.IsMigratedKey));
			if (!this._isMigrated)
			{
				this._isMigrationScheduled = true;
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005754 File Offset: 0x00003954
		public void PostLoad()
		{
			if (this._isMigrationScheduled)
			{
				this.MigrateWaterSources();
				this._eventBus.Unregister(this);
				this._isMigrated = true;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005778 File Offset: 0x00003978
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			IWaterSource component = blockObjectSetEvent.BlockObject.GetComponent<IWaterSource>();
			if (component != null)
			{
				this._waterSourcesToMigrate.Add(component);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000057A4 File Offset: 0x000039A4
		public void MigrateOutflows(Span<ColumnOutflows> outflows)
		{
			if (this._isMigrationScheduled)
			{
				int length = outflows.Length;
				for (int i = 0; i < length; i++)
				{
					WaterSimulationMigrator.ScaleOutflows(outflows[i]);
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057DC File Offset: 0x000039DC
		public void MigrateWaterSources()
		{
			foreach (IWaterSource waterSource in this._waterSourcesToMigrate)
			{
				waterSource.SetSpecifiedStrength(WaterSimulationMigrator.ScaleRatio * waterSource.SpecifiedStrength);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000583C File Offset: 0x00003A3C
		public static void ScaleOutflows(ref ColumnOutflows outflows)
		{
			outflows.BottomFlow = new TargetedFlow(outflows.BottomFlow.Flow * WaterSimulationMigrator.ScaleRatio, outflows.BottomFlow.Index3D);
			outflows.LeftFlow = new TargetedFlow(outflows.LeftFlow.Flow * WaterSimulationMigrator.ScaleRatio, outflows.LeftFlow.Index3D);
			outflows.TopFlow = new TargetedFlow(outflows.TopFlow.Flow * WaterSimulationMigrator.ScaleRatio, outflows.TopFlow.Index3D);
			outflows.RightFlow = new TargetedFlow(outflows.RightFlow.Flow * WaterSimulationMigrator.ScaleRatio, outflows.RightFlow.Index3D);
			if (outflows.Outflows != null)
			{
				for (int i = 0; i < outflows.Outflows.Count; i++)
				{
					TargetedFlow targetedFlow = outflows.Outflows[i];
					outflows.Outflows[i] = new TargetedFlow(targetedFlow.Flow * WaterSimulationMigrator.ScaleRatio, targetedFlow.Index3D);
				}
			}
		}

		// Token: 0x040000D4 RID: 212
		public static readonly float ScaleRatio = 0.5f;

		// Token: 0x040000D5 RID: 213
		public static readonly SingletonKey WaterSimulationMigratorKey = new SingletonKey("WaterSimulationMigrator");

		// Token: 0x040000D6 RID: 214
		public static readonly PropertyKey<bool> IsMigratedKey = new PropertyKey<bool>("IsMigrated");

		// Token: 0x040000D7 RID: 215
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x040000D8 RID: 216
		public readonly EventBus _eventBus;

		// Token: 0x040000D9 RID: 217
		public readonly HashSet<IWaterSource> _waterSourcesToMigrate = new HashSet<IWaterSource>();

		// Token: 0x040000DA RID: 218
		public bool _isMigrated;

		// Token: 0x040000DB RID: 219
		public bool _isMigrationScheduled;
	}
}
