using System;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000019 RID: 25
	public class WaterEvaporationMap : ISaveableSingleton, ILoadableSingleton, ITickableSingleton, IThreadSafeWaterEvaporationMap
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000048A8 File Offset: 0x00002AA8
		public WaterEvaporationMap(ISingletonLoader singletonLoader, MapIndexService mapIndexService, FloatPackedListSerializer floatPackedListSerializer)
		{
			this._singletonLoader = singletonLoader;
			this._mapIndexService = mapIndexService;
			this._floatPackedListSerializer = floatPackedListSerializer;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000048D0 File Offset: 0x00002AD0
		public float[] UnsafeEvaporationModifiers
		{
			get
			{
				return this._evaporationModifiers.Current;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000048DD File Offset: 0x00002ADD
		public ReadOnlyArray<float> EvaporationModifiers
		{
			get
			{
				return new ReadOnlyArray<float>(this._evaporationModifiers.Buffered);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000048EF File Offset: 0x00002AEF
		public void Load()
		{
			this._evaporationModifiers.Initialize(this._mapIndexService.VerticalStride);
			this._evaporationModifiers.Fill(1f);
			this._evaporationModifiers.Unify();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004924 File Offset: 0x00002B24
		[BackwardCompatible(2024, 1, 24, Compatibility.Map)]
		public unsafe void LoadData(ReadOnlyArray<byte> threadSafeColumnCounts)
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WaterEvaporationMap.WaterEvaporationMapKey, out objectLoader))
			{
				int levels = objectLoader.Has<int>(WaterEvaporationMap.LevelsKey) ? objectLoader.Get(WaterEvaporationMap.LevelsKey) : 1;
				PackedList<float> packedList = objectLoader.Get<PackedList<float>>(WaterEvaporationMap.EvaporationModifiersKey, this._floatPackedListSerializer);
				float[] array = this._mapIndexService.Unpack<float>(packedList, levels);
				int verticalStride = this._mapIndexService.VerticalStride;
				int num = array.Length;
				foreach (int num2 in this._mapIndexService.Indices2D)
				{
					for (int i = 0; i < (int)(*threadSafeColumnCounts[num2]); i++)
					{
						int num3 = i * verticalStride + num2;
						if (num3 < num)
						{
							this._evaporationModifiers.Current[num3] = array[num3];
						}
					}
				}
				this._evaporationModifiers.Unify();
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A08 File Offset: 0x00002C08
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(WaterEvaporationMap.WaterEvaporationMapKey);
			int num = this._evaporationModifiers.Current.Length / this._mapIndexService.VerticalStride;
			singleton.Set(WaterEvaporationMap.LevelsKey, num);
			singleton.Set<PackedList<float>>(WaterEvaporationMap.EvaporationModifiersKey, this._mapIndexService.Pack<float>(this._evaporationModifiers.Current, num), this._floatPackedListSerializer);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A6D File Offset: 0x00002C6D
		public void Tick()
		{
			this._evaporationModifiers.Swap();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A7A File Offset: 0x00002C7A
		public void Resize(int maxColumnCount)
		{
			this._evaporationModifiers.ResizeAndFill(this._mapIndexService.VerticalStride * maxColumnCount, 1f);
		}

		// Token: 0x04000087 RID: 135
		public static readonly SingletonKey WaterEvaporationMapKey = new SingletonKey("WaterEvaporationMap");

		// Token: 0x04000088 RID: 136
		public static readonly PropertyKey<PackedList<float>> EvaporationModifiersKey = new PropertyKey<PackedList<float>>("EvaporationModifiers");

		// Token: 0x04000089 RID: 137
		public static readonly PropertyKey<int> LevelsKey = new PropertyKey<int>("Levels");

		// Token: 0x0400008A RID: 138
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400008B RID: 139
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400008C RID: 140
		public readonly FloatPackedListSerializer _floatPackedListSerializer;

		// Token: 0x0400008D RID: 141
		public readonly BufferedArray<float> _evaporationModifiers = new BufferedArray<float>();
	}
}
