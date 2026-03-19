using System;
using System.Text;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000030 RID: 48
	public class WaterMapLoader
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004B5A File Offset: 0x00002D5A
		public WaterMapLoader(MapIndexService mapIndexService, FloatPackedListSerializer floatPackedListSerializer, ISingletonLoader singletonLoader)
		{
			this._mapIndexService = mapIndexService;
			this._floatPackedListSerializer = floatPackedListSerializer;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004B78 File Offset: 0x00002D78
		[BackwardCompatible(2023, 11, 7, Compatibility.Map)]
		public void Load(Span<WaterColumn> waterColumns, Span<ColumnOutflows> outflows)
		{
			IObjectLoader waterMap;
			if (this._singletonLoader.TryGetSingleton(WaterMapLoader.WaterMapKey, out waterMap))
			{
				this.LoadWater(waterMap, waterColumns, outflows);
				this.LoadContamination(waterColumns);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004BAC File Offset: 0x00002DAC
		public void LoadWater(IObjectLoader waterMap, Span<WaterColumn> waterColumns, Span<ColumnOutflows> outflows)
		{
			PackedList<float> packedList = waterMap.Get<PackedList<float>>(WaterMapLoader.WaterDepthsKey, this._floatPackedListSerializer);
			float[] array = this._mapIndexService.Unpack<float>(packedList, 1);
			PackedList<WaterMapLoader.WaterFlow> packedList2 = waterMap.Get<PackedList<WaterMapLoader.WaterFlow>>(WaterMapLoader.OutflowsKey, new WaterMapLoader.WaterFlowPackedListSerializer());
			WaterMapLoader.WaterFlow[] array2 = this._mapIndexService.Unpack<WaterMapLoader.WaterFlow>(packedList2, 1);
			for (int i = 0; i < this._mapIndexService.MaxIndex; i++)
			{
				ref WaterColumn ptr = waterColumns[i];
				ptr.WaterDepth = array[i];
				ptr.Overflow = 0f;
				ref ColumnOutflows ptr2 = outflows[i];
				int stride = this._mapIndexService.Stride;
				int index3D = i - stride;
				int index3D2 = i - 1;
				int index3D3 = i + stride;
				int index3D4 = i + 1;
				ptr2.BottomFlow = new TargetedFlow(array2[i].Bottom, index3D);
				ptr2.LeftFlow = new TargetedFlow(array2[i].Left, index3D2);
				ptr2.TopFlow = new TargetedFlow(array2[i].Top, index3D3);
				ptr2.RightFlow = new TargetedFlow(array2[i].Right, index3D4);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004CCC File Offset: 0x00002ECC
		public void LoadContamination(Span<WaterColumn> waterColumns)
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WaterMapLoader.ContaminationMapKey, out objectLoader))
			{
				this.LoadContamination(objectLoader.Get<PackedList<float>>(WaterMapLoader.ContaminationsKey, this._floatPackedListSerializer), waterColumns);
				return;
			}
			if (this._singletonLoader.TryGetSingleton(WaterMapLoader.PollutionMapKey, out objectLoader))
			{
				this.LoadContamination(objectLoader.Get<PackedList<float>>(WaterMapLoader.PollutionsKey, this._floatPackedListSerializer), waterColumns);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004D34 File Offset: 0x00002F34
		public void LoadContamination(PackedList<float> contaminationsPacked, Span<WaterColumn> waterColumns)
		{
			float[] array = this._mapIndexService.Unpack<float>(contaminationsPacked, 1);
			for (int i = 0; i < this._mapIndexService.MaxIndex; i++)
			{
				waterColumns[i].Contamination = array[i];
			}
		}

		// Token: 0x040000B1 RID: 177
		public static readonly SingletonKey WaterMapKey = new SingletonKey("WaterMap");

		// Token: 0x040000B2 RID: 178
		public static readonly PropertyKey<PackedList<float>> WaterDepthsKey = new PropertyKey<PackedList<float>>("WaterDepths");

		// Token: 0x040000B3 RID: 179
		public static readonly PropertyKey<PackedList<WaterMapLoader.WaterFlow>> OutflowsKey = new PropertyKey<PackedList<WaterMapLoader.WaterFlow>>("Outflows");

		// Token: 0x040000B4 RID: 180
		public static readonly SingletonKey ContaminationMapKey = new SingletonKey("ContaminationMap");

		// Token: 0x040000B5 RID: 181
		public static readonly PropertyKey<PackedList<float>> ContaminationsKey = new PropertyKey<PackedList<float>>("Contaminations");

		// Token: 0x040000B6 RID: 182
		public static readonly SingletonKey PollutionMapKey = new SingletonKey("PollutionMap");

		// Token: 0x040000B7 RID: 183
		public static readonly PropertyKey<PackedList<float>> PollutionsKey = new PropertyKey<PackedList<float>>("Pollutions");

		// Token: 0x040000B8 RID: 184
		public readonly MapIndexService _mapIndexService;

		// Token: 0x040000B9 RID: 185
		public readonly FloatPackedListSerializer _floatPackedListSerializer;

		// Token: 0x040000BA RID: 186
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x02000031 RID: 49
		public class WaterFlowPackedListSerializer : PackedListSerializer<WaterMapLoader.WaterFlow>
		{
			// Token: 0x060000E4 RID: 228 RVA: 0x00004DEE File Offset: 0x00002FEE
			public override void Serialize(WaterMapLoader.WaterFlow value, StringBuilder stringBuilder)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00004DF8 File Offset: 0x00002FF8
			public override WaterMapLoader.WaterFlow Deserialize(string value)
			{
				string[] array = value.Split(WaterMapLoader.WaterFlowPackedListSerializer.Separator, StringSplitOptions.None);
				return new WaterMapLoader.WaterFlow
				{
					Bottom = float.Parse(array[0]),
					Left = float.Parse(array[1]),
					Top = float.Parse(array[2]),
					Right = float.Parse(array[3])
				};
			}

			// Token: 0x040000BB RID: 187
			public new static readonly char Separator = ':';
		}

		// Token: 0x02000032 RID: 50
		public struct WaterFlow
		{
			// Token: 0x040000BC RID: 188
			public float Bottom;

			// Token: 0x040000BD RID: 189
			public float Left;

			// Token: 0x040000BE RID: 190
			public float Top;

			// Token: 0x040000BF RID: 191
			public float Right;
		}
	}
}
