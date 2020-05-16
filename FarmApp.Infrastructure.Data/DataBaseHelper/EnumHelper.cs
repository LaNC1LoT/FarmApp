using FarmApp.Domain.Core.Attributes;
using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FarmApp.Infrastructure.Data.DataBaseHelper
{
    public static class EnumHelper
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static IEnumerable<TEntity> EnumToList<TEntity>(this Type @enum)
            where TEntity : class, IEnum, new()
        {
            if (!@enum.IsEnum) 
                throw new ArgumentException($"{@enum} not equals Enum.");

            IList<TEntity> result = new List<TEntity>();
            foreach (var value in Enum.GetValues(@enum))
            {
                var attribute = ((Enum)value).GetAttribute<EnumHelperAttribute>();
                TEntity entity = new TEntity
                {
                    Id = (int)value,
                    EnumName = value.ToString(),
                    Description = attribute?.Description ?? "Не указано",
                    IsDeleted = attribute?.IsDeleted ?? false
                };

                result.Add(entity);
            };
            return result;
        }

        /// <summary>
        /// Extension, set property for entity
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <typeparam name="TProperty">Type value</typeparam>
        /// <param name="target">this Entity</param>
        /// <param name="memberLamda">Expression func</param>
        /// <param name="value">value for set</param>
        /// <exception cref="NullReferenceException">Thrown when any is null</exception>
        public static void SetPropertyValue<TEntity, TProperty>(this TEntity target, Expression<Func<TEntity, TProperty>> memberLamda, TProperty value)
            where TEntity : class
        {
            if (target == null)
                throw new NullReferenceException($"{typeof(TEntity)} is null.");

            if (memberLamda == null)
                throw new NullReferenceException($"{typeof(Expression<Func<TEntity, TProperty>>)} is null.");

            if (value == null)
                throw new NullReferenceException($"{typeof(TEntity)} is null.");

            if (memberLamda.Body is MemberExpression memberSelectorExpression)
            {
                if (memberSelectorExpression.Member is PropertyInfo property)
                {
                    property.SetValue(target, value, null);
                }
                else throw new NullReferenceException($"{typeof(PropertyInfo)} is null.");
            }
            else throw new NullReferenceException($"{typeof(MemberExpression)} is null.");
        }
    }
}
