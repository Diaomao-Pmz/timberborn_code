using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Timberborn.SerializationSystem
{
	// Token: 0x02000007 RID: 7
	public class JsonMerger
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000247C File Offset: 0x0000067C
		public JObject Merge(IEnumerable<JObject> jsons)
		{
			JObject jobject = null;
			foreach (JObject json in jsons)
			{
				jobject = this.ProcessAndMergeJsons(jobject, json);
			}
			return jobject;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024CC File Offset: 0x000006CC
		public JObject ProcessAndMergeJsons(JObject baseJson, JObject json)
		{
			if (baseJson != null)
			{
				this.ProcessTokens(baseJson);
			}
			this.ProcessTokens(json);
			JObject jobject = JsonMerger.MergeJsons(baseJson, json);
			this.ProcessArrayAppenders(jobject);
			this.ProcessArrayRemovals(jobject);
			this.ProcessPropertyDeletions(jobject);
			this._arrayAppenders.Clear();
			this._arrayRemovals.Clear();
			this._propertyDeletions.Clear();
			return jobject;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002529 File Offset: 0x00000729
		public void ProcessTokens(JObject json)
		{
			JsonMerger.ProcessArrayTokens(json, JsonKeywords.Append, this._arrayAppenders);
			JsonMerger.ProcessArrayTokens(json, JsonKeywords.Remove, this._arrayRemovals);
			this.ProcessDeleteTokens(json, JsonKeywords.Delete);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000255C File Offset: 0x0000075C
		public static void ProcessArrayTokens(JToken parentToken, string keyword, ICollection<JsonMerger.ArrayModification> arrayModifications)
		{
			foreach (JProperty jproperty in parentToken.Children<JProperty>().ToList<JProperty>())
			{
				if (!jproperty.Name.EndsWith(JsonKeywords.Nested, StringComparison.OrdinalIgnoreCase))
				{
					if (jproperty.Name.EndsWith(keyword, StringComparison.OrdinalIgnoreCase))
					{
						if (jproperty.Value.Type != JTokenType.Array)
						{
							throw new ArgumentException("Cannot modify non-array property: " + jproperty.Path + " of " + parentToken.Path);
						}
						JsonMerger.CreateArrayModification(jproperty, keyword, arrayModifications);
					}
					else if (jproperty.Value.Type == JTokenType.Object)
					{
						JsonMerger.ProcessArrayTokens(jproperty.Value, keyword, arrayModifications);
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000262C File Offset: 0x0000082C
		public void ProcessDeleteTokens(JToken parentToken, string keyword)
		{
			foreach (JProperty jproperty in parentToken.Children<JProperty>().ToList<JProperty>())
			{
				if (!jproperty.Name.EndsWith(JsonKeywords.Nested, StringComparison.OrdinalIgnoreCase))
				{
					if (jproperty.Name.EndsWith(keyword, StringComparison.OrdinalIgnoreCase))
					{
						string item = jproperty.Path.Replace(keyword, string.Empty, StringComparison.OrdinalIgnoreCase);
						this._propertyDeletions.Add(item);
						jproperty.Remove();
					}
					else if (jproperty.Value.Type == JTokenType.Object)
					{
						this.ProcessDeleteTokens(jproperty.Value, keyword);
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026E8 File Offset: 0x000008E8
		public static void CreateArrayModification(JProperty modifyingProperty, string keyword, ICollection<JsonMerger.ArrayModification> arrayModifications)
		{
			string path = modifyingProperty.Path.Replace(keyword, string.Empty, StringComparison.OrdinalIgnoreCase);
			string parentPath = (modifyingProperty.Parent == null) ? string.Empty : modifyingProperty.Parent.Path;
			modifyingProperty.Remove();
			JToken value = modifyingProperty.Value;
			modifyingProperty.Value = JValue.CreateNull();
			JProperty jProperty = new JProperty(modifyingProperty.Name.Replace(keyword, string.Empty, StringComparison.OrdinalIgnoreCase), value);
			arrayModifications.Add(new JsonMerger.ArrayModification(jProperty, path, parentPath));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002762 File Offset: 0x00000962
		public static JObject MergeJsons(JObject mergedJson, JObject json)
		{
			if (mergedJson == null)
			{
				return json;
			}
			mergedJson.Merge(json, JsonMerger.ReplaceSettings);
			return mergedJson;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002778 File Offset: 0x00000978
		public void ProcessArrayAppenders(JContainer mergedJson)
		{
			foreach (JsonMerger.ArrayModification arrayModification in this._arrayAppenders)
			{
				JToken jtoken = mergedJson.SelectToken(arrayModification.Path);
				if (jtoken != null)
				{
					JsonMerger.AddToExistingArrayProperty(jtoken, arrayModification.JProperty);
				}
				else
				{
					JsonMerger.AddNewArrayProperty(mergedJson, arrayModification);
				}
			}
			this._arrayAppenders.Clear();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027F4 File Offset: 0x000009F4
		public static void AddToExistingArrayProperty(JToken existingArray, JProperty arrayAppender)
		{
			if (existingArray.Type != JTokenType.Array)
			{
				throw new ArgumentException("Cannot append to non-array path: " + existingArray.Path);
			}
			JArray jarray = (JArray)existingArray;
			using (IEnumerator<JToken> enumerator = arrayAppender.Value.Children().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JToken jTokenToAppend = enumerator.Current;
					if (!jarray.Any((JToken t) => JToken.DeepEquals(t, jTokenToAppend)))
					{
						jarray.Add(jTokenToAppend);
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002894 File Offset: 0x00000A94
		public static void AddNewArrayProperty(JContainer mergedJson, JsonMerger.ArrayModification arrayModification)
		{
			if (string.IsNullOrEmpty(arrayModification.ParentPath))
			{
				mergedJson.Add(arrayModification.JProperty);
				return;
			}
			JObject jobject = mergedJson.SelectToken(arrayModification.ParentPath) as JObject;
			if (jobject != null)
			{
				jobject.Add(arrayModification.JProperty);
				return;
			}
			Debug.LogWarning("Parent path not found: " + arrayModification.ParentPath + ". Was probably removed by other json.");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028F8 File Offset: 0x00000AF8
		public void ProcessArrayRemovals(JToken mergedJson)
		{
			foreach (JsonMerger.ArrayModification arrayModification in this._arrayRemovals)
			{
				JToken jtoken = mergedJson.SelectToken(arrayModification.Path);
				if (jtoken != null)
				{
					if (jtoken.Type != JTokenType.Array)
					{
						throw new ArgumentException("Cannot remove from non-array path: " + jtoken.Path);
					}
					JsonMerger.RemoveFromExistingArrayProperty((JArray)jtoken, arrayModification.JProperty);
				}
			}
			this._arrayRemovals.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002990 File Offset: 0x00000B90
		public static void RemoveFromExistingArrayProperty(JArray existingArray, JProperty arrayRemoval)
		{
			foreach (JToken t in arrayRemoval.Value.Children())
			{
				for (int i = 0; i < existingArray.Count; i++)
				{
					if (JToken.DeepEquals(existingArray[i], t))
					{
						existingArray.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A08 File Offset: 0x00000C08
		public void ProcessPropertyDeletions(JToken mergedJson)
		{
			foreach (string text in this._propertyDeletions)
			{
				JToken jtoken = mergedJson.SelectToken(text);
				if (jtoken != null)
				{
					JProperty jproperty = jtoken.Parent as JProperty;
					if (jproperty != null)
					{
						jproperty.Remove();
					}
					else
					{
						JProperty jproperty2 = jtoken as JProperty;
						if (jproperty2 != null)
						{
							jproperty2.Remove();
						}
						else
						{
							Debug.LogWarning("Cannot delete path (not a property): " + text);
						}
					}
				}
			}
			this._propertyDeletions.Clear();
		}

		// Token: 0x0400000D RID: 13
		public static readonly JsonMergeSettings ReplaceSettings = new JsonMergeSettings
		{
			MergeArrayHandling = MergeArrayHandling.Replace
		};

		// Token: 0x0400000E RID: 14
		public readonly List<JsonMerger.ArrayModification> _arrayAppenders = new List<JsonMerger.ArrayModification>();

		// Token: 0x0400000F RID: 15
		public readonly List<JsonMerger.ArrayModification> _arrayRemovals = new List<JsonMerger.ArrayModification>();

		// Token: 0x04000010 RID: 16
		public readonly List<string> _propertyDeletions = new List<string>();

		// Token: 0x02000008 RID: 8
		public class ArrayModification
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002AE4 File Offset: 0x00000CE4
			public JProperty JProperty { get; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600001F RID: 31 RVA: 0x00002AEC File Offset: 0x00000CEC
			public string Path { get; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002AF4 File Offset: 0x00000CF4
			public string ParentPath { get; }

			// Token: 0x06000021 RID: 33 RVA: 0x00002AFC File Offset: 0x00000CFC
			public ArrayModification(JProperty jProperty, string path, string parentPath)
			{
				this.JProperty = jProperty;
				this.Path = path;
				this.ParentPath = parentPath;
			}
		}
	}
}
