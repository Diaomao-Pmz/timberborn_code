using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemUI
{
	// Token: 0x02000008 RID: 8
	public class ImportGoodIcon
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000023E8 File Offset: 0x000005E8
		public DistrictDistributableGoodProvider DistrictDistributableGoodProvider { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x000023F1 File Offset: 0x000005F1
		public ImportGoodIcon(string goodId, VisualElement importableIcon, VisualElement nonImportableIcon)
		{
			this._goodId = goodId;
			this._importableIcon = importableIcon;
			this._nonImportableIcon = nonImportableIcon;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000240E File Offset: 0x0000060E
		public void SetDistrictDistributableGoodProvider(DistrictDistributableGoodProvider districtDistributableGoodProvider)
		{
			this.DistrictDistributableGoodProvider = districtDistributableGoodProvider;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002418 File Offset: 0x00000618
		public void Update()
		{
			bool flag = this.DistrictDistributableGoodProvider.IsImportEnabled(this._goodId);
			this._importableIcon.ToggleDisplayStyle(flag);
			this._nonImportableIcon.ToggleDisplayStyle(!flag);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002452 File Offset: 0x00000652
		public void Clear()
		{
			this.DistrictDistributableGoodProvider = null;
		}

		// Token: 0x04000015 RID: 21
		public readonly string _goodId;

		// Token: 0x04000016 RID: 22
		public readonly VisualElement _importableIcon;

		// Token: 0x04000017 RID: 23
		public readonly VisualElement _nonImportableIcon;
	}
}
