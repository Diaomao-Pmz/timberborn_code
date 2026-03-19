using System;
using System.Reflection;
using Timberborn.TickSystem;

namespace Timberborn.MapEditorTickSystem
{
	// Token: 0x02000005 RID: 5
	public class MapEditorTickingMode : ITickingMode
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool SingletonIsActiveInThisMode(object singleton)
		{
			return singleton.GetType().GetCustomAttribute(MapEditorTickingMode.AttributeType) != null;
		}

		// Token: 0x04000006 RID: 6
		public static readonly Type AttributeType = typeof(MapEditorTickableAttribute);
	}
}
