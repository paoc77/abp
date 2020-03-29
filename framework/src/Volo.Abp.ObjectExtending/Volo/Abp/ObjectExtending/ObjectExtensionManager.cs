﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace Volo.Abp.ObjectExtending
{
    public class ObjectExtensionManager
    {
        public static ObjectExtensionManager Instance { get; set; } = new ObjectExtensionManager();

        protected Dictionary<Type, ObjectExtensionInfo> ObjectsExtensions { get; }

        protected ObjectExtensionManager()
        {
            ObjectsExtensions = new Dictionary<Type, ObjectExtensionInfo>();
        }

        public virtual ObjectExtensionManager AddOrUpdate<TObject>(
            [CanBeNull] Action<ObjectExtensionInfo> configureAction = null)
            where TObject : IHasExtraProperties
        {
            return AddOrUpdate(typeof(TObject), configureAction);
        }

        public virtual ObjectExtensionManager AddOrUpdate(
            [NotNull] Type type,
            [CanBeNull] Action<ObjectExtensionInfo> configureAction = null)
        {
            Check.NotNull(type, nameof(type));

            var extensionInfo = ObjectsExtensions.GetOrAdd(
                type,
                () => new ObjectExtensionInfo(type)
            );

            configureAction?.Invoke(extensionInfo);

            return this;
        }

        public virtual ObjectExtensionInfo GetOrNull<TObject>()
        {
            return GetOrNull(typeof(TObject));
        }

        public virtual ObjectExtensionInfo GetOrNull([NotNull] Type type)
        {
            Check.NotNull(type, nameof(type));

            return ObjectsExtensions.GetOrDefault(type);
        }
    }
}