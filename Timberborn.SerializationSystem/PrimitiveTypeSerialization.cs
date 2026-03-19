using System;
using System.Globalization;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.SerializationSystem
{
	// Token: 0x0200000A RID: 10
	public static class PrimitiveTypeSerialization
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002B28 File Offset: 0x00000D28
		public static object Serialize(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is int)
			{
				int num = (int)value;
				return num;
			}
			if (value is float)
			{
				float num2 = (float)value;
				return num2;
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				return flag;
			}
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			SerializedObject serializedObject = value as SerializedObject;
			if (serializedObject != null)
			{
				return serializedObject;
			}
			if (value is char)
			{
				char c = (char)value;
				return new string(c, 1);
			}
			if (value is Quaternion)
			{
				Quaternion quaternion = (Quaternion)value;
				SerializedObject serializedObject2 = new SerializedObject();
				serializedObject2.Set<float>("X", quaternion.x);
				serializedObject2.Set<float>("Y", quaternion.y);
				serializedObject2.Set<float>("Z", quaternion.z);
				serializedObject2.Set<float>("W", quaternion.w);
				return serializedObject2;
			}
			if (value is Vector3)
			{
				Vector3 vector = (Vector3)value;
				SerializedObject serializedObject3 = new SerializedObject();
				serializedObject3.Set<float>("X", vector.x);
				serializedObject3.Set<float>("Y", vector.y);
				serializedObject3.Set<float>("Z", vector.z);
				return serializedObject3;
			}
			if (value is Vector3Int)
			{
				Vector3Int vector3Int = (Vector3Int)value;
				SerializedObject serializedObject4 = new SerializedObject();
				serializedObject4.Set<int>("X", vector3Int.x);
				serializedObject4.Set<int>("Y", vector3Int.y);
				serializedObject4.Set<int>("Z", vector3Int.z);
				return serializedObject4;
			}
			if (value is Vector2)
			{
				Vector2 vector2 = (Vector2)value;
				SerializedObject serializedObject5 = new SerializedObject();
				serializedObject5.Set<float>("X", vector2.x);
				serializedObject5.Set<float>("Y", vector2.y);
				return serializedObject5;
			}
			if (value is Vector2Int)
			{
				Vector2Int vector2Int = (Vector2Int)value;
				SerializedObject serializedObject6 = new SerializedObject();
				serializedObject6.Set<int>("X", vector2Int.x);
				serializedObject6.Set<int>("Y", vector2Int.y);
				return serializedObject6;
			}
			if (value is Guid)
			{
				return ((Guid)value).ToString();
			}
			if (value is Color)
			{
				Color color = (Color)value;
				SerializedObject serializedObject7 = new SerializedObject();
				serializedObject7.Set<float>("r", color.r);
				serializedObject7.Set<float>("g", color.g);
				serializedObject7.Set<float>("b", color.b);
				serializedObject7.Set<float>("a", color.a);
				return serializedObject7;
			}
			Enum @enum = value as Enum;
			if (@enum == null)
			{
				throw new ArgumentException(string.Format("Can't serialize {0} of type '{1}'", value, value.GetType()));
			}
			return PrimitiveTypeSerialization.SerializeEnum(@enum);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E08 File Offset: 0x00001008
		public static object Deserialize(object value, Type type)
		{
			try
			{
				if (type == typeof(int) || type == typeof(bool) || type == typeof(SerializedObject))
				{
					return Convert.ChangeType(value, type);
				}
				if (type == typeof(string))
				{
					return (value == null) ? null : Convert.ChangeType(value, type);
				}
				if (type == typeof(float))
				{
					string text = value as string;
					if (text != null)
					{
						return float.Parse(text, CultureInfo.InvariantCulture);
					}
					return Convert.ChangeType(value, type);
				}
				else
				{
					if (type == typeof(char))
					{
						return ((string)value)[0];
					}
					if (type == typeof(Quaternion))
					{
						SerializedObject serializedObject = (SerializedObject)value;
						return new Quaternion(serializedObject.Get<float>("X"), serializedObject.Get<float>("Y"), serializedObject.Get<float>("Z"), serializedObject.Get<float>("W"));
					}
					if (type == typeof(Vector3))
					{
						SerializedObject serializedObject2 = (SerializedObject)value;
						return new Vector3(serializedObject2.Get<float>("X"), serializedObject2.Get<float>("Y"), serializedObject2.Get<float>("Z"));
					}
					if (type == typeof(Vector3Int))
					{
						SerializedObject serializedObject3 = (SerializedObject)value;
						return new Vector3Int(serializedObject3.Get<int>("X"), serializedObject3.Get<int>("Y"), serializedObject3.Get<int>("Z"));
					}
					if (type == typeof(Vector2))
					{
						SerializedObject serializedObject4 = (SerializedObject)value;
						return new Vector2(serializedObject4.Get<float>("X"), serializedObject4.Get<float>("Y"));
					}
					if (type == typeof(Vector2Int))
					{
						SerializedObject serializedObject5 = (SerializedObject)value;
						return new Vector2Int(serializedObject5.Get<int>("X"), serializedObject5.Get<int>("Y"));
					}
					if (type == typeof(Guid))
					{
						return Guid.Parse((string)value);
					}
					if (type == typeof(Color))
					{
						SerializedObject serializedObject6 = (SerializedObject)value;
						return new Color(serializedObject6.Get<float>("r"), serializedObject6.Get<float>("g"), serializedObject6.Get<float>("b"), serializedObject6.Get<float>("a"));
					}
					if (type.IsEnum)
					{
						return PrimitiveTypeSerialization.DeserializeEnum(value, type);
					}
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(string.Format("Exception while deserializing {0} to {1}", value, type), innerException);
			}
			throw new ArgumentException(string.Format("Can't deserialize {0} to type {1}", value, type));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003130 File Offset: 0x00001330
		public static object SerializeEnum(Enum enumValue)
		{
			return enumValue.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003138 File Offset: 0x00001338
		[BackwardCompatible(2025, 2, 7, Compatibility.Map)]
		public static object DeserializeEnum(object value, Type type)
		{
			SerializedObject serializedObject = value as SerializedObject;
			if (serializedObject != null)
			{
				return Enum.Parse(type, serializedObject.Get<string>("Value"));
			}
			return Enum.Parse(type, (string)value);
		}
	}
}
