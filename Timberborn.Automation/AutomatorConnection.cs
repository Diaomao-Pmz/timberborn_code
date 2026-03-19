using System;

namespace Timberborn.Automation
{
	// Token: 0x02000011 RID: 17
	public class AutomatorConnection
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003EBB File Offset: 0x000020BB
		public Automator Receiver { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003EC3 File Offset: 0x000020C3
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003ECB File Offset: 0x000020CB
		public Automator Transmitter { get; private set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ED4 File Offset: 0x000020D4
		public AutomatorConnection(Automator receiver, AutomationRunner automationRunner)
		{
			this.Receiver = receiver;
			this._automationRunner = automationRunner;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003EEA File Offset: 0x000020EA
		public ConnectionState State
		{
			get
			{
				if (this.Transmitter == null)
				{
					return ConnectionState.Disconnected;
				}
				if (this.Transmitter.State != AutomatorState.On)
				{
					return ConnectionState.Off;
				}
				return ConnectionState.On;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003F07 File Offset: 0x00002107
		public bool BooleanState
		{
			get
			{
				return this.State == ConnectionState.On;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003F12 File Offset: 0x00002112
		public bool IsConnected
		{
			get
			{
				return this.Transmitter != null;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F20 File Offset: 0x00002120
		public void Connect(Automator transmitter)
		{
			if (transmitter != null && transmitter.IsTransmitter)
			{
				if (this.Transmitter != transmitter)
				{
					this.DisconnectInternal();
					this.Transmitter = transmitter;
					transmitter.ConnectToOutput(this);
					if (this.Receiver.RegisteredForRunning && transmitter.RegisteredForRunning)
					{
						this._automationRunner.MergePartitions(this.Receiver.Partition, transmitter.Partition);
					}
					if (this.Receiver.RegisteredForRunning)
					{
						AutomatorPartition partition = this.Receiver.Partition;
						if (partition != null)
						{
							partition.InvalidatePlan();
						}
					}
					this.Receiver.OnInputReconnected();
					return;
				}
			}
			else
			{
				this.Disconnect();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FBE File Offset: 0x000021BE
		public void Disconnect()
		{
			if (this.Transmitter != null)
			{
				this.DisconnectInternal();
				this.Receiver.OnInputReconnected();
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FD9 File Offset: 0x000021D9
		public void Remove()
		{
			this.Disconnect();
			this.Receiver.RemoveInput(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FF0 File Offset: 0x000021F0
		public void DisconnectInternal()
		{
			Automator transmitter = this.Transmitter;
			if (transmitter != null)
			{
				transmitter.DisconnectFromOutput(this);
				this.Transmitter = null;
				if (transmitter.RegisteredForRunning)
				{
					AutomatorPartition partition = transmitter.Partition;
					if (partition != null)
					{
						partition.InvalidatePlan();
					}
				}
				if (this.Receiver.RegisteredForRunning)
				{
					AutomatorPartition partition2 = this.Receiver.Partition;
					if (partition2 != null)
					{
						partition2.InvalidatePlan();
					}
				}
				if (this.Receiver.RegisteredForRunning && transmitter.RegisteredForRunning && transmitter.Partition == this.Receiver.Partition)
				{
					this._automationRunner.ReassignExistingPartition(this.Receiver.Partition);
				}
			}
		}

		// Token: 0x04000051 RID: 81
		public readonly AutomationRunner _automationRunner;
	}
}
