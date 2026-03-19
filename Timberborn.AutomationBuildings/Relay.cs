using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000033 RID: 51
	public class Relay : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<Relay>, IDuplicable, ICombinationalTransmitter, ITransmitter
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00006B52 File Offset: 0x00004D52
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00006B5A File Offset: 0x00004D5A
		public RelayMode Mode { get; private set; }

		// Token: 0x0600023A RID: 570 RVA: 0x00006B63 File Offset: 0x00004D63
		public Relay(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006B72 File Offset: 0x00004D72
		public Automator InputA
		{
			get
			{
				return this._inputA.Transmitter;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006B7F File Offset: 0x00004D7F
		public Automator InputB
		{
			get
			{
				return this._inputB.Transmitter;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006B8C File Offset: 0x00004D8C
		public bool UsesInputB
		{
			get
			{
				bool result;
				switch (this.Mode)
				{
				case RelayMode.Not:
					result = false;
					break;
				case RelayMode.And:
					result = true;
					break;
				case RelayMode.Or:
					result = true;
					break;
				case RelayMode.Xor:
					result = true;
					break;
				case RelayMode.Passthrough:
					result = false;
					break;
				default:
					throw new ArgumentOutOfRangeException(string.Format("Unexpected value: {0}", this.Mode));
				}
				return result;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006BEC File Offset: 0x00004DEC
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._inputA = this._automator.AddInput();
			this._inputB = this._automator.AddInput();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006C1C File Offset: 0x00004E1C
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Relay.RelayKey);
			component.Set<RelayMode>(Relay.ModeKey, this.Mode);
			if (this.InputA)
			{
				component.Set<Automator>(Relay.InputAKey, this.InputA, this._referenceSerializer.Of<Automator>());
			}
			if (this.InputB)
			{
				component.Set<Automator>(Relay.InputBKey, this.InputB, this._referenceSerializer.Of<Automator>());
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006C98 File Offset: 0x00004E98
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Relay.RelayKey);
			this.Mode = component.Get<RelayMode>(Relay.ModeKey);
			Automator transmitter;
			if (component.Has<Automator>(Relay.InputAKey) && component.GetObsoletable<Automator>(Relay.InputAKey, this._referenceSerializer.Of<Automator>(), out transmitter))
			{
				this._inputA.Connect(transmitter);
			}
			Automator transmitter2;
			if (this.UsesInputB && component.Has<Automator>(Relay.InputBKey) && component.GetObsoletable<Automator>(Relay.InputBKey, this._referenceSerializer.Of<Automator>(), out transmitter2))
			{
				this._inputB.Connect(transmitter2);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006D30 File Offset: 0x00004F30
		public void DuplicateFrom(Relay source)
		{
			this.SetMode(source.Mode);
			this._inputA.Connect(source.InputA);
			this._inputB.Connect(source.InputB);
			this.Evaluate();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006D66 File Offset: 0x00004F66
		public void SetMode(RelayMode relayMode)
		{
			this.Mode = relayMode;
			if (!this.UsesInputB)
			{
				this._inputB.Disconnect();
			}
			this.Evaluate();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006D88 File Offset: 0x00004F88
		public void SetInputA(Automator automator)
		{
			this._inputA.Connect(automator);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006D96 File Offset: 0x00004F96
		public void SetInputB(Automator automator)
		{
			if (this.UsesInputB)
			{
				this._inputB.Connect(automator);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006DAC File Offset: 0x00004FAC
		public void Evaluate()
		{
			Automator automator = this._automator;
			bool state;
			switch (this.Mode)
			{
			case RelayMode.Not:
				state = !this._inputA.BooleanState;
				break;
			case RelayMode.And:
				state = (this._inputA.BooleanState && this._inputB.BooleanState);
				break;
			case RelayMode.Or:
				state = (this._inputA.BooleanState || this._inputB.BooleanState);
				break;
			case RelayMode.Xor:
				state = (this._inputA.BooleanState ^ this._inputB.BooleanState);
				break;
			case RelayMode.Passthrough:
				state = this._inputA.BooleanState;
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected value: {0}", this.Mode));
			}
			automator.SetState(state);
		}

		// Token: 0x04000109 RID: 265
		public static readonly ComponentKey RelayKey = new ComponentKey("Relay");

		// Token: 0x0400010A RID: 266
		public static readonly PropertyKey<RelayMode> ModeKey = new PropertyKey<RelayMode>("Mode");

		// Token: 0x0400010B RID: 267
		public static readonly PropertyKey<Automator> InputAKey = new PropertyKey<Automator>("InputA");

		// Token: 0x0400010C RID: 268
		public static readonly PropertyKey<Automator> InputBKey = new PropertyKey<Automator>("InputB");

		// Token: 0x0400010E RID: 270
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400010F RID: 271
		public AutomatorConnection _inputA;

		// Token: 0x04000110 RID: 272
		public AutomatorConnection _inputB;

		// Token: 0x04000111 RID: 273
		public Automator _automator;
	}
}
