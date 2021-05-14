using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.DependencyInjection.Exceptions;
using Sudoku.DependencyInjection.Registration;

namespace Sudoku.DependencyInjection.Container
{
public class DependencyInjectionContainer : IContainer
    {
        private readonly IList<IRegisteredObject> _registeredObjects;

        public DependencyInjectionContainer()
        {
            _registeredObjects = new List<IRegisteredObject>();
        }

        /// <summary>
        /// Register a type to the container
        /// </summary>
        /// <typeparam name="TResolve">Type to resolve</typeparam>
        /// <typeparam name="TConcrete">Type that is used to resolve</typeparam>
        public void Register<TResolve, TConcrete>() where TConcrete : class, TResolve
        {
            Register<TResolve, TConcrete>(LifeTime.Transient);
        }

        /// <summary>
        /// Register a type to the container
        /// </summary>
        /// <param name="lifeTime">life time of instance</param>
        /// <typeparam name="TResolve">Type requested to resolved</typeparam>
        /// <typeparam name="TConcrete">Type that will be resolved</typeparam>
        public void Register<TResolve, TConcrete>(LifeTime lifeTime) where TConcrete : class, TResolve
        {
            _registeredObjects.Add(new RegisteredObject<TResolve, TConcrete>(lifeTime));
        }

        /// <summary>
        /// Add a registration to the container
        /// </summary>
        /// <param name="instance">Concrete instance</param>
        /// <typeparam name="TResolve">Type requested to resolved</typeparam>
        /// <typeparam name="TConcrete">Type that will be resolved</typeparam>
        public void Register<TResolve, TConcrete>(TConcrete instance) where TConcrete : class, TResolve
        {
            _registeredObjects.Add(new RegisteredObject<TResolve, TConcrete>(LifeTime.Singleton, instance));
        }

        /// <summary>
        /// Resolve an object by registered type
        /// </summary>
        /// <exception cref="TypeNotRegisteredException">If type is not registered in the IocContainer</exception>
        /// <typeparam name="TResolve"></typeparam>
        /// <param name="args">Extra constructor params</param>
        /// <returns></returns>
        public TResolve Resolve<TResolve>(params object[] args) where TResolve : class
        {
            return ResolveType(typeof(TResolve), args) as TResolve;
        }
        
        /// <summary>
        /// Resolve an object by registered type
        /// </summary>
        /// <exception cref="TypeNotRegisteredException">If type is not registered in the IocContainer</exception>
        /// <param name="toResolve">Type to resolve</param>
        /// <param name="args">Extra constructor params</param>
        /// <returns></returns>
        public object Resolve(Type toResolve, params object[] args)
        {
            return ResolveType(toResolve, args);
        }

        private object ResolveType(Type toResolve, params object[] args)
        {
            // Resolve extra params
            var param = args.Where(o =>
            {
                var type = o.GetType();
                return type == toResolve || toResolve.IsAssignableFrom(type);
            }).ToList();
            
            var count = param.Count;
            if (count > 0)
                return count > 1 ? 
                    throw new AmbiguousTypeException("Cannot resolve two types params of the same type") : 
                    param[0];

            // Resolve registered params
            var registeredObject = _registeredObjects.FirstOrDefault(o => o.ToResolve == toResolve);
            if (registeredObject == null)
                throw new TypeNotRegisteredException($"The type {toResolve.Name} has not been registered");

            return GetInstance(registeredObject, args);
        }

        private object GetInstance(IRegisteredObject registeredObject, params object[] args)
        {
            if (registeredObject.Instance != null && registeredObject.LifeTime == LifeTime.Singleton)
                return registeredObject.Instance;

            var @params = ResolveConstructorParameters(registeredObject, args).ToArray();

            registeredObject.CreateInstance(@params);
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(IRegisteredObject registeredObject, params object[] args)
        {
            var ctorInfo = registeredObject.Concrete.GetConstructors().First();
            foreach (var parameter in ctorInfo.GetParameters())
            {
                yield return ResolveType(parameter.ParameterType, args);
            }
        }
    }
}