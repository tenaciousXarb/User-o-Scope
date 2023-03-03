using System.ComponentModel;
using System.Reflection;

namespace UserApp.BusinessServices.Validator
{
    public class ObjectValidator
    {
        public static void CheckNullOrEmpty<T>(T obj)
        {
            if (obj != null)
            {
                var properties = typeof(T).GetProperties();

                if (typeof(T).Name == "PageDetails")
                {
                    foreach (var property in properties)
                    {
                        if (Convert.ToInt32(property.GetValue(obj)) <= 0)
                        {
                            throw new ArgumentException($"{property.GetCustomAttribute<DisplayNameAttribute>().DisplayName} must be greater than zero!");
                        }
                    }
                }
                else
                {
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(obj);

                        if (value != null)
                        {
                            if (property.GetType().Name == "String")
                            {
                                if (value.ToString() == String.Empty)
                                {
                                    throw new ArgumentException($"{property.Name} can't be or empty!");
                                }
                            }
                            else if (value.GetType().Name == "Int32")
                            {
                                if (Convert.ToInt32(value) <= 0)
                                {
                                    throw new ArgumentException($"{property.Name} can't be null and has to be a non-negative integer!");
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"{property.Name} can't be null!");
                        }
                    }
                }
            }
            else
            {
                throw new NullReferenceException($"Null {typeof(T).Name} object");
            }
            return;
        }
    }
}
