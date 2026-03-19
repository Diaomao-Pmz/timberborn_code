using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200002B RID: 43
	public class SpecTypeCache
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000047CB File Offset: 0x000029CB
		public SpecTypeCache(FrozenDictionary<string, Type> typeMap)
		{
			this._typeMap = typeMap;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000047DC File Offset: 0x000029DC
		public static SpecTypeCache Create()
		{
			return new SpecTypeCache(AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly) => assembly.GetTypes()).Where(new Func<Type, bool>(SpecTypeCache.IsSpecClass)).SelectMany(new Func<Type, IEnumerable<KeyValuePair<string, Type>>>(SpecTypeCache.GetSpecEntries)).ToFrozenDictionary(null));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004844 File Offset: 0x00002A44
		public bool TryGetType(string key, out Type type)
		{
			if (this._typeMap.TryGetValue(key, out type))
			{
				return true;
			}
			if (key != SpecTypeCache.ChildrenProperty)
			{
				Debug.LogWarning("No type found for key " + key);
			}
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004875 File Offset: 0x00002A75
		public static bool IsSpecClass(Type type)
		{
			return type.IsClass && !type.IsAbstract && SpecTypeCache.SpecType.IsAssignableFrom(type);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004894 File Offset: 0x00002A94
		public static IEnumerable<KeyValuePair<string, Type>> GetSpecEntries(Type type)
		{
			SpecAliasAttribute specAliasAttribute = type.GetCustomAttributes(SpecTypeCache.SpecAliasAttribute, true).SingleOrDefault<object>() as SpecAliasAttribute;
			if (specAliasAttribute != null)
			{
				foreach (string key in specAliasAttribute.Aliases)
				{
					yield return new KeyValuePair<string, Type>(key, type);
				}
				ImmutableArray<string>.Enumerator enumerator = default(ImmutableArray<string>.Enumerator);
			}
			yield return new KeyValuePair<string, Type>(type.Name, type);
			yield break;
		}

		// Token: 0x04000068 RID: 104
		public static readonly string ChildrenProperty = "Children";

		// Token: 0x04000069 RID: 105
		public static readonly Type SpecType = typeof(ComponentSpec);

		// Token: 0x0400006A RID: 106
		public static readonly Type SpecAliasAttribute = typeof(SpecAliasAttribute);

		// Token: 0x0400006B RID: 107
		public readonly FrozenDictionary<string, Type> _typeMap;
	}
}
