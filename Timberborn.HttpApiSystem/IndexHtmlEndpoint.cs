using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Timberborn.SingletonSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200003A RID: 58
	public class IndexHtmlEndpoint : IHttpApiEndpoint, ILoadableSingleton
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000641D File Offset: 0x0000461D
		public IndexHtmlEndpoint(HttpApiCacheBuster httpApiCacheBuster, IEnumerable<IHttpApiPageSection> httpApiPageSections)
		{
			this._httpApiCacheBuster = httpApiCacheBuster;
			this._httpApiPageSections = httpApiPageSections.ToImmutableArray<IHttpApiPageSection>();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006438 File Offset: 0x00004638
		public void Load()
		{
			this._template = Handlebars.Compile(File.ReadAllText(IndexHtmlEndpoint.TemplatePath));
			this._httpApiPageSectionsOrdered = (from section in this._httpApiPageSections
			orderby section.Order
			select section).ToImmutableArray<IHttpApiPageSection>();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006494 File Offset: 0x00004694
		public Task<bool> TryHandle(HttpListenerContext context)
		{
			IndexHtmlEndpoint.<TryHandle>d__7 <TryHandle>d__;
			<TryHandle>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryHandle>d__.<>4__this = this;
			<TryHandle>d__.context = context;
			<TryHandle>d__.<>1__state = -1;
			<TryHandle>d__.<>t__builder.Start<IndexHtmlEndpoint.<TryHandle>d__7>(ref <TryHandle>d__);
			return <TryHandle>d__.<>t__builder.Task;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000064E0 File Offset: 0x000046E0
		public Task Handle(HttpListenerContext context)
		{
			IndexHtmlEndpoint.<Handle>d__8 <Handle>d__;
			<Handle>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Handle>d__.<>4__this = this;
			<Handle>d__.context = context;
			<Handle>d__.<>1__state = -1;
			<Handle>d__.<>t__builder.Start<IndexHtmlEndpoint.<Handle>d__8>(ref <Handle>d__);
			return <Handle>d__.<>t__builder.Task;
		}

		// Token: 0x040000EC RID: 236
		public static readonly string TemplatePath = Path.Combine(HttpApi.RootPath, "index.hbs");

		// Token: 0x040000ED RID: 237
		public readonly HttpApiCacheBuster _httpApiCacheBuster;

		// Token: 0x040000EE RID: 238
		public readonly ImmutableArray<IHttpApiPageSection> _httpApiPageSections;

		// Token: 0x040000EF RID: 239
		public HandlebarsTemplate<object, object> _template;

		// Token: 0x040000F0 RID: 240
		public ImmutableArray<IHttpApiPageSection> _httpApiPageSectionsOrdered;
	}
}
