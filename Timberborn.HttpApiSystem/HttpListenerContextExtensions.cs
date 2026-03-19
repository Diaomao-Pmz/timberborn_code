using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200002E RID: 46
	public static class HttpListenerContextExtensions
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005950 File Offset: 0x00003B50
		public static Task WriteText(this HttpListenerContext context, string text, int statusCode)
		{
			HttpListenerContextExtensions.<WriteText>d__0 <WriteText>d__;
			<WriteText>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteText>d__.context = context;
			<WriteText>d__.text = text;
			<WriteText>d__.statusCode = statusCode;
			<WriteText>d__.<>1__state = -1;
			<WriteText>d__.<>t__builder.Start<HttpListenerContextExtensions.<WriteText>d__0>(ref <WriteText>d__);
			return <WriteText>d__.<>t__builder.Task;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000059A4 File Offset: 0x00003BA4
		public static Task WriteHtml(this HttpListenerContext context, string text)
		{
			HttpListenerContextExtensions.<WriteHtml>d__1 <WriteHtml>d__;
			<WriteHtml>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteHtml>d__.context = context;
			<WriteHtml>d__.text = text;
			<WriteHtml>d__.<>1__state = -1;
			<WriteHtml>d__.<>t__builder.Start<HttpListenerContextExtensions.<WriteHtml>d__1>(ref <WriteHtml>d__);
			return <WriteHtml>d__.<>t__builder.Task;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000059F0 File Offset: 0x00003BF0
		public static Task WriteJson(this HttpListenerContext context, object json)
		{
			HttpListenerContextExtensions.<WriteJson>d__2 <WriteJson>d__;
			<WriteJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteJson>d__.context = context;
			<WriteJson>d__.json = json;
			<WriteJson>d__.<>1__state = -1;
			<WriteJson>d__.<>t__builder.Start<HttpListenerContextExtensions.<WriteJson>d__2>(ref <WriteJson>d__);
			return <WriteJson>d__.<>t__builder.Task;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005A3C File Offset: 0x00003C3C
		public static Task Write(this HttpListenerContext context, string contentType, byte[] bytes)
		{
			HttpListenerContextExtensions.<Write>d__3 <Write>d__;
			<Write>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Write>d__.context = context;
			<Write>d__.contentType = contentType;
			<Write>d__.bytes = bytes;
			<Write>d__.<>1__state = -1;
			<Write>d__.<>t__builder.Start<HttpListenerContextExtensions.<Write>d__3>(ref <Write>d__);
			return <Write>d__.<>t__builder.Task;
		}
	}
}
