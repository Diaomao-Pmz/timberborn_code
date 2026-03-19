using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x0200000D RID: 13
	public static class TimbermeshExtensions
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024B4 File Offset: 0x000006B4
		public static Vector3 ToVector3(this Vector3Float self)
		{
			return new Vector3(self.X, self.Y, self.Z);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024CD File Offset: 0x000006CD
		public static Quaternion ToQuaternion(this QuaternionFloat self)
		{
			return new Quaternion(self.X, self.Y, self.Z, self.W);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024EC File Offset: 0x000006EC
		public static void ReadProperties(this Node node, string name, ICollection<Vector2> target)
		{
			VertexProperty vertexProperty = node.VertexProperties.Get(name);
			if (vertexProperty != null)
			{
				for (int i = 0; i < node.VertexCount; i++)
				{
					target.Add(TimbermeshExtensions.ReadFloat2(vertexProperty, i));
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002528 File Offset: 0x00000728
		public static void ReadProperties(this Node node, string name, ICollection<Vector3> target)
		{
			VertexProperty vertexProperty = node.VertexProperties.Get(name);
			if (vertexProperty != null)
			{
				for (int i = 0; i < node.VertexCount; i++)
				{
					target.Add(TimbermeshExtensions.ReadFloat3(vertexProperty, i));
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002564 File Offset: 0x00000764
		public static void ReadProperties(this Node node, string name, ICollection<Vector4> target)
		{
			VertexProperty vertexProperty = node.VertexProperties.Get(name);
			if (vertexProperty != null)
			{
				for (int i = 0; i < node.VertexCount; i++)
				{
					target.Add(TimbermeshExtensions.ReadFloat4(vertexProperty, i));
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025A0 File Offset: 0x000007A0
		public static void ReadProperties(this Node node, string name, ICollection<Color> target)
		{
			VertexProperty vertexProperty = node.VertexProperties.Get(name);
			if (vertexProperty != null)
			{
				for (int i = 0; i < node.VertexCount; i++)
				{
					target.Add(TimbermeshExtensions.ReadFloat4(vertexProperty, i));
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025E0 File Offset: 0x000007E0
		public static VertexProperty Get(this IEnumerable<VertexProperty> properties, string name)
		{
			return properties.FirstOrDefault((VertexProperty p) => p.Name == name);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000260C File Offset: 0x0000080C
		public static Vector2 ReadFloat2(VertexProperty property, int itemIndex)
		{
			int num = itemIndex * 8;
			float num2 = BitConverter.ToSingle(property.Data, num);
			float num3 = BitConverter.ToSingle(property.Data, num + 4);
			return new Vector2(num2, num3);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002640 File Offset: 0x00000840
		public static Vector3 ReadFloat3(VertexProperty property, int itemIndex)
		{
			int num = itemIndex * 12;
			float num2 = BitConverter.ToSingle(property.Data, num);
			float num3 = BitConverter.ToSingle(property.Data, num + 4);
			float num4 = BitConverter.ToSingle(property.Data, num + 8);
			return new Vector3(num2, num3, num4);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002684 File Offset: 0x00000884
		public static Vector4 ReadFloat4(VertexProperty property, int itemIndex)
		{
			int num = itemIndex * 16;
			float num2 = BitConverter.ToSingle(property.Data, num);
			float num3 = BitConverter.ToSingle(property.Data, num + 4);
			float num4 = BitConverter.ToSingle(property.Data, num + 8);
			float num5 = BitConverter.ToSingle(property.Data, num + 12);
			return new Vector4(num2, num3, num4, num5);
		}
	}
}
