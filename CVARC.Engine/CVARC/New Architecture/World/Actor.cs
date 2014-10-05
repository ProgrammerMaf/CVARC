﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public class Actor<TActorManager,TWorld> : IActor
        where TActorManager : IActorManager
        where TWorld : IWorld
    {
        protected TActorManager Manager { get; private set; }
        public TWorld World { get; private set; }
        IWorld IActor.World { get { return World; } }

        public string ObjectId { get; private set; }

        public Type GetWorldType
        {
            get { return typeof(TWorld); }
        }

        public Type GetManagerType
        {
            get { return typeof(TActorManager); }
        }


        public virtual void Initialize(IActorManager rules, IWorld world, string actorObjectId)
        {
            try
            {
                Manager = (TActorManager)rules;
            }
            catch { throw new Exception("This should not happen, because rules are checked to match this robot"); }
            try
            {
                World = (TWorld)world;
            }
            catch { throw new Exception("The actor " + GetType().Name + " is not designed for world " + world.GetType().Name); }

            ObjectId = actorObjectId;
        }


        
    }
}