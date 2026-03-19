using System;
using Timberborn.AreaSelectionSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystemUI
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectSelectionDrawerFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002230 File Offset: 0x00000430
		public BlockObjectSelectionDrawerFactory(Highlighter highlighter, RectangleBoundsDrawerFactory rectangleBoundsDrawerFactory, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._highlighter = highlighter;
			this._rectangleBoundsDrawerFactory = rectangleBoundsDrawerFactory;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000224D File Offset: 0x0000044D
		public BlockObjectSelectionDrawer Create(Color blockObjectHighlightColor, Color areaTileColor, Color areaSideColor)
		{
			return new BlockObjectSelectionDrawer(this._rectangleBoundsDrawerFactory.Create(areaTileColor, areaSideColor), new RollingHighlighter(this._highlighter), blockObjectHighlightColor, this._measurableAreaDrawer);
		}

		// Token: 0x0400000E RID: 14
		public readonly Highlighter _highlighter;

		// Token: 0x0400000F RID: 15
		public readonly RectangleBoundsDrawerFactory _rectangleBoundsDrawerFactory;

		// Token: 0x04000010 RID: 16
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;
	}
}
