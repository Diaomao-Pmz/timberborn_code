using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000011 RID: 17
	public class HttpAdaptersJsonEndpoint : IHttpApiEndpoint
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003326 File Offset: 0x00001526
		public HttpAdaptersJsonEndpoint(HttpApiIntermediary httpApiIntermediary)
		{
			this._httpApiIntermediary = httpApiIntermediary;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003358 File Offset: 0x00001558
		public Task<bool> TryHandle(HttpListenerContext context)
		{
			HttpAdaptersJsonEndpoint.<TryHandle>d__4 <TryHandle>d__;
			<TryHandle>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryHandle>d__.<>4__this = this;
			<TryHandle>d__.context = context;
			<TryHandle>d__.<>1__state = -1;
			<TryHandle>d__.<>t__builder.Start<HttpAdaptersJsonEndpoint.<TryHandle>d__4>(ref <TryHandle>d__);
			return <TryHandle>d__.<>t__builder.Task;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033A4 File Offset: 0x000015A4
		public Task ProcessAdapters(HttpListenerContext context)
		{
			HttpAdaptersJsonEndpoint.<ProcessAdapters>d__5 <ProcessAdapters>d__;
			<ProcessAdapters>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAdapters>d__.<>4__this = this;
			<ProcessAdapters>d__.context = context;
			<ProcessAdapters>d__.<>1__state = -1;
			<ProcessAdapters>d__.<>t__builder.Start<HttpAdaptersJsonEndpoint.<ProcessAdapters>d__5>(ref <ProcessAdapters>d__);
			return <ProcessAdapters>d__.<>t__builder.Task;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033F0 File Offset: 0x000015F0
		public Task ProcessAdapter(HttpListenerContext context, Match match)
		{
			HttpAdaptersJsonEndpoint.<ProcessAdapter>d__6 <ProcessAdapter>d__;
			<ProcessAdapter>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAdapter>d__.<>4__this = this;
			<ProcessAdapter>d__.context = context;
			<ProcessAdapter>d__.match = match;
			<ProcessAdapter>d__.<>1__state = -1;
			<ProcessAdapter>d__.<>t__builder.Start<HttpAdaptersJsonEndpoint.<ProcessAdapter>d__6>(ref <ProcessAdapter>d__);
			return <ProcessAdapter>d__.<>t__builder.Task;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003443 File Offset: 0x00001643
		public static object Json(HttpAdapterSnapshot adapter)
		{
			return new
			{
				name = adapter.Name,
				state = adapter.State
			};
		}

		// Token: 0x0400003C RID: 60
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x0400003D RID: 61
		public readonly Regex _adaptersPath = new Regex("^/api/adapters/?$", RegexOptions.Compiled);

		// Token: 0x0400003E RID: 62
		public readonly Regex _adapterPath = new Regex("^/api/adapters/(?<name>[^/]+)/?$", RegexOptions.Compiled);
	}
}
