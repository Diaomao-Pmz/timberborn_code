using System;

namespace Timberborn.SceneLoading
{
	// Token: 0x02000006 RID: 6
	public interface ISceneLoader
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		long LastLoadTimeMs { get; }

		// Token: 0x06000008 RID: 8
		void LoadScene(ISceneParameters sceneParameters, string tip);

		// Token: 0x06000009 RID: 9
		void LoadSceneInstantly(ISceneParameters sceneParameters, string tip);

		// Token: 0x0600000A RID: 10
		void LoadSceneInstantly(ISceneParameters sceneParameters);

		// Token: 0x0600000B RID: 11
		bool HasAnySceneParameters();

		// Token: 0x0600000C RID: 12
		bool TryGetSceneParameters<T>(out T sceneParameters) where T : ISceneParameters;

		// Token: 0x0600000D RID: 13
		T GetSceneParameters<T>() where T : ISceneParameters;
	}
}
