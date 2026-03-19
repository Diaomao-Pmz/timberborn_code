using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.FireworkSystem;
using UnityEngine;

namespace Timberborn.FireworkSystemUI
{
	// Token: 0x02000004 RID: 4
	public class FireworkIdDropdownProvider : BaseComponent, IAwakableComponent, IInitializableEntity, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public FireworkIdDropdownProvider(FireworkSpecService fireworkSpecService)
		{
			this._fireworkSpecService = fireworkSpecService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DA File Offset: 0x000002DA
		public void Awake()
		{
			this._fireworkLauncher = base.GetComponent<FireworkLauncher>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E8 File Offset: 0x000002E8
		public void InitializeEntity()
		{
			this._items = this._fireworkSpecService.GetFireworkIds();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public string GetValue()
		{
			return this._fireworkLauncher.FireworkId;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public void SetValue(string value)
		{
			this._fireworkLauncher.SetFireworkId(value);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002116 File Offset: 0x00000316
		public string FormatDisplayText(string value, bool selected)
		{
			return this._fireworkSpecService.GetFireworkSpec(value).DisplayName.Value;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212E File Offset: 0x0000032E
		public Sprite GetIcon(string value)
		{
			return null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002131 File Offset: 0x00000331
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04000006 RID: 6
		public readonly FireworkSpecService _fireworkSpecService;

		// Token: 0x04000007 RID: 7
		public FireworkLauncher _fireworkLauncher;

		// Token: 0x04000008 RID: 8
		public ImmutableArray<string> _items;
	}
}
