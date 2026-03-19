using System;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000009 RID: 9
	public class CustomInputBindingSerializer
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002238 File Offset: 0x00000438
		public CustomInputBindingSerializer(SerializedObjectReaderWriter serializedObjectReaderWriter)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002248 File Offset: 0x00000448
		public string Serialize(CustomInputBinding customInputBinding)
		{
			SerializedObject serializedObject = new SerializedObject();
			serializedObject.Set<string>(CustomInputBindingSerializer.PathKey, customInputBinding.Path);
			SerializedObject serializedObject2 = new SerializedObject();
			serializedObject2.Set<string>(CustomInputBindingSerializer.InputModifiersValueKey, customInputBinding.InputModifiers.ToString());
			serializedObject.Set<SerializedObject>(CustomInputBindingSerializer.InputModifiersKey, serializedObject2);
			if (!string.IsNullOrEmpty(customInputBinding.DefaultName))
			{
				serializedObject.Set<string>(CustomInputBindingSerializer.DefaultNameKey, customInputBinding.DefaultName);
			}
			SerializedObject serializedObject3 = new SerializedObject();
			serializedObject3.Set<SerializedObject>(CustomInputBindingSerializer.InputBindingKey, serializedObject);
			return this._serializedObjectReaderWriter.WriteJson(serializedObject3);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022DC File Offset: 0x000004DC
		public CustomInputBinding Deserialize(string spec)
		{
			CustomInputBinding result;
			try
			{
				SerializedObject serializedObject = this._serializedObjectReaderWriter.ReadJson(spec).Get<SerializedObject>(CustomInputBindingSerializer.InputBindingKey);
				result = new CustomInputBinding(serializedObject.Get<string>(CustomInputBindingSerializer.PathKey), serializedObject.Get<SerializedObject>(CustomInputBindingSerializer.InputModifiersKey).Get<InputModifiers>(CustomInputBindingSerializer.InputModifiersValueKey), serializedObject.Has(CustomInputBindingSerializer.DefaultNameKey) ? serializedObject.Get<string>(CustomInputBindingSerializer.DefaultNameKey) : string.Empty);
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Exception while trying to deserialize:\n{0}\n\n{1}", spec, arg));
				result = null;
			}
			return result;
		}

		// Token: 0x04000010 RID: 16
		public static readonly string PathKey = "Path";

		// Token: 0x04000011 RID: 17
		public static readonly string InputModifiersKey = "InputModifiers";

		// Token: 0x04000012 RID: 18
		public static readonly string InputModifiersValueKey = "Value";

		// Token: 0x04000013 RID: 19
		public static readonly string DefaultNameKey = "DefaultName";

		// Token: 0x04000014 RID: 20
		public static readonly string InputBindingKey = "InputBindingSpecification";

		// Token: 0x04000015 RID: 21
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;
	}
}
