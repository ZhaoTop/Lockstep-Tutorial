using System;
using Lockstep.Math;

namespace Lockstep.Game {
    public abstract partial class BaseService : ServiceReferenceHolder, IService, ILifeCycle, ITimeMachine {
        public virtual void DoInit(object objParent){}
        public virtual void DoAwake(IServiceContainer services){ }
        public virtual void DoStart(){ }
        public virtual void DoDestroy(){ }
        public virtual void OnApplicationQuit(){ }

        protected BaseService(){
            cmdBuffer = new CommandBuffer();
            cmdBuffer.Init(this, GetRollbackFunc());
        }

        
        protected ICommandBuffer cmdBuffer;

        protected virtual FuncUndoCommands GetRollbackFunc(){
            return null;
        }

        public int CurTick => _commonStateService.Tick;

        public virtual void Backup(int tick){ }

        public virtual void RollbackTo(int tick){
            cmdBuffer?.Jump(CurTick, tick);
        }

        public virtual void Clean(int maxVerifiedTick){
            cmdBuffer?.Clean(maxVerifiedTick);
        }
    }
}