using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200000E RID: 14
	public class HttpAdapter : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<HttpAdapter>, IDuplicable, IFinishedStateListener, IRegisteredComponent, IAutomatableNeeder, ITerminal
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public string SwitchedOnWebhookUrl { get; set; } = HttpAdapter.DefaultSwitchedOnUrl;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002CA9 File Offset: 0x00000EA9
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public string SwitchedOffWebhookUrl { get; set; } = HttpAdapter.DefaultSwitchedOffUrl;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CBA File Offset: 0x00000EBA
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public HttpWebhookMethod Method { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002CCB File Offset: 0x00000ECB
		public HttpAdapter(HttpApiIntermediary httpApiIntermediary, HttpWebhookCaller httpWebhookCaller)
		{
			this._httpApiIntermediary = httpApiIntermediary;
			this._httpWebhookCaller = httpWebhookCaller;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002D09 File Offset: 0x00000F09
		public bool NeedsAutomatable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D0C File Offset: 0x00000F0C
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002D14 File Offset: 0x00000F14
		public bool SwitchedOnWebhookEnabled
		{
			get
			{
				return this._switchedOnWebhookEnabled;
			}
			set
			{
				this._switchedOnWebhookEnabled = value;
				if (!value)
				{
					this._lastOnCallSuccessful = -1;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D31 File Offset: 0x00000F31
		public bool SwitchedOffWebhookEnabled
		{
			get
			{
				return this._switchedOffWebhookEnabled;
			}
			set
			{
				this._switchedOffWebhookEnabled = value;
				if (!value)
				{
					this._lastOffCallSuccessful = -1;
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D48 File Offset: 0x00000F48
		public bool? LastOnCallSuccessful
		{
			get
			{
				if (this._lastOnCallSuccessful != -1)
				{
					return new bool?(this._lastOnCallSuccessful == 1);
				}
				return null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D7C File Offset: 0x00000F7C
		public bool? LastOffCallSuccessful
		{
			get
			{
				if (this._lastOffCallSuccessful != -1)
				{
					return new bool?(this._lastOffCallSuccessful == 1);
				}
				return null;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DAE File Offset: 0x00000FAE
		public string[] AllWebhookUrls
		{
			get
			{
				return new string[]
				{
					this.SwitchedOnWebhookUrl,
					this.SwitchedOffWebhookUrl
				};
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._uniquelyNamedEntity = base.GetComponent<UniquelyNamedEntity>();
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DF3 File Offset: 0x00000FF3
		public void OnEnterFinishedState()
		{
			this.AddSnapshot();
			this._uniquelyNamedEntity.EntityNameChanged += this.OnUniqueNameChanged;
			this._uniquelyNamedEntity.IsUniqueChanged += this.OnUniqueNameChanged;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E29 File Offset: 0x00001029
		public void OnExitFinishedState()
		{
			this.RemoveSnapshot();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E34 File Offset: 0x00001034
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(HttpAdapter.HttpAdapterKey);
			if (this.SwitchedOnWebhookEnabled)
			{
				component.Set(HttpAdapter.SwitchedOnWebhookEnabledKey, this.SwitchedOnWebhookEnabled);
			}
			if (this.SwitchedOffWebhookEnabled)
			{
				component.Set(HttpAdapter.SwitchedOffWebhookEnabledKey, this.SwitchedOffWebhookEnabled);
			}
			if (this.SwitchedOnWebhookUrl != HttpAdapter.DefaultSwitchedOnUrl)
			{
				component.Set(HttpAdapter.SwitchedOnWebhookUrlKey, this.SwitchedOnWebhookUrl);
			}
			if (this.SwitchedOffWebhookUrl != HttpAdapter.DefaultSwitchedOffUrl)
			{
				component.Set(HttpAdapter.SwitchedOffWebhookUrlKey, this.SwitchedOffWebhookUrl);
			}
			component.Set<HttpWebhookMethod>(HttpAdapter.MethodKey, this.Method);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(HttpAdapter.HttpAdapterKey, out objectLoader))
			{
				this.SwitchedOnWebhookEnabled = (objectLoader.Has<bool>(HttpAdapter.SwitchedOnWebhookEnabledKey) && objectLoader.Get(HttpAdapter.SwitchedOnWebhookEnabledKey));
				this.SwitchedOffWebhookEnabled = (objectLoader.Has<bool>(HttpAdapter.SwitchedOffWebhookEnabledKey) && objectLoader.Get(HttpAdapter.SwitchedOffWebhookEnabledKey));
				if (objectLoader.Has<string>(HttpAdapter.SwitchedOnWebhookUrlKey))
				{
					this.SwitchedOnWebhookUrl = objectLoader.Get(HttpAdapter.SwitchedOnWebhookUrlKey);
				}
				if (objectLoader.Has<string>(HttpAdapter.SwitchedOffWebhookUrlKey))
				{
					this.SwitchedOffWebhookUrl = objectLoader.Get(HttpAdapter.SwitchedOffWebhookUrlKey);
				}
				if (objectLoader.Has<HttpWebhookMethod>(HttpAdapter.MethodKey))
				{
					this.Method = objectLoader.Get<HttpWebhookMethod>(HttpAdapter.MethodKey);
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F93 File Offset: 0x00001193
		public void DuplicateFrom(HttpAdapter source)
		{
			this.SwitchedOnWebhookEnabled = source.SwitchedOnWebhookEnabled;
			this.SwitchedOffWebhookEnabled = source.SwitchedOffWebhookEnabled;
			this.SwitchedOnWebhookUrl = source.SwitchedOnWebhookUrl;
			this.SwitchedOffWebhookUrl = source.SwitchedOffWebhookUrl;
			this.Method = source.Method;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FD1 File Offset: 0x000011D1
		public void Evaluate()
		{
			this.AddSnapshot();
			this.EnqueueCalls();
			this.UpdateLight();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FE5 File Offset: 0x000011E5
		public void RegisterSuccessfulCall(bool state)
		{
			if (state)
			{
				this._lastOnCallSuccessful = 1;
				return;
			}
			this._lastOffCallSuccessful = 1;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FFD File Offset: 0x000011FD
		public void RegisterFailedCall(bool state)
		{
			if (state)
			{
				this._lastOnCallSuccessful = 0;
				return;
			}
			this._lastOffCallSuccessful = 0;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003015 File Offset: 0x00001215
		public void OnUniqueNameChanged(object sender, EventArgs e)
		{
			this.RemoveSnapshot();
			this.AddSnapshot();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003024 File Offset: 0x00001224
		public void AddSnapshot()
		{
			if (this._uniquelyNamedEntity.IsUnique)
			{
				this._snapshotName = this._uniquelyNamedEntity.EntityName;
				this._httpApiIntermediary.AddAdapterSnapshot(new HttpAdapterSnapshot(this._snapshotName, this._automatable.State == ConnectionState.On));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003073 File Offset: 0x00001273
		public void RemoveSnapshot()
		{
			if (this._snapshotName != null)
			{
				this._httpApiIntermediary.RemoveAdapterSnapshot(this._snapshotName);
				this._snapshotName = null;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003098 File Offset: 0x00001298
		public void EnqueueCalls()
		{
			ConnectionState state = this._automatable.State;
			if (this._previousState != null)
			{
				if (this.SwitchedOnWebhookEnabled)
				{
					ConnectionState? previousState = this._previousState;
					ConnectionState connectionState = ConnectionState.Off;
					if ((previousState.GetValueOrDefault() == connectionState & previousState != null) && state == ConnectionState.On)
					{
						this._httpWebhookCaller.Enqueue(this, true, this.ReplaceTokens(this.SwitchedOnWebhookUrl), this.Method);
						goto IL_AE;
					}
				}
				if (this.SwitchedOffWebhookEnabled)
				{
					ConnectionState? previousState = this._previousState;
					ConnectionState connectionState = ConnectionState.On;
					if ((previousState.GetValueOrDefault() == connectionState & previousState != null) && state == ConnectionState.Off)
					{
						this._httpWebhookCaller.Enqueue(this, false, this.ReplaceTokens(this.SwitchedOffWebhookUrl), this.Method);
					}
				}
			}
			IL_AE:
			this._previousState = new ConnectionState?(state);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000315F File Offset: 0x0000135F
		public string ReplaceTokens(string url)
		{
			return url.Replace("{name}", Uri.EscapeDataString(this._uniquelyNamedEntity.EntityName));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000317C File Offset: 0x0000137C
		public void UpdateLight()
		{
			if (this._automatable.State == ConnectionState.On)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x0400001F RID: 31
		public static readonly ComponentKey HttpAdapterKey = new ComponentKey("HttpAdapter");

		// Token: 0x04000020 RID: 32
		public static readonly PropertyKey<bool> SwitchedOnWebhookEnabledKey = new PropertyKey<bool>("SwitchedOnWebbookEnabledKey");

		// Token: 0x04000021 RID: 33
		public static readonly PropertyKey<bool> SwitchedOffWebhookEnabledKey = new PropertyKey<bool>("SwitchedOffWebbookEnabledKey");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<string> SwitchedOnWebhookUrlKey = new PropertyKey<string>("SwitchedOnWebbookUrlKey");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<string> SwitchedOffWebhookUrlKey = new PropertyKey<string>("SwitchedOffWebbookUrlKey");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<HttpWebhookMethod> MethodKey = new PropertyKey<HttpWebhookMethod>("MethodKey");

		// Token: 0x04000025 RID: 37
		public static readonly string DefaultSwitchedOnUrl = "http://localhost:8081/on/{name}";

		// Token: 0x04000026 RID: 38
		public static readonly string DefaultSwitchedOffUrl = "http://localhost:8081/off/{name}";

		// Token: 0x0400002A RID: 42
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x0400002B RID: 43
		public readonly HttpWebhookCaller _httpWebhookCaller;

		// Token: 0x0400002C RID: 44
		public Automatable _automatable;

		// Token: 0x0400002D RID: 45
		public UniquelyNamedEntity _uniquelyNamedEntity;

		// Token: 0x0400002E RID: 46
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400002F RID: 47
		public Guid _entityId;

		// Token: 0x04000030 RID: 48
		public ConnectionState? _previousState;

		// Token: 0x04000031 RID: 49
		public volatile int _lastOnCallSuccessful = -1;

		// Token: 0x04000032 RID: 50
		public volatile int _lastOffCallSuccessful = -1;

		// Token: 0x04000033 RID: 51
		public bool _switchedOnWebhookEnabled;

		// Token: 0x04000034 RID: 52
		public bool _switchedOffWebhookEnabled;

		// Token: 0x04000035 RID: 53
		public string _snapshotName;
	}
}
