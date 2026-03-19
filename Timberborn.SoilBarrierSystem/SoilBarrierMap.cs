using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.SoilBarrierSystem
{
	// Token: 0x02000009 RID: 9
	[MapEditorTickable]
	public class SoilBarrierMap : ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023FF File Offset: 0x000005FF
		public SoilBarrierMap(MapIndexService mapIndexService)
		{
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002419 File Offset: 0x00000619
		public ReadOnlyArray<bool> AboveMoistureBarriers
		{
			get
			{
				return new ReadOnlyArray<bool>(this._aboveMoistureBarriers);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002426 File Offset: 0x00000626
		public ReadOnlyArray<bool> FullMoistureBarriers
		{
			get
			{
				return new ReadOnlyArray<bool>(this._fullMoistureBarriers);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002433 File Offset: 0x00000633
		public ReadOnlyArray<bool> ContaminationBarriers
		{
			get
			{
				return new ReadOnlyArray<bool>(this._contaminationBarriers);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002440 File Offset: 0x00000640
		public void Load()
		{
			int maxSize3D = this._mapIndexService.MaxSize3D;
			this._aboveMoistureBarriers = new bool[maxSize3D];
			this._fullMoistureBarriers = new bool[maxSize3D];
			this._contaminationBarriers = new bool[maxSize3D];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000247D File Offset: 0x0000067D
		public void AddAboveMoistureBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(true, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.AboveMoisture));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002498 File Offset: 0x00000698
		public void RemoveAboveMoistureBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(false, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.AboveMoisture));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B3 File Offset: 0x000006B3
		public void AddFullMoistureBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(true, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.FullMoisture));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024CE File Offset: 0x000006CE
		public void RemoveFullMoistureBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(false, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.FullMoisture));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024E9 File Offset: 0x000006E9
		public void AddContaminationBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(true, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.Contamination));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002504 File Offset: 0x00000704
		public void RemoveContaminationBarrierAt(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new SoilBarrierMap.Modification(false, this.GetIndex(coordinates), SoilBarrierMap.BarrierType.Contamination));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000251F File Offset: 0x0000071F
		public void Tick()
		{
			this.ProcessModifications();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002528 File Offset: 0x00000728
		public void ProcessModifications()
		{
			while (!this._modifications.IsEmpty<SoilBarrierMap.Modification>())
			{
				SoilBarrierMap.Modification modification = this._modifications.Dequeue();
				switch (modification.BarrierType)
				{
				case SoilBarrierMap.BarrierType.AboveMoisture:
					this._aboveMoistureBarriers[modification.Index] = modification.Added;
					break;
				case SoilBarrierMap.BarrierType.FullMoisture:
					this._fullMoistureBarriers[modification.Index] = modification.Added;
					break;
				case SoilBarrierMap.BarrierType.Contamination:
					this._contaminationBarriers[modification.Index] = modification.Added;
					break;
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025AF File Offset: 0x000007AF
		public int GetIndex(Vector3Int coordinates)
		{
			return this._mapIndexService.CoordinatesToIndex3D(coordinates);
		}

		// Token: 0x04000011 RID: 17
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000012 RID: 18
		public readonly Queue<SoilBarrierMap.Modification> _modifications = new Queue<SoilBarrierMap.Modification>();

		// Token: 0x04000013 RID: 19
		public bool[] _aboveMoistureBarriers;

		// Token: 0x04000014 RID: 20
		public bool[] _fullMoistureBarriers;

		// Token: 0x04000015 RID: 21
		public bool[] _contaminationBarriers;

		// Token: 0x0200000A RID: 10
		public readonly struct Modification
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000023 RID: 35 RVA: 0x000025BD File Offset: 0x000007BD
			public bool Added { get; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000024 RID: 36 RVA: 0x000025C5 File Offset: 0x000007C5
			public int Index { get; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000025 RID: 37 RVA: 0x000025CD File Offset: 0x000007CD
			public SoilBarrierMap.BarrierType BarrierType { get; }

			// Token: 0x06000026 RID: 38 RVA: 0x000025D5 File Offset: 0x000007D5
			public Modification(bool added, int index, SoilBarrierMap.BarrierType barrierType)
			{
				this.Added = added;
				this.Index = index;
				this.BarrierType = barrierType;
			}
		}

		// Token: 0x0200000B RID: 11
		public enum BarrierType
		{
			// Token: 0x0400001A RID: 26
			AboveMoisture,
			// Token: 0x0400001B RID: 27
			FullMoisture,
			// Token: 0x0400001C RID: 28
			Contamination
		}
	}
}
