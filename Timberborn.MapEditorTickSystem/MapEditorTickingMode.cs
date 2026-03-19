using System;
using System.Reflection;
using Timberborn.TickSystem;

namespace Timberborn.MapEditorTickSystem
{
	// Token: 0x02000004 RID: 4
	internal class MapEditorTickingMode : ITickingMode
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool SingletonIsActiveInThisMode(object singleton)
		{
			return singleton.GetType().GetCustomAttribute(MapEditorTickingMode.AttributeType) != null;
		}

		// Token: 0x04000001 RID: 1
		private static readonly Type AttributeType = typeof(MapEditorTickableAttribute);
	}
}
