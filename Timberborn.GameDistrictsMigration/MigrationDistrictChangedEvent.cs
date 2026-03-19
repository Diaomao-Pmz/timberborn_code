using System;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000014 RID: 20
	public class MigrationDistrictChangedEvent
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002DBA File Offset: 0x00000FBA
		public bool HighlightLeftDistrict { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DC2 File Offset: 0x00000FC2
		public bool HighlightRightDistrict { get; }

		// Token: 0x06000066 RID: 102 RVA: 0x00002DCA File Offset: 0x00000FCA
		public MigrationDistrictChangedEvent(bool highlightLeftDistrict, bool highlightRightDistrict)
		{
			this.HighlightLeftDistrict = highlightLeftDistrict;
			this.HighlightRightDistrict = highlightRightDistrict;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static MigrationDistrictChangedEvent Create()
		{
			return new MigrationDistrictChangedEvent(false, false);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public static MigrationDistrictChangedEvent CreateWithLeftHighlight()
		{
			return new MigrationDistrictChangedEvent(true, false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public static MigrationDistrictChangedEvent CreateWithRightHighlight()
		{
			return new MigrationDistrictChangedEvent(false, true);
		}
	}
}
