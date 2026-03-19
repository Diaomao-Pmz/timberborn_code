using System;
using System.Collections.Generic;
using System.Reflection;
using Timberborn.Common;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000004 RID: 4
	public class EventBus : IPostLoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void PostLoad()
		{
			while (this._earlyEvents.Count > 0)
			{
				this.PostNow(this._earlyEvents.Dequeue());
			}
			this._ready = true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public void Register(object subscriber)
		{
			this.InvokeOrEnqueue(delegate
			{
				this.RegisterNow(subscriber);
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public void Unregister(object subscriber)
		{
			this.InvokeOrEnqueue(delegate
			{
				this.UnregisterNow(subscriber);
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		public void Unregister(ReadOnlyList<object> subscribers)
		{
			using (List<object>.Enumerator enumerator = subscribers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object subscriber = enumerator.Current;
					if (this._subscriptions.IsSubscriber(subscriber))
					{
						this.InvokeOrEnqueue(delegate
						{
							this.UnregisterNow(subscriber);
						});
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D4 File Offset: 0x000003D4
		public void Post(object eventObject)
		{
			if (this._ready)
			{
				this.PostNow(eventObject);
				return;
			}
			this._earlyEvents.Enqueue(eventObject);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
		public void PostNow(object eventObject)
		{
			bool posting = this._posting;
			this._posting = true;
			try
			{
				Type type = eventObject.GetType();
				IEnumerable<Subscription> enumerable = this._subscriptions.Get(type);
				if (enumerable != null)
				{
					foreach (Subscription subscription in enumerable)
					{
						subscription.Action(eventObject);
					}
				}
			}
			finally
			{
				if (!posting)
				{
					this.InvokePendingActions();
				}
				this._posting = posting;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002288 File Offset: 0x00000488
		public void InvokeOrEnqueue(Action action)
		{
			if (this._posting)
			{
				this._pendingActions.Enqueue(action);
				return;
			}
			action();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A5 File Offset: 0x000004A5
		public void InvokePendingActions()
		{
			while (this._pendingActions.Count > 0)
			{
				this._pendingActions.Dequeue()();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
		public void RegisterNow(object subscriber)
		{
			Type type = subscriber.GetType();
			List<MethodInfo> list;
			if (this._methodCache.TryGetValue(type, out list))
			{
				using (List<MethodInfo>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MethodInfo method = enumerator.Current;
						this.RegisterMethod(subscriber, method);
					}
					return;
				}
			}
			List<MethodInfo> list2 = new List<MethodInfo>();
			this._methodCache[type] = list2;
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.GetCustomAttribute<OnEventAttribute>() != null)
				{
					if (!methodInfo.IsPublic)
					{
						throw new ArgumentException(type.FullName + ".$" + methodInfo.Name + " must be public");
					}
					list2.Add(methodInfo);
					this.RegisterMethod(subscriber, methodInfo);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023B0 File Offset: 0x000005B0
		public void RegisterMethod(object subscriber, MethodInfo method)
		{
			if (method.ReturnType != typeof(void))
			{
				throw new ArgumentException(string.Format("Can't register {0} of {1}. ", method, subscriber.GetType()) + "Listening methods must return void.");
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != 1)
			{
				throw new ArgumentException(string.Format("Can't register {0} of {1}. ", method, subscriber.GetType()) + "Listening methods must have exactly one parameter.");
			}
			Type parameterType = parameters[0].ParameterType;
			Action<object> action = delegate(object e)
			{
				method.Invoke(subscriber, new object[]
				{
					e
				});
			};
			this._subscriptions.Add(parameterType, subscriber, action);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000247C File Offset: 0x0000067C
		public void UnregisterNow(object subscriber)
		{
			this._subscriptions.RemoveAll(subscriber);
		}

		// Token: 0x04000006 RID: 6
		public readonly SubscriptionRegistry _subscriptions = new SubscriptionRegistry();

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<Type, List<MethodInfo>> _methodCache = new Dictionary<Type, List<MethodInfo>>();

		// Token: 0x04000008 RID: 8
		public readonly Queue<Action> _pendingActions = new Queue<Action>();

		// Token: 0x04000009 RID: 9
		public readonly Queue<object> _earlyEvents = new Queue<object>();

		// Token: 0x0400000A RID: 10
		public bool _ready;

		// Token: 0x0400000B RID: 11
		public bool _posting;
	}
}
