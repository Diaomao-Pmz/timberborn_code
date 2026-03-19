using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000009 RID: 9
	public static class ExceptionListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00002724 File Offset: 0x00000924
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x00002758 File Offset: 0x00000958
		public static event EventHandler FirstUncaughtException;

		// Token: 0x06000023 RID: 35 RVA: 0x0000278B File Offset: 0x0000098B
		[RuntimeInitializeOnLoadMethod(1)]
		public static void Initialize()
		{
			Application.logMessageReceived += new Application.LogCallback(ExceptionListener.OnLog);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000279E File Offset: 0x0000099E
		public static void OnLog(string logString, string stackTrace, LogType type)
		{
			if (type == 4)
			{
				ExceptionListener.OnUncaughtException(logString, stackTrace);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027AC File Offset: 0x000009AC
		public static void OnUncaughtException(string logString, string stackTrace)
		{
			if (!ExceptionListener.AnyUncaughtException)
			{
				ExceptionListener.AnyUncaughtException = true;
				Application.logMessageReceived -= new Application.LogCallback(ExceptionListener.OnLog);
				string text = string.Format("First uncaught exception at {0:u}", DateTime.Now);
				Debug.LogError(Application.isEditor ? string.Concat(new string[]
				{
					text,
					" (click for details)\n\n",
					logString,
					"\n\n",
					stackTrace,
					"\n\n"
				}) : string.Concat(new string[]
				{
					text,
					"\n\n",
					logString,
					"\n\n",
					stackTrace,
					"\n\n"
				}));
				ExceptionListener.RememberError(logString, stackTrace);
				if (!ExceptionListener.ContinueOnErrors)
				{
					ExceptionListener.StopAllRootObjects();
				}
				ExceptionListener.InvokeListeners();
				CrashSceneLoader.LoadCrashSceneIfEnabled();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002878 File Offset: 0x00000A78
		public static void RememberError(string logString, string stackTrace)
		{
			ErrorReporter.LogString = logString;
			ErrorReporter.StackTrace = stackTrace;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002888 File Offset: 0x00000A88
		public static void InvokeListeners()
		{
			try
			{
				EventHandler firstUncaughtException = ExceptionListener.FirstUncaughtException;
				if (firstUncaughtException != null)
				{
					firstUncaughtException(null, EventArgs.Empty);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Exception while invoking listeners: {0}", arg));
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028D0 File Offset: 0x00000AD0
		public static void StopAllRootObjects()
		{
			Debug.Log("Stopping all root objects in active scene");
			try
			{
				GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
				for (int i = 0; i < rootGameObjects.Length; i++)
				{
					rootGameObjects[i].SetActive(false);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Exception while stopping objects: {0}", arg));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002934 File Offset: 0x00000B34
		public static bool ContinueOnErrors
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000012 RID: 18
		public static bool AnyUncaughtException;
	}
}
