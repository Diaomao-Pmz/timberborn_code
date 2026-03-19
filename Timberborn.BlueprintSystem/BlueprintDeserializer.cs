using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000011 RID: 17
	public class BlueprintDeserializer
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002D8F File Offset: 0x00000F8F
		public BlueprintDeserializer(BasicDeserializer basicDeserializer, AdvancedDeserializer advancedDeserializer, SerializedObjectReaderWriter serializedObjectReaderWriter, BlueprintFileBundleLoader blueprintFileBundleLoader)
		{
			this._basicDeserializer = basicDeserializer;
			this._advancedDeserializer = advancedDeserializer;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._blueprintFileBundleLoader = blueprintFileBundleLoader;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DBF File Offset: 0x00000FBF
		public IEnumerable<Type> GetSpecTypes(SerializedObject serializedObject)
		{
			foreach (string key in serializedObject.Properties())
			{
				Type type;
				if (this._specTypeCache.TryGetType(key, out type))
				{
					yield return type;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DD6 File Offset: 0x00000FD6
		public Blueprint DeserializeSafe(BlueprintFileBundle blueprintFileBundle)
		{
			return this.DeserializeRoot(blueprintFileBundle, true);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public Blueprint DeserializeUnsafe(BlueprintFileBundle blueprintFileBundle)
		{
			Blueprint result;
			try
			{
				result = this.DeserializeRoot(blueprintFileBundle, false);
			}
			catch (Exception)
			{
				Debug.LogError("Failed to deserialize blueprint " + blueprintFileBundle.Path);
				throw;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E24 File Offset: 0x00001024
		public Blueprint DeserializeRoot(BlueprintFileBundle blueprintFileBundle, bool safe)
		{
			SerializedObject serializedObject = this.FileBundleToSerializedObject(blueprintFileBundle);
			ImmutableArray<string> nestedBlueprints = ImmutableArray.Create<string>(blueprintFileBundle.Path);
			return this.DeserializeBlueprint(serializedObject, safe, nestedBlueprints, blueprintFileBundle.Name);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E54 File Offset: 0x00001054
		public SerializedObject FileBundleToSerializedObject(BlueprintFileBundle blueprintFileBundle)
		{
			return this._serializedObjectReaderWriter.ReadJsons(blueprintFileBundle.Jsons);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E6C File Offset: 0x0000106C
		public Blueprint DeserializeBlueprint(SerializedObject serializedObject, bool safe, ImmutableArray<string> nestedBlueprints, string name)
		{
			return new Blueprint(name, this.DeserializeSpecs(serializedObject, safe), this.DeserializeChildren(serializedObject, safe, nestedBlueprints));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E86 File Offset: 0x00001086
		public IEnumerable<ComponentSpec> DeserializeSpecs(SerializedObject serializedObject, bool safe)
		{
			foreach (string text in serializedObject.Properties())
			{
				if (text != BlueprintDeserializer.ChildrenProperty)
				{
					SerializedObject serializedObject2 = serializedObject.Get<SerializedObject>(text);
					Type type;
					if (this._specTypeCache.TryGetType(text, out type))
					{
						yield return this.DeserializeSpec(serializedObject2, type);
					}
					else
					{
						if (!safe)
						{
							throw new ArgumentException("No type found for key " + text);
						}
						string content = this._serializedObjectReaderWriter.WriteJson(serializedObject2);
						yield return new NonExistingSpec
						{
							SpecName = text,
							Content = content
						};
					}
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EA4 File Offset: 0x000010A4
		public ComponentSpec DeserializeSpec(SerializedObject serializedObject, Type type)
		{
			object instance = this._basicDeserializer.Deserialize(serializedObject, type);
			return (ComponentSpec)this._advancedDeserializer.Deserialize(instance, type);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002ED4 File Offset: 0x000010D4
		public ImmutableArray<Blueprint> DeserializeChildren(SerializedObject serializedObject, bool safe, ImmutableArray<string> nestedBlueprints)
		{
			List<Blueprint> list = new List<Blueprint>();
			if (serializedObject.Has(BlueprintDeserializer.ChildrenProperty))
			{
				SerializedObject serializedObject2 = serializedObject.Get<SerializedObject>(BlueprintDeserializer.ChildrenProperty);
				foreach (string text in serializedObject2.Properties())
				{
					SerializedObject serializedObject3 = serializedObject2.Get<SerializedObject>(text);
					list.Add(text.Contains(JsonKeywords.Nested) ? this.DeserializeNestedBlueprint(serializedObject3, safe, nestedBlueprints, text) : this.DeserializeBlueprint(serializedObject3, safe, nestedBlueprints, text));
				}
			}
			return list.ToImmutableArray<Blueprint>();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F74 File Offset: 0x00001174
		public Blueprint DeserializeNestedBlueprint(SerializedObject serializedObject, bool safe, ImmutableArray<string> nestedBlueprints, string name)
		{
			string text = serializedObject.Get<string>(NestedBlueprintSpec.BlueprintPathKey);
			if (nestedBlueprints.Contains(text))
			{
				throw new InvalidOperationException("Circular reference detected in nested Blueprints: " + string.Join("->", nestedBlueprints) + "->" + text);
			}
			Blueprint originalBlueprint = this.DeserializeNestedBlueprint(serializedObject, safe, nestedBlueprints, text, name);
			return this.AddNestedBlueprintSpec(originalBlueprint, serializedObject, text);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FD4 File Offset: 0x000011D4
		public Blueprint DeserializeNestedBlueprint(SerializedObject serializedObject, bool safe, ImmutableArray<string> nestedBlueprints, string blueprintPath, string name)
		{
			BlueprintFileBundle bundle = this._blueprintFileBundleLoader.GetBundle(blueprintPath);
			if (bundle != null)
			{
				return this.DeserializeNestedBlueprintUnwrapped(serializedObject, bundle, safe, nestedBlueprints.Add(blueprintPath), name);
			}
			if (safe)
			{
				return new Blueprint(name, ImmutableArray.Create<ComponentSpec>(), ImmutableArray<Blueprint>.Empty);
			}
			throw new InvalidOperationException("Blueprint bundle not found for path: " + blueprintPath);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003034 File Offset: 0x00001234
		public Blueprint DeserializeNestedBlueprintUnwrapped(SerializedObject serializedObject, BlueprintFileBundle blueprintFileBundle, bool safe, ImmutableArray<string> nestedBlueprints, string name)
		{
			BlueprintFileBundle blueprintFileBundle2 = serializedObject.Has(NestedBlueprintSpec.ModificationKey) ? blueprintFileBundle.AddJson(this.GetModificationJson(serializedObject), "Modifying") : blueprintFileBundle;
			SerializedObject serializedObject2 = this.FileBundleToSerializedObject(blueprintFileBundle2);
			return this.DeserializeBlueprint(serializedObject2, safe, nestedBlueprints, name);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003078 File Offset: 0x00001278
		public Blueprint AddNestedBlueprintSpec(Blueprint originalBlueprint, SerializedObject serializedObject, string blueprintPath)
		{
			string modification = serializedObject.Has(NestedBlueprintSpec.ModificationKey) ? this.GetModificationJson(serializedObject) : string.Empty;
			NestedBlueprintSpec newSpec = new NestedBlueprintSpec
			{
				BlueprintPath = blueprintPath,
				Modification = modification
			};
			return new Blueprint(originalBlueprint, null, newSpec);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030BD File Offset: 0x000012BD
		public string GetModificationJson(SerializedObject serializedObject)
		{
			return this._serializedObjectReaderWriter.WriteJson(serializedObject.Get<SerializedObject>(NestedBlueprintSpec.ModificationKey));
		}

		// Token: 0x04000021 RID: 33
		public static readonly string ChildrenProperty = "Children";

		// Token: 0x04000022 RID: 34
		public readonly BasicDeserializer _basicDeserializer;

		// Token: 0x04000023 RID: 35
		public readonly AdvancedDeserializer _advancedDeserializer;

		// Token: 0x04000024 RID: 36
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x04000025 RID: 37
		public readonly BlueprintFileBundleLoader _blueprintFileBundleLoader;

		// Token: 0x04000026 RID: 38
		public readonly SpecTypeCache _specTypeCache = SpecTypeCache.Create();
	}
}
