using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.MapItemsUI
{
	// Token: 0x0200000C RID: 12
	public class MapItemProvider
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002666 File Offset: 0x00000866
		public MapItemProvider(OfficialMapItemFactory officialMapItemFactory, UserMapItemFactory userMapItemFactory, IEnumerable<ICustomMapItemFactory> customMapItemFactories)
		{
			this._officialMapItemFactory = officialMapItemFactory;
			this._userMapItemFactory = userMapItemFactory;
			this._customMapItemFactories = customMapItemFactories;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002683 File Offset: 0x00000883
		public IEnumerable<MapItem> GetOfficialMaps()
		{
			return this._officialMapItemFactory.Create();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002690 File Offset: 0x00000890
		public IEnumerable<MapItem> GetUserMaps()
		{
			return this._userMapItemFactory.Create();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000269D File Offset: 0x0000089D
		public IEnumerable<MapItem> GetCustomMaps()
		{
			return this.GetUserMaps().Concat(this._customMapItemFactories.SelectMany((ICustomMapItemFactory factory) => factory.Create()));
		}

		// Token: 0x04000024 RID: 36
		public readonly OfficialMapItemFactory _officialMapItemFactory;

		// Token: 0x04000025 RID: 37
		public readonly UserMapItemFactory _userMapItemFactory;

		// Token: 0x04000026 RID: 38
		public readonly IEnumerable<ICustomMapItemFactory> _customMapItemFactories;
	}
}
