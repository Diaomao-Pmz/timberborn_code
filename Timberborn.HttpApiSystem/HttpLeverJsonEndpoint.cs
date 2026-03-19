using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000022 RID: 34
	public class HttpLeverJsonEndpoint : IHttpApiEndpoint
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000492C File Offset: 0x00002B2C
		public HttpLeverJsonEndpoint(HttpApiIntermediary httpApiIntermediary)
		{
			this._httpApiIntermediary = httpApiIntermediary;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000499C File Offset: 0x00002B9C
		public Task<bool> TryHandle(HttpListenerContext context)
		{
			HttpLeverJsonEndpoint.<TryHandle>d__7 <TryHandle>d__;
			<TryHandle>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryHandle>d__.<>4__this = this;
			<TryHandle>d__.context = context;
			<TryHandle>d__.<>1__state = -1;
			<TryHandle>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<TryHandle>d__7>(ref <TryHandle>d__);
			return <TryHandle>d__.<>t__builder.Task;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000049E8 File Offset: 0x00002BE8
		public Task ProcessLevers(HttpListenerContext context)
		{
			HttpLeverJsonEndpoint.<ProcessLevers>d__8 <ProcessLevers>d__;
			<ProcessLevers>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessLevers>d__.<>4__this = this;
			<ProcessLevers>d__.context = context;
			<ProcessLevers>d__.<>1__state = -1;
			<ProcessLevers>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<ProcessLevers>d__8>(ref <ProcessLevers>d__);
			return <ProcessLevers>d__.<>t__builder.Task;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A34 File Offset: 0x00002C34
		public Task ProcessLever(HttpListenerContext context, Match match)
		{
			HttpLeverJsonEndpoint.<ProcessLever>d__9 <ProcessLever>d__;
			<ProcessLever>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessLever>d__.<>4__this = this;
			<ProcessLever>d__.context = context;
			<ProcessLever>d__.match = match;
			<ProcessLever>d__.<>1__state = -1;
			<ProcessLever>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<ProcessLever>d__9>(ref <ProcessLever>d__);
			return <ProcessLever>d__.<>t__builder.Task;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004A88 File Offset: 0x00002C88
		public Task ProcessSwitch(HttpListenerContext context, Match match, bool state)
		{
			HttpLeverJsonEndpoint.<ProcessSwitch>d__10 <ProcessSwitch>d__;
			<ProcessSwitch>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessSwitch>d__.<>4__this = this;
			<ProcessSwitch>d__.context = context;
			<ProcessSwitch>d__.match = match;
			<ProcessSwitch>d__.state = state;
			<ProcessSwitch>d__.<>1__state = -1;
			<ProcessSwitch>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<ProcessSwitch>d__10>(ref <ProcessSwitch>d__);
			return <ProcessSwitch>d__.<>t__builder.Task;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public Task ProcessColor(HttpListenerContext context, Match match)
		{
			HttpLeverJsonEndpoint.<ProcessColor>d__11 <ProcessColor>d__;
			<ProcessColor>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessColor>d__.<>4__this = this;
			<ProcessColor>d__.context = context;
			<ProcessColor>d__.match = match;
			<ProcessColor>d__.<>1__state = -1;
			<ProcessColor>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<ProcessColor>d__11>(ref <ProcessColor>d__);
			return <ProcessColor>d__.<>t__builder.Task;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004B38 File Offset: 0x00002D38
		public static Task WriteOK(HttpListenerContext context)
		{
			HttpLeverJsonEndpoint.<WriteOK>d__12 <WriteOK>d__;
			<WriteOK>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteOK>d__.context = context;
			<WriteOK>d__.<>1__state = -1;
			<WriteOK>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<WriteOK>d__12>(ref <WriteOK>d__);
			return <WriteOK>d__.<>t__builder.Task;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static Task Write404(HttpListenerContext context, string name)
		{
			HttpLeverJsonEndpoint.<Write404>d__13 <Write404>d__;
			<Write404>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Write404>d__.context = context;
			<Write404>d__.name = name;
			<Write404>d__.<>1__state = -1;
			<Write404>d__.<>t__builder.Start<HttpLeverJsonEndpoint.<Write404>d__13>(ref <Write404>d__);
			return <Write404>d__.<>t__builder.Task;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public static object Json(HttpLeverSnapshot httpLeverSnapshot)
		{
			return new
			{
				name = httpLeverSnapshot.Name,
				state = httpLeverSnapshot.State,
				springReturn = httpLeverSnapshot.IsSpringReturn
			};
		}

		// Token: 0x0400008A RID: 138
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x0400008B RID: 139
		public readonly Regex _leversPath = new Regex("^/api/levers/?$", RegexOptions.Compiled);

		// Token: 0x0400008C RID: 140
		public readonly Regex _leverPath = new Regex("^/api/levers/(?<name>[^/]+)/?$", RegexOptions.Compiled);

		// Token: 0x0400008D RID: 141
		public readonly Regex _switchOnPath = new Regex("^/api/switch-on/(?<name>[^/]+)/?$", RegexOptions.Compiled);

		// Token: 0x0400008E RID: 142
		public readonly Regex _switchOffPath = new Regex("^/api/switch-off/(?<name>[^/]+)/?$", RegexOptions.Compiled);

		// Token: 0x0400008F RID: 143
		public readonly Regex _colorPath = new Regex("^/api/color/(?<name>[^/]+)/(?<color>[0-9a-fA-F]{6})$/?$", RegexOptions.Compiled);
	}
}
