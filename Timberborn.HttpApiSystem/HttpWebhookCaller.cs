using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000033 RID: 51
	public class HttpWebhookCaller : IUnloadableSingleton
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00005F02 File Offset: 0x00004102
		public void Unload()
		{
			this.Stop();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F0C File Offset: 0x0000410C
		public void Start()
		{
			if (this._thread == null)
			{
				this._http = new HttpClient();
				this._http.Timeout = HttpWebhookCaller.Timeout;
				this._thread = new Thread(new ThreadStart(this.ThreadLoop))
				{
					IsBackground = true,
					Name = "HttpWebhookCaller"
				};
				this._thread.Start();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005F70 File Offset: 0x00004170
		public void Stop()
		{
			if (this._thread != null)
			{
				this._pendingCalls.CompleteAdding();
				Thread thread = this._thread;
				if (thread != null)
				{
					thread.Join();
				}
				this._http = null;
				this._thread = null;
				this._pendingCalls = new BlockingCollection<HttpWebhookCaller.Call>();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005FAF File Offset: 0x000041AF
		public void Enqueue(HttpAdapter httpAdapter, bool state, string url, HttpWebhookMethod method)
		{
			if (this._thread != null && !string.IsNullOrWhiteSpace(url) && this._pendingCalls.Count < HttpWebhookCaller.MaxPendingCalls)
			{
				this._pendingCalls.Add(new HttpWebhookCaller.Call(httpAdapter, state, url.Trim(), method));
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005FF0 File Offset: 0x000041F0
		public void ThreadLoop()
		{
			StringContent content = new StringContent("");
			HttpWebhookCaller.Call call;
			while (this._pendingCalls.TryTake(out call, -1))
			{
				Uri uri;
				if (HttpWebhookCaller.IsAllowedUrl(call.Url, out uri))
				{
					if (uri.IsLoopback || this._nonLoopbackLimiter.TryAcquirePermit())
					{
						string host = uri.Host;
						try
						{
							HttpWebhookMethod method = call.Method;
							HttpResponseMessage result;
							if (method != HttpWebhookMethod.Get)
							{
								if (method != HttpWebhookMethod.Post)
								{
									throw new ArgumentOutOfRangeException(call.Method.ToString());
								}
								result = this._http.PostAsync(uri, content).Result;
							}
							else
							{
								result = this._http.GetAsync(uri).Result;
							}
							using (HttpResponseMessage httpResponseMessage = result)
							{
								httpResponseMessage.EnsureSuccessStatusCode();
								call.HttpAdapter.RegisterSuccessfulCall(call.State);
								this.ResetBackoff(host);
							}
							continue;
						}
						catch (Exception arg)
						{
							call.HttpAdapter.RegisterFailedCall(call.State);
							int num = uri.IsLoopback ? 1000 : this.IncrementAndGetBackoffMs(host);
							Debug.Log(string.Format("Failed webhook call to {0}, backing off for {1}ms.\n", call.Url, num) + string.Format("{0}", arg));
							Thread.Sleep(num);
							this.ClearPendingCalls();
							continue;
						}
					}
					call.HttpAdapter.RegisterFailedCall(call.State);
					Debug.Log(string.Format("Throttled webhook call to {0}", uri));
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006190 File Offset: 0x00004390
		public void ClearPendingCalls()
		{
			HttpWebhookCaller.Call call;
			while (this._pendingCalls.TryTake(out call))
			{
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000061AC File Offset: 0x000043AC
		public void ResetBackoff(string host)
		{
			this._consecutiveFailuresByHost[host] = 0;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000061BC File Offset: 0x000043BC
		public int IncrementAndGetBackoffMs(string host)
		{
			int num = CollectionExtensions.GetValueOrDefault<string, int>(this._consecutiveFailuresByHost, host) + 1;
			this._consecutiveFailuresByHost[host] = num;
			return HttpWebhookCaller.BackoffMs[Math.Min(num - 1, HttpWebhookCaller.BackoffMs.Length - 1)];
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000061FC File Offset: 0x000043FC
		public static bool IsAllowedUrl(string input, out Uri uri)
		{
			return Uri.TryCreate(input, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
		}

		// Token: 0x040000D7 RID: 215
		public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000D8 RID: 216
		public static readonly int MaxPendingCalls = 100;

		// Token: 0x040000D9 RID: 217
		public static readonly int[] BackoffMs = new int[]
		{
			1000,
			2000,
			4000,
			8000,
			16000,
			32000
		};

		// Token: 0x040000DA RID: 218
		public BlockingCollection<HttpWebhookCaller.Call> _pendingCalls = new BlockingCollection<HttpWebhookCaller.Call>();

		// Token: 0x040000DB RID: 219
		public Thread _thread;

		// Token: 0x040000DC RID: 220
		public HttpClient _http;

		// Token: 0x040000DD RID: 221
		public readonly TimeWindowLimiter _nonLoopbackLimiter = new TimeWindowLimiter(30, TimeSpan.FromMinutes(1.0));

		// Token: 0x040000DE RID: 222
		public readonly Dictionary<string, int> _consecutiveFailuresByHost = new Dictionary<string, int>();

		// Token: 0x02000034 RID: 52
		public struct Call
		{
			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600011B RID: 283 RVA: 0x0000629B File Offset: 0x0000449B
			public readonly HttpAdapter HttpAdapter { get; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600011C RID: 284 RVA: 0x000062A3 File Offset: 0x000044A3
			public readonly bool State { get; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600011D RID: 285 RVA: 0x000062AB File Offset: 0x000044AB
			public readonly string Url { get; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600011E RID: 286 RVA: 0x000062B3 File Offset: 0x000044B3
			public readonly HttpWebhookMethod Method { get; }

			// Token: 0x0600011F RID: 287 RVA: 0x000062BB File Offset: 0x000044BB
			public Call(HttpAdapter httpAdapter, bool state, string url, HttpWebhookMethod method)
			{
				this.HttpAdapter = httpAdapter;
				this.State = state;
				this.Url = url;
				this.Method = method;
			}
		}
	}
}
