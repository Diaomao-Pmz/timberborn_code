using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000026 RID: 38
	public class Memory : BaseComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IDuplicable<Memory>, IDuplicable, IFinishedStateListener, ISequentialTransmitter, ITransmitter
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000532E File Offset: 0x0000352E
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005336 File Offset: 0x00003536
		public MemoryMode Mode { get; private set; }

		// Token: 0x060001A1 RID: 417 RVA: 0x0000533F File Offset: 0x0000353F
		public Memory(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000534E File Offset: 0x0000354E
		public Automator InputA
		{
			get
			{
				return this._inputA.Transmitter;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000535B File Offset: 0x0000355B
		public Automator InputB
		{
			get
			{
				return this._inputB.Transmitter;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005368 File Offset: 0x00003568
		public Automator ResetInput
		{
			get
			{
				return this._resetInput.Transmitter;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005378 File Offset: 0x00003578
		public bool UsesInputB
		{
			get
			{
				bool result;
				switch (this.Mode)
				{
				case MemoryMode.SetReset:
					result = false;
					break;
				case MemoryMode.Toggle:
					result = false;
					break;
				case MemoryMode.Latch:
					result = true;
					break;
				case MemoryMode.FlipFlop:
					result = true;
					break;
				default:
					throw new ArgumentOutOfRangeException(string.Format("Unexpected value: {0}", this.Mode));
				}
				return result;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000053D0 File Offset: 0x000035D0
		public bool IsProcessingNewInput
		{
			get
			{
				return this._nextState != this._state;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000053E4 File Offset: 0x000035E4
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._inputA = this._automator.AddInput();
			this._inputB = this._automator.AddInput();
			this._resetInput = this._automator.AddInput();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005430 File Offset: 0x00003630
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Memory.MemoryKey);
			component.Set<MemoryMode>(Memory.ModeKey, this.Mode);
			if (this.InputA)
			{
				component.Set<Automator>(Memory.InputAKey, this.InputA, this._referenceSerializer.Of<Automator>());
			}
			if (this.InputB)
			{
				component.Set<Automator>(Memory.InputBKey, this.InputB, this._referenceSerializer.Of<Automator>());
			}
			if (this.ResetInput)
			{
				component.Set<Automator>(Memory.ResetInputKey, this.ResetInput, this._referenceSerializer.Of<Automator>());
			}
			if (this._state)
			{
				component.Set(Memory.StateKey, this._state);
			}
			if (this._previousAState)
			{
				component.Set(Memory.PreviousAStateKey, this._previousAState);
			}
			if (this._previousBState)
			{
				component.Set(Memory.PreviousBStateKey, this._previousBState);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005520 File Offset: 0x00003720
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader2;
			IObjectLoader objectLoader = entityLoader.TryGetComponent(Memory.MemoryKey, out objectLoader2) ? objectLoader2 : entityLoader.GetComponent(new ComponentKey("Latch"));
			this.Mode = objectLoader.Get<MemoryMode>(Memory.ModeKey);
			Automator transmitter;
			if (objectLoader.Has<Automator>(Memory.InputAKey) && objectLoader.GetObsoletable<Automator>(Memory.InputAKey, this._referenceSerializer.Of<Automator>(), out transmitter))
			{
				this._inputA.Connect(transmitter);
			}
			Automator transmitter2;
			if (this.UsesInputB && objectLoader.Has<Automator>(Memory.InputBKey) && objectLoader.GetObsoletable<Automator>(Memory.InputBKey, this._referenceSerializer.Of<Automator>(), out transmitter2))
			{
				this._inputB.Connect(transmitter2);
			}
			Automator transmitter3;
			if (objectLoader.Has<Automator>(Memory.ResetInputKey) && objectLoader.GetObsoletable<Automator>(Memory.ResetInputKey, this._referenceSerializer.Of<Automator>(), out transmitter3))
			{
				this._resetInput.Connect(transmitter3);
			}
			this._state = (objectLoader.Has<bool>(Memory.StateKey) && objectLoader.Get(Memory.StateKey));
			this._previousAState = (objectLoader.Has<bool>(Memory.PreviousAStateKey) && objectLoader.Get(Memory.PreviousAStateKey));
			this._previousBState = (objectLoader.Has<bool>(Memory.PreviousBStateKey) && objectLoader.Get(Memory.PreviousBStateKey));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005666 File Offset: 0x00003866
		public void InitializeEntity()
		{
			this.UpdateOutputState();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005670 File Offset: 0x00003870
		public void DuplicateFrom(Memory source)
		{
			this.SetMode(source.Mode);
			this._inputA.Connect(source.InputA);
			this._inputB.Connect(source.InputB);
			this._resetInput.Connect(source.ResetInput);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000056BC File Offset: 0x000038BC
		public void OnEnterFinishedState()
		{
			this._previousAState = this._inputA.BooleanState;
			this._previousBState = this._inputB.BooleanState;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000387E File Offset: 0x00001A7E
		public void OnExitFinishedState()
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000056E0 File Offset: 0x000038E0
		public void SetMode(MemoryMode memoryMode)
		{
			this.Mode = memoryMode;
			if (!this.UsesInputB)
			{
				this._inputB.Disconnect();
			}
			this.EvaluateNext();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005702 File Offset: 0x00003902
		public void SetInputA(Automator automator)
		{
			this._inputA.Connect(automator);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005710 File Offset: 0x00003910
		public void SetInputB(Automator automator)
		{
			if (this.UsesInputB)
			{
				this._inputB.Connect(automator);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005726 File Offset: 0x00003926
		public void SetResetInput(Automator automator)
		{
			this._resetInput.Connect(automator);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005734 File Offset: 0x00003934
		public void Reset()
		{
			this._state = false;
			this.UpdateOutputState();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005744 File Offset: 0x00003944
		public void EvaluateNext()
		{
			bool flag = !this.ResetState;
			if (flag)
			{
				bool flag2;
				switch (this.Mode)
				{
				case MemoryMode.SetReset:
					flag2 = ((this._state || this.AState) && !this.BState);
					break;
				case MemoryMode.Toggle:
					flag2 = (this.ARising ? (!this._state) : this._state);
					break;
				case MemoryMode.Latch:
					flag2 = (this.BState ? this.AState : this._state);
					break;
				case MemoryMode.FlipFlop:
					flag2 = (this.BRising ? this.AState : this._state);
					break;
				default:
					throw new ArgumentOutOfRangeException(string.Format("Unexpected value: {0}", this.Mode));
				}
				flag = flag2;
			}
			this._nextState = flag;
			this._nextPreviousAState = this._inputA.BooleanState;
			this._nextPreviousBState = this._inputB.BooleanState;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005833 File Offset: 0x00003A33
		public void CommitTick()
		{
			this._state = this._nextState;
			this._previousAState = this._nextPreviousAState;
			this._previousBState = this._nextPreviousBState;
			this.UpdateOutputState();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000585F File Offset: 0x00003A5F
		public bool AState
		{
			get
			{
				return this._inputA.BooleanState;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000586C File Offset: 0x00003A6C
		public bool BState
		{
			get
			{
				return this._inputB.BooleanState;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005879 File Offset: 0x00003A79
		public bool ResetState
		{
			get
			{
				return this._resetInput.BooleanState;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005886 File Offset: 0x00003A86
		public bool ARising
		{
			get
			{
				return this._inputA.BooleanState && !this._previousAState;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000058A0 File Offset: 0x00003AA0
		public bool BRising
		{
			get
			{
				return this._inputB.BooleanState && !this._previousBState;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000058BA File Offset: 0x00003ABA
		public void UpdateOutputState()
		{
			this._automator.SetState(this._state);
		}

		// Token: 0x040000A6 RID: 166
		public static readonly ComponentKey MemoryKey = new ComponentKey("Memory");

		// Token: 0x040000A7 RID: 167
		public static readonly PropertyKey<MemoryMode> ModeKey = new PropertyKey<MemoryMode>("Mode");

		// Token: 0x040000A8 RID: 168
		public static readonly PropertyKey<Automator> InputAKey = new PropertyKey<Automator>("InputA");

		// Token: 0x040000A9 RID: 169
		public static readonly PropertyKey<Automator> InputBKey = new PropertyKey<Automator>("InputB");

		// Token: 0x040000AA RID: 170
		public static readonly PropertyKey<Automator> ResetInputKey = new PropertyKey<Automator>("ResetInput");

		// Token: 0x040000AB RID: 171
		public static readonly PropertyKey<bool> StateKey = new PropertyKey<bool>("State");

		// Token: 0x040000AC RID: 172
		public static readonly PropertyKey<bool> PreviousAStateKey = new PropertyKey<bool>("PreviousAState");

		// Token: 0x040000AD RID: 173
		public static readonly PropertyKey<bool> PreviousBStateKey = new PropertyKey<bool>("PreviousBState");

		// Token: 0x040000AF RID: 175
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x040000B0 RID: 176
		public Automator _automator;

		// Token: 0x040000B1 RID: 177
		public AutomatorConnection _inputA;

		// Token: 0x040000B2 RID: 178
		public AutomatorConnection _inputB;

		// Token: 0x040000B3 RID: 179
		public AutomatorConnection _resetInput;

		// Token: 0x040000B4 RID: 180
		public bool _state;

		// Token: 0x040000B5 RID: 181
		public bool _previousAState;

		// Token: 0x040000B6 RID: 182
		public bool _previousBState;

		// Token: 0x040000B7 RID: 183
		public bool _nextState;

		// Token: 0x040000B8 RID: 184
		public bool _nextPreviousAState;

		// Token: 0x040000B9 RID: 185
		public bool _nextPreviousBState;
	}
}
