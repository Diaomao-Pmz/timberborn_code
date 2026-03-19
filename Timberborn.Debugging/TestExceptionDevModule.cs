using System;
using Timberborn.Versioning;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Timberborn.Debugging
{
	// Token: 0x02000010 RID: 16
	public class TestExceptionDevModule : IDevModule
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000240B File Offset: 0x0000060B
		public TestExceptionDevModule(DevModeManager devModeManager)
		{
			this._devModeManager = devModeManager;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000241C File Offset: 0x0000061C
		public DevModuleDefinition GetDefinition()
		{
			DevModuleDefinition.Builder builder = new DevModuleDefinition.Builder();
			if (GameVersions.CurrentVersion.IsDevelopmentVersion)
			{
				builder.AddMethod(DevMethod.Create("Test exception", new Action(TestExceptionDevModule.ThrowTestException)));
				builder.AddMethod(DevMethod.Create("Test exception non-dev", new Action(this.ThrowTestExceptionNonDev)));
				builder.AddMethod(DevMethod.Create("Test native abort", new Action(TestExceptionDevModule.Abort)));
				builder.AddMethod(DevMethod.Create("Test warning", new Action(TestExceptionDevModule.Warn)));
			}
			return builder.Build();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024B8 File Offset: 0x000006B8
		public static void ThrowTestException()
		{
			throw new Exception("Test");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024C4 File Offset: 0x000006C4
		public void ThrowTestExceptionNonDev()
		{
			this._devModeManager.Disable();
			TestExceptionDevModule.ThrowTestException();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024D6 File Offset: 0x000006D6
		public static void Abort()
		{
			Utils.ForceCrash(2);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024DE File Offset: 0x000006DE
		public static void Warn()
		{
			Debug.LogWarning("Test warning");
		}

		// Token: 0x04000018 RID: 24
		public readonly DevModeManager _devModeManager;
	}
}
