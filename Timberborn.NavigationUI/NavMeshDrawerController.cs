using System;
using Timberborn.CursorToolSystem;
using Timberborn.Debugging;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.NavigationUI
{
	// Token: 0x02000006 RID: 6
	public class NavMeshDrawerController : IDevModule, IUpdatableSingleton
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000212D File Offset: 0x0000032D
		public NavMeshDrawerController(INavMeshDrawer navMeshDrawer, CursorCoordinatesPicker cursorCoordinatesPicker)
		{
			this._navMeshDrawer = navMeshDrawer;
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002143 File Offset: 0x00000343
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle nav mesh", new Action(this.Toggle))).Build();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216A File Offset: 0x0000036A
		public void UpdateSingleton()
		{
			if (this._draw)
			{
				this.DrawNavMesh();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217A File Offset: 0x0000037A
		public void Toggle()
		{
			this._draw = !this._draw;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000218C File Offset: 0x0000038C
		public void DrawNavMesh()
		{
			CursorCoordinates? cursorCoordinates = this._cursorCoordinatesPicker.Pick();
			if (cursorCoordinates != null)
			{
				CursorCoordinates valueOrDefault = cursorCoordinates.GetValueOrDefault();
				this._navMeshDrawer.DrawForOneFrameAroundCoordinates(valueOrDefault.TileCoordinates);
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly INavMeshDrawer _navMeshDrawer;

		// Token: 0x0400000A RID: 10
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;

		// Token: 0x0400000B RID: 11
		public bool _draw;
	}
}
