﻿using Grace.DependencyInjection.Lifestyle;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Grace.DependencyInjection
{
    /// <summary>
    /// Interface for configuring a non generic type
    /// </summary>
    public interface IFluentExportStrategyConfiguration
    {
        /// <summary>
        /// Export as a specific type
        /// </summary>
        /// <param name="type">type to export as</param>
        /// <returns>configuraiton object</returns>
        IFluentExportStrategyConfiguration As(Type type);

        /// <summary>
        /// Mark the export as externally owned so the container does not track for disposal
        /// </summary>
        /// <returns>configuraiton object</returns>
        IFluentExportStrategyConfiguration ExternallyOwned();

        /// <summary>
        /// Import a specific member
        /// </summary>
        /// <param name="selector">selector method, can be null</param>
        /// <returns>configuraiton object</returns>
        IFluentExportStrategyConfiguration ImportMembers(Func<MemberInfo, bool> selector = null);
        
        /// <summary>
        /// Apply a lifestlye to export strategy
        /// </summary>
        ILifestylePicker<IFluentExportStrategyConfiguration> Lifestyle { get; }

        /// <summary>
        /// Assign a custom lifestyle to an export
        /// </summary>
        /// <param name="lifestyle"></param>
        /// <returns>configuraiton object</returns>
        IFluentExportStrategyConfiguration UsingLifestyle(ICompiledLifestyle lifestyle);

        /// <summary>
        /// Apply a condition on when to use strategy
        /// </summary>
        IWhenConditionConfiguration<IFluentExportStrategyConfiguration> When { get; }

        /// <summary>
        /// Export with specific metadata
        /// </summary>
        /// <param name="key">metadata key</param>
        /// <param name="value">metadata value</param>
        /// <returns>configuraiton object</returns>
        IFluentExportStrategyConfiguration WithMetadata(object key, object value);
    }

    /// <summary>
    /// Represents a class that configures an export
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFluentExportStrategyConfiguration<T>
    {
        /// <summary>
        /// Apply an action to the export just after construction
        /// </summary>
        /// <param name="applyAction">action to apply to export upon construction</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> Apply(Action<T> applyAction);

        /// <summary>
        /// Export as a specific type
        /// </summary>
        /// <param name="type">type to export as</param>
        /// <returns></returns>
        IFluentExportStrategyConfiguration<T> As(Type type);

        /// <summary>
        /// Export as a particular type
        /// </summary>
        /// <typeparam name="TInterface">type to export as</typeparam>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> As<TInterface>();

        /// <summary>
        /// Export as a keyed type
        /// </summary>
        /// <typeparam name="TInterface">export type</typeparam>
        /// <param name="key">key to export under</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> AsKeyed<TInterface>(object key);

        /// <summary>
        /// Export the type by the interfaces it implements
        /// </summary>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> ByInterfaces(Func<Type, bool> filter = null);

        /// <summary>
        /// You can provide a cleanup method to be called 
        /// </summary>
        /// <param name="disposalCleanupDelegate">action to call when disposing</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> DisposalCleanupDelegate(Action<T> disposalCleanupDelegate);

        /// <summary>
        /// Export a public member of the type (property, field or method with return value)
        /// </summary>
        /// <typeparam name="TValue">type to export</typeparam>
        /// <param name="memberExpression">member expression</param>
        /// <returns></returns>
        IFluentExportMemberConfiguration<T> ExportMember<TValue>(Expression<Func<T, TValue>> memberExpression);

        /// <summary>
        /// Mark an export as externally owned means the container will not track and dispose the instance
        /// </summary>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> ExternallyOwned();

        /// <summary>
        /// Mark specific members to be injected
        /// </summary>
        /// <param name="selector">select specific members, if null all public members will be injected</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> ImportMembers(Func<MemberInfo, bool> selector = null);

        /// <summary>
        /// Import a specific property
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="property">property expression</param>
        /// <returns>configuration object</returns>
        IFluentImportPropertyConfiguration<T, TProp> ImportProperty<TProp>(Expression<Func<T, TProp>> property);

        /// <summary>
        /// Assign a lifestyle to this export
        /// </summary>
        ILifestylePicker<IFluentExportStrategyConfiguration<T>> Lifestyle { get; }

        /// <summary>
        /// Export using a specific lifestyle
        /// </summary>
        /// <param name="lifestyle">lifestlye to use</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> UsingLifestyle(ICompiledLifestyle lifestyle);

        /// <summary>
        /// Add a condition to when this export can be used
        /// </summary>
        IWhenConditionConfiguration<IFluentExportStrategyConfiguration<T>> When { get; }

        /// <summary>
        /// Add a specific value for a particuar parameter in the constructor
        /// </summary>
        /// <typeparam name="TParam">type of parameter</typeparam>
        /// <param name="paramValue">Func(T) value for the parameter</param>
        /// <returns>configuration object</returns>
        IFluentWithCtorConfiguration<T, TParam> WithCtorParam<TParam>(Func<TParam> paramValue = null);

        /// <summary>
        /// Add a specific value for a particuar parameter in the constructor
        /// </summary>
        /// <typeparam name="TParam">type of parameter</typeparam>
        /// <param name="paramValue">Func(IInjectionScope, IInjectionContext, T) value for the parameter</param>
        /// <returns>configuration object</returns>
        IFluentWithCtorConfiguration<T, TParam> WithCtorParam<TParam>(Func<IExportLocatorScope, StaticInjectionContext, IInjectionContext, TParam> paramValue);

        /// <summary>
        /// Adds metadata to an export
        /// </summary>
        /// <param name="key">metadata key</param>
        /// <param name="value">metadata value</param>
        /// <returns>configuration object</returns>
        IFluentExportStrategyConfiguration<T> WithMetadata(object key, object value);

    }
}
