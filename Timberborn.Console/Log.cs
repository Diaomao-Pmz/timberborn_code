using System;
using UnityEngine;

namespace Timberborn.Console
{
	// Token: 0x02000008 RID: 8
	public readonly struct Log
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002853 File Offset: 0x00000A53
		public string Message { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000285B File Offset: 0x00000A5B
		public LogType LogType { get; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002863 File Offset: 0x00000A63
		public Log(string message, LogType logType)
		{
			this.Message = message;
			this.LogType = logType;
		}
	}
}
