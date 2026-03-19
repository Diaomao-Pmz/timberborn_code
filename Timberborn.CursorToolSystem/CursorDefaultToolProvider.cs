using System;
using Timberborn.ToolSystem;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x0200000B RID: 11
	public class CursorDefaultToolProvider : IDefaultToolProvider
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002839 File Offset: 0x00000A39
		public CursorDefaultToolProvider(CursorTool cursorTool)
		{
			this._cursorTool = cursorTool;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002848 File Offset: 0x00000A48
		public ITool DefaultTool
		{
			get
			{
				return this._cursorTool;
			}
		}

		// Token: 0x0400002B RID: 43
		public readonly CursorTool _cursorTool;
	}
}
