using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devarc
{
    public abstract class BaseFsmController<STATE, FSM, OWNER> : BaseController<OWNER>
        where STATE : struct, IConvertible
        where FSM : BaseFsmObject<STATE, OWNER>
        where OWNER : BaseObject
    {
        public STATE CurrentStateType => mCurrentState != null ? mCurrentState.State : default(STATE);
        public FSM CurrentState => mCurrentState;
        FSM mCurrentState = null;
        Dictionary<STATE, FSM> mStates = new Dictionary<STATE, FSM>();

        class TRANS
        {
            public STATE state;
            public object[] args;
        }
        List<TRANS> mChangingStates = new List<TRANS>();
        bool mIsChanging = false;

        public override void Clear()
        {
            mCurrentState = null;
            mIsChanging = false;
        }

        protected override void onLateUpdate()
        {
            mCurrentState?.Tick();
        }

        public FSM Get(STATE state)
        {
            FSM obj = null;
            mStates.TryGetValue(state, out obj);
            return obj;
        }

        public void ChangeState(STATE state, params object[] args)
        {
            TRANS data = new TRANS();
            data.state = state;
            data.args = args;
            mChangingStates.Add(data);
            updateState();
        }

        protected void registerState(OWNER owner, FSM state)
        {
            if (mStates.TryAdd(state.State, state))
            {
                state.Init(owner);
            }
        }

        void updateState()
        {
            if (mIsChanging)
                return;

            mIsChanging = true;
            while (mChangingStates.Count > 0)
            {
                int i = mChangingStates.Count - 1;
                var data = mChangingStates[i];
                mChangingStates.RemoveAt(i);

                FSM fsm = Get(data.state);
                if (fsm == null)
                {
                    Debug.LogError($"Cannot find FSM: key={data.state}");
                    continue;
                }

                if (mCurrentState != null)
                {
                    mCurrentState.Exit();
                }
                mCurrentState = fsm;
                mCurrentState.Enter(data.args);
            }
            mIsChanging = false;
        }
    }
}
