using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Timberborn.SerializationSystem
{
	// Token: 0x02000004 RID: 4
	public class JsonDiffer
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public string GenerateDiffJson(string originalJson, string modifiedJson)
		{
			JToken jtoken = JsonConvert.DeserializeObject<JToken>(originalJson);
			JToken jtoken2 = JsonConvert.DeserializeObject<JToken>(modifiedJson);
			JObject jobject = JsonDiffer.DiffObject(jtoken as JObject, jtoken2 as JObject);
			if (jobject != null)
			{
				return JsonConvert.SerializeObject(jobject, Formatting.Indented);
			}
			return string.Empty;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public static JObject DiffObject(JObject original, JObject modified)
		{
			JObject jobject = new JObject();
			foreach (JProperty jproperty in modified.Properties())
			{
				JProperty jproperty2 = original.Property(jproperty.Name);
				if (jproperty2 == null)
				{
					jobject[jproperty.Name] = jproperty.Value;
				}
				else
				{
					JToken jtoken = JsonDiffer.DiffToken(jproperty2.Value, jproperty.Value);
					if (jtoken != null)
					{
						if (jproperty2.Value.Type == JTokenType.Array)
						{
							using (IEnumerator<JProperty> enumerator2 = ((JObject)jtoken).Properties().GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									JProperty jproperty3 = enumerator2.Current;
									jobject[jproperty.Name + jproperty3.Name] = jproperty3.Value;
								}
								continue;
							}
						}
						jobject[jproperty.Name] = jtoken;
					}
				}
			}
			foreach (JProperty jproperty4 in original.Properties())
			{
				if (modified.Property(jproperty4.Name) == null)
				{
					jobject[jproperty4.Name + JsonKeywords.Delete] = new JObject();
				}
			}
			if (!jobject.HasValues)
			{
				return null;
			}
			return jobject;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002274 File Offset: 0x00000474
		public static JToken DiffToken(JToken original, JToken modified)
		{
			if (original.Type != modified.Type)
			{
				return modified;
			}
			JTokenType type = original.Type;
			if (type == JTokenType.Object)
			{
				return JsonDiffer.DiffObject(original as JObject, modified as JObject);
			}
			if (type == JTokenType.Array)
			{
				return JsonDiffer.DiffArray(original as JArray, modified as JArray);
			}
			if (!JToken.DeepEquals(original, modified))
			{
				return modified;
			}
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022D4 File Offset: 0x000004D4
		public static JObject DiffArray(JArray original, JArray modified)
		{
			JObject jobject = new JObject();
			JsonDiffer.AddRemovedItems(original, modified, jobject);
			JsonDiffer.AddAppendedItems(original, modified, jobject);
			if (!jobject.HasValues)
			{
				return null;
			}
			return jobject;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002304 File Offset: 0x00000504
		public static void AddRemovedItems(JArray original, JArray modified, JObject diff)
		{
			IEnumerable<string> first = new HashSet<string>(from x in original
			select x.ToString(Formatting.None));
			HashSet<string> second = new HashSet<string>(from x in modified
			select x.ToString(Formatting.None));
			List<string> source = first.Except(second).ToList<string>();
			if (source.Any<string>())
			{
				diff[JsonKeywords.Remove] = new JArray(source.Select(new Func<string, JToken>(JToken.Parse)));
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000239C File Offset: 0x0000059C
		public static void AddAppendedItems(JArray original, JArray modified, JObject diff)
		{
			List<JToken> list = modified.ToList<JToken>();
			foreach (JToken jtoken in original)
			{
				for (int i = 0; i < list.Count; i++)
				{
					JToken jtoken2 = list[i];
					if (jtoken.Type == jtoken2.Type && JToken.DeepEquals(jtoken, jtoken2))
					{
						list.RemoveAt(i);
						break;
					}
				}
			}
			if (list.Any<JToken>())
			{
				diff[JsonKeywords.Append] = new JArray(list);
			}
		}
	}
}
