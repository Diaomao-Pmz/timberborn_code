using System;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000039 RID: 57
	public interface IHttpApiPageSection
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012B RID: 299
		int Order { get; }

		// Token: 0x0600012C RID: 300
		string BuildBody();

		// Token: 0x0600012D RID: 301
		string BuildFooter();
	}
}
