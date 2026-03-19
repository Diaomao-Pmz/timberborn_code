using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000017 RID: 23
	public class UniquelyNamedEntityService
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003274 File Offset: 0x00001474
		public bool TryGet(string name, out UniquelyNamedEntity uniquelyNamedEntity)
		{
			List<UniquelyNamedEntity> list;
			if (this._entitiesByName.TryGetValue(name, out list) && list.Count == 1)
			{
				uniquelyNamedEntity = list[0];
				return true;
			}
			uniquelyNamedEntity = null;
			return false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032AC File Offset: 0x000014AC
		public void RegisterName(string name, UniquelyNamedEntity newEntity)
		{
			List<UniquelyNamedEntity> orAdd = this._entitiesByName.GetOrAdd(name);
			foreach (UniquelyNamedEntity uniquelyNamedEntity in orAdd)
			{
				uniquelyNamedEntity.SetNonUnique();
			}
			orAdd.Add(newEntity);
			if (orAdd.Count == 1)
			{
				newEntity.SetUnique();
				return;
			}
			newEntity.SetNonUnique();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003324 File Offset: 0x00001524
		public void UnregisterName(string name, UniquelyNamedEntity removedEntity)
		{
			List<UniquelyNamedEntity> list;
			if (!this._entitiesByName.TryGetValue(name, out list) || !list.Remove(removedEntity))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Entity '",
					removedEntity.Name,
					"' is not registered under name '",
					name,
					"'"
				}));
			}
			if (list.Count == 1)
			{
				list[0].SetUnique();
				return;
			}
			if (list.Count == 0)
			{
				this._entitiesByName.Remove(name);
			}
		}

		// Token: 0x0400003A RID: 58
		public readonly Dictionary<string, List<UniquelyNamedEntity>> _entitiesByName = new Dictionary<string, List<UniquelyNamedEntity>>();
	}
}
