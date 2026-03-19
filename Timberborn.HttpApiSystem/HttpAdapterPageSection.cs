using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using HandlebarsDotNet;
using Timberborn.SingletonSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200000F RID: 15
	public class HttpAdapterPageSection : IHttpApiPageSection, ILoadableSingleton
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000321F File Offset: 0x0000141F
		public HttpAdapterPageSection(HttpApiIntermediary httpApiIntermediary)
		{
			this._httpApiIntermediary = httpApiIntermediary;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000322E File Offset: 0x0000142E
		public int Order
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003235 File Offset: 0x00001435
		public void Load()
		{
			this._template = Handlebars.Compile(File.ReadAllText(HttpAdapterPageSection.TemplatePath));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000324C File Offset: 0x0000144C
		public string BuildBody()
		{
			ImmutableArray<HttpAdapterSnapshot> adapters = this._httpApiIntermediary.GetAdapters();
			return this._template(new
			{
				adaptersUrl = "/api/adapters",
				adapters = from adapter in adapters
				orderby adapter.Name
				select new
				{
					name = adapter.Name,
					state = adapter.State,
					url = "/api/adapters/" + Uri.EscapeDataString(adapter.Name)
				}
			}, null);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000032C9 File Offset: 0x000014C9
		public string BuildFooter()
		{
			return "";
		}

		// Token: 0x04000036 RID: 54
		public static readonly string TemplatePath = Path.Combine(HttpApi.RootPath, "index-adapters.hbs");

		// Token: 0x04000037 RID: 55
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x04000038 RID: 56
		public HandlebarsTemplate<object, object> _template;
	}
}
