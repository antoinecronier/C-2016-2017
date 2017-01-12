using ClassLibrary2.Entities.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Entities.Generator
{
    public class EntityGenerator<T> where T : class
    {
        private Reflectionner reflectionner;
        private Dictionary<String, Object> itemProperties;

        public EntityGenerator()
        {
            reflectionner = new Reflectionner();

            if (typeof(T).Name.Equals(TypeEnum.LIST))
            {
                var type = Type.GetType(typeof(List<T>).AssemblyQualifiedName);
                var list = (List<T>)Activator.CreateInstance(type);
                itemProperties = reflectionner.ReadClass<T>();
            }
            else
            {
                itemProperties = reflectionner.ReadClass<T>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inheritance"></param>
        /// <returns></returns>
        public T GenerateItem(Int32 inheritance = 2)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            if (inheritance > 0)
            {
                inheritance--;

                foreach (var item in itemProperties)
                {
                    PropertyInfo property = typeof(T).GetProperty(item.Key);
                    if (property.CanWrite && property.GetSetMethod(/*nonPublic*/ true).IsPublic)
                    {
                        switch (property.PropertyType.Name)
                        {
                            case TypeEnum.INT32:
                                property.SetValue(result, Faker.Number.RandomNumber(Int32.MaxValue));
                                break;
                            case TypeEnum.INT:
                                property.SetValue(result, Faker.Number.RandomNumber(Int32.MaxValue));
                                break;
                            case TypeEnum.STRING:
                                property.SetValue(result, Faker.Name.FullName());
                                break;
                            case TypeEnum.LIST:
                                //object generator1 = Activator.CreateInstance(
                                //    typeof(EntityGenerator<>).MakeGenericType(property.PropertyType.GetGenericArguments()));

                                object list = Activator.CreateInstance(
                                    typeof(List<>).MakeGenericType(property.PropertyType.GetGenericArguments()));

                                //TODO find a way to add items to list
                                //for (int i = 0; i < Faker.Number.RandomNumber(0, 10); i++)
                                //{
                                //    typeof(List<>).GetMethod("Add").MakeGenericMethod(property.PropertyType.GetGenericArguments()[0]).Invoke(list, new object[] {
                                //        typeof(EntityGenerator<>).GetMethod("GenerateItem").Invoke(generator1, new object[] { 2 }) });
                                //}

                                property.SetValue(result, list);
                                break;
                            default:
                                //object generator = Activator.CreateInstance(typeof(EntityGenerator<>)
                                //    .MakeGenericType(new Type[] { property.PropertyType }));
                                //property.SetValue(result, generator.GetType().GetMethod("GenerateItem").Invoke(generator, new object[] { 2 }));
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public IEnumerable<T> GenerateListItems()
        {
            List<T> result = (List<T>)Activator.CreateInstance(typeof(List<T>));
            for (int i = 0; i < Faker.Number.RandomNumber(0, 100); i++)
            {
                result.Add(GenerateItem());
            }
            return result;
        }
    }
}
