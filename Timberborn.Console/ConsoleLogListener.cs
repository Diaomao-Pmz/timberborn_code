using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.Console
{
	// Token: 0x02000005 RID: 5
	public static class ConsoleLogListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002110 File Offset: 0x00000310
		public static event EventHandler<Log> OnLogReceived;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public static event EventHandler<Log> OnFirstWarningOrErrorReceived;

		// Token: 0x06000009 RID: 9 RVA: 0x000021AB File Offset: 0x000003AB
		[RuntimeInitializeOnLoadMethod(4)]
		public static void Initialize()
		{
			Application.logMessageReceived += new Application.LogCallback(ConsoleLogListener.OnLogMessageReceived);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BE File Offset: 0x000003BE
		public static ImmutableArray<Log> GetAllLogs()
		{
			return ConsoleLogListener.Logs.ToImmutableArray<Log>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
		public static void OnLogMessageReceived(string message, string stacktrace, LogType type)
		{
			try
			{
				Log log = new Log(message, type);
				ConsoleLogListener.Logs.Enqueue(log);
				ConsoleLogListener.Trim();
				EventHandler<Log> onLogReceived = ConsoleLogListener.OnLogReceived;
				if (onLogReceived != null)
				{
					onLogReceived(null, log);
				}
				if ((type == 2 || type == null) && !ConsoleLogListener.AnyWarningOrError)
				{
					ConsoleLogListener.AnyWarningOrError = true;
					EventHandler<Log> onFirstWarningOrErrorReceived = ConsoleLogListener.OnFirstWarningOrErrorReceived;
					if (onFirstWarningOrErrorReceived != null)
					{
						onFirstWarningOrErrorReceived(null, log);
					}
				}
			}
			catch (Exception arg)
			{
				Application.logMessageReceived -= new Application.LogCallback(ConsoleLogListener.OnLogMessageReceived);
				Debug.LogError(string.Format("Exception while processing a log message: {0}", arg));
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002260 File Offset: 0x00000460
		public static void Trim()
		{
			while (ConsoleLogListener.Logs.Count > ConsoleLogListener.MaxLogs)
			{
				ConsoleLogListener.Logs.Dequeue();
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly int MaxLogs = 1000;

		// Token: 0x04000007 RID: 7
		public static readonly Queue<Log> Logs = new Queue<Log>();

		// Token: 0x04000008 RID: 8
		public static bool AnyWarningOrError;
	}
}
