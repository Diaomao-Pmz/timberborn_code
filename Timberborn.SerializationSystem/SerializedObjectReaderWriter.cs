using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Timberborn.SerializationSystem
{
	// Token: 0x0200000D RID: 13
	public class SerializedObjectReaderWriter
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003478 File Offset: 0x00001678
		public SerializedObjectReaderWriter(JsonMerger jsonMerger)
		{
			this._jsonMerger = jsonMerger;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003488 File Offset: 0x00001688
		public void WriteJson(SerializedObject serializedObject, Stream stream)
		{
			JObject jobject = this.SerializeObject(serializedObject);
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
					{
						jobject.WriteTo(jsonTextWriter);
					}
				}
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003504 File Offset: 0x00001704
		public string WriteJson(SerializedObject serializedObject)
		{
			return this.SerializeObject(serializedObject).ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003514 File Offset: 0x00001714
		public SerializedObject ReadJson(Stream stream)
		{
			SerializedObject result;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				string text = streamReader.ReadToEnd().Replace("\":-.,", "\":0.0,");
				result = this.ReadJson(text);
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003564 File Offset: 0x00001764
		public SerializedObject ReadJson(string text)
		{
			return this.DeserializeObject(JObject.Parse(text));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003572 File Offset: 0x00001772
		public SerializedObject ReadJsons(IEnumerable<string> texts)
		{
			return this.DeserializeObject(this._jsonMerger.Merge(texts.Select(new Func<string, JObject>(JObject.Parse))));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003598 File Offset: 0x00001798
		public JObject SerializeObject(SerializedObject serializedObject)
		{
			JObject jobject = new JObject();
			foreach (string text in serializedObject.Properties())
			{
				object serialized = serializedObject.GetSerialized(text);
				jobject.Add(text, this.SerializeAnything(serialized));
			}
			return jobject;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035FC File Offset: 0x000017FC
		public SerializedObject DeserializeObject(JToken jToken)
		{
			if (jToken.Type != JTokenType.Object)
			{
				throw new ArgumentException(string.Format("{0} is not a JProperty, can't deserialize it.", jToken));
			}
			SerializedObject serializedObject = new SerializedObject();
			foreach (JProperty jproperty in jToken.Children<JProperty>())
			{
				object obj = this.DeserializeAnything(jproperty.Value);
				Array array = obj as Array;
				if (array != null)
				{
					serializedObject.SetArray(jproperty.Name, array);
				}
				else
				{
					serializedObject.Set<object>(jproperty.Name, obj);
				}
			}
			return serializedObject;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000036A0 File Offset: 0x000018A0
		public JToken SerializeAnything(object value)
		{
			SerializedObject serializedObject = value as SerializedObject;
			JToken result;
			if (serializedObject == null)
			{
				Array array = value as Array;
				if (array == null)
				{
					result = SerializedObjectReaderWriter.SerializaBasicType(value);
				}
				else
				{
					result = this.SerializeArray(array);
				}
			}
			else
			{
				result = this.SerializeObject(serializedObject);
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000036E0 File Offset: 0x000018E0
		public object DeserializeAnything(JToken jToken)
		{
			JTokenType type = jToken.Type;
			object result;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					result = this.DeserializeArray(jToken);
				}
				else
				{
					result = SerializedObjectReaderWriter.DeserializeBasicType(jToken);
				}
			}
			else
			{
				result = this.DeserializeObject(jToken);
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003718 File Offset: 0x00001918
		public JArray SerializeArray(Array array)
		{
			JArray jarray = new JArray();
			foreach (object value in array)
			{
				jarray.Add(this.SerializeAnything(value));
			}
			return jarray;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003774 File Offset: 0x00001974
		public object[] DeserializeArray(JToken jToken)
		{
			if (jToken.Type != JTokenType.Array)
			{
				throw new ArgumentException(string.Format("Argument is not a {0}.", JTokenType.Array));
			}
			object[] array = new object[jToken.Children().Count<JToken>()];
			int num = 0;
			foreach (JToken jToken2 in jToken.Children())
			{
				array[num] = this.DeserializeAnything(jToken2);
				num++;
			}
			return array;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003808 File Offset: 0x00001A08
		public static JToken SerializaBasicType(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is int)
			{
				int num = (int)value;
				return new JValue((long)num);
			}
			if (value is float)
			{
				float value2 = (float)value;
				return new JValue(value2);
			}
			if (value is bool)
			{
				bool value3 = (bool)value;
				return new JValue(value3);
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(string.Format("Can't create JToken from {0} of type {1}", value, value.GetType()));
			}
			return new JValue(text);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000388C File Offset: 0x00001A8C
		public static object DeserializeBasicType(JToken jToken)
		{
			switch (jToken.Type)
			{
			case JTokenType.Integer:
				return jToken.Value<int>();
			case JTokenType.Float:
				return jToken.Value<float>();
			case JTokenType.String:
				return jToken.Value<string>();
			case JTokenType.Boolean:
				return jToken.Value<bool>();
			case JTokenType.Null:
				return null;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000016 RID: 22
		public readonly JsonMerger _jsonMerger;
	}
}
