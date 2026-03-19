using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001F RID: 31
	public class ZiplineTowerOperationValidator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000FD RID: 253 RVA: 0x000046D4 File Offset: 0x000028D4
		// (remove) Token: 0x060000FE RID: 254 RVA: 0x0000470C File Offset: 0x0000290C
		public event EventHandler OperativeStateChanged;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004741 File Offset: 0x00002941
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004749 File Offset: 0x00002949
		public bool IsOperative { get; private set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00004752 File Offset: 0x00002952
		public ZiplineTowerOperationValidator(ZiplineCableRenderer ziplineCableRenderer)
		{
			this._ziplineCableRenderer = ziplineCableRenderer;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004777 File Offset: 0x00002977
		public void Awake()
		{
			this._ziplineTower = base.GetComponent<ZiplineTower>();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004785 File Offset: 0x00002985
		public void OnEnterFinishedState()
		{
			this._ziplineTower.ConnectionTargetsChanged += this.OnConnectionTargetsChanged;
			this.UpdateState();
			this.NotifyConnectedInNetwork();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000047AA File Offset: 0x000029AA
		public void OnExitFinishedState()
		{
			this._ziplineTower.ConnectionTargetsChanged -= this.OnConnectionTargetsChanged;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000047C3 File Offset: 0x000029C3
		public void OnConnectionTargetsChanged(object sender, EventArgs e)
		{
			this.UpdateState();
			this.NotifyConnectedInNetwork();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000047D4 File Offset: 0x000029D4
		public void UpdateState()
		{
			this.FindConnectedTowers(this._ziplineTower);
			int num = 0;
			using (HashSet<ZiplineTower>.Enumerator enumerator = this._connectedTowersCache.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasComponent<ZiplineStationSpec>())
					{
						num++;
					}
				}
			}
			this.ClearCache();
			this.UpdateState(num);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004848 File Offset: 0x00002A48
		public void UpdateState(int connectedStations)
		{
			float num = this._ziplineTower.HasComponent<ZiplineStationSpec>() ? ZiplineTowerOperationValidator.MinConnectionsForStation : ZiplineTowerOperationValidator.MinConnectionsForPylon;
			bool flag = (float)connectedStations >= num && this._ziplineTower.IsActive;
			if (flag != this.IsOperative)
			{
				this.IsOperative = flag;
				EventHandler operativeStateChanged = this.OperativeStateChanged;
				if (operativeStateChanged != null)
				{
					operativeStateChanged(this, EventArgs.Empty);
				}
				this.UpdateRenderer();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000048B0 File Offset: 0x00002AB0
		public void NotifyConnectedInNetwork()
		{
			this.FindConnectedTowers(this._ziplineTower);
			foreach (ZiplineTower ziplineTower in this._connectedTowersCache)
			{
				ziplineTower.GetComponent<ZiplineTowerOperationValidator>().UpdateState();
			}
			this.ClearCache();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004918 File Offset: 0x00002B18
		public void FindConnectedTowers(ZiplineTower startTower)
		{
			using (List<ZiplineTower>.Enumerator enumerator = startTower.ConnectionTargets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ZiplineTower ziplineTower = enumerator.Current;
					if (ziplineTower.IsActive)
					{
						this._towersToCheckCache.Enqueue(ziplineTower);
					}
				}
				goto IL_BD;
			}
			IL_49:
			ZiplineTower ziplineTower2 = this._towersToCheckCache.Dequeue();
			if (ziplineTower2 != startTower && this._connectedTowersCache.Add(ziplineTower2) && !ziplineTower2.HasComponent<ZiplineStationSpec>())
			{
				foreach (ZiplineTower ziplineTower3 in ziplineTower2.ConnectionTargets)
				{
					if (ziplineTower3 != startTower && ziplineTower3.IsActive)
					{
						this._towersToCheckCache.Enqueue(ziplineTower3);
					}
				}
			}
			IL_BD:
			if (this._towersToCheckCache.Count <= 0)
			{
				return;
			}
			goto IL_49;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004A10 File Offset: 0x00002C10
		public void ClearCache()
		{
			this._connectedTowersCache.Clear();
			this._towersToCheckCache.Clear();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004A28 File Offset: 0x00002C28
		public void UpdateRenderer()
		{
			foreach (ZiplineTower otherZiplineTower in this._ziplineTower.ConnectionTargets)
			{
				this._ziplineCableRenderer.UpdateConnection(this._ziplineTower, otherZiplineTower);
			}
		}

		// Token: 0x0400005F RID: 95
		public static readonly float MinConnectionsForStation = 1f;

		// Token: 0x04000060 RID: 96
		public static readonly float MinConnectionsForPylon = 2f;

		// Token: 0x04000063 RID: 99
		public readonly ZiplineCableRenderer _ziplineCableRenderer;

		// Token: 0x04000064 RID: 100
		public ZiplineTower _ziplineTower;

		// Token: 0x04000065 RID: 101
		public readonly HashSet<ZiplineTower> _connectedTowersCache = new HashSet<ZiplineTower>();

		// Token: 0x04000066 RID: 102
		public readonly Queue<ZiplineTower> _towersToCheckCache = new Queue<ZiplineTower>();
	}
}
