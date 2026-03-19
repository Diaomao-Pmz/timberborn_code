using System;
using System.Collections.Generic;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000013 RID: 19
	public class ValueLoader : IValueLoader
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00002B1B File Offset: 0x00000D1B
		public ValueLoader(object value)
		{
			this._value = value;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002B2A File Offset: 0x00000D2A
		public int AsInt()
		{
			return this.AsPrimitive<int>();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002B32 File Offset: 0x00000D32
		public float AsFloat()
		{
			return this.AsPrimitive<float>();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002B3A File Offset: 0x00000D3A
		public bool AsBool()
		{
			return this.AsPrimitive<bool>();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002B42 File Offset: 0x00000D42
		public string AsString()
		{
			return this.AsPrimitive<string>();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002B4A File Offset: 0x00000D4A
		public char AsChar()
		{
			return this.AsPrimitive<char>();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002B52 File Offset: 0x00000D52
		public Quaternion AsQuaternion()
		{
			return this.AsPrimitive<Quaternion>();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002B5A File Offset: 0x00000D5A
		public Vector3 AsVector3()
		{
			return this.AsPrimitive<Vector3>();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002B62 File Offset: 0x00000D62
		public Vector3Int AsVector3Int()
		{
			return this.AsPrimitive<Vector3Int>();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002B6A File Offset: 0x00000D6A
		public Vector2 AsVector2()
		{
			return this.AsPrimitive<Vector2>();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002B72 File Offset: 0x00000D72
		public Vector2Int AsVector2Int()
		{
			return this.AsPrimitive<Vector2Int>();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002B7A File Offset: 0x00000D7A
		public Color AsColor()
		{
			return this.AsPrimitive<Color>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002B82 File Offset: 0x00000D82
		public Guid AsGuid()
		{
			return this.AsPrimitive<Guid>();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002B8C File Offset: 0x00000D8C
		public T As<T>(IValueSerializer<T> serializer)
		{
			T result;
			if (this.AsObsoletable<T>(serializer, out result))
			{
				return result;
			}
			throw new ObsoleteValueException(string.Format("Couldn't deconvert to {0}", typeof(T)));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002BBF File Offset: 0x00000DBF
		public bool AsObsoletable<T>(IValueSerializer<T> serializer, out T value)
		{
			return SaveConversions.Deconvert<T>(this.AsPrimitive<SerializedObject>(), serializer, out value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002BCE File Offset: 0x00000DCE
		public List<int> AsIntList()
		{
			return this.AsPrimitiveList<int>();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public List<float> AsFloatList()
		{
			return this.AsPrimitiveList<float>();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002BDE File Offset: 0x00000DDE
		public List<bool> AsBoolList()
		{
			return this.AsPrimitiveList<bool>();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public List<string> AsStringList()
		{
			return this.AsPrimitiveList<string>();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002BEE File Offset: 0x00000DEE
		public List<char> AsCharList()
		{
			return this.AsPrimitiveList<char>();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public List<Quaternion> AsQuaternionList()
		{
			return this.AsPrimitiveList<Quaternion>();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002BFE File Offset: 0x00000DFE
		public List<Vector3> AsVector3List()
		{
			return this.AsPrimitiveList<Vector3>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002C06 File Offset: 0x00000E06
		public List<Vector3Int> AsVector3IntList()
		{
			return this.AsPrimitiveList<Vector3Int>();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002C0E File Offset: 0x00000E0E
		public List<Vector2> AsVector2List()
		{
			return this.AsPrimitiveList<Vector2>();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002C16 File Offset: 0x00000E16
		public List<Vector2Int> AsVector2IntList()
		{
			return this.AsPrimitiveList<Vector2Int>();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002C1E File Offset: 0x00000E1E
		public List<Color> AsColorList()
		{
			return this.AsPrimitiveList<Color>();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002C26 File Offset: 0x00000E26
		public List<Guid> AsGuidList()
		{
			return this.AsPrimitiveList<Guid>();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002C2E File Offset: 0x00000E2E
		public List<T> AsList<T>(IValueSerializer<T> serializer)
		{
			return SaveConversions.DeconvertList<T>(serializer, this.AsPrimitiveList<SerializedObject>());
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002C3C File Offset: 0x00000E3C
		public IObjectLoader AsObject()
		{
			return new ObjectLoader((SerializedObject)this._value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002C4E File Offset: 0x00000E4E
		public bool IsObject()
		{
			return this._value is SerializedObject;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002C5E File Offset: 0x00000E5E
		public bool IsList()
		{
			return this._value is object[];
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002C6E File Offset: 0x00000E6E
		public T AsPrimitive<T>()
		{
			return (T)((object)PrimitiveTypeSerialization.Deserialize(this._value, typeof(T)));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002C8C File Offset: 0x00000E8C
		public List<T> AsPrimitiveList<T>()
		{
			object[] array = (object[])this._value;
			List<T> list = new List<T>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add((T)((object)PrimitiveTypeSerialization.Deserialize(array[i], typeof(T))));
			}
			return list;
		}

		// Token: 0x0400000C RID: 12
		public readonly object _value;
	}
}
