using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000009 RID: 9
	public class ColumnOutflowsPackedListSerializer : PackedListSerializer<ColumnOutflows>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
		public override void Serialize(ColumnOutflows value, StringBuilder stringBuilder)
		{
			if (value.BottomFlow.Index3D == -1 && value.LeftFlow.Index3D == -1 && value.TopFlow.Index3D == -1 && value.RightFlow.Index3D == -1)
			{
				stringBuilder.Append(ColumnOutflowsPackedListSerializer.EmptyOutFlowsValue);
				return;
			}
			ColumnOutflowsPackedListSerializer.SerializeTargetedFlow(value.BottomFlow, stringBuilder);
			stringBuilder.Append(ColumnOutflowsPackedListSerializer.Separator);
			ColumnOutflowsPackedListSerializer.SerializeTargetedFlow(value.LeftFlow, stringBuilder);
			stringBuilder.Append(ColumnOutflowsPackedListSerializer.Separator);
			ColumnOutflowsPackedListSerializer.SerializeTargetedFlow(value.TopFlow, stringBuilder);
			stringBuilder.Append(ColumnOutflowsPackedListSerializer.Separator);
			ColumnOutflowsPackedListSerializer.SerializeTargetedFlow(value.RightFlow, stringBuilder);
			if (value.Outflows != null)
			{
				foreach (TargetedFlow value2 in value.Outflows)
				{
					stringBuilder.Append(ColumnOutflowsPackedListSerializer.Separator);
					ColumnOutflowsPackedListSerializer.SerializeTargetedFlow(value2, stringBuilder);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002280 File Offset: 0x00000480
		public override ColumnOutflows Deserialize(string value)
		{
			if (value == ColumnOutflowsPackedListSerializer.EmptyOutFlowsValue)
			{
				return new ColumnOutflows
				{
					BottomFlow = new TargetedFlow
					{
						Index3D = -1
					},
					LeftFlow = new TargetedFlow
					{
						Index3D = -1
					},
					TopFlow = new TargetedFlow
					{
						Index3D = -1
					},
					RightFlow = new TargetedFlow
					{
						Index3D = -1
					}
				};
			}
			string[] array = value.Split(ColumnOutflowsPackedListSerializer.Separator, StringSplitOptions.None);
			return new ColumnOutflows
			{
				BottomFlow = ColumnOutflowsPackedListSerializer.DeserializeTargetedFlow(array[0]),
				LeftFlow = ColumnOutflowsPackedListSerializer.DeserializeTargetedFlow(array[1]),
				TopFlow = ColumnOutflowsPackedListSerializer.DeserializeTargetedFlow(array[2]),
				RightFlow = ColumnOutflowsPackedListSerializer.DeserializeTargetedFlow(array[3]),
				Outflows = ColumnOutflowsPackedListSerializer.DeserializeOutflows(array)
			};
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002364 File Offset: 0x00000564
		public static void SerializeTargetedFlow(TargetedFlow value, StringBuilder stringBuilder)
		{
			int index3D = value.Index3D;
			if (index3D == -1)
			{
				stringBuilder.Append(ColumnOutflowsPackedListSerializer.EmptyOutFlowsValue);
				return;
			}
			stringBuilder.Append(index3D.ToString());
			stringBuilder.Append(ColumnOutflowsPackedListSerializer.TargetedFlowSeparator);
			stringBuilder.Append(CommonNumberSerializer.SerializeFloat(value.Flow));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023B8 File Offset: 0x000005B8
		public static List<TargetedFlow> DeserializeOutflows(IReadOnlyList<string> values)
		{
			if (values.Count > 4)
			{
				List<TargetedFlow> list = new List<TargetedFlow>();
				for (int i = 4; i < values.Count; i++)
				{
					list.Add(ColumnOutflowsPackedListSerializer.DeserializeTargetedFlow(values[i]));
				}
				return list;
			}
			return null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023FC File Offset: 0x000005FC
		public static TargetedFlow DeserializeTargetedFlow(string value)
		{
			if (value == ColumnOutflowsPackedListSerializer.EmptyOutFlowsValue)
			{
				return new TargetedFlow
				{
					Index3D = -1
				};
			}
			string[] array = value.Split(ColumnOutflowsPackedListSerializer.TargetedFlowSeparator, StringSplitOptions.None);
			return new TargetedFlow
			{
				Index3D = int.Parse(array[0]),
				Flow = float.Parse(array[1])
			};
		}

		// Token: 0x04000011 RID: 17
		public new static readonly char Separator = ':';

		// Token: 0x04000012 RID: 18
		public static readonly char TargetedFlowSeparator = '|';

		// Token: 0x04000013 RID: 19
		public static readonly string EmptyOutFlowsValue = "0";
	}
}
