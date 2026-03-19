using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Bots;
using Timberborn.EntitySystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000007 RID: 7
	public class Tube : BaseComponent, IAwakableComponent, IFinishedStateListener, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler TubeDeleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler VisitorsChanged;

		// Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public Tube(TubeMap tubeMap)
		{
			this._tubeMap = tubeMap;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002202 File Offset: 0x00000402
		public bool HasAnyVisitor
		{
			get
			{
				return this._beaverVisitors.Count > 0 || this._botVisitors.Count > 0;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002222 File Offset: 0x00000422
		public bool HasBotVisitor
		{
			get
			{
				return this._botVisitors.Count > 0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002232 File Offset: 0x00000432
		public bool CanBeVisited
		{
			get
			{
				return this._blockObject.IsFinished;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000223F File Offset: 0x0000043F
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000224D File Offset: 0x0000044D
		public void OnEnterFinishedState()
		{
			this._tubeMap.SetTube(this, this._blockObject.Coordinates);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002266 File Offset: 0x00000466
		public void OnExitFinishedState()
		{
			this._tubeMap.UnsetTube(this._blockObject.Coordinates);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000227E File Offset: 0x0000047E
		public void AddVisitor(TubeVisitor visitor)
		{
			if (this.GetVisitors(visitor).Add(visitor))
			{
				EventHandler visitorsChanged = this.VisitorsChanged;
				if (visitorsChanged == null)
				{
					return;
				}
				visitorsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A5 File Offset: 0x000004A5
		public void RemoveVisitor(TubeVisitor visitor)
		{
			if (this.GetVisitors(visitor).Remove(visitor))
			{
				EventHandler visitorsChanged = this.VisitorsChanged;
				if (visitorsChanged == null)
				{
					return;
				}
				visitorsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022CC File Offset: 0x000004CC
		public void DeleteEntity()
		{
			EventHandler tubeDeleted = this.TubeDeleted;
			if (tubeDeleted == null)
			{
				return;
			}
			tubeDeleted(this, EventArgs.Empty);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022E4 File Offset: 0x000004E4
		public HashSet<TubeVisitor> GetVisitors(TubeVisitor visitor)
		{
			if (!visitor.HasComponent<BotSpec>())
			{
				return this._beaverVisitors;
			}
			return this._botVisitors;
		}

		// Token: 0x0400000A RID: 10
		public readonly TubeMap _tubeMap;

		// Token: 0x0400000B RID: 11
		public BlockObject _blockObject;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<TubeVisitor> _beaverVisitors = new HashSet<TubeVisitor>();

		// Token: 0x0400000D RID: 13
		public readonly HashSet<TubeVisitor> _botVisitors = new HashSet<TubeVisitor>();
	}
}
