using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.RelationSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Automation
{
	// Token: 0x0200000F RID: 15
	public class Automator : BaseComponent, IAwakableComponent, IInitializableEntity, IPostLoadableEntity, IDeletableEntity, IFinishedStateListener, IPersistentEntity, IRelationOwner
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005E RID: 94 RVA: 0x00003580 File Offset: 0x00001780
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x000035B8 File Offset: 0x000017B8
		public event EventHandler IsCyclicOrBlockedChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000060 RID: 96 RVA: 0x000035F0 File Offset: 0x000017F0
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00003628 File Offset: 0x00001828
		public event EventHandler RelationsChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000365D File Offset: 0x0000185D
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003665 File Offset: 0x00001865
		public int Evaluations { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000366E File Offset: 0x0000186E
		public ReadOnlyList<AutomatorConnection> InputConnections { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003676 File Offset: 0x00001876
		public ReadOnlyList<AutomatorConnection> OutputConnections { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000367E File Offset: 0x0000187E
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003686 File Offset: 0x00001886
		public bool IsCyclicOrBlocked { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000368F File Offset: 0x0000188F
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003697 File Offset: 0x00001897
		public AutomatorPartition Partition { get; internal set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000036A0 File Offset: 0x000018A0
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000036A8 File Offset: 0x000018A8
		internal bool RegisteredForRunning { get; private set; }

		// Token: 0x0600006C RID: 108 RVA: 0x000036B4 File Offset: 0x000018B4
		public Automator(AutomationRunner automationRunner)
		{
			this._automationRunner = automationRunner;
			this.InputConnections = this._inputConnections.AsReadOnlyList<AutomatorConnection>();
			this.OutputConnections = this._outputConnections.AsReadOnlyList<AutomatorConnection>();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003706 File Offset: 0x00001906
		public string AutomatorName
		{
			get
			{
				return this._namedEntity.EntityName;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003714 File Offset: 0x00001914
		public string AutomatorId
		{
			get
			{
				return this._entityComponent.EntityId.ToString();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000373A File Offset: 0x0000193A
		public NamedEntitySortingKey SortingKey
		{
			get
			{
				return this._namedEntity.SortingKey;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003747 File Offset: 0x00001947
		public bool IsTransmitter
		{
			get
			{
				return this._transmitter != null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003754 File Offset: 0x00001954
		public AutomatorState State
		{
			get
			{
				AutomatorState result;
				switch (this._state)
				{
				case AutomatorState.Off:
					result = AutomatorState.Off;
					break;
				case AutomatorState.On:
					result = (this._blockObject.IsFinished ? AutomatorState.On : AutomatorState.Off);
					break;
				case AutomatorState.Error:
					result = AutomatorState.Error;
					break;
				default:
					throw new Exception(string.Format("Unexpected state {0}", this._state));
				}
				return result;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000037B4 File Offset: 0x000019B4
		public AutomatorState UnfinishedState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000037BC File Offset: 0x000019BC
		public int Usages
		{
			get
			{
				return this._outputConnections.Count;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000037C9 File Offset: 0x000019C9
		public bool IsProcessingNewInput
		{
			get
			{
				ISequentialTransmitter sequentialTransmitter = this._sequentialTransmitter;
				return sequentialTransmitter != null && sequentialTransmitter.IsProcessingNewInput;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037DC File Offset: 0x000019DC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._namedEntity = base.GetComponent<NamedEntity>();
			this._entityComponent = base.GetComponent<EntityComponent>();
			this._transmitter = base.GetComponent<ITransmitter>();
			this._samplingTransmitter = (this._transmitter as ISamplingTransmitter);
			this._combinationalTransmitter = (this._transmitter as ICombinationalTransmitter);
			this._sequentialTransmitter = (this._transmitter as ISequentialTransmitter);
			this._terminals = base.GetComponentsAllocating<ITerminal>();
			this._listeners = base.GetComponentsAllocating<IAutomatorListener>();
			this.ValidateAwake();
			base.DisableComponent();
			this._awoken = true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003877 File Offset: 0x00001A77
		public void InitializeEntity()
		{
			this._automationRunner.Register(this);
			this.RegisteredForRunning = true;
			this.SchedulePartition();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003892 File Offset: 0x00001A92
		public void PostLoadEntity()
		{
			if (this.IsSamplingTransmitter)
			{
				this.Sample();
			}
			if (this._state != AutomatorState.Off)
			{
				this.NotifyOrPostponeListeners();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038B0 File Offset: 0x00001AB0
		public void DeleteEntity()
		{
			while (!this._inputConnections.IsEmpty<AutomatorConnection>())
			{
				List<AutomatorConnection> inputConnections = this._inputConnections;
				inputConnections[inputConnections.Count - 1].Remove();
			}
			while (!this._outputConnections.IsEmpty<AutomatorConnection>())
			{
				List<AutomatorConnection> outputConnections = this._outputConnections;
				outputConnections[outputConnections.Count - 1].Disconnect();
			}
			this.RegisteredForRunning = false;
			this._automationRunner.Unregister(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000391E File Offset: 0x00001B1E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.SchedulePartition();
			if (this._state != AutomatorState.Off)
			{
				this.NotifyListenersNow();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000393A File Offset: 0x00001B3A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003944 File Offset: 0x00001B44
		public AutomatorConnection AddInput()
		{
			this.ValidateAddInput();
			AutomatorConnection automatorConnection = new AutomatorConnection(this, this._automationRunner);
			this._inputConnections.Add(automatorConnection);
			return automatorConnection;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003971 File Offset: 0x00001B71
		public void SetState(bool state)
		{
			this.SetStateInternal(state ? AutomatorState.On : AutomatorState.Off);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003980 File Offset: 0x00001B80
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsTransmitter)
			{
				entitySaver.GetComponent(Automator.AutomatorKey).Set<AutomatorState>(Automator.StateKey, this._state);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000039A8 File Offset: 0x00001BA8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Automator.AutomatorKey, out objectLoader) && this.IsTransmitter && objectLoader.Has<AutomatorState>(Automator.StateKey))
			{
				this._state = objectLoader.Get<AutomatorState>(Automator.StateKey);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000039EA File Offset: 0x00001BEA
		public IEnumerable<BaseComponent> GetRelations()
		{
			int num;
			for (int i = 0; i < this._inputConnections.Count; i = num + 1)
			{
				Automator transmitter = this._inputConnections[i].Transmitter;
				if (transmitter != this)
				{
					yield return transmitter;
				}
				num = i;
			}
			for (int i = 0; i < this._outputConnections.Count; i = num + 1)
			{
				Automator receiver = this._outputConnections[i].Receiver;
				if (receiver != this)
				{
					yield return receiver;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000039FA File Offset: 0x00001BFA
		public bool IsSamplingTransmitter
		{
			get
			{
				return this._samplingTransmitter != null;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003A05 File Offset: 0x00001C05
		public bool IsCombinationalTransmitter
		{
			get
			{
				return this._combinationalTransmitter != null;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003A10 File Offset: 0x00001C10
		public bool IsSequentialTransmitter
		{
			get
			{
				return this._sequentialTransmitter != null;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003A1B File Offset: 0x00001C1B
		public bool IsTerminal
		{
			get
			{
				return this._terminals != null && !this._terminals.IsEmpty<ITerminal>();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A35 File Offset: 0x00001C35
		public void ConnectToOutput(AutomatorConnection automatorConnection)
		{
			this._outputConnections.Add(automatorConnection);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003A43 File Offset: 0x00001C43
		public void DisconnectFromOutput(AutomatorConnection automatorConnection)
		{
			this._outputConnections.Remove(automatorConnection);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003A52 File Offset: 0x00001C52
		public void RemoveInput(AutomatorConnection automatorConnection)
		{
			this._inputConnections.Remove(automatorConnection);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003A61 File Offset: 0x00001C61
		public void SetCyclicOrBlocked(bool value)
		{
			if (this.IsCyclicOrBlocked != value)
			{
				this.IsCyclicOrBlocked = value;
				EventHandler isCyclicOrBlockedChanged = this.IsCyclicOrBlockedChanged;
				if (isCyclicOrBlockedChanged == null)
				{
					return;
				}
				isCyclicOrBlockedChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003A89 File Offset: 0x00001C89
		public void Sample()
		{
			this._samplingTransmitter.Sample();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A98 File Offset: 0x00001C98
		public void EvaluateCombinational()
		{
			if (this.IsCyclicOrBlocked)
			{
				this.SetStateInternal(AutomatorState.Error);
			}
			else
			{
				this._combinationalTransmitter.Evaluate();
			}
			int evaluations = this.Evaluations;
			this.Evaluations = evaluations + 1;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public void EvaluateNext()
		{
			if (base.Enabled)
			{
				this._sequentialTransmitter.EvaluateNext();
				int evaluations = this.Evaluations;
				this.Evaluations = evaluations + 1;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B04 File Offset: 0x00001D04
		public void CommitTick()
		{
			if (base.Enabled)
			{
				this._sequentialTransmitter.CommitTick();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B1C File Offset: 0x00001D1C
		public void EvaluateTerminal()
		{
			if (base.Enabled)
			{
				for (int i = 0; i < this._terminals.Count; i++)
				{
					this._terminals[i].Evaluate();
				}
				int evaluations = this.Evaluations;
				this.Evaluations = evaluations + 1;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B68 File Offset: 0x00001D68
		public void NotifyListenersNow()
		{
			for (int i = 0; i < this._listeners.Count; i++)
			{
				this._listeners[i].OnAutomatorStateChanged();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B9C File Offset: 0x00001D9C
		public void OnInputReconnected()
		{
			this.SchedulePartition();
			EventHandler relationsChanged = this.RelationsChanged;
			if (relationsChanged == null)
			{
				return;
			}
			relationsChanged(this, EventArgs.Empty);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003BBA File Offset: 0x00001DBA
		public bool CanHaveInput
		{
			get
			{
				return this.IsCombinationalTransmitter || this.IsSequentialTransmitter || this.IsTerminal;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public void SetStateInternal(AutomatorState newState)
		{
			this.ValidateSetState();
			if (this._state != newState)
			{
				this._state = newState;
				if (this._blockObject.IsFinished)
				{
					this.SchedulePartition();
				}
				this.NotifyOrPostponeListeners();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C05 File Offset: 0x00001E05
		public void SchedulePartition()
		{
			if (this.Partition != null)
			{
				this._automationRunner.Schedule(this.Partition);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003C20 File Offset: 0x00001E20
		public void NotifyOrPostponeListeners()
		{
			AutomatorPartition partition = this.Partition;
			if (partition == null)
			{
				return;
			}
			partition.NotifyOrPostponeAutomatorListeners(this);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003C34 File Offset: 0x00001E34
		public void ValidateAwake()
		{
			if (this._transmitter != null && !this._terminals.IsEmpty<ITerminal>())
			{
				throw new Exception("Automator (" + base.Name + ") cannot be both a transmitter and a terminal.");
			}
			if (this._transmitter == null && this._terminals.IsEmpty<ITerminal>())
			{
				throw new Exception("Automator (" + base.Name + ") must be either a transmitter or a terminal by supplying a component which implements one of: ITransmitter, ISamplingTransmitter, ICombinationalTransmitter, ISequentialTransmitter, or one or more ITerminal components.");
			}
			if (!this.CanHaveInput && !this._inputConnections.IsEmpty<AutomatorConnection>())
			{
				throw new Exception("Automator (" + base.Name + ") has inputs but is not combinational, sequential or terminal.");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003CD1 File Offset: 0x00001ED1
		public void ValidateAddInput()
		{
			if (this._awoken && !this.CanHaveInput)
			{
				throw new InvalidOperationException("Trying to add input to Automator (" + base.Name + ") which is not combinational, sequential or terminal.");
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003CFE File Offset: 0x00001EFE
		public void ValidateSetState()
		{
			if (this._awoken && !this.IsTransmitter)
			{
				throw new InvalidOperationException("Trying to call SetState on a non-transmitter Automator (" + base.Name + ").");
			}
		}

		// Token: 0x0400002F RID: 47
		public static readonly ComponentKey AutomatorKey = new ComponentKey("Automator");

		// Token: 0x04000030 RID: 48
		public static readonly PropertyKey<AutomatorState> StateKey = new PropertyKey<AutomatorState>("State");

		// Token: 0x04000039 RID: 57
		public int Indegree;

		// Token: 0x0400003A RID: 58
		public long PlanVersion;

		// Token: 0x0400003B RID: 59
		public bool PostponedNotifyListeners;

		// Token: 0x0400003C RID: 60
		public readonly AutomationRunner _automationRunner;

		// Token: 0x0400003D RID: 61
		public BlockObject _blockObject;

		// Token: 0x0400003E RID: 62
		public NamedEntity _namedEntity;

		// Token: 0x0400003F RID: 63
		public EntityComponent _entityComponent;

		// Token: 0x04000040 RID: 64
		public ITransmitter _transmitter;

		// Token: 0x04000041 RID: 65
		public ISamplingTransmitter _samplingTransmitter;

		// Token: 0x04000042 RID: 66
		public ICombinationalTransmitter _combinationalTransmitter;

		// Token: 0x04000043 RID: 67
		public ISequentialTransmitter _sequentialTransmitter;

		// Token: 0x04000044 RID: 68
		public List<ITerminal> _terminals;

		// Token: 0x04000045 RID: 69
		public List<IAutomatorListener> _listeners;

		// Token: 0x04000046 RID: 70
		public readonly List<AutomatorConnection> _inputConnections = new List<AutomatorConnection>();

		// Token: 0x04000047 RID: 71
		public readonly List<AutomatorConnection> _outputConnections = new List<AutomatorConnection>();

		// Token: 0x04000048 RID: 72
		public bool _awoken;

		// Token: 0x04000049 RID: 73
		public AutomatorState _state;
	}
}
