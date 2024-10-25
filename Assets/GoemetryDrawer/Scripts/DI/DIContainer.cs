
using System.Collections.Generic;
using System;

namespace Assets.GoemetryDrawer.Scripts.DI
{
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIEntry> _entriesMap = new();
        private readonly HashSet<(string, Type)> _cache = new();

        public DIContainer(DIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public DIEntry RegisterFactory<T>(Func<DIContainer, T> factory)
        {
            return RegisterFactory(null, factory);
        }

        public DIEntry RegisterFactory<T>(string tag, Func<DIContainer, T> factory)
        {
            var type = typeof(T);
            var key = (tag, type);

            if (_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"[FACTORY]: Tag {key.Item1} and type {key.Item2.FullName} already registered");
            }
            var entry = new DIEntry<T>(this, factory);
            _entriesMap[key] = entry;
            return entry;
        }

        public DIEntry RegisterInstance<T>(T instance)
        {
            return RegisterInstance(null, instance);
        }

        public DIEntry RegisterInstance<T>(string tag, T instance)
        {
            var type = typeof(T);
            var key = (tag, type);

            if (_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"[INSTANCE]: Tag {key.Item1} and type {key.Item2.FullName} already registered");
            }

            var entry = new DIEntry<T>(instance);
            _entriesMap[key] = entry;
            return entry;
        }

        public T Resolve<T>()
        {
            return Resolve<T>(null);
        }

        public T Resolve<T>(string tag)
        {
            var type = typeof(T);
            var key = (tag, type);

            return Resolve<T>(key);
        }

        public T Resolve<T>((string, Type) key)
        {
            try
            {
                if (_cache.Contains(key))
                {
                    throw new Exception($"Error dependency [CYCLIC]: tag {key.Item1} and type {key.Item2.FullName}");
                }

                _cache.Add(key);

                if (_entriesMap.TryGetValue(key, out var entry))
                {
                    return entry.Resolve<T>();
                }

                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(key);
                }
            }
            finally
            {
                _cache.Remove(key);
            }
            throw new Exception($"Error dependency [NOT FOUND]: tag {key.Item1} and type {key.Item2.FullName}");
        }

        public void Dispose()
        {
            var entries = _entriesMap.Values;

            foreach (var entry in entries)
            {
                entry?.Dispose();
            }
        }
    }
}

