using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlueprintPrefabSystem
{
	// Token: 0x02000004 RID: 4
	public class BlueprintPrefabConverter
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlueprintPrefabConverter(IEnumerable<ISpecToPrefabConverter> specToPrefabConverters)
		{
			this._specToPrefabConverters = specToPrefabConverters.ToImmutableArray<ISpecToPrefabConverter>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public GameObject Convert(Blueprint blueprint, Transform parent)
		{
			GameObject gameObject = new GameObject(blueprint.Name);
			gameObject.transform.SetParent(parent.transform);
			foreach (ComponentSpec spec in blueprint.Specs)
			{
				foreach (ISpecToPrefabConverter specToPrefabConverter in this._specToPrefabConverters)
				{
					if (specToPrefabConverter.CanConvert(spec))
					{
						specToPrefabConverter.Convert(gameObject, spec);
					}
				}
			}
			foreach (Blueprint blueprint2 in blueprint.Children)
			{
				this.Convert(blueprint2, gameObject.transform);
			}
			return gameObject;
		}

		// Token: 0x04000006 RID: 6
		public readonly ImmutableArray<ISpecToPrefabConverter> _specToPrefabConverters;
	}
}
