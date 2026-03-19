using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.AssetSystem;
using Timberborn.Common;
using Timberborn.SerializationSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000028 RID: 40
	public class SpecService : ISpecService, ILoadableSingleton
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004368 File Offset: 0x00002568
		public SpecService(SerializedObjectReaderWriter serializedObjectReaderWriter, BlueprintDeserializer blueprintDeserializer, BlueprintFileBundleLoader blueprintFileBundleLoader, BlueprintSourceService blueprintSourceService, IEnumerable<IBlueprintModifierProvider> blueprintModifierProviders)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._blueprintDeserializer = blueprintDeserializer;
			this._blueprintFileBundleLoader = blueprintFileBundleLoader;
			this._blueprintSourceService = blueprintSourceService;
			this._blueprintModifierProviders = blueprintModifierProviders.ToImmutableArray<IBlueprintModifierProvider>();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000043BC File Offset: 0x000025BC
		public void Load()
		{
			using (IEnumerator<BlueprintFileBundle> enumerator = this._blueprintFileBundleLoader.GetBundles(string.Empty).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BlueprintFileBundle blueprintBundle = enumerator.Current;
					Lazy<Blueprint> lazy = new Lazy<Blueprint>(() => this.Deserialize(blueprintBundle));
					this._cachedBlueprintsByPath.Add(blueprintBundle.Path, lazy);
					SerializedObject serializedObject = this._serializedObjectReaderWriter.ReadJsons(blueprintBundle.Jsons);
					foreach (Type key in this._blueprintDeserializer.GetSpecTypes(serializedObject))
					{
						this._cachedBlueprintsBySpecs.GetOrAdd(key).Add(lazy);
					}
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000044C0 File Offset: 0x000026C0
		public Blueprint GetBlueprint(string blueprintPath)
		{
			string key = AssetPathHelper.NormalizePath(blueprintPath);
			Lazy<Blueprint> lazy;
			if (this._cachedBlueprintsByPath.TryGetValue(key, out lazy))
			{
				return lazy.Value;
			}
			throw new ArgumentException("Blueprint not found at path: " + blueprintPath);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000044FC File Offset: 0x000026FC
		public T GetSingleSpec<T>() where T : ComponentSpec
		{
			List<Lazy<Blueprint>> orDefault = this._cachedBlueprintsBySpecs.GetOrDefault(typeof(T));
			int count = orDefault.Count;
			if (count > 1)
			{
				throw new Exception(string.Format("Multiple blueprints found for {0}", typeof(T)));
			}
			if (count == 0)
			{
				throw new Exception(string.Format("No blueprint found for {0}", typeof(T)));
			}
			return orDefault[0].Value.GetSpec<T>();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004574 File Offset: 0x00002774
		public IEnumerable<T> GetSpecs<T>() where T : ComponentSpec
		{
			List<Lazy<Blueprint>> orDefault = this._cachedBlueprintsBySpecs.GetOrDefault(typeof(T));
			if (orDefault != null)
			{
				foreach (Lazy<Blueprint> lazy in orDefault)
				{
					yield return lazy.Value.GetSpec<T>();
				}
				List<Lazy<Blueprint>>.Enumerator enumerator = default(List<Lazy<Blueprint>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004584 File Offset: 0x00002784
		public Blueprint Deserialize(BlueprintFileBundle blueprintFileBundle)
		{
			foreach (IBlueprintModifierProvider blueprintModifierProvider in this._blueprintModifierProviders)
			{
				foreach (string json in blueprintModifierProvider.GetModifiers(blueprintFileBundle.Path))
				{
					blueprintFileBundle = blueprintFileBundle.AddJson(json, blueprintModifierProvider.ModifierName);
				}
			}
			Blueprint blueprint = this._blueprintDeserializer.DeserializeUnsafe(blueprintFileBundle);
			this._blueprintSourceService.Add(blueprint, blueprintFileBundle);
			return blueprint;
		}

		// Token: 0x0400005A RID: 90
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x0400005B RID: 91
		public readonly BlueprintDeserializer _blueprintDeserializer;

		// Token: 0x0400005C RID: 92
		public readonly BlueprintFileBundleLoader _blueprintFileBundleLoader;

		// Token: 0x0400005D RID: 93
		public readonly BlueprintSourceService _blueprintSourceService;

		// Token: 0x0400005E RID: 94
		public readonly ImmutableArray<IBlueprintModifierProvider> _blueprintModifierProviders;

		// Token: 0x0400005F RID: 95
		public readonly Dictionary<string, Lazy<Blueprint>> _cachedBlueprintsByPath = new Dictionary<string, Lazy<Blueprint>>();

		// Token: 0x04000060 RID: 96
		public readonly Dictionary<Type, List<Lazy<Blueprint>>> _cachedBlueprintsBySpecs = new Dictionary<Type, List<Lazy<Blueprint>>>();
	}
}
