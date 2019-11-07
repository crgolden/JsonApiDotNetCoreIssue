﻿using System;
using System.Collections.Generic;
using System.Linq;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Internal;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JsonApiDotNetCoreIssue
{
	/// <summary>
	/// The default implementation for IHooksDiscovery
	/// </summary>
	public class HooksDiscoveryOverride<TEntity> : IHooksDiscovery<TEntity> where TEntity : class, IIdentifiable
	{
		private readonly Type _boundResourceDefinitionType = typeof(ResourceDefinition<TEntity>);
		private readonly ResourceHook[] _allHooks;
		private readonly ResourceHook[] _databaseValuesAttributeAllowed =
		{
			ResourceHook.BeforeUpdate,
			ResourceHook.BeforeUpdateRelationship,
			ResourceHook.BeforeDelete
		};

		/// <inheritdoc/>
		public ResourceHook[] ImplementedHooks { get; private set; }
		public ResourceHook[] DatabaseValuesEnabledHooks { get; private set; }
		public ResourceHook[] DatabaseValuesDisabledHooks { get; private set; }

		public HooksDiscoveryOverride(IServiceProvider provider)
		{
			_allHooks = Enum.GetValues(typeof(ResourceHook))
							.Cast<ResourceHook>()
							.Where(h => h != ResourceHook.None)
							.ToArray();
            using (var scope = provider.CreateScope())
            {
                var containerType = scope.ServiceProvider.GetService(_boundResourceDefinitionType)?.GetType();
                if (containerType == null || containerType == _boundResourceDefinitionType)
                    return;
                DiscoverImplementedHooksForModel(containerType);
            }
		}

		/// <summary>
		/// Discovers the implemented hooks for a model.
		/// </summary>
		/// <returns>The implemented hooks for model.</returns>
		void DiscoverImplementedHooksForModel(Type containerType)
		{
			var implementedHooks = new List<ResourceHook>();
			var databaseValuesEnabledHooks = new List<ResourceHook> { ResourceHook.BeforeImplicitUpdateRelationship }; // this hook can only be used with enabled database values
			var databaseValuesDisabledHooks = new List<ResourceHook>();
			foreach (var hook in _allHooks)
			{
				var method = containerType.GetMethod(hook.ToString("G"));
				if (method.DeclaringType == _boundResourceDefinitionType)
					continue;

				implementedHooks.Add(hook);
				var attr = method.GetCustomAttributes(true).OfType<LoadDatabaseValues>().SingleOrDefault();
				if (attr != null)
				{
					if (!_databaseValuesAttributeAllowed.Contains(hook))
					{
						throw new JsonApiSetupException($"DatabaseValuesAttribute cannot be used on hook" +
							$"{hook.ToString("G")} in resource definition  {containerType.Name}");
					}
					var targetList = attr.value ? databaseValuesEnabledHooks : databaseValuesDisabledHooks;
					targetList.Add(hook);
				}
			}

			ImplementedHooks = implementedHooks.ToArray();
			DatabaseValuesDisabledHooks = databaseValuesDisabledHooks.ToArray();
			DatabaseValuesEnabledHooks = databaseValuesEnabledHooks.ToArray();
		}
	}
}