using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.SerializationSystem;
using Timberborn.Versioning;
using UnityEngine;

namespace Timberborn.Modding
{
	// Token: 0x0200000B RID: 11
	public class ManifestLoader
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002687 File Offset: 0x00000887
		public ManifestLoader(SerializedObjectReaderWriter serializedObjectReaderWriter)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002698 File Offset: 0x00000898
		public bool TryLoadManifest(string modDirectory, out ModManifest manifest)
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(modDirectory, ManifestLoader.ManifestFileName));
			if (fileInfo.Exists)
			{
				return this.TryDeserializeManifest(fileInfo, out manifest);
			}
			manifest = null;
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026CC File Offset: 0x000008CC
		public bool TryDeserializeManifest(FileInfo manifestFile, out ModManifest manifest)
		{
			bool result;
			try
			{
				using (FileStream fileStream = manifestFile.OpenRead())
				{
					SerializedObject serializedObject = this._serializedObjectReaderWriter.ReadJson(fileStream);
					manifest = new ModManifest(serializedObject.Get<string>("Name"), serializedObject.Has("Description") ? serializedObject.Get<string>("Description") : string.Empty, Version.Create(serializedObject.Get<string>("Version")), serializedObject.Get<string>("Id"), Version.Create(serializedObject.Get<string>("MinimumGameVersion")), ManifestLoader.GetVersionedMods(serializedObject, "RequiredMods"), ManifestLoader.GetVersionedMods(serializedObject, "OptionalMods"));
					result = true;
				}
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Failed to load mod manifest from {0}: {1}", manifestFile.FullName, arg));
				manifest = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027A8 File Offset: 0x000009A8
		public static IEnumerable<VersionedMod> GetVersionedMods(SerializedObject save, string arrayName)
		{
			if (save.Has(arrayName))
			{
				foreach (SerializedObject serializedObject in save.GetArray<SerializedObject>(arrayName))
				{
					string id = serializedObject.Get<string>("Id");
					string version = serializedObject.Has("MinimumVersion") ? serializedObject.Get<string>("MinimumVersion") : "0";
					yield return new VersionedMod(id, Version.Create(version));
				}
				SerializedObject[] array = null;
			}
			yield break;
		}

		// Token: 0x04000014 RID: 20
		public static readonly string ManifestFileName = "manifest.json";

		// Token: 0x04000015 RID: 21
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;
	}
}
