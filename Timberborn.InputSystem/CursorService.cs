using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.InputSystem
{
	// Token: 0x02000007 RID: 7
	public class CursorService : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public CursorService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000211A File Offset: 0x0000031A
		public Vector2 CursorOffset
		{
			get
			{
				if (!this._useMacOsCursor)
				{
					return this._cursorSpec.WindowsCursorOffset;
				}
				return this._cursorSpec.MacOsCursorOffset;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void Load()
		{
			this._useMacOsCursor = ApplicationPlatform.IsMacOS();
			this._cursorSpecs = (from spec in this._specService.GetSpecs<CustomCursorSpec>()
			where spec.Blueprint.IsAllowedByFeatureToggles()
			select spec).ToDictionary((CustomCursorSpec cursor) => cursor.Id);
			this.ResetCursor();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B3 File Offset: 0x000003B3
		public void SetCursor(string cursorName)
		{
			this.SetCursor(this._cursorSpecs[cursorName]);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C7 File Offset: 0x000003C7
		public void SetTemporaryCursor(string cursorName)
		{
			this.SetCursorImage(this._cursorSpecs[cursorName]);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DB File Offset: 0x000003DB
		public void ResetCursor()
		{
			this.SetCursor(CursorService.DefaultCursorName);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		public void ResetTemporaryCursor()
		{
			this.SetCursor(this._cursorSpec);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F6 File Offset: 0x000003F6
		public void SetCursor(CustomCursorSpec cursorSpec)
		{
			this.SetCursorImage(cursorSpec);
			this._cursorSpec = cursorSpec;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002206 File Offset: 0x00000406
		public void SetCursorImage(CustomCursorSpec cursorSpec)
		{
			AssetRef<Texture2D> assetRef = this._useMacOsCursor ? cursorSpec.MacOsCursor : cursorSpec.WindowsCursor;
			Cursor.SetCursor((assetRef != null) ? assetRef.Asset : null, cursorSpec.Hotspot, 0);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string DefaultCursorName = "DefaultCursor";

		// Token: 0x04000009 RID: 9
		public readonly ISpecService _specService;

		// Token: 0x0400000A RID: 10
		public CustomCursorSpec _cursorSpec;

		// Token: 0x0400000B RID: 11
		public bool _useMacOsCursor;

		// Token: 0x0400000C RID: 12
		public Dictionary<string, CustomCursorSpec> _cursorSpecs = new Dictionary<string, CustomCursorSpec>();
	}
}
