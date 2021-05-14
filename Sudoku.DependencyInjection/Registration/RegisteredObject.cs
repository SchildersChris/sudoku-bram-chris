using System;

namespace Sudoku.DependencyInjection.Registration
{
    public class RegisteredObject<TResolve, TConcrete> : IRegisteredObject where TConcrete : class, TResolve
    {
        public Type ToResolve { get; }
        public Type Concrete { get; }
        public LifeTime LifeTime { get; }
        public object Instance { get; private set; }

        /// <summary>
        /// Type registration
        /// </summary>
        /// <param name="lifeTime">Lifetime of the instance</param>
        public RegisteredObject(LifeTime lifeTime)
        {
            LifeTime = lifeTime;
            Concrete = typeof(TConcrete);
            ToResolve = typeof(TResolve);
        }

        /// <summary>
        /// Type registration
        /// </summary>
        /// <param name="lifeTime">Lifetime of the instance</param>
        /// <param name="instance">Concrete instance</param>
        public RegisteredObject(LifeTime lifeTime, TConcrete instance)
        {
            LifeTime = lifeTime;
            Instance = instance;
            Concrete = typeof(TConcrete);
            ToResolve = typeof(TResolve);
        }

        /// <summary>
        /// Gets the instance 
        /// </summary>
        /// <param name="args">Instance arguments</param>
        public void CreateInstance(params object[] args)
        {
            Instance = Activator.CreateInstance(Concrete, args);
        }
    }
}