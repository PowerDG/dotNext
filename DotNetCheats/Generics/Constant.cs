using System;

namespace Cheats.Generics
{
    /// <summary>
    /// Allows to use constant values as generic parameters.
    /// </summary>
    /// <remarks>
    /// Derived class must be sealed or abstract. If class is sealed
    /// then it should have at least one constructor without parameters.
    /// </remarks>
    /// <typeparam name="T">Type of constant to be passed as generic parameter.</typeparam>
    public abstract class Constant<T>
    {
        private static class ValueHolder<G>
            where G: Constant<T>, new()
        {
            internal static readonly T Value = new G();
        }

        private readonly T Value;

        /// <summary>
        /// Initializes a new generic-level constant.
        /// </summary>
        /// <param name="constVal">Constant value.</param>
        protected Constant(T constVal)
        {
            Value = constVal;
        }

        public static implicit operator T(Constant<T> other) => other.Value;
        
        /// <summary>
        /// Extracts constant value from generic parameter.
        /// </summary>
        /// <typeparam name="G">A type representing a constant value.</typeparam>
        /// <returns>Constant value extracted from generic.</returns>
        public static T Of<G>()
            where G: Constant<T>, new()
            => ValueHolder<G>.Value;
    }
}