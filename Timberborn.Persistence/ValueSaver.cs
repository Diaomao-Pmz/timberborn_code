using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000014 RID: 20
	public class ValueSaver : IValueSaver
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002CDA File Offset: 0x00000EDA
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public object Value { get; private set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x00002CEB File Offset: 0x00000EEB
		public void AsInt(int value)
		{
			this.Value = value;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public void AsFloat(float value)
		{
			this.Value = value;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002D07 File Offset: 0x00000F07
		public void AsBool(bool value)
		{
			this.Value = value;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002D15 File Offset: 0x00000F15
		public void AsString(string value)
		{
			this.Value = value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002D1E File Offset: 0x00000F1E
		public void AsChar(char value)
		{
			this.Value = value;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void AsQuaternion(Quaternion value)
		{
			this.Value = value;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002D3A File Offset: 0x00000F3A
		public void AsVector3(Vector3 value)
		{
			this.Value = value;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002D48 File Offset: 0x00000F48
		public void AsVector3Int(Vector3Int value)
		{
			this.Value = value;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002D56 File Offset: 0x00000F56
		public void AsVector2(Vector2 value)
		{
			this.Value = value;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002D64 File Offset: 0x00000F64
		public void AsVector2Int(Vector2Int value)
		{
			this.Value = value;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002D72 File Offset: 0x00000F72
		public void AsColor(Color value)
		{
			this.Value = value;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002D80 File Offset: 0x00000F80
		public void AsGuid(Guid value)
		{
			this.Value = value;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002D8E File Offset: 0x00000F8E
		public void As<T>(T value, IValueSerializer<T> serializer)
		{
			this.Value = SaveConversions.Convert<T>(value, serializer);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002D9D File Offset: 0x00000F9D
		public void AsIntList(IReadOnlyCollection<int> values)
		{
			this.Value = values.ToArray<int>();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002DAB File Offset: 0x00000FAB
		public void AsFloatList(IReadOnlyCollection<float> values)
		{
			this.Value = values.ToArray<float>();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002DB9 File Offset: 0x00000FB9
		public void AsBoolList(IReadOnlyCollection<bool> values)
		{
			this.Value = values.ToArray<bool>();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public void AsStringList(IReadOnlyCollection<string> values)
		{
			this.Value = values.ToArray<string>();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public void AsCharList(IReadOnlyCollection<char> values)
		{
			this.Value = values.ToArray<char>();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public void AsQuaternionList(IReadOnlyCollection<Quaternion> values)
		{
			this.Value = values.ToArray<Quaternion>();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public void AsVector3List(IReadOnlyCollection<Vector3> values)
		{
			this.Value = values.ToArray<Vector3>();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002DFF File Offset: 0x00000FFF
		public void AsVector3IntList(IReadOnlyCollection<Vector3Int> values)
		{
			this.Value = values.ToArray<Vector3Int>();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002E0D File Offset: 0x0000100D
		public void AsVector2List(IReadOnlyCollection<Vector2> values)
		{
			this.Value = values.ToArray<Vector2>();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002E1B File Offset: 0x0000101B
		public void AsVector2IntList(IReadOnlyCollection<Vector2Int> values)
		{
			this.Value = values.ToArray<Vector2Int>();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002E29 File Offset: 0x00001029
		public void AsColorList(IReadOnlyCollection<Color> values)
		{
			this.Value = values.ToArray<Color>();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002E37 File Offset: 0x00001037
		public void AsGuidList(IReadOnlyCollection<Guid> values)
		{
			this.Value = values.ToArray<Guid>();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002E45 File Offset: 0x00001045
		public void AsList<T>(IReadOnlyCollection<T> values, IValueSerializer<T> serializer)
		{
			this.Value = SaveConversions.ConvertList<T>(values, serializer);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002E54 File Offset: 0x00001054
		public IObjectSaver AsObject()
		{
			SerializedObject serializedObject = new SerializedObject();
			this.Value = serializedObject;
			return new ObjectSaver(serializedObject);
		}
	}
}
