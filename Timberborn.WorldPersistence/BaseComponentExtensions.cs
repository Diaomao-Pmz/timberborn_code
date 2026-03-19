using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000004 RID: 4
	public static class BaseComponentExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static T GetNamedComponent<T>(this BaseComponent component, string componentName) where T : class, INamedComponent
		{
			component.GetComponents<INamedComponent>(BaseComponentExtensions.Components);
			INamedComponent namedComponent = BaseComponentExtensions.Components.SingleOrDefault((INamedComponent c) => c.ComponentName == componentName);
			BaseComponentExtensions.Components.Clear();
			return (T)((object)namedComponent);
		}

		// Token: 0x04000006 RID: 6
		public static readonly List<INamedComponent> Components = new List<INamedComponent>();
	}
}
