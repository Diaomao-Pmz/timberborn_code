using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.MapRepositorySystem;
using Timberborn.SceneLoading;
using UnityEngine;

namespace Timberborn.MapEditorSceneLoading
{
	// Token: 0x02000006 RID: 6
	public class MapEditorSceneLoader
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public MapEditorSceneLoader(ISceneLoader sceneLoader, ILoc loc, ISpecService specService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._sceneLoader = sceneLoader;
			this._loc = loc;
			this._specService = specService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002123 File Offset: 0x00000323
		public void StartNewMap(Vector2Int mapSize)
		{
			this._sceneLoader.LoadScene(MapEditorSceneParameters.CreateNewMapParameters(mapSize), this.Tip());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void StartNewMapInstantly(Vector2Int mapSize)
		{
			this._sceneLoader.LoadSceneInstantly(MapEditorSceneParameters.CreateNewMapParameters(mapSize), this.Tip());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002155 File Offset: 0x00000355
		public void LoadMap(MapFileReference mapFileReference)
		{
			this._sceneLoader.LoadScene(MapEditorSceneParameters.CreateExistingMapParameters(mapFileReference), this.Tip());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216E File Offset: 0x0000036E
		public void LoadMapInstantly(MapFileReference mapFileReference)
		{
			this._sceneLoader.LoadSceneInstantly(MapEditorSceneParameters.CreateExistingMapParameters(mapFileReference), this.Tip());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002188 File Offset: 0x00000388
		private string Tip()
		{
			MapEditorTipSpec singleSpec = this._specService.GetSingleSpec<MapEditorTipSpec>();
			string listElement = this._randomNumberGenerator.GetListElement<string>(singleSpec.Tips);
			return this._loc.T(listElement);
		}

		// Token: 0x04000003 RID: 3
		private readonly ISceneLoader _sceneLoader;

		// Token: 0x04000004 RID: 4
		private readonly ILoc _loc;

		// Token: 0x04000005 RID: 5
		private readonly ISpecService _specService;

		// Token: 0x04000006 RID: 6
		private readonly IRandomNumberGenerator _randomNumberGenerator;
	}
}
