using Sudoku.DependencyInjection.Container;
using Sudoku.DependencyInjection.Exceptions;
using Xunit;

namespace Sudoku.DependencyInjection.Test.Container
{
    public class DependencyInjectionContainerTests
    {
        [Fact]
        public void Should_Throw_TypeNotRegisteredException()
        {
            var container = new DependencyInjectionContainer();
            // container.Register<IConcrete, Concrete>(); -- Intentionally commented

            Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<IConcrete>());
        }

        [Fact]
        public void Should_Create_Instance()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();

            var instance = container.Resolve<IConcrete>();
            Assert.IsAssignableFrom<IConcrete>(instance);
        }

        [Fact]
        public void Should_Create_InstanceWithParam()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();
            container.Register<IConcreteWithParam, ConcreteWithParam>();

            var instance = container.Resolve<IConcreteWithParam>();
            Assert.IsAssignableFrom<IConcreteWithParam>(instance);
        }

        [Fact]
        public void Should_Create_InstanceWithParamNested()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();
            container.Register<IConcreteWithParam, ConcreteWithParam>();
            container.Register<IConcreteWithParamNested, ConcreteWithParamNested>();

            var instance = container.Resolve<IConcreteWithParamNested>();
            Assert.IsAssignableFrom<IConcreteWithParamNested>(instance);
        }
        
        [Fact]
        public void Should_Create_InstanceWithParamNestedExtraParams()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();
            container.Register<IConcreteWithParam, ConcreteWithParam>();
            container.Register<IConcreteWithParamNested, ConcreteWithParamNested>();
            container.Register<IConcreteWithParamNestedExtraParams, ConcreteWithParamNestedExtraParams>();

            var instance = container.Resolve<IConcreteWithParamNestedExtraParams>("string", 1);
            Assert.IsAssignableFrom<IConcreteWithParamNestedExtraParams>(instance);
        }

        [Fact]
        public void Should_Throw_AmbiguousTypeException()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();
            container.Register<IConcreteWithParam, ConcreteWithParam>();
            container.Register<IConcreteWithParamNested, ConcreteWithParamNested>();
            container.Register<IConcreteWithParamNestedExtraParams, ConcreteWithParamNestedExtraParams>();

            Assert.Throws<AmbiguousTypeException>(() => 
                container.Resolve<IConcreteWithParamNestedExtraParams>("string", "string2", 1));
        }
        
        [Fact]
        public void Should_Create_Singleton_Instance()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>(LifeTime.Singleton);
            
            /* 
             * Resolving 2 times with the same object means its a singleton
             */
            var instance = container.Resolve<IConcrete>();
            Assert.Same(instance, container.Resolve<IConcrete>()); 
        }

        [Fact]
        public void Should_Create_Transient_Instance()
        {
            var container = new DependencyInjectionContainer();
            container.Register<IConcrete, Concrete>();
            
            /*
             * Resolving 2 times with a different object means its a transient
             */
            var instance = container.Resolve<IConcrete>();
            Assert.NotSame(instance, container.Resolve<IConcrete>());
        }
    }
    
    public interface IConcrete { }
    public class Concrete : IConcrete { }

    internal interface IConcreteWithParam { }
    public class ConcreteWithParam : IConcreteWithParam
    {
        public ConcreteWithParam(IConcrete concrete) { }
    }
    
    internal interface IConcreteWithParamNested { }
    internal class ConcreteWithParamNested : IConcreteWithParamNested
    {
        public ConcreteWithParamNested(IConcreteWithParam concreteWithParams) { }
    }
    
    internal interface IConcreteWithParamNestedExtraParams { }
    internal class ConcreteWithParamNestedExtraParams : IConcreteWithParamNestedExtraParams
    {
        public int ExtraParam { get; set; }
        public string ExtraParam2 { get; set; }
        public IConcreteWithParam ConcreteWithParam { get; set; }
        
        public ConcreteWithParamNestedExtraParams(IConcreteWithParam concreteWithParams, int extraParam, string extraParam2)
        {
            ConcreteWithParam = concreteWithParams;
            
            ExtraParam = extraParam;
            ExtraParam2 = extraParam2;
        }
    }
}