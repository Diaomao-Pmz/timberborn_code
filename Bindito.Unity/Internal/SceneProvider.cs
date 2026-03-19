using System;
using UnityEngine.SceneManagement;

namespace Bindito.Unity.Internal
{
	// Token: 0x02000076 RID: 118
	public class SceneProvider : ISceneProvider
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002F69 File Offset: 0x00001169
		public Scene Scene { get; }

		// Token: 0x0600010E RID: 270 RVA: 0x00002F71 File Offset: 0x00001171
		public SceneProvider(Scene scene)
		{
			this.Scene = scene;
		}
	}
}
