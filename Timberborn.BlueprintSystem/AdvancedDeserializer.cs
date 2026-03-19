using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000007 RID: 7
	public class AdvancedDeserializer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AdvancedDeserializer(IEnumerable<IDeserializer> customDeserializers)
		{
			this._customDeserializers = customDeserializers.ToImmutableArray<IDeserializer>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public object Deserialize(object instance, Type type)
		{
			if (this._customDeserializers.Length > 0)
			{
				this.DeserializeInternal(instance, type);
			}
			return instance;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public void DeserializeInternal(object instance, Type type)
		{
			foreach (PropertyInfo propertyInfo in AdvancedDeserializer.GetSerializedProperties(type))
			{
				SerializeAttribute customAttribute = propertyInfo.GetCustomAttribute<SerializeAttribute>();
				PropertyInfo property = type.GetProperty(customAttribute.SourceName, AdvancedDeserializer.AllInstanceFlag);
				if (property == null)
				{
					throw new InvalidOperationException("No source field found for serialized field: " + propertyInfo.Name);
				}
				object value = property.GetValue(instance);
				if (value != null)
				{
					object value2 = this.DeserializeValue(value, propertyInfo.PropertyType);
					propertyInfo.SetValue(instance, value2);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D0 File Offset: 0x000003D0
		public static IEnumerable<PropertyInfo> GetSerializedProperties(Type type)
		{
			return type.GetProperties(AdvancedDeserializer.AllInstanceFlag).Where(delegate(PropertyInfo propertyInfo)
			{
				SerializeAttribute customAttribute = propertyInfo.GetCustomAttribute<SerializeAttribute>();
				return customAttribute != null && customAttribute.HasSource;
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002201 File Offset: 0x00000401
		public object DeserializeValue(object sourceValue, Type type)
		{
			if (!AdvancedDeserializer.IsImmutableArray(type))
			{
				return this.DeserializeSingle(sourceValue, type);
			}
			return this.DeserializeArray(sourceValue, type);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public static bool IsImmutableArray(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImmutableArray<>);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002240 File Offset: 0x00000440
		public object DeserializeArray(object sourceValue, Type type)
		{
			foreach (IDeserializer deserializer in this._customDeserializers)
			{
				if (deserializer.DeserializedType == type)
				{
					Array array = (Array)sourceValue;
					Array array2 = Array.CreateInstance(type, array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						array2.SetValue(deserializer.Deserialize(array.GetValue(i)), i);
					}
					return array2;
				}
			}
			throw new InvalidOperationException("No deserializer found for type: " + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022D8 File Offset: 0x000004D8
		public object DeserializeSingle(object sourceValue, Type type)
		{
			foreach (IDeserializer deserializer in this._customDeserializers)
			{
				if (deserializer.DeserializedType == type)
				{
					return deserializer.Deserialize(sourceValue);
				}
			}
			throw new InvalidOperationException("No deserializer found for type: " + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x04000008 RID: 8
		public static readonly BindingFlags AllInstanceFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000009 RID: 9
		public readonly ImmutableArray<IDeserializer> _customDeserializers;
	}
}
