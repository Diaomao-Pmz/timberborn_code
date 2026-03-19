using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Timberborn.SerializationSystem;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200000E RID: 14
	public class BasicDeserializer
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002762 File Offset: 0x00000962
		public BasicDeserializer(AssetRefDeserializer assetRefDeserializer)
		{
			this._assetRefDeserializer = assetRefDeserializer;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002774 File Offset: 0x00000974
		public object Deserialize(SerializedObject serializedObject, Type type)
		{
			object obj = Activator.CreateInstance(type);
			foreach (PropertyInfo propertyInfo in type.GetSerializedProperties())
			{
				object value = this.GetValue(serializedObject, propertyInfo);
				if (value != null)
				{
					propertyInfo.SetValue(obj, value);
				}
			}
			return obj;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027D8 File Offset: 0x000009D8
		public object GetValue(SerializedObject serializedObject, PropertyInfo serializedProperty)
		{
			if (!BasicDeserializer.IsImmutableArray(serializedProperty.PropertyType))
			{
				return this.DeserializeSingle(serializedObject, serializedProperty);
			}
			return this.DeserializeArray(serializedObject, serializedProperty);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000221C File Offset: 0x0000041C
		public static bool IsImmutableArray(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImmutableArray<>);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027F8 File Offset: 0x000009F8
		public object DeserializeArray(SerializedObject serializedObject, PropertyInfo serializedProperty)
		{
			string name = serializedProperty.Name;
			Type type = serializedProperty.PropertyType.GenericTypeArguments[0];
			Array array;
			if (this._assetRefDeserializer.TryDeserializeArray(serializedObject, name, type, out array))
			{
				return BasicDeserializer.AsImmutableArray(array, type);
			}
			if (type.IsSerializable() && serializedObject.Has(name))
			{
				return BasicDeserializer.AsImmutableArray(this.DeserializeArray(serializedObject, name, type), type);
			}
			Array array2;
			if (serializedObject.TryGetArray(name, type, out array2))
			{
				return BasicDeserializer.AsImmutableArray(array2, type);
			}
			return BasicDeserializer.AsImmutableArray(Array.CreateInstance(type, 0), type);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002878 File Offset: 0x00000A78
		public object DeserializeSingle(SerializedObject serializedObject, PropertyInfo serializedProperty)
		{
			string name = serializedProperty.Name;
			Type propertyType = serializedProperty.PropertyType;
			object result;
			if (this._assetRefDeserializer.TryDeserialize(serializedObject, name, propertyType, out result))
			{
				return result;
			}
			if (propertyType.IsSerializable() && serializedObject.Has(name))
			{
				return this.Deserialize(serializedObject.Get<SerializedObject>(name), propertyType);
			}
			Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
			object result2;
			if (serializedObject.TryGet(name, type, out result2))
			{
				return result2;
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028E4 File Offset: 0x00000AE4
		public Array DeserializeArray(SerializedObject serializedObject, string name, Type type)
		{
			SerializedObject[] array = serializedObject.GetArray<SerializedObject>(name);
			Array array2 = Array.CreateInstance(type, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				array2.SetValue(this.Deserialize(array[i], type), i);
			}
			return array2;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002923 File Offset: 0x00000B23
		public static object AsImmutableArray(Array array, Type type)
		{
			return typeof(ImmutableArray<>).MakeGenericType(new Type[]
			{
				type
			}).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single<ConstructorInfo>().Invoke(new object[]
			{
				array
			});
		}

		// Token: 0x04000016 RID: 22
		public readonly AssetRefDeserializer _assetRefDeserializer;
	}
}
