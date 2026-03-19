using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000017 RID: 23
	public class HttpApi : IUnloadableSingleton, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007D RID: 125 RVA: 0x00003950 File Offset: 0x00001B50
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x00003988 File Offset: 0x00001B88
		public event EventHandler IsRunningChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007F RID: 127 RVA: 0x000039C0 File Offset: 0x00001BC0
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x000039F8 File Offset: 0x00001BF8
		public event EventHandler UrlChanged;

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003A2D File Offset: 0x00001C2D
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003A35 File Offset: 0x00001C35
		public bool IsRunning { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003A3E File Offset: 0x00001C3E
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003A46 File Offset: 0x00001C46
		public string Url { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003A4F File Offset: 0x00001C4F
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003A57 File Offset: 0x00001C57
		public string ErrorMessage { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003A60 File Offset: 0x00001C60
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003A68 File Offset: 0x00001C68
		public int Port { get; private set; } = HttpApi.DefaultPort;

		// Token: 0x06000089 RID: 137 RVA: 0x00003A71 File Offset: 0x00001C71
		public HttpApi(ISingletonLoader singletonLoader, HttpWebhookCaller httpWebhookCaller, IEnumerable<IHttpApiEndpoint> httpApiEndpoints)
		{
			this._singletonLoader = singletonLoader;
			this._httpWebhookCaller = httpWebhookCaller;
			this._httpApiEndpoints = httpApiEndpoints.ToImmutableArray<IHttpApiEndpoint>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(HttpApi.HttpApiKey, out objectLoader) && objectLoader.Has<int>(HttpApi.PortKey))
			{
				this.Port = objectLoader.Get(HttpApi.PortKey);
			}
			this.UpdateUrl();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(HttpApi.HttpApiKey);
			if (this.Port != HttpApi.DefaultPort)
			{
				singleton.Set(HttpApi.PortKey, this.Port);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B20 File Offset: 0x00001D20
		public void Start()
		{
			if (!this.IsRunning)
			{
				Debug.Log("Starting HttpApi at " + this.Url);
				try
				{
					this._httpListener = new HttpListener();
					this._httpListener.Prefixes.Add(this.Url);
					this._httpListener.Start();
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
					this._httpListener = null;
					this.ErrorMessage = ex.Message;
					return;
				}
				this._task = Task.Run(new Func<Task>(this.ProcessRequests));
				this._httpWebhookCaller.Start();
				this.IsRunning = true;
				this.ErrorMessage = null;
				this.NotifyIsRunningChanged();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public void Stop()
		{
			this.StopInternal(true);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003BE9 File Offset: 0x00001DE9
		public void Unload()
		{
			this.StopInternal(false);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public void SetPort(ushort value)
		{
			if (this.Port != (int)value)
			{
				this.Port = (int)value;
				this.UpdateUrl();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void StopInternal(bool notify)
		{
			if (this.IsRunning)
			{
				this._stopping = true;
				this._httpListener.Stop();
				this._httpListener.Close();
				if (!this._task.Wait(HttpApi.TaskStopTimeout))
				{
					Debug.Log("Failed to stop HttpApi task!");
				}
				this._httpListener = null;
				this._task = null;
				this._httpWebhookCaller.Stop();
				Debug.Log("Stopped HttpApi");
				this.IsRunning = false;
				this._stopping = false;
				if (notify)
				{
					this.NotifyIsRunningChanged();
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C98 File Offset: 0x00001E98
		public void UpdateUrl()
		{
			string text = string.Format("http://localhost:{0}/", this.Port);
			if (this.Url != text)
			{
				this.Url = text;
				EventHandler urlChanged = this.UrlChanged;
				if (urlChanged == null)
				{
					return;
				}
				urlChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public Task ProcessRequests()
		{
			HttpApi.<ProcessRequests>d__43 <ProcessRequests>d__;
			<ProcessRequests>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessRequests>d__.<>4__this = this;
			<ProcessRequests>d__.<>1__state = -1;
			<ProcessRequests>d__.<>t__builder.Start<HttpApi.<ProcessRequests>d__43>(ref <ProcessRequests>d__);
			return <ProcessRequests>d__.<>t__builder.Task;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D2C File Offset: 0x00001F2C
		public Task<bool> TryHandleWithEndpoints(HttpListenerContext context)
		{
			HttpApi.<TryHandleWithEndpoints>d__44 <TryHandleWithEndpoints>d__;
			<TryHandleWithEndpoints>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryHandleWithEndpoints>d__.<>4__this = this;
			<TryHandleWithEndpoints>d__.context = context;
			<TryHandleWithEndpoints>d__.<>1__state = -1;
			<TryHandleWithEndpoints>d__.<>t__builder.Start<HttpApi.<TryHandleWithEndpoints>d__44>(ref <TryHandleWithEndpoints>d__);
			return <TryHandleWithEndpoints>d__.<>t__builder.Task;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D77 File Offset: 0x00001F77
		public void NotifyIsRunningChanged()
		{
			EventHandler isRunningChanged = this.IsRunningChanged;
			if (isRunningChanged == null)
			{
				return;
			}
			isRunningChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003D90 File Offset: 0x00001F90
		public static Task Process404(HttpListenerContext context)
		{
			HttpApi.<Process404>d__46 <Process404>d__;
			<Process404>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Process404>d__.context = context;
			<Process404>d__.<>1__state = -1;
			<Process404>d__.<>t__builder.Start<HttpApi.<Process404>d__46>(ref <Process404>d__);
			return <Process404>d__.<>t__builder.Task;
		}

		// Token: 0x04000051 RID: 81
		public static readonly string RootPath = Path.Combine(Application.streamingAssetsPath, "HttpApi");

		// Token: 0x04000052 RID: 82
		public static readonly SingletonKey HttpApiKey = new SingletonKey("HttpApi");

		// Token: 0x04000053 RID: 83
		public static readonly PropertyKey<int> PortKey = new PropertyKey<int>("Port");

		// Token: 0x04000054 RID: 84
		public static readonly int DefaultPort = 8080;

		// Token: 0x04000055 RID: 85
		public static readonly TimeSpan TaskStopTimeout = TimeSpan.FromMilliseconds(5000.0);

		// Token: 0x0400005C RID: 92
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400005D RID: 93
		public readonly HttpWebhookCaller _httpWebhookCaller;

		// Token: 0x0400005E RID: 94
		public readonly ImmutableArray<IHttpApiEndpoint> _httpApiEndpoints;

		// Token: 0x0400005F RID: 95
		public HttpListener _httpListener;

		// Token: 0x04000060 RID: 96
		public Task _task;

		// Token: 0x04000061 RID: 97
		public HandlebarsTemplate<object, object> _indexTemplate;

		// Token: 0x04000062 RID: 98
		public volatile bool _stopping;
	}
}
