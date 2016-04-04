using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PoolMember : MonoBehaviour
    {
        private Action returnTopoolAction;
        public void SetInfo(Action retAction)
        {
            returnTopoolAction = retAction;
        }
        
        public void Return()
        {
            returnTopoolAction();
        }
    }
}