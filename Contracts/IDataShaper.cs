using Entities.Models;
using System.Reflection;

namespace Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(
            IEnumerable<T> entities, string fieldsString);

        ShapedEntity ShapeData(T entity, string fieldsString);

        private ShapedEntity FetchDataForEntity(
            T entity, IEnumerable<PropertyInfo> requiredPropery)
        {
            var shapedObject = new ShapedEntity();

            foreach (var property in requiredPropery)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (Guid)objectProperty.GetValue(entity);

            return shapedObject;
        }
    }
}
