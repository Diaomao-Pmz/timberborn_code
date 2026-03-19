using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SerializationSystem;
using Timberborn.Versioning;

namespace Timberborn.WorldSerialization
{
	// Token: 0x02000008 RID: 8
	public class SerializedWorld
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002309 File Offset: 0x00000509
		public Version Version { get; }

		// Token: 0x06000019 RID: 25 RVA: 0x00002311 File Offset: 0x00000511
		public SerializedWorld(Version version)
		{
			this.Version = version;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002336 File Offset: 0x00000536
		public IEnumerable<SerializedEntity> Entities()
		{
			return this._entities.AsReadOnlyEnumerable<SerializedEntity>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002343 File Offset: 0x00000543
		public IEnumerable<SerializedSingleton> Singletons()
		{
			return this._singletons.Values.AsReadOnlyEnumerable<SerializedSingleton>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002355 File Offset: 0x00000555
		public void AddEntity(SerializedEntity serializedEntity)
		{
			this._entities.Add(serializedEntity);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002363 File Offset: 0x00000563
		public void AddSingleton(SerializedSingleton serializedSingleton)
		{
			this._singletons.Add(serializedSingleton.Name, serializedSingleton);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002378 File Offset: 0x00000578
		public SerializedObject GetOrAddSingleton(string name)
		{
			SerializedSingleton serializedSingleton;
			if (!this._singletons.TryGetValue(name, out serializedSingleton))
			{
				serializedSingleton = new SerializedSingleton(name);
				this._singletons.Add(name, serializedSingleton);
			}
			return serializedSingleton.Value;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B0 File Offset: 0x000005B0
		public SerializedObject GetSingleton(string name)
		{
			SerializedSingleton serializedSingleton;
			if (this._singletons.TryGetValue(name, out serializedSingleton))
			{
				return serializedSingleton.Value;
			}
			throw new ArgumentException("Singleton '" + name + "' does not exist.");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E9 File Offset: 0x000005E9
		public bool HasSingleton(string name)
		{
			return this._singletons.ContainsKey(name);
		}

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<string, SerializedSingleton> _singletons = new Dictionary<string, SerializedSingleton>();

		// Token: 0x04000010 RID: 16
		public readonly List<SerializedEntity> _entities = new List<SerializedEntity>();
	}
}
