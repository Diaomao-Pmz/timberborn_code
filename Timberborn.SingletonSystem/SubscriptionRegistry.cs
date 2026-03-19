using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.SingletonSystem
{
	// Token: 0x0200001A RID: 26
	public class SubscriptionRegistry
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000029D0 File Offset: 0x00000BD0
		public void Add(Type eventType, object subscriber, Action<object> action)
		{
			if (!this._subscriptions.ContainsKey(eventType))
			{
				this._subscriptions[eventType] = new Dictionary<object, Action<object>>();
			}
			if (this._subscriptions[eventType].ContainsKey(subscriber))
			{
				throw new ArgumentException(string.Format("There is already an entry for {0}", subscriber));
			}
			this._subscriptions[eventType].Add(subscriber, action);
			this._subscribers.Add(subscriber);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A44 File Offset: 0x00000C44
		public void RemoveAll(object subscriber)
		{
			this._subscribers.Remove(subscriber);
			foreach (KeyValuePair<Type, Dictionary<object, Action<object>>> keyValuePair in this._subscriptions)
			{
				keyValuePair.Value.Remove(subscriber);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AAC File Offset: 0x00000CAC
		public IEnumerable<Subscription> Get(Type eventType)
		{
			if (!this._subscriptions.ContainsKey(eventType))
			{
				return null;
			}
			return this.GetSubscriptions(eventType);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public bool IsSubscriber(object subscriber)
		{
			return this._subscribers.Contains(subscriber);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public IEnumerable<Subscription> GetSubscriptions(Type eventType)
		{
			return from subscription in this._subscriptions[eventType]
			select new Subscription(subscription.Key, subscription.Value);
		}

		// Token: 0x04000026 RID: 38
		public readonly Dictionary<Type, Dictionary<object, Action<object>>> _subscriptions = new Dictionary<Type, Dictionary<object, Action<object>>>();

		// Token: 0x04000027 RID: 39
		public readonly HashSet<object> _subscribers = new HashSet<object>();
	}
}
