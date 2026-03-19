using System;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000018 RID: 24
	public class PathService : IPathService
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003685 File Offset: 0x00001885
		public PathService(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000369B File Offset: 0x0000189B
		public bool IsPath(Vector3Int coordinates)
		{
			return this.IsPath(coordinates, true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036A5 File Offset: 0x000018A5
		public bool IsPath(Vector3Int coordinates, bool allowUnfinished)
		{
			if (!allowUnfinished)
			{
				return PathService.IsPath(this._blockService.GetPathObjectAt(coordinates), false);
			}
			return PathService.IsPath(this._blockService.GetPathObjectAt(coordinates), true) || PathService.IsPath(this._previewBlockService.GetPathObjectAt(coordinates), true);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036E5 File Offset: 0x000018E5
		public static bool IsPath(BlockObject blockObject, bool allowUnfinished)
		{
			return blockObject && (allowUnfinished || blockObject.IsFinished) && blockObject.HasComponent<PathSpec>();
		}

		// Token: 0x0400004C RID: 76
		public readonly IBlockService _blockService;

		// Token: 0x0400004D RID: 77
		public readonly PreviewBlockService _previewBlockService;
	}
}
