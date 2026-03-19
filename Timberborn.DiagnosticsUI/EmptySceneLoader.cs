using System;
using Timberborn.Debugging;
using Timberborn.SceneLoading;
using Timberborn.Versioning;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x02000005 RID: 5
	public class EmptySceneLoader : IDevModule
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public EmptySceneLoader(ISceneLoader sceneLoader)
		{
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002130 File Offset: 0x00000330
		public DevModuleDefinition GetDefinition()
		{
			DevModuleDefinition.Builder builder = new DevModuleDefinition.Builder();
			if (GameVersions.CurrentVersion.IsDevelopmentVersion)
			{
				builder.AddMethod(DevMethod.Create("Load empty scene", new Action(this.LoadEmptyScene)));
			}
			return builder.Build();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002175 File Offset: 0x00000375
		public void LoadEmptyScene()
		{
			this._sceneLoader.LoadSceneInstantly(new EmptySceneLoader.EmptySceneParameters());
		}

		// Token: 0x04000006 RID: 6
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x02000006 RID: 6
		public class EmptySceneParameters : ISceneParameters
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000008 RID: 8 RVA: 0x00002187 File Offset: 0x00000387
			public int SceneIndex
			{
				get
				{
					return 5;
				}
			}
		}
	}
}
