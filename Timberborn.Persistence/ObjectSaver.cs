using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x0200000D RID: 13
	public class ObjectSaver : IObjectSaver
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000026B6 File Offset: 0x000008B6
		public ObjectSaver(SerializedObject serializedObject)
		{
			this._serializedObject = serializedObject;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000026C5 File Offset: 0x000008C5
		public void Set(PropertyKey<int> key, int value)
		{
			this._serializedObject.Set<int>(key.Name, value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000026DA File Offset: 0x000008DA
		public void Set(PropertyKey<float> key, float value)
		{
			this._serializedObject.Set<float>(key.Name, value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000026EF File Offset: 0x000008EF
		public void Set(PropertyKey<bool> key, bool value)
		{
			this._serializedObject.Set<bool>(key.Name, value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002704 File Offset: 0x00000904
		public void Set(PropertyKey<string> key, string value)
		{
			this._serializedObject.Set<string>(key.Name, value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002719 File Offset: 0x00000919
		public void Set(PropertyKey<char> key, char value)
		{
			this._serializedObject.Set<char>(key.Name, value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000272E File Offset: 0x0000092E
		public void Set(PropertyKey<Quaternion> key, Quaternion value)
		{
			this._serializedObject.Set<Quaternion>(key.Name, value);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002743 File Offset: 0x00000943
		public void Set(PropertyKey<Vector3> key, Vector3 value)
		{
			this._serializedObject.Set<Vector3>(key.Name, value);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002758 File Offset: 0x00000958
		public void Set(PropertyKey<Vector3Int> key, Vector3Int value)
		{
			this._serializedObject.Set<Vector3Int>(key.Name, value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000276D File Offset: 0x0000096D
		public void Set(PropertyKey<Vector2> key, Vector2 value)
		{
			this._serializedObject.Set<Vector2>(key.Name, value);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002782 File Offset: 0x00000982
		public void Set(PropertyKey<Vector2Int> key, Vector2Int value)
		{
			this._serializedObject.Set<Vector2Int>(key.Name, value);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002797 File Offset: 0x00000997
		public void Set(PropertyKey<Color> key, Color value)
		{
			this._serializedObject.Set<Color>(key.Name, value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000027AC File Offset: 0x000009AC
		public void Set(PropertyKey<Guid> key, Guid value)
		{
			this._serializedObject.Set<Guid>(key.Name, value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000027C1 File Offset: 0x000009C1
		public void Set<T>(PropertyKey<T> key, T value) where T : Enum
		{
			this._serializedObject.Set<T>(key.Name, value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000027D6 File Offset: 0x000009D6
		public void Set<T>(PropertyKey<T> key, T value, IValueSerializer<T> serializer)
		{
			this._serializedObject.Set<object>(key.Name, SaveConversions.Convert<T>(value, serializer));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000027F1 File Offset: 0x000009F1
		public void Set(ListKey<int> key, IReadOnlyCollection<int> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<int>());
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000280B File Offset: 0x00000A0B
		public void Set(ListKey<float> key, IReadOnlyCollection<float> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<float>());
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002825 File Offset: 0x00000A25
		public void Set(ListKey<bool> key, IReadOnlyCollection<bool> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<bool>());
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000283F File Offset: 0x00000A3F
		public void Set(ListKey<string> key, IReadOnlyCollection<string> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<string>());
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002859 File Offset: 0x00000A59
		public void Set(ListKey<char> key, IReadOnlyCollection<char> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<char>());
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002873 File Offset: 0x00000A73
		public void Set(ListKey<Quaternion> key, IReadOnlyCollection<Quaternion> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Quaternion>());
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000288D File Offset: 0x00000A8D
		public void Set(ListKey<Vector3> key, IReadOnlyCollection<Vector3> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Vector3>());
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000028A7 File Offset: 0x00000AA7
		public void Set(ListKey<Vector3Int> key, IReadOnlyCollection<Vector3Int> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Vector3Int>());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000028C1 File Offset: 0x00000AC1
		public void Set(ListKey<Vector2> key, IReadOnlyCollection<Vector2> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Vector2>());
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000028DB File Offset: 0x00000ADB
		public void Set(ListKey<Vector2Int> key, IReadOnlyCollection<Vector2Int> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Vector2Int>());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000028F5 File Offset: 0x00000AF5
		public void Set(ListKey<Color> key, IReadOnlyCollection<Color> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Color>());
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000290F File Offset: 0x00000B0F
		public void Set(ListKey<Guid> key, IReadOnlyCollection<Guid> values)
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<Guid>());
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002929 File Offset: 0x00000B29
		public void Set<T>(ListKey<T> key, IReadOnlyCollection<T> values) where T : Enum
		{
			this._serializedObject.SetArray(key.Name, values.ToArray<T>());
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002943 File Offset: 0x00000B43
		public void Set<T>(ListKey<T> key, IReadOnlyCollection<T> values, IValueSerializer<T> serializer)
		{
			this._serializedObject.SetArray(key.Name, SaveConversions.ConvertList<T>(values, serializer));
		}

		// Token: 0x04000008 RID: 8
		public readonly SerializedObject _serializedObject;
	}
}
