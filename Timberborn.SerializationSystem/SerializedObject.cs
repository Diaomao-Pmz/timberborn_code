using System;
using System.Collections.Generic;

namespace Timberborn.SerializationSystem
{
	// Token: 0x0200000C RID: 12
	public class SerializedObject : IEquatable<SerializedObject>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003193 File Offset: 0x00001393
		public SerializedObject()
		{
			this._properties = new Dictionary<string, object>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000031A6 File Offset: 0x000013A6
		public SerializedObject(Dictionary<string, object> properties)
		{
			this._properties = properties;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000031B5 File Offset: 0x000013B5
		public void Set<T>(string name, T value)
		{
			this._properties[name] = PrimitiveTypeSerialization.Serialize(value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000031D0 File Offset: 0x000013D0
		public void SetArray(string name, Array values)
		{
			object[] array = new object[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = PrimitiveTypeSerialization.Serialize(values.GetValue(i));
			}
			this._properties[name] = array;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003216 File Offset: 0x00001416
		public T Get<T>(string name)
		{
			return (T)((object)this.Get(name, typeof(T)));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003230 File Offset: 0x00001430
		public object Get(string name, Type type)
		{
			object result;
			if (this.TryGet(name, type, out result))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException("name", "Property not found: '" + name + "'");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003268 File Offset: 0x00001468
		public T GetOrDefault<T>(string name, T defaultValue)
		{
			object obj;
			if (this.TryGet(name, typeof(T), out obj))
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003294 File Offset: 0x00001494
		public bool TryGet(string name, Type type, out object value)
		{
			object value2;
			if (this._properties.TryGetValue(name, out value2))
			{
				value = PrimitiveTypeSerialization.Deserialize(value2, type);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000032C0 File Offset: 0x000014C0
		public object GetSerialized(string name)
		{
			object result;
			if (this._properties.TryGetValue(name, out result))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException("name", "Property not found: '" + name + "'");
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000032FC File Offset: 0x000014FC
		public T[] GetArray<T>(string name)
		{
			Array array;
			if (this.TryGetArray(name, typeof(T), out array))
			{
				return (T[])array;
			}
			throw new ArgumentOutOfRangeException("name", "Property not found: '" + name + "'");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003340 File Offset: 0x00001540
		public bool TryGetArray(string name, Type type, out Array array)
		{
			object obj;
			if (this._properties.TryGetValue(name, out obj))
			{
				object[] array2 = (object[])obj;
				array = Array.CreateInstance(type, array2.Length);
				for (int i = 0; i < array2.Length; i++)
				{
					object value = PrimitiveTypeSerialization.Deserialize(array2[i], type);
					array.SetValue(value, i);
				}
				return true;
			}
			array = null;
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003396 File Offset: 0x00001596
		public bool Has(string name)
		{
			return this._properties.ContainsKey(name);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000033A4 File Offset: 0x000015A4
		public IEnumerable<string> Properties()
		{
			return this._properties.Keys;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033B4 File Offset: 0x000015B4
		public bool Equals(SerializedObject other)
		{
			if (other == null)
			{
				return false;
			}
			if (this._properties.Count != other._properties.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this._properties)
			{
				object obj;
				if (!other._properties.TryGetValue(keyValuePair.Key, out obj))
				{
					return false;
				}
				SerializedObject serializedObject = obj as SerializedObject;
				if (serializedObject != null)
				{
					if (!serializedObject.Equals((SerializedObject)keyValuePair.Value))
					{
						return false;
					}
				}
				else if (!obj.Equals(keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000015 RID: 21
		public readonly Dictionary<string, object> _properties;
	}
}
