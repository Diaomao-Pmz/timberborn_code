using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Automation
{
	// Token: 0x02000005 RID: 5
	public class Automatable : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<Automatable>, IDuplicable, ITerminal
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		public event EventHandler InputStateChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002184 File Offset: 0x00000384
		public event EventHandler InputReconnected;

		// Token: 0x06000009 RID: 9 RVA: 0x000021B9 File Offset: 0x000003B9
		public Automatable(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021D4 File Offset: 0x000003D4
		public ConnectionState State
		{
			get
			{
				ConnectionState result;
				switch (this._inputConnection.State)
				{
				case ConnectionState.Disconnected:
					result = ConnectionState.Disconnected;
					break;
				case ConnectionState.Off:
					result = ConnectionState.Off;
					break;
				case ConnectionState.On:
					result = (this._blockObject.IsFinished ? ConnectionState.On : ConnectionState.Off);
					break;
				default:
					throw new Exception(string.Format("Unexpected state {0}", this._inputConnection.State));
				}
				return result;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000223E File Offset: 0x0000043E
		public bool IsAutomated
		{
			get
			{
				return this._inputConnection.IsConnected;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000224B File Offset: 0x0000044B
		public Automator Input
		{
			get
			{
				return this._inputConnection.Transmitter;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._inputConnection = base.GetComponent<Automator>().AddInput();
			base.GetComponents<IAutomatableNeeder>(this._automatableNeeders);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002283 File Offset: 0x00000483
		public void SetInput(Automator automator)
		{
			if (automator != this._inputConnection.Transmitter)
			{
				this._inputConnection.Connect(automator);
				this.NotifyInputReconnected();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A5 File Offset: 0x000004A5
		public void Save(IEntitySaver entitySaver)
		{
			if (this._inputConnection.IsConnected)
			{
				entitySaver.GetComponent(Automatable.AutomatableKey).Set<Automator>(Automatable.InputKey, this.Input, this._referenceSerializer.Of<Automator>());
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022DC File Offset: 0x000004DC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			Automator transmitter;
			if (entityLoader.TryGetComponent(Automatable.AutomatableKey, out objectLoader) && objectLoader.GetObsoletable<Automator>(Automatable.InputKey, this._referenceSerializer.Of<Automator>(), out transmitter))
			{
				this._inputConnection.Connect(transmitter);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000231E File Offset: 0x0000051E
		public void DuplicateFrom(Automatable source)
		{
			this.SetInput(source._inputConnection.Transmitter);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002331 File Offset: 0x00000531
		public void Evaluate()
		{
			if (this._lastNotifyState != this.State)
			{
				this._lastNotifyState = this.State;
				this.NotifyInputStateChanged();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002354 File Offset: 0x00000554
		public bool IsNeeded()
		{
			using (List<IAutomatableNeeder>.Enumerator enumerator = this._automatableNeeders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.NeedsAutomatable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023B0 File Offset: 0x000005B0
		public void NotifyInputStateChanged()
		{
			EventHandler inputStateChanged = this.InputStateChanged;
			if (inputStateChanged == null)
			{
				return;
			}
			inputStateChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C8 File Offset: 0x000005C8
		public void NotifyInputReconnected()
		{
			EventHandler inputReconnected = this.InputReconnected;
			if (inputReconnected == null)
			{
				return;
			}
			inputReconnected(this, EventArgs.Empty);
		}

		// Token: 0x04000007 RID: 7
		public static readonly ComponentKey AutomatableKey = new ComponentKey("Automatable");

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<Automator> InputKey = new PropertyKey<Automator>("Input");

		// Token: 0x0400000B RID: 11
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;

		// Token: 0x0400000D RID: 13
		public AutomatorConnection _inputConnection;

		// Token: 0x0400000E RID: 14
		public readonly List<IAutomatableNeeder> _automatableNeeders = new List<IAutomatableNeeder>();

		// Token: 0x0400000F RID: 15
		public ConnectionState _lastNotifyState;
	}
}
