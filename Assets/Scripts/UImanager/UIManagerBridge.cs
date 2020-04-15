using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bridge
{
    public interface IBridgeReciever<T>
    {
        void TakeData(T t);
    }

    public class UIManagerBridge<T>
    {
        public UIManagerBridge(IBridgeReciever<T> BridgeReciever, T t)
        {
            BridgeReciever.TakeData(t);
        }
    }
}

