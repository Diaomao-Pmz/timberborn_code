using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.WorldSerialization
{
	// Token: 0x02000005 RID: 5
	public class SerializedEntity : IEquatable<SerializedEntity>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public Guid Id { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002127 File Offset: 0x00000327
		public string TemplateName { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000212F File Offset: 0x0000032F
		public SerializedEntity(Guid id, string templateName)
		{
			this.Id = id;
			this.TemplateName = templateName;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		public IEnumerable<string> Components()
		{
			return this._components.Keys.AsReadOnlyEnumerable<string>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002162 File Offset: 0x00000362
		public bool HasComponents()
		{
			return this._components.Count > 0;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002172 File Offset: 0x00000372
		public bool HasComponent(string name)
		{
			return this._components.ContainsKey(name);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002180 File Offset: 0x00000380
		public void AddComponent(SerializedComponent serializedComponent)
		{
			this._components.Add(serializedComponent.Name, serializedComponent);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002194 File Offset: 0x00000394
		public SerializedComponent GetComponent(string name)
		{
			if (!this.HasComponent(name))
			{
				throw new ArgumentOutOfRangeException(string.Format("Component {0} wasn't found in entity {1} {2}", name, this.TemplateName, this.Id));
			}
			return this._components[name];
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021D0 File Offset: 0x000003D0
		public SerializedComponent GetOrAddComponent(string name)
		{
			return this._components.GetOrAdd(name, () => new SerializedComponent(name));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002208 File Offset: 0x00000408
		public bool Equals(SerializedEntity other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Id != other.Id || this.TemplateName != other.TemplateName)
			{
				return false;
			}
			if (this._components.Count != other._components.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, SerializedComponent> keyValuePair in this._components)
			{
				SerializedComponent other2;
				if (!other._components.TryGetValue(keyValuePair.Key, out other2) || !keyValuePair.Value.Equals(other2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, SerializedComponent> _components = new Dictionary<string, SerializedComponent>();
	}
}
