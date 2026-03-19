using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x0200000C RID: 12
	public class ObjectLoader : IObjectLoader
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000023BB File Offset: 0x000005BB
		public ObjectLoader(SerializedObject serializedObject)
		{
			this._serializedObject = serializedObject;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000023CA File Offset: 0x000005CA
		public int Get(PropertyKey<int> key)
		{
			return this._serializedObject.Get<int>(key.Name);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000023DE File Offset: 0x000005DE
		public float Get(PropertyKey<float> key)
		{
			return this._serializedObject.Get<float>(key.Name);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000023F2 File Offset: 0x000005F2
		public bool Get(PropertyKey<bool> key)
		{
			return this._serializedObject.Get<bool>(key.Name);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002406 File Offset: 0x00000606
		public string Get(PropertyKey<string> key)
		{
			return this._serializedObject.Get<string>(key.Name);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000241A File Offset: 0x0000061A
		public char Get(PropertyKey<char> key)
		{
			return this._serializedObject.Get<char>(key.Name);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000242E File Offset: 0x0000062E
		public Quaternion Get(PropertyKey<Quaternion> key)
		{
			return this._serializedObject.Get<Quaternion>(key.Name);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002442 File Offset: 0x00000642
		public Vector3 Get(PropertyKey<Vector3> key)
		{
			return this._serializedObject.Get<Vector3>(key.Name);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002456 File Offset: 0x00000656
		public Vector3Int Get(PropertyKey<Vector3Int> key)
		{
			return this._serializedObject.Get<Vector3Int>(key.Name);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000246A File Offset: 0x0000066A
		public Vector2 Get(PropertyKey<Vector2> key)
		{
			return this._serializedObject.Get<Vector2>(key.Name);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000247E File Offset: 0x0000067E
		public Vector2Int Get(PropertyKey<Vector2Int> key)
		{
			return this._serializedObject.Get<Vector2Int>(key.Name);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002492 File Offset: 0x00000692
		public Color Get(PropertyKey<Color> key)
		{
			return this._serializedObject.Get<Color>(key.Name);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000024A6 File Offset: 0x000006A6
		public Guid Get(PropertyKey<Guid> key)
		{
			return this._serializedObject.Get<Guid>(key.Name);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000024BA File Offset: 0x000006BA
		public T Get<T>(PropertyKey<T> key) where T : Enum
		{
			return this._serializedObject.Get<T>(key.Name);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000024D0 File Offset: 0x000006D0
		public T Get<T>(PropertyKey<T> key, IValueSerializer<T> serializer)
		{
			T result;
			if (this.GetObsoletable<T>(key, serializer, out result))
			{
				return result;
			}
			throw new ObsoleteValueException(string.Format("Couldn't deconvert {0} to {1}", key.Name, typeof(T)));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000250B File Offset: 0x0000070B
		public bool GetObsoletable<T>(PropertyKey<T> key, IValueSerializer<T> serializer, out T value)
		{
			return SaveConversions.Deconvert<T>(this._serializedObject.GetSerialized(key.Name), serializer, out value);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002526 File Offset: 0x00000726
		public List<int> Get(ListKey<int> key)
		{
			return this._serializedObject.GetArray<int>(key.Name).ToList<int>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000253F File Offset: 0x0000073F
		public List<float> Get(ListKey<float> key)
		{
			return this._serializedObject.GetArray<float>(key.Name).ToList<float>();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002558 File Offset: 0x00000758
		public List<bool> Get(ListKey<bool> key)
		{
			return this._serializedObject.GetArray<bool>(key.Name).ToList<bool>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002571 File Offset: 0x00000771
		public List<string> Get(ListKey<string> key)
		{
			return this._serializedObject.GetArray<string>(key.Name).ToList<string>();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000258A File Offset: 0x0000078A
		public List<char> Get(ListKey<char> key)
		{
			return this._serializedObject.GetArray<char>(key.Name).ToList<char>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000025A3 File Offset: 0x000007A3
		public List<Quaternion> Get(ListKey<Quaternion> key)
		{
			return this._serializedObject.GetArray<Quaternion>(key.Name).ToList<Quaternion>();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000025BC File Offset: 0x000007BC
		public List<Vector3> Get(ListKey<Vector3> key)
		{
			return this._serializedObject.GetArray<Vector3>(key.Name).ToList<Vector3>();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000025D5 File Offset: 0x000007D5
		public List<Vector3Int> Get(ListKey<Vector3Int> key)
		{
			return this._serializedObject.GetArray<Vector3Int>(key.Name).ToList<Vector3Int>();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000025EE File Offset: 0x000007EE
		public List<Vector2> Get(ListKey<Vector2> key)
		{
			return this._serializedObject.GetArray<Vector2>(key.Name).ToList<Vector2>();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002607 File Offset: 0x00000807
		public List<Vector2Int> Get(ListKey<Vector2Int> key)
		{
			return this._serializedObject.GetArray<Vector2Int>(key.Name).ToList<Vector2Int>();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002620 File Offset: 0x00000820
		public List<Color> Get(ListKey<Color> key)
		{
			return this._serializedObject.GetArray<Color>(key.Name).ToList<Color>();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002639 File Offset: 0x00000839
		public List<Guid> Get(ListKey<Guid> key)
		{
			return this._serializedObject.GetArray<Guid>(key.Name).ToList<Guid>();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002652 File Offset: 0x00000852
		public List<T> Get<T>(ListKey<T> key) where T : Enum
		{
			return this._serializedObject.GetArray<T>(key.Name).ToList<T>();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000266B File Offset: 0x0000086B
		public List<T> Get<T>(ListKey<T> key, IValueSerializer<T> serializer)
		{
			return SaveConversions.DeconvertList<T>(serializer, (object[])this._serializedObject.GetSerialized(key.Name));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000268A File Offset: 0x0000088A
		public bool Has<T>(PropertyKey<T> key)
		{
			return this.Has(key.Name);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002699 File Offset: 0x00000899
		public bool Has<T>(ListKey<T> key)
		{
			return this.Has(key.Name);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000026A8 File Offset: 0x000008A8
		public bool Has(string name)
		{
			return this._serializedObject.Has(name);
		}

		// Token: 0x04000007 RID: 7
		public readonly SerializedObject _serializedObject;
	}
}
