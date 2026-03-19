using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000004 RID: 4
	public class CachedTemplate
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public GameObject Prefab { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public ImmutableArray<CachedTemplateInitializer> Initializers { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public ImmutableArray<Type> Components { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public CachedTemplate(GameObject prefab, IEnumerable<CachedTemplateInitializer> initializers, List<Type> components)
		{
			this.Prefab = prefab;
			this.Initializers = initializers.ToImmutableArray<CachedTemplateInitializer>();
			this.Components = components.ToImmutableArray<Type>();
		}
	}
}
